using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Scriptable/Light")]
public class ScriptableLights : ScriptableObject
{
    LightManager scriptLights;

    public string nameMyListLights;

    public string nameApaga, nameAcende;

    public bool GetLampadaAcender() {
        scriptLights = GameObject.Find("Lampadas").GetComponent<LightManager>();

        scriptLights.myListLights.TryGetValue(nameMyListLights, out Light light);

        return light.enabled = true;
    }

    public bool GetLampadaApagar() {
        scriptLights = GameObject.Find("Lampadas").GetComponent<LightManager>();

        scriptLights.myListLights.TryGetValue(nameMyListLights, out Light light);

        return light.enabled = false;
    }
}
