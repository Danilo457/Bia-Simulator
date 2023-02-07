using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    Menu menu;

    /* Button, Start - Exit - volta ao Menu */
    [SerializeField] GameObject[] buttons;

    [SerializeField] GameObject[] componentsInteracoes;

    void Awake()
    {
        menu = FindObjectOfType<Menu>();

        for (int i = 0; i < componentsInteracoes.Length; i++)
            componentsInteracoes[i].SetActive(true);
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

    public void ButtonSocialize() =>
        Debug.Log("Uma escolha de Assunto para comesar um Dialogo");
}
