using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
/* Bibliotecas Bia-Simulator */
using DialogosIndices;

public class CanvasManager : MonoBehaviour
{
    Menu menu;
    MouseController mouse;
    DialogueTrigger dialogueTrigger;
    Estudantes estudantes;

    /* Button, Start - Exit - volta ao Menu */
    [SerializeField] GameObject[] buttons;

    public GameObject[] componentsInteracoes;

    [HideInInspector] public bool carregamento;

    [Header("        Dialogos")]
    [SerializeField] TMP_Text escolhaTXT1;
    [SerializeField] GameObject UIManager;
    [SerializeField] List<GameObject> opicoesEscolhas = new List<GameObject>();

    int indice;

    void Awake()
    {
        menu = FindObjectOfType<Menu>();
        mouse = FindObjectOfType<MouseController>();
        dialogueTrigger = FindObjectOfType<DialogueTrigger>();

        componentsInteracoes[0].SetActive(true); // O Circulo de 8 Escolhas
    }

    void Start()
    {
        componentsInteracoes[1].SetActive(false); // Caixa de Escolhas Assunto
        UIManager.SetActive(false); // Caixa de Dilogos Inicia False

        for (int i = 0; i < opicoesEscolhas.Count; i++)
            opicoesEscolhas[i].SetActive(false);

        opicoesEscolhas[0].SetActive(true);
    }

    public void Indice(int indice) => // Seta Cada Indice para os Dialogos dos NPCs
        this.indice = indice;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (menu.escape) {
                for (int i = 0; i < buttons.Length; i++)
                    buttons[i].gameObject.SetActive(true);
            }else {
                for (int i = 0; i < buttons.Length; i++)
                    buttons[i].gameObject.SetActive(false);
            }
        }
    }

    public void LoadMenu(string name) =>
        SceneManager.LoadSceneAsync(name);

    public void ExitGame() =>
        Application.Quit();

    public void ButtonSocialize() 
    { /* Buttons Circulo de Escolhas */
        carregamento = true;

        componentsInteracoes[0].SetActive(false); // O Circulo de 8 Escolhas
        componentsInteracoes[1].SetActive(true); // Caixa de Escolhas Assunto
    }

    public void AtualizaOpcoesEscolhas(Estudantes estudantes)
    {
        this.estudantes = estudantes;

        DialogueEscolhas.IntroduceYourself(escolhaTXT1, estudantes.indice);

        foreach (var opcaoEscolha in opicoesEscolhas) // Percorre todas as Escolhas
            opcaoEscolha.SetActive(false); // Desabilita todas as Escolhas

        if (estudantes.indice < 2)
            opicoesEscolhas[0].SetActive(true); // Abilita Escolha 01

        opicoesEscolhas[6].SetActive(true); // Abilita o Exit
    }

    void AtualizaOpicoes()
    {
        componentsInteracoes[1].SetActive(false); // Caixa de Escolhas Assunto
        UIManager.SetActive(true);

        estudantes.indice++;
        AtualizaOpcoesEscolhas(estudantes); // Atualizar ao Clik do Botton

        mouse.MouseConfined();
    }

    public void ButtonAssunt()
    { /* Button Opisao 1 */
        if (estudantes.indice < 2)
        {
            AtualizaOpicoes();
            DialogueEscolhas.Apesentacao(dialogueTrigger, indice, estudantes.indice - 1);
        }
    }

    public void Opisao02()
    { /* Button Opisao 2 */
        if (opicoesEscolhas[2].activeInHierarchy)
        {
            // Continuar...
        }
    }

    // Opisao? Continuar...
}
