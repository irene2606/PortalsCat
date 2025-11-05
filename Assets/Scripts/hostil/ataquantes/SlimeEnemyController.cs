using System.Collections;
using UnityEngine;

public class SlimeEnemyController : MonoBehaviour
{
    private Animator myAnim;
    private Transform target;
    public Transform homePos;
    [SerializeField] private float speed;

    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;

    private bool isAttacking;
    private bool isDamaged;

    public LayerMask solidObjectsLayer; // Capa para objetos sólidos con los que puede colisionar

    private HurtPlayer hurtPlayer;
    private GameControllerHostil gameController;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        if (myAnim == null)
        {
            Debug.LogError("Animator no encontrado en el enemigo.");
        }

        var playerMovement = FindObjectOfType<PlayerMovementHostil>();
        if (playerMovement != null)
        {
            target = playerMovement.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto PlayerMovementHostil en la escena.");
        }

        hurtPlayer = GetComponent<HurtPlayer>();
        gameController = FindObjectOfType<GameControllerHostil>();
    }

    void Update()
    {
        if (gameController != null && gameController.jugadorEncontróGato) // Si el jugador ya encontró al gato
        {
            SetIdle();
            return;
        }

        if (isDamaged)
        {
            ApplyKnockback();
        }
        else if (isAttacking)
        {
            ApplyAttack();// Mientras ataca, no hace nada más
        }
        else
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        if (target == null) return; // Si no hay jugador, no hacer nada

        float distanceToPlayer = Vector3.Distance(target.position, transform.position);

        if (distanceToPlayer <= maxRange && distanceToPlayer >= minRange)
        {
            FollowPlayer();
        }
        else if (distanceToPlayer > maxRange)
        {
            GoHome();
        }
    }

    public void FollowPlayer()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 nextPosition = transform.position + direction * speed * Time.deltaTime;

        if (!IsSolid(nextPosition)) // Verificar si el próximo movimiento está bloqueado
        {
            myAnim.SetBool("enMovimiento", true);
            myAnim.SetFloat("moveX", direction.x);
            myAnim.SetFloat("moveY", direction.y);

            transform.position = nextPosition;
        }
        else
        {
            myAnim.SetBool("enMovimiento", false);
        }
    }

    public void GoHome()
    {
        if (homePos == null) return;

        Vector3 direction = (homePos.position - transform.position).normalized;
        Vector3 nextPosition = transform.position + direction * speed * Time.deltaTime;

        if (!IsSolid(nextPosition))
        {
            myAnim.SetBool("enMovimiento", true);
            myAnim.SetFloat("moveX", direction.x);
            myAnim.SetFloat("moveY", direction.y);

            transform.position = nextPosition;

            if (Vector3.Distance(transform.position, homePos.position) < 0.1f)
            {
                myAnim.SetBool("enMovimiento", false);
            }
        }
        else
        {
            myAnim.SetBool("enMovimiento", false);
        }
    }

    private bool IsSolid(Vector3 position)
    {
        Debug.Log("colisionando");
        // Verificar si hay colisión con un objeto en la capa de objetos sólidos
        return Physics2D.OverlapCircle(position, 0.2f, solidObjectsLayer) != null;
    }

    public void Attack()
    {
        if (!isAttacking && !isDamaged)
        {
            StartCoroutine(ApplyAttack());
        }
    }

    private IEnumerator ApplyAttack()
    {
        isAttacking = true;

        myAnim.SetBool("atacando", true);
        yield return new WaitForSeconds(2f); // Duración del ataque

        myAnim.SetBool("atacando", false);
        isAttacking = false;
    }

    public void ReceiveDamage()
    {
        isDamaged = true;
    }

    private void ApplyKnockback()
    {
        StartCoroutine(ApplyKnockbackCoroutine());
    }

    private IEnumerator ApplyKnockbackCoroutine()
    {
        Vector2 moveDirection = new Vector2(
            myAnim.GetFloat("moveX"),
            myAnim.GetFloat("moveY")
        ).normalized;

        Vector2 knockbackPosition = (Vector2)transform.position - moveDirection * 0.01f;

        float knockbackSpeed = 1f;
        while (Vector2.Distance(transform.position, knockbackPosition) > 0.009f)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                knockbackPosition,
                knockbackSpeed * Time.deltaTime
            );

            yield return null;
        }

        yield return new WaitForSeconds(1f);
        myAnim.SetBool("enMovimiento", true);

        isDamaged = false;
        if (hurtPlayer != null)
        {
            hurtPlayer.Damaged(false);
        }
    }
    
    public void SetIdle()
    {
        myAnim.SetBool("enMovimiento", false);
        isAttacking = false;
        isDamaged = false;
    }
}
