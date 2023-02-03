using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    [HideInInspector] public bool escape;

    [HideInInspector] public int num;

    GameObject fundo;
    [Header("        Menu Inicial")]
    [SerializeField] GameObject spriteMenu;
    [SerializeField] GameObject painelInicial;
    [SerializeField] GameObject painelTitulo;
    [SerializeField] GameObject spriteGarota;
    [SerializeField] GameObject infoVersion;
    [SerializeField] GameObject panelSettings;
    [SerializeField] GameObject buttonsPersonPlayer;
    [SerializeField] GameObject returnPanelConfig;
    [SerializeField] GameObject fundoTrocaDeCabelos;
    [SerializeField] GameObject fundoActvCabelo;
    [SerializeField] GameObject blusaTrocaDeCor;
    [SerializeField] GameObject cursor;

    public GameObject controlSettings;
    public List<Button> buttonsToDisable;

    [Space]

    [SerializeField] RawImage CaixaCores;

    [SerializeField] RawImage trocaDeCabelo;

    [SerializeField] List<Color> cor = new List<Color>();
    [SerializeField] List<Texture2D> textureCabelo = new List<Texture2D>();

    [HideInInspector] public int index;
    [HideInInspector] public bool actvBlusa;
    [HideInInspector] public bool actvCabelo;
    [HideInInspector] public float sensibility;

    bool sceneArmarios;
    bool boolSettings;

    void Awake()
    {
        CaixaCores.color = cor[0];
        trocaDeCabelo.texture = textureCabelo[0];

        spriteMenu.SetActive(true);
        painelInicial.SetActive(true);
        painelTitulo.SetActive(true);
        spriteGarota.SetActive(true);
        infoVersion.SetActive(true);

        cursor.SetActive(true);

        panelSettings.SetActive(false);
        buttonsPersonPlayer.SetActive(false);
        returnPanelConfig.SetActive(false);
        controlSettings.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && sceneArmarios) {
            escape = !escape;

            if (fundo == null)
                fundo = GameObject.Find("Panel");

            if (sceneArmarios)
                fundo.GetComponent<Image>().enabled = escape;

            if (escape) { /* Cursor Mouse */
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

     //           Time.timeScale = 0; // Pause
            }else {
    //            Cursor.lockState = CursorLockMode.Locked;
    //            Cursor.visible = false;

                Time.timeScale = 1; // UnPause
            }
        }
    }

    public void LoadScene(string name) {
        SceneManager.LoadSceneAsync(name);
#pragma warning disable CS0618 // O tipo ou membro � obsoleto
        Application.LoadLevelAdditive("Essencial");
#pragma warning restore CS0618 // O tipo ou membro � obsoleto

        sceneArmarios = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MenuInicial() {
        painelInicial.SetActive(true);
        painelTitulo.SetActive(true);
        spriteGarota.SetActive(true);

        panelSettings.SetActive(false);
    }

    public void Settings() {
        painelInicial.SetActive(false);
        painelTitulo.SetActive(false);
        spriteGarota.SetActive(false);
        buttonsPersonPlayer.SetActive(false);

        panelSettings.SetActive(true);
    }

    public void ButtonPersonalizarPlayer()
    {
        panelSettings.SetActive(false);
        buttonsPersonPlayer.SetActive(true);
    }

    public void ButtonBlusaSintura()
    {
        blusaTrocaDeCor.SetActive(true);

        buttonsPersonPlayer.SetActive(false);
        returnPanelConfig.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ButtonTrocaDeCabelo()
    {
        fundoTrocaDeCabelos.SetActive(true);
        fundoActvCabelo.SetActive(true);

        buttonsPersonPlayer.SetActive(false);
        returnPanelConfig.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReturnPanelConfig()
    {
        buttonsPersonPlayer.SetActive(true);
        returnPanelConfig.SetActive(false);

        fundoTrocaDeCabelos.SetActive(false);
        fundoActvCabelo.SetActive(false);
        blusaTrocaDeCor.SetActive(false);
        controlSettings.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ControlSettings()
    {
        panelSettings.SetActive(false);

        returnPanelConfig.SetActive(true);
        controlSettings.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }



    public void ToggleBlusaDaSintura() =>
        actvBlusa = !actvBlusa;

    public void ToggleCabelo() =>
        actvCabelo = !actvCabelo;

    public void ButtonRetroceder() {
        index--;

        if (index < 0)
            index = 7;

        trocaDeCabelo.texture = textureCabelo[index];
    }

    public void ButtonAvansar() {
        index++;

        if (index > 7)
            index = 0;

        trocaDeCabelo.texture = textureCabelo[index];
    }

    public void ButtonSettingsCorVerdClaro() {
        CaixaCores.color = cor[0];

        num = 0;
    }

    public void ButtonSettingsCorAzulClaro() {
        CaixaCores.color = cor[1];

        num = 1;
    }

    public void ButtonSettingsCorAmarelo() {
        CaixaCores.color = cor[2];

        num = 2;
    }

    public void ButtonSettingsCorRosa() {
        CaixaCores.color = cor[3];

        num = 3;
    }

    public void ButtonSettingsCorRoxo() {
        CaixaCores.color = cor[4];

        num = 4;
    }

    public void SensibilitySlider(float value) => sensibility = value;

    public void ExitGame() =>
        Application.Quit();
}
