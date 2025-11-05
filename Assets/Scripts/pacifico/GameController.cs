using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameState { Playing, Waiting, Dialog }

public class GameController : MonoBehaviour
{
    public int tiempoLimite; // Tiempo límite en segundos (5 minutos)
    public TextMeshProUGUI tiempoTexto; // Referencia al texto del tiempo
    public TextMeshProUGUI puntosTexto; // Referencia al texto de los puntos
    public GameObject panelInicial; // Panel inicial
    public GameObject panelFin; // Panel de fin del juego
    public GameObject panelPuntos; // Panel de fin del juego
    public GameObject panelPocoTiempo; // Panel que aparece cuando quedan 30 segundos
    public TMP_Text finText; // Texto del panel de fin
    public TMP_Text detallesFinText; // Texto con los detalles finales
    public Button menuIniBoton; // Botón de menú inicial
    public Button continuarJugBoton; // Botón de continuar jugando
    public TMP_Text menuIniText; 
    public TMP_Text ContinuarJugText; 
    public PlayerMovement playerMovement;
    public AudioSource backgroundMusic; // Referencia al AudioSource del sonido de fondo

    private float tiempoRestante; // Tiempo restante en segundos
    private int puntos = 0; // Puntos iniciales
    private int puntosActuales = 0;
    private bool juegoTerminado = false;
    private float tiempoInicial;
    private bool panelMostrado = false; // Controlar si ya se mostró el panel
    //private bool juegoIniciado = false; // Controlar si el juego ya inició

    private GameState estadoJuego;


    public GameObject panelPausa; // Referencia al panel de pausa

    private bool juegoPausado = false;


    private bool sonidoActivo = true;
    public TMP_Text SonidoText; // Texto del panel de fin

    private void Start()
    {
        // Recuperar puntos si existen
        puntos = PlayerPrefsManager.ObtenerPuntos();

        tiempoRestante = tiempoLimite;
        tiempoInicial = tiempoLimite;

        ActualizarUI();
        panelInicial.SetActive(true); // Mostrar el panel inicial
        panelFin.SetActive(false); // Asegurarnos de que el panel esté oculto al inicio
        panelPuntos.SetActive(false); // Ocultar el panel de puntos inicialmente
        tiempoTexto.gameObject.SetActive(false); // Ocultar el tiempo hasta que inicie el juego
        puntosTexto.gameObject.SetActive(false); // Ocultar los puntos hasta que inicie el juego
        panelPocoTiempo.SetActive(false); // Asegurarnos de que el panel esté oculto al inicio

        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }

        estadoJuego = GameState.Waiting; // El estado inicial es esperando a que comience el juego

        // Configurar el pitch inicial de la música
        if (backgroundMusic != null)
        {
            backgroundMusic.pitch = 1.0f; // Normal
        }

        // Suscribir eventos del DialogManager
        DialogManager1.Instance.OnShowDialog += () =>
        {
            estadoJuego = GameState.Dialog;
        };

        DialogManager1.Instance.OnHideDialog += () =>
        {
            if (estadoJuego == GameState.Dialog)
                estadoJuego = GameState.Playing;
        };
    }

    private void Update()
    {
        if (estadoJuego == GameState.Waiting)
        {
            // Esperar a que el jugador pulse "E" para iniciar el juego
            if (Input.GetKeyDown(KeyCode.E))
            {
                IniciarJuego();
            }
            return;
        }

        if (estadoJuego == GameState.Dialog)
        {
            // Manejar la actualización del diálogo
            DialogManager1.Instance.HandleUpdate();
            return;
        }

        if (estadoJuego == GameState.Playing && !juegoTerminado)
        {
            // Restar tiempo
            tiempoRestante -= Time.deltaTime;

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                FinDelJuego(false); // No encontró al gato
            }

            // Mostrar el panel si el tiempo es menor o igual a 30 segundos y no se ha mostrado antes
            if (tiempoRestante <= 30 && !panelMostrado)
            {
                StartCoroutine(MostrarPanelPocoTiempo());
                panelMostrado = true; // Marcar como mostrado para evitar repetición
            }

            ActualizarUI();
            ActualizarMusica(); // Actualizar pitch de la música
        }
    }

    public void PausarJuego()
    {
        if (juegoPausado) return;

        juegoPausado = true;
        Time.timeScale = 0f; // Detener el tiempo del juego

        // Desactivar el movimiento del jugador y enemigos
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Mostrar el panel de pausa
        if (panelPausa != null)
        {
            panelPausa.SetActive(true);
        }
    }

    public void ReanudarJuego()
    {
        if (!juegoPausado) return;

        juegoPausado = false;
        Time.timeScale = 1f; // Reanudar el tiempo del juego

        // Reactivar el movimiento del jugador y enemigos
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Ocultar el panel de pausa
        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }
    }

    private void IniciarJuego()
    {
        estadoJuego = GameState.Playing;
        //juegoIniciado = true;
        panelInicial.SetActive(false); // Ocultar el panel inicial
        panelPuntos.SetActive(true); // Mostrar los puntos
        tiempoTexto.gameObject.SetActive(true); // Mostrar el tiempo
        puntosTexto.gameObject.SetActive(true); // Mostrar los puntos
    }

    private IEnumerator MostrarPanelPocoTiempo()
    {
        panelPocoTiempo.SetActive(true); // Mostrar el panel
        yield return new WaitForSeconds(5f); // Esperar 1 segundo
        panelPocoTiempo.SetActive(false); // Ocultar el panel
    }

    private void ActualizarUI()
    {
        // Formatear tiempo en minutos:segundos
        int minutos = Mathf.FloorToInt(tiempoRestante / 60);
        int segundos = Mathf.FloorToInt(tiempoRestante % 60);
        tiempoTexto.text = $"{minutos:0}:{segundos:00}";

        // Cambiar el color del texto si quedan 30 segundos o menos
        if (tiempoRestante <= 30)
        {
            tiempoTexto.color = Color.red; // Cambia a rojo
        }
        else
        {
            tiempoTexto.color = Color.white; // Vuelve a blanco si es necesario
        }

        // Actualizar puntos
        puntosTexto.text = $"{puntos}";
    }

    private void ActualizarMusica()
    {
        // Acelerar música cuando queden menos de 30 segundos
        if (backgroundMusic != null)
        {
            if (tiempoRestante <= 30)
            {
                backgroundMusic.pitch = 1.5f; // Aumentar pitch (más rápido)
            }
            else
            {
                backgroundMusic.pitch = 1.0f; // Volver a la velocidad normal
            }
        }
    }

    public void SumarPuntosPorTiempoRestante()
    {
        if (juegoTerminado) return;

        puntosActuales = Mathf.FloorToInt(tiempoRestante);

        // Sumar puntos basados en el tiempo restante
        puntos += puntosActuales;
        PlayerPrefsManager.GuardarPuntos(puntos);// Guardar puntos persistentes
        ActualizarUI();
    }

    public void FinDelJuego(bool victoria)
    {
        juegoTerminado = true;
        panelFin.SetActive(true);
        panelPuntos.SetActive(false);
        menuIniBoton.gameObject.SetActive(true);
        menuIniText.text = "Volver";
        tiempoTexto.gameObject.SetActive(false);
        puntosTexto.gameObject.SetActive(false);

        // Deshabilitar el movimiento del jugador
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (backgroundMusic != null)
        {
            backgroundMusic.pitch = 1.0f; // Resetear pitch al terminar el juego
        }

        if (victoria)
        {
            // Configurar el texto y color del panel de fin
            panelPocoTiempo.SetActive(false);
            finText.text = "¡Enhorabuena! Tu gato y tu podeis volver a casa.";
            panelFin.GetComponent<Image>().color = new Color(0, 0, 0, 0.9f); // Negro mate
            continuarJugBoton.gameObject.SetActive(true);
            ContinuarJugText.text = "Siguiente Nivel";

            // Mostrar puntos y tiempo
            float tiempoTomado = tiempoInicial - tiempoRestante;
            int minutos = Mathf.FloorToInt(tiempoTomado / 60);
            int segundos = Mathf.FloorToInt(tiempoTomado % 60);

            detallesFinText.text = $"Puntos: {puntosActuales}\nTiempo: {minutos:0}:{segundos:00}";

            // Desbloquear el siguiente nivel
            int nivelActual = SceneManager.GetActiveScene().buildIndex - 2;
            PlayerPrefsManager.GuardarNivelDesbloqueadoPacifico(nivelActual + 1);
        }
        else
        {
            // Configurar el texto y color del panel de fin
            finText.text = "Se cerro el portal, no encontraste a tiempo al gato.";
            panelFin.GetComponent<Image>().color = new Color(0.631f, 0.106f, 0.192f, 0.9f); 
            continuarJugBoton.gameObject.SetActive(true);
            ContinuarJugText.text = "Repetir";

            detallesFinText.text = ""; // No mostrar detalles en caso de derrota
        }
    }

    public void ContinuarJuego()
    {
        int nivelActual = SceneManager.GetActiveScene().buildIndex;

        // Cargar el siguiente nivel si se ganó, o repetir el nivel si se perdió
        if (ContinuarJugText.text == "Siguiente Nivel")
        {
            SceneManager.LoadScene(nivelActual + 1);
        }
        else
        {
            SceneManager.LoadScene(nivelActual); // Repetir el nivel
        }
    }

    public void ReiniciarJuego()
    {
        ReanudarJuego();
        int nivelActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(nivelActual); // Repetir el nivel
    }

    public void MenuInicial()
    {
        SceneManager.LoadScene("Eleccion"); // Asumiendo que el menú inicial es el índice 0
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








