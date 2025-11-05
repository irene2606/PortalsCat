using System.Collections;
using UnityEngine;
using TMPro;


public class PlayerMovement : MonoBehaviour, Interactable
{
    public float speed;
    private bool isMoving;
    //private bool isDialog;
    private Vector2 input;

    private Animator animator;

    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    public LayerMask islandLayer;

    // Referencias para el diálogo
    public GameObject dialogPanel;
    [SerializeField] private TMP_Text dialogText;

    private GameController gameController;



    private void Awake()
    {
        animator = GetComponent<Animator>();
        dialogPanel.SetActive(false); // Asegurarnos de que el panel esté oculto al inicio
        gameController = FindObjectOfType<GameController>(); // Obtener referencia al GameController
    }

    private void Update()
    {
        if (!isMoving) // Permitir movimiento si no está interactuando
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var playerPos = transform.position;
                playerPos.x += input.x;
                playerPos.y += input.y;

                if (!isSolid(playerPos)&& !DialogManager1.Instance.isOpenDialog() && isInIsland(playerPos))
                {
                    StartCoroutine(Move(playerPos));
                }
            }
        }
        animator.SetBool("isMoving", isMoving);

        /*if (Input.GetKeyDown(KeyCode.E) && !isDialog)
        {
            Interact();
        }*/

        if(!DialogManager1.Instance.isOpenDialog() && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    IEnumerator Move(Vector3 playerPos)
    {
        isMoving = true;

        while ((playerPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = playerPos;

        isMoving = false;
    }

    private bool isSolid(Vector3 playerPos)
    {
        if (Physics2D.OverlapCircle(playerPos, 0.2f, solidObjectsLayer | interactableLayer) != null)
        {
            return true;
        }
        return false;
    }

    private bool isInIsland(Vector3 playerPos)
    {
        if (Physics2D.OverlapCircle(playerPos, 0.2f, islandLayer) != null)
        {
             return true;
        }
        return false;
    }

    public void Interact()
    {
        var playerDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + playerDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if (collider != null)
        {
           if (collider.CompareTag("Portal"))
            {
                Debug.Log("Jugador interactuó con el portal.");
                gameController.PausarJuego();
            }
            else if (collider.CompareTag("Gato"))
            {
                collider.GetComponent<Interactable>()?.Interact();

                // Mostrar el diálogo y reproducir la animación solo si la etiqueta es "Gato"
                StartCoroutine(ShowDialogAndPlayAnimation());
            }
            else
            {
                // Escribir en la consola si el objeto no tiene la etiqueta "Gato"
                Debug.Log("NPC1: Este objeto no es un gato.");
                collider.GetComponent<Interactable>()?.Interact();
            }
        }
    }

    IEnumerator ShowDialogAndPlayAnimation()
    {
        //isDialog = true;

        // Mostrar el panel de diálogo
        dialogPanel.SetActive(true);
        dialogText.text = "¡¡¡TE ENCONTRE!!! Aqui estabaas";

        yield return new WaitForSeconds(2); // Esperar 2 segundos

        // Sumar puntos y finalizar el juego
        gameController.SumarPuntosPorTiempoRestante();
        gameController.FinDelJuego(true);

        dialogPanel.SetActive(false);
        //isDialog = false;
    }
}




