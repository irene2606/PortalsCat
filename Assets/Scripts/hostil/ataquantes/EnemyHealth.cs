using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    private bool flashActive;
    [SerializeField] private float flashLength = 0f;
    private float flashCounter = 0f;
    private SpriteRenderer EnemySprite;
    private Animator myAnim;

    private bool muerto;

    // Start is called before the first frame update
    void Start()
    {
        EnemySprite = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>(); // Asegúrate de asignar correctamente el Animator
    }

    // Update is called once per frame
    void Update()
    {
        if (flashActive)
        {
            if (flashCounter > flashLength * 0.99f)
            {
                EnemySprite.color = new Color(EnemySprite.color.r, EnemySprite.color.g, EnemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 0.82f)
            {
                EnemySprite.color = new Color(EnemySprite.color.r, EnemySprite.color.g, EnemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 0.66f)
            {
                EnemySprite.color = new Color(EnemySprite.color.r, EnemySprite.color.g, EnemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 0.49f)
            {
                EnemySprite.color = new Color(EnemySprite.color.r, EnemySprite.color.g, EnemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 0.33f)
            {
                EnemySprite.color = new Color(EnemySprite.color.r, EnemySprite.color.g, EnemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 0.16f)
            {
                EnemySprite.color = new Color(EnemySprite.color.r, EnemySprite.color.g, EnemySprite.color.b, 1f);
            }
            else if (flashCounter > 0f)
            {
                EnemySprite.color = new Color(EnemySprite.color.r, EnemySprite.color.g, EnemySprite.color.b, 0f);
            }
            else
            {
                EnemySprite.color = new Color(EnemySprite.color.r, EnemySprite.color.g, EnemySprite.color.b, 1f);
                flashActive = false;
            }

            flashCounter -= Time.deltaTime;
        }
    }

    public void HurtEnemy(int damageToGive)
    {
        currentHealth -= damageToGive;
        Debug.Log($"Vida actual enemigo: {currentHealth}");

        flashActive = true;
        flashCounter = flashLength;

        if (currentHealth <= 0 && !muerto)
        {
            muerto = true; // Marcar al enemigo como "muerto"
            myAnim.SetBool("muerto", true);

            // Iniciar la corrutina para manejar la muerte
            StartCoroutine(StopAnimatorAfterDeath());
        }
    }

    private IEnumerator StopAnimatorAfterDeath()
    {
        // Esperar la duración de la animación "Muerto"
        float deathAnimationLength = myAnim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(deathAnimationLength);

        // Deshabilitar el Animator para mantener el último frame de la animación
        myAnim.enabled = false;

        Debug.Log("Animación de muerte finalizada, destruyendo objeto...");

        // Destruir el objeto después de la animación
        Destroy(gameObject);
    }

}
