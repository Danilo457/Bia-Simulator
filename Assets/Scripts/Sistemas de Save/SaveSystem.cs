using UnityEngine;

public static class SaveSystem
{
    public static void SaveConfig(int indexCabelo, int indexBlusa, float sensibility, 
        bool fullscreenPuseira, bool fullscreenBlusa, bool fullscreenCabeloPlayer)
    {
        PlayerPrefs.SetInt("cabeloIndice", indexCabelo); // Save Modelo do Cabelo
        PlayerPrefs.SetInt("blusaIndice", indexBlusa); // Save Cor da Blusa que ta na Sintura
        PlayerPrefs.SetFloat("Sensibility", sensibility); // Save a Semsibilidade do Mouse
        PlayerPrefs.SetInt("FullscreenPuseira", fullscreenPuseira ? 1 : 0); // Save Puseira dos Brasos { true : false }
        PlayerPrefs.SetInt("FullscreenBlusa", fullscreenBlusa ? 1 : 0); // Save Blusa Amarrada a Sintura { true : false }
        PlayerPrefs.SetInt("FullscreenCabeloPlayer", fullscreenCabeloPlayer ? 1 : 0); // Save Player Careca { true : false }
        PlayerPrefs.Save();
    }

    public static void LoadConfig(out int indexCabelo, out int indexBlusa, out float sensibility, 
        out bool fullscreenPuseira, out bool fullscreenBlusa, out bool fullscreenCabeloPlayer)
    {
        indexCabelo = PlayerPrefs.GetInt("cabeloIndice", 1); // Carrega Modelo do Cabelo
        indexBlusa = PlayerPrefs.GetInt("blusaIndice", 1); // Carrega Cor da Blusa que ta na Sintura
        sensibility = PlayerPrefs.GetFloat("Sensibility", 1.0f); // Carrega a Semsibilidade do Mouse
        fullscreenPuseira = PlayerPrefs.GetInt("FullscreenPuseira", 1) == 1; // Carrega Puseira dos Brasos { true : false }
        fullscreenBlusa = PlayerPrefs.GetInt("FullscreenBlusa", 1) == 1; // Carrega Blusa Amarrada a Sintura { true : false }
        fullscreenCabeloPlayer = PlayerPrefs.GetInt("FullscreenCabeloPlayer", 1) == 1; // Carrega Player Careca { true : false }
    }
}
