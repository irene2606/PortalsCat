using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager3 : MonoBehaviour
{
    public Animator animator; // Referencia al Animator del fondo completo
    public GameObject dialogPanel; // Panel del diálogo
    [SerializeField] private TMP_Text dialogText;
    public Button playButton; // Botón "Jugar"
    public TextMeshProUGUI puntosTexto; // Referencia al texto de los puntos

    public GameObject opcionesPanel; // Panel del diálogo
    private bool sonidoActivo = true;
    public TMP_Text SonidoText; // Texto del panel de fin
    public AudioSource backgroundMusic;

    private int puntos = 0; // Puntos iniciales

    void Start()
    {
        // Configurar estado inicial
        dialogPanel.SetActive(false);
        playButton.onClick.AddListener(StartSequence);
        puntos = PlayerPrefsManager.ObtenerPuntos();
        puntosTexto.text = $"Puntos: {puntos}";
        opcionesPanel.SetActive(false);
    }

    void StartSequence()
    {
        // Reproducir la animación "gato"
        animator.Play("gato");
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        // Esperamos a que termine la animación "gato"
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Reproducimos la animación "mago"
        animator.Play("mago");

        // Mostramos el panel de diálogo
        dialogPanel.SetActive(true);
        dialogText.text = "NO!! Espera!!... Otra vez a buscarle :(";

        // Esperamos 2 segundos
        yield return new WaitForSeconds(2);

        // Ocultamos el panel de diálogo
        dialogPanel.SetActive(false);

        // Esperamos a que termine la animación "mago"
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        // Cambiamos a la escena del juego
        SceneManager.LoadScene("Eleccion");
    }

    public void mostrarOpciones(){
        opcionesPanel.SetActive(true);
    }

    public void quitarOpciones(){
        opcionesPanel.SetActive(false);
    }

    public void AlternarSonido()
    {
        if (backgroundMusic != null)
        {
            if (sonidoActivo)
            {
                backgroundMusic.volume = 0f; // Silenciar el sonido
                sonidoActivo = false;
                SonidoText.text = "Activar sonido";
            }
            else
            {
                backgroundMusic.volume = 1f; // Restaurar el sonido al volumen original
                sonidoActivo = true;
                SonidoText.text = "Desactivar sonido";
            }
        }
    }
    
}

