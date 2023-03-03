using System.Collections.Generic;
using UnityEngine;

public class DetectaNPCs : MonoBehaviour
{
    [HideInInspector] public bool local;
    [HideInInspector] public bool saiu;
    public bool isDisabled = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            local = true;
            saiu = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        local = false;
        saiu = true;
    }
}
