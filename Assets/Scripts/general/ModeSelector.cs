using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelector : MonoBehaviour
{
    public LevelManager levelManager; // Referencia al LevelManager

    public void SeleccionarModoPacifico()
    {
        levelManager.CambiarModo(LevelManager.Mode.Pacifico);
    }

    public void SeleccionarModoHostil()
    {
        levelManager.CambiarModo(LevelManager.Mode.Hostil);
    }
}

