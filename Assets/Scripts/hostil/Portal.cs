using UnityEngine;

public class Portal : MonoBehaviour
{
    public LayerMask playerLayer; // Capa del jugador
    private bool isPlayerNearby = false; // Bandera para saber si el jugador está cerca

    [SerializeField] private GameControllerHostil gameController;

    private void Update()
    {
        
    }

    public void Interact()
    {
        gameController.PausarJuego();
    }

    
}





