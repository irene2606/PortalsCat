using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ReiniciarPuntos : MonoBehaviour
{
    public TextMeshProUGUI puntosTexto;
    
    private int puntos = 0;
    public void Reiniciar()
    {
        PlayerPrefsManager.ReiniciarPuntos();
        puntos = PlayerPrefsManager.ObtenerPuntos();
        puntosTexto.text = $"Puntos: {puntos}";
        PlayerPrefsManager.ReiniciarNiveles();
    }
}