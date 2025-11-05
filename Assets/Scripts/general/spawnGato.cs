using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGato : MonoBehaviour
{
    [SerializeField] private Transform[] puntos; // Puntos donde puede aparecer el gato
    [SerializeField] private GameObject[] gato;  // Prefabs de gatos disponibles para instanciar

    // Start se ejecuta al inicio del juego
    void Start()
    {
        // Crea un único gato al inicio del juego
        CrearGato();
    }

    private void CrearGato()
    {
        // Selecciona aleatoriamente uno de los prefabs de gato
        int numeroGato = Random.Range(0, gato.Length);

        // Selecciona aleatoriamente uno de los puntos disponibles
        int indicePunto = Random.Range(0, puntos.Length);

        // Obtiene la posición del punto seleccionado
        Vector3 posicionSeleccionada = puntos[indicePunto].position;

        // Instancia el gato en la posición seleccionada
        Instantiate(gato[numeroGato], posicionSeleccionada, Quaternion.identity);
    }
}
