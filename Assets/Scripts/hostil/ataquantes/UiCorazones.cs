using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
    public Image heartsImage; // Imagen UI para los corazones
    public Sprite[] heartSprites; // Array de 13 sprites (0 a 12)
    private HealthManager healthManager; // Referencia al HealthManager

    private void Start()
    {
        // Busca automáticamente el HealthManager en la escena
        healthManager = FindObjectOfType<HealthManager>();

        if (healthManager == null)
        {
            Debug.LogError("HealthManager no encontrado. Asegúrate de que el objeto del jugador tiene el script HealthManager.");
        }

        UpdateHeartsUI(); // Inicializar la UI con la salud actual
    }

    public void UpdateHeartsUI()
    {
        if (healthManager == null) return;

        // Determina el índice del sprite basado en la vida actual
        int spriteIndex = GetSpriteIndexFromHealth(healthManager.currentHealth);

        // Debug para verificar cálculos
        Debug.Log($"Vida actual: {healthManager.currentHealth}, Índice del sprite: {spriteIndex}");

        // Cambiar el sprite de la imagen UI
        heartsImage.sprite = heartSprites[spriteIndex];
    }

    private int GetSpriteIndexFromHealth(int currentHealth)
    {
        // Relación directa entre salud y sprite
        switch (currentHealth)
        {
            case 120: return 0;
            case 110: return 1;
            case 100: return 2;
            case 90: return 3;
            case 80: return 4;
            case 70: return 5;
            case 60: return 6;
            case 50: return 7;
            case 40: return 8;
            case 30: return 9;
            case 20: return 10;
            case 10: return 11;
            case 0: return 12; // Sprite de corazón vacío
            default:
                Debug.LogWarning("Salud no válida detectada, usando sprite de emergencia.");
                return 12; // Sprite por defecto en caso de valores inválidos
        }
    }
}




