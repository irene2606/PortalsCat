using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    [SerializeField] private Animator animator;       // Controlador de animaciones
    [SerializeField] private GameObject playButton;   // Botón de jugar
    [SerializeField] public DialogManager1 dialogManager; // Referencia al DialogManager

    [SerializeField] Dialog1 dialog;

    private void Start()
    {
        animator.Play("inicio"); // Comienza con la animación de inicio
    }

    public void OnPlayButtonClicked()
    {
        Debug.Log("botonPulsado");
        // Desactiva el botón de "Jugar"
        playButton.SetActive(false);

        // Activa el Trigger para "Gato Portal"
        animator.SetTrigger("PlayGatoPortal");
    }

    // Método llamado al final de "Gato Portal"
    public void OnGatoPortalFinished()
    {
        // Mostrar el diálogo del mago
        StartCoroutine(DialogManager1.Instance.ShowDialog(dialog));
    }
    

    private void OnDialogFinished()
    {
        // Activa el Trigger para "Mago Portal"NO!! Espera!!... Otra vez a buscarle
        animator.SetTrigger("PlayMagoPortal");
    }

    public void OnMagoPortalFinished()
    {
        // Cambia a la escena del juego
        SceneManager.LoadScene("Eleccion");
    }

}

