using UnityEngine;

public class Detecta : MonoBehaviour
{
    [HideInInspector] public bool local;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            local = true;
    }

    void OnTriggerExit(Collider other) =>
        local = false;
}
