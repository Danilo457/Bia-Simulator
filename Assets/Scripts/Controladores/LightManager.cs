using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] SceneDosArmarios lights = new SceneDosArmarios();

    public Dictionary<string, Light> myListLights = new Dictionary<string, Light>();

    void Awake()
    {
        for (int i = 0; i < lights.luses.Count; i++)
        {
            string key = $"light Sala Armarios {i + 1:00}";
            myListLights.Add(key, lights.luses[i]);
        }
    }
}

[System.Serializable]
public class SceneDosArmarios
{
    public List<Light> luses = new List<Light>();
}
