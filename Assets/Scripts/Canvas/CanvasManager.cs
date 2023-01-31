using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    Menu menu;

    /* Button, Start - Exit - volta ao Menu */
    [SerializeField] GameObject[] buttons;

    void Awake()
    {
        menu = FindObjectOfType<Menu>();
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
}
