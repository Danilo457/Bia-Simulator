using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    Menu menu;
    MouseController mouse;
    DialogueTrigger dialogueTrigger;

    /* Button, Start - Exit - volta ao Menu */
    [SerializeField] GameObject[] buttons;

    [SerializeField] GameObject[] componentsInteracoes;

    [HideInInspector] public bool carregamento;

    [Header("        Ativar os Dialogos")]
    [SerializeField] GameObject UIManager;

    void Awake()
    {
        menu = FindObjectOfType<Menu>();
        mouse = FindObjectOfType<MouseController>();
        dialogueTrigger = FindObjectOfType<DialogueTrigger>();

        componentsInteracoes[0].SetActive(true);
    }

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

    /* Buttons Circulo de Escolhas */

    public void ButtonSocialize() {
        carregamento = true;

        componentsInteracoes[0].SetActive(false);
        componentsInteracoes[1].SetActive(true);
    }

    public void CaixaEscolhas() {
        componentsInteracoes[1].SetActive(false);

        UIManager.SetActive(true);

        dialogueTrigger.StartDialogue();

        mouse.MouseConfined();
    }
}
