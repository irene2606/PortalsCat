using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth;

    private bool flashActive;
    [SerializeField] private float flashLength=0f;
    private float flashCounter=0f;
    private SpriteRenderer playerSprite;
    private Animator myAnim;

    private bool muerto=false;

    private bool isDead;

    public GameControllerHostil gameController;

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();

        // Asegúrate de tener una referencia válida al GameControllerHostil
        if (gameController == null)
        {
            gameController = FindObjectOfType<GameControllerHostil>();
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        if(flashActive && !muerto)
        {
            if (flashCounter>flashLength * 0.99f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }else if(flashCounter>flashLength * 0.82f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }else if(flashCounter>flashLength * 0.66f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }else if(flashCounter>flashLength * 0.49f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }else if(flashCounter>flashLength * 0.33f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }else if(flashCounter>flashLength * 0.16f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }else if(flashCounter>0f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }else
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                flashActive=false;
            }

            flashCounter-=Time.deltaTime;
        }
        
    }

    public void HurtPlayer(int damageToGive)
    {
        currentHealth -= damageToGive;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegúrate de que no baje de 0 ni supere el máximo
        Debug.Log($"Vida actual: {currentHealth}");

        flashActive = true;
        flashCounter = flashLength;

        // Notificar al HealthUIManager para que actualice la UI
        FindObjectOfType<HealthUIManager>()?.UpdateHeartsUI();

        if (currentHealth <= 0)
        {
            myAnim.SetBool("Muerto", true);
            isDead = true;

            // Iniciar una corrutina para esperar al final de la animación y detener el Animator
            StartCoroutine(StopAnimatorAfterDeath());

        }
        
    }

    private IEnumerator StopAnimatorAfterDeath()
    {
        // Esperar la duración de la animación "Muerto"
        float deathAnimationLength = myAnim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(deathAnimationLength);

        // Detener el Animator para mantener el último frame
        myAnim.enabled = false;

        muerto=true;

        yield return new WaitForSeconds(1f);

        if (gameController != null)
        {
            gameController.HaMuerto(true);
            gameController.FinDelJuego(false);
            
        }

        // Opción: Si quieres también desactivar la física, puedes desactivar el Rigidbody2D o Collider
        // GetComponent<Rigidbody2D>().simulated = false;
        // GetComponent<Collider2D>().enabled = false;

        Debug.Log("Animación de muerte finalizada, el personaje está inmóvil.");
    }

    public bool IsDead()
    {
        return isDead; // Permite que otros scripts accedan al estado de muerte
    }
   


}
