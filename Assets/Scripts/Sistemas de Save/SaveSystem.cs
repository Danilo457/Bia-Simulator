using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveConfig(int indexCabelo, int indexBlusa, float sensibility, 
        bool fullscreenPuseira, bool fullscreenBlusa, bool fullscreenCabeloPlayer)
    {
        PlayerPrefs.SetInt("cabeloIndice", indexCabelo);
        PlayerPrefs.SetInt("blusaIndice", indexBlusa);
        PlayerPrefs.SetFloat("Sensibility", sensibility);
        PlayerPrefs.SetInt("FullscreenPuseira", fullscreenPuseira ? 1 : 0);
        PlayerPrefs.SetInt("FullscreenBlusa", fullscreenBlusa ? 1 : 0);
        PlayerPrefs.SetInt("FullscreenCabeloPlayer", fullscreenCabeloPlayer ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static void LoadConfig(out int indexCabelo, out int indexBlusa, out float sensibility, 
        out bool fullscreenPuseira, out bool fullscreenBlusa, out bool fullscreenCabeloPlayer)
    {
        indexCabelo = PlayerPrefs.GetInt("cabeloIndice", 1);
        indexBlusa = PlayerPrefs.GetInt("blusaIndice", 1);
        sensibility = PlayerPrefs.GetFloat("Sensibility", 1.0f);
        fullscreenPuseira = PlayerPrefs.GetInt("FullscreenPuseira", 1) == 1;
        fullscreenBlusa = PlayerPrefs.GetInt("FullscreenBlusa", 1) == 1;
        fullscreenCabeloPlayer = PlayerPrefs.GetInt("FullscreenCabeloPlayer", 1) == 1;
    }
}
