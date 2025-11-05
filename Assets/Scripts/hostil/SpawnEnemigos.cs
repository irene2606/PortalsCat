using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnEnemigos : MonoBehaviour
{
    private float minX, maxX, minY, maxY;

    [SerializeField] private Transform[] puntos; // Puntos del mapa para definir el rango de aparición
    [SerializeField] private GameObject[] enemigos;  // Prefabs de gatos disponibles para instanciar

    // Start se ejecuta al inicio del juego
    void Start()
    {
        // Calcula las posiciones mínima y máxima de los puntos dados
        maxX = puntos.Max(punto => punto.position.x);
        minX = puntos.Min(punto => punto.position.x);
        maxY = puntos.Max(punto => punto.position.y);
        minY = puntos.Min(punto => punto.position.y);

        // Crea un único gato al inicio del juego
        CrearGato();
    }

    private void CrearGato()
    {
        // Selecciona aleatoriamente uno de los prefabs de gato
        int numeroGato = Random.Range(0, enemigos.Length);

        // Genera una posición aleatoria dentro del rango definido
        Vector2 posicionAleatoria = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        // Instancia el gato en la posición generada
        Instantiate(enemigos[numeroGato], posicionAleatoria, Quaternion.identity);

        //TODO: ddecidir si se van a generar cada x tiempo o todos a la vez en ambos habria que 
        //modificar el codigo y ademas en el mapa añadir los puntos entre los que puede spawnear
        //
    }
}
