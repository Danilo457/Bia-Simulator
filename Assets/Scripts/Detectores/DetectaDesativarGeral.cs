using UnityEngine;

public class DetectaDesativarGeral : MonoBehaviour
{
    public string nameCollider;

    [SerializeField] ScriptableLights lights;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (nameCollider == lights.nameMyListLights + lights.nameAcende)
                lights.GetLampadaAcender();

            if (nameCollider == lights.nameMyListLights + lights.nameApaga)
                lights.GetLampadaApagar();
        }
    }
}
