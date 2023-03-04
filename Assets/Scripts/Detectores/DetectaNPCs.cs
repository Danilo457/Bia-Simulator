using UnityEngine;

public class DetectaNPCs : MonoBehaviour
{
    SystemPersonagens systemPersonagens;

    [HideInInspector] public bool local;
    [HideInInspector] public bool saiu;

    string nameNPC;
    int index;
    internal bool scriptEnabled;

    private void Start() =>
        systemPersonagens = FindObjectOfType<SystemPersonagens>();

    public void ColetaDados(string name, int indice)
    {
        nameNPC = name;
        index = indice;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && systemPersonagens.namesPersonagens[index] == nameNPC)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

            local = true;
            saiu = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && systemPersonagens.namesPersonagens[index] == nameNPC)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

            local = false;
            saiu = true;
        }
    }
}
