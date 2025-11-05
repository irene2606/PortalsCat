using UnityEngine;

public static class PlayerPrefsManager
{
    private const string PuntosKey = "Puntos"; // Clave para almacenar puntos
    private const string MaxLevelUnlockedPacificoKey = "MaxLevelUnlockedPacifico"; // Clave para niveles pacíficos
    private const string MaxLevelUnlockedHostilKey = "MaxLevelUnlockedHostil"; // Clave para niveles hostiles

    // Guardar puntos
    public static void GuardarPuntos(int puntos)
    {
        PlayerPrefs.SetInt(PuntosKey, puntos);
        PlayerPrefs.Save(); // Asegurar que los datos se guarden inmediatamente
    }

    // Recuperar puntos
    public static int ObtenerPuntos()
    {
        return PlayerPrefs.GetInt(PuntosKey, 0); // Retorna 0 si no hay puntos almacenados
    }

    // Reiniciar puntos
    public static void ReiniciarPuntos()
    {
        PlayerPrefs.DeleteKey(PuntosKey);
    }

    // Verificar si hay puntos guardados
    public static bool HayPuntosGuardados()
    {
        return PlayerPrefs.HasKey(PuntosKey);
    }

    // Guardar el nivel máximo desbloqueado en el modo pacífico
    public static void GuardarNivelDesbloqueadoPacifico(int nivel)
    {
        PlayerPrefs.SetInt(MaxLevelUnlockedPacificoKey, nivel);
        PlayerPrefs.Save();
    }

    // Obtener el nivel máximo desbloqueado en el modo pacífico
    public static int ObtenerNivelDesbloqueadoPacifico()
    {
        return PlayerPrefs.GetInt(MaxLevelUnlockedPacificoKey, 1); // Por defecto, Nivel 1 del modo pacífico está desbloqueado
    }

    // Guardar el nivel máximo desbloqueado en el modo hostil
    public static void GuardarNivelDesbloqueadoHostil(int nivel)
    {
        PlayerPrefs.SetInt(MaxLevelUnlockedHostilKey, nivel);
        PlayerPrefs.Save();
    }

    // Obtener el nivel máximo desbloqueado en el modo hostil
    public static int ObtenerNivelDesbloqueadoHostil()
    {
        return PlayerPrefs.GetInt(MaxLevelUnlockedHostilKey, 1); // Por defecto, Nivel 1 del modo hostil está desbloqueado
    }

    // Reiniciar niveles desbloqueados en ambos modos
    public static void ReiniciarNiveles()
    {
        PlayerPrefs.DeleteKey(MaxLevelUnlockedPacificoKey);
        PlayerPrefs.DeleteKey(MaxLevelUnlockedHostilKey);
        PlayerPrefs.Save();
    }
}

