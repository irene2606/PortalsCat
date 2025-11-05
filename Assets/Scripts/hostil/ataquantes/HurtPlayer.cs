using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtPlayer : MonoBehaviour
{
    public Transform player; // Referencia al Transform del jugador
    public float detectionRange = 2f; // Rango de detección para "golpear" al jugador
    private float waitToLoad = 2f;
    private bool reloading;
    private bool canHurt = true; // Controla si se puede golpear al jugador
    [SerializeField] private int damageToGive;

    private bool isDamaged;

    private GameControllerHostil gameController; // Para verificar si el jugador encontró al gato

    void Start()
    {
        gameController = FindObjectOfType<GameControllerHostil>(); // Obtener referencia al controlador del juego
    }


    void Update()
    {
        if (reloading)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }else if (gameController.jugadorEncontróGato) // Si el jugador encontró al gato, no realizar más daño
        {
            return;
        }else
        {
            CheckPlayerDistance();
        }
    }

    private void CheckPlayerDistance()
    {
        HealthManager healthManager = player.GetComponent<HealthManager>();

        // Si el jugador ha muerto, el slime entra en estado de reposo
        if (healthManager != null && (healthManager.IsDead() || gameController.jugadorEncontróGato))
        {
            SlimeEnemyController slimeController = gameObject.GetComponent<SlimeEnemyController>();
            if (slimeController != null)
            {
                slimeController.SetIdle(); // Activamos el estado de reposo
            }
            return; // Salimos para evitar más acciones
        }

        // Continuar con la lógica de ataque si el jugador no está muerto
        if (Vector3.Distance(transform.position, player.position) <= detectionRange && canHurt && !isDamaged)
        {
            StartCoroutine(HurtPlayerCoroutine());
        }
    }


    private IEnumerator HurtPlayerCoroutine()
    {
        canHurt = false; // Bloqueamos el golpe temporalmente
        Debug.Log("Golpeando al jugador...");

        // Llamar a la función Attack() del slime
        SlimeEnemyController slimeController = gameObject.GetComponent<SlimeEnemyController>();
        if (slimeController != null)
        {
            slimeController.Attack(); // Llamar a la función Attack() del slime
            yield return new WaitForSeconds(1f);
        }
        
        // Llamamos a la función que daña al jugador
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            player.gameObject.GetComponent<HealthManager>().HurtPlayer(damageToGive);
        }

        yield return new WaitForSeconds(2f); // Esperamos antes de permitir otro golpe
        canHurt = true;
    }

    public void Damaged(bool estado)
    {
        isDamaged=estado;
    }

}



