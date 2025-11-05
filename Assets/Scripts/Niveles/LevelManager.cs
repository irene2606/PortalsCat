using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons; // Referencia a los botones de los niveles
    public enum Mode { Pacifico, Hostil } // Enumeración para los dos modos

    public Mode currentMode; // Establecer el modo por defecto a Pacífico

    void Start()
    {
        // Configurar botones según los niveles desbloqueados
        ConfigurarBotones();
    }

    void ConfigurarBotones()
    {
        int maxLevelUnlocked = 0;

        // Dependiendo del modo actual, obtenemos el progreso de los niveles
        if (currentMode == Mode.Pacifico)
        {
            maxLevelUnlocked = PlayerPrefsManager.ObtenerNivelDesbloqueadoPacifico();
        }
        else if (currentMode == Mode.Hostil)
        {
            maxLevelUnlocked = PlayerPrefsManager.ObtenerNivelDesbloqueadoHostil();
        }

        // Configurar los botones de niveles basados en el progreso de ese modo
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 <= maxLevelUnlocked)
            {
                levelButtons[i].interactable = true; // Habilitar el botón
            }
            else
            {
                levelButtons[i].interactable = false; // Deshabilitar el botón
            }
        }
    }

    public void CompletarNivel(int nivelIndex)
    {
        int maxLevelUnlocked = 0;

        // Guardar el progreso del nivel completado dependiendo del modo
        if (currentMode == Mode.Pacifico)
        {
            maxLevelUnlocked = PlayerPrefsManager.ObtenerNivelDesbloqueadoPacifico();
            if (nivelIndex + 1 > maxLevelUnlocked)
            {
                PlayerPrefsManager.GuardarNivelDesbloqueadoPacifico(nivelIndex + 1);
            }
        }
        else if (currentMode == Mode.Hostil)
        {
            maxLevelUnlocked = PlayerPrefsManager.ObtenerNivelDesbloqueadoHostil();
            if (nivelIndex + 1 > maxLevelUnlocked)
            {
                PlayerPrefsManager.GuardarNivelDesbloqueadoHostil(nivelIndex + 1);
            }
        }

        ConfigurarBotones(); // Actualizar el estado de los botones
    }

    public void CambiarModo(Mode nuevoModo)
    {
        currentMode = nuevoModo; // Cambiar al modo seleccionado
        ConfigurarBotones(); // Actualizar los botones según el nuevo modo
    }

    public void CargarNivel(string sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }

    public void ReiniciarProgreso()
    {
        // Reiniciar todos los niveles
        PlayerPrefsManager.ReiniciarNiveles();
        ConfigurarBotones();

    }
}

