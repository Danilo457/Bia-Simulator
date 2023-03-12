using UnityEngine;

public class DetectaNPCs : MonoBehaviour
{
    Estudantes estudantes;
    SystemPersonagens systemPersonagens;
    CanvasManager canvasManager;

    public int indice; // variável para armazenar o índice do NPC

    [HideInInspector] public bool local;
    [HideInInspector] public bool saiu;

    void Start()
    {
        systemPersonagens = FindObjectOfType<SystemPersonagens>();
        canvasManager = FindObjectOfType<CanvasManager>();

        estudantes = GameObject.Find(systemPersonagens.namesPersonagens[indice]).GetComponent<Estudantes>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            local = true;
            saiu = false;

            systemPersonagens.EntrouTrigger(this,estudantes, indice);

            canvasManager.Indice(indice); // Leva o Indice para o Dialogo
            canvasManager.AtualizaOpcoesEscolhas(estudantes); // Leva a Referencia Script Estudantes
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
