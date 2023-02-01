using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    [HideInInspector] public bool escape;

    [HideInInspector] public int num;

    GameObject fundo;
    [Header("        Fundos de Paineis")]
    [SerializeField] GameObject fundoSettings;
    [SerializeField] GameObject blusaTrocaDeCor;
    [SerializeField] GameObject fundoTrocaDeCabelos;
    [SerializeField] GameObject fundoActvCabelo;
    [SerializeField] GameObject panelSettings;
    public GameObject controlSettings;
    public List<Button> buttonsToDisable;

    [Header("        Buttons")]
    [SerializeField] GameObject buttonBlusaSintura;
    [SerializeField] GameObject buttonTrocaCabelos;

    [Space]

    [SerializeField] RawImage CaixaCores;

    [SerializeField] RawImage trocaDeCabelo;

    [SerializeField] List<Color> cor = new List<Color>();
    [SerializeField] List<Texture2D> textureCabelo = new List<Texture2D>();

    [HideInInspector] public int index;
    [HideInInspector] public bool actvBlusa;
    [HideInInspector] public bool actvCabelo;
    [HideInInspector] public float sensibility = 5;

    bool sceneArmarios;
    bool boolSettings;
    bool boolBlusa;
    bool boolCabelos;

    void Awake()
    {
        CaixaCores.color = cor[0];
        trocaDeCabelo.texture = textureCabelo[0];

        panelSettings.SetActive(false);
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

                Time.timeScale = 0; // Pause
            }else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

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

    public void SettingsInicial() {
        fundoSettings.SetActive(true);
        panelSettings.SetActive(true);

        buttonBlusaSintura.SetActive(false);
        buttonTrocaCabelos.SetActive(false);

        fundoTrocaDeCabelos.SetActive(false);
        fundoActvCabelo.SetActive(false);
        blusaTrocaDeCor.SetActive(false);
        controlSettings.SetActive(false);
    }

    public void Settings() {
        fundoSettings.SetActive(false);
        panelSettings.SetActive(false);

        buttonBlusaSintura.SetActive(true);
        buttonTrocaCabelos.SetActive(true);

        fundoTrocaDeCabelos.SetActive(false);
        fundoActvCabelo.SetActive(false);
        blusaTrocaDeCor.SetActive(false);
    }

    public void ControlSettings()
    {
        panelSettings.SetActive(false);
        
        
        controlSettings.SetActive(true);
    }

    public void ButtonPersonalizarPlayer() {
        panelSettings.SetActive(false);


        buttonBlusaSintura.SetActive(true);
        buttonTrocaCabelos.SetActive(true);
    }



    public void ButtonBlusaSintura() {
        boolBlusa = !boolBlusa;

        blusaTrocaDeCor.SetActive(boolBlusa);

        if (boolCabelos && boolBlusa)
            ButtonTrocaDeCabelo();
    }

    public void ToggleBlusaDaSintura() =>
        actvBlusa = !actvBlusa;

    public void ToggleCabelo() =>
        actvCabelo = !actvCabelo;

    public void ButtonTrocaDeCabelo() {
        boolCabelos = !boolCabelos;

        fundoTrocaDeCabelos.SetActive(boolCabelos);
        fundoActvCabelo.SetActive(boolCabelos);

        if (boolBlusa && boolCabelos)
            ButtonBlusaSintura();
    }

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
