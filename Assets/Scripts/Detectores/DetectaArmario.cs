using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectaArmario : MonoBehaviour
{
    [HideInInspector] public bool local;

    [SerializeField] string nameDono;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(nameDono))
            local = true;
    }

    void OnTriggerExit(Collider other) =>
        local = false;
}
