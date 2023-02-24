using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveConfig(float sensibility, bool fullscreenPuseira, bool fullscreenBlusa)
    {
        PlayerPrefs.SetFloat("Sensibility", sensibility);
        PlayerPrefs.SetInt("FullscreenPuseira", fullscreenPuseira ? 1 : 0);
        PlayerPrefs.SetInt("FullscreenBlusa", fullscreenBlusa ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static void LoadConfig(out float sensibility, out bool fullscreenPuseira, out bool fullscreenBlusa)
    {
        sensibility = PlayerPrefs.GetFloat("Sensibility", 1.0f);
        fullscreenPuseira = PlayerPrefs.GetInt("FullscreenPuseira", 1) == 1;
        fullscreenBlusa = PlayerPrefs.GetInt("FullscreenBlusa", 1) == 1;
    }
}
