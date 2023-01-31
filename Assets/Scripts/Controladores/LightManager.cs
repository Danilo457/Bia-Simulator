using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightManager : MonoBehaviour
{
    [SerializeField] SceneDosArmarios lights = new SceneDosArmarios();

    public Dictionary<string, Light> myListLights = new Dictionary<string, Light>();

    void Awake()
    {
        myListLights.Add("light Sala Armarios 01", lights.luses[0]);
        myListLights.Add("light Sala Armarios 02", lights.luses[1]);
        myListLights.Add("light Sala Armarios 03", lights.luses[2]);
        myListLights.Add("light Sala Armarios 04", lights.luses[3]);
        myListLights.Add("light Sala Armarios 05", lights.luses[4]);
        myListLights.Add("light Sala Armarios 06", lights.luses[5]);
        myListLights.Add("light Sala Armarios 07", lights.luses[6]);
        myListLights.Add("light Sala Armarios 08", lights.luses[7]);
        myListLights.Add("light Sala Armarios 09", lights.luses[8]);
    }
}

[Serializable]
public class SceneDosArmarios
{
    public List<Light> luses = new List<Light>();
}
