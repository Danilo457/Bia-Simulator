using UnityEngine;

public class Detecta : MonoBehaviour
{
    [HideInInspector] public bool local;
    [HideInInspector] public bool saiu;

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
        if (other.CompareTag("Player"))
        {
            local = false;
            saiu = true;
        }
    }
}
