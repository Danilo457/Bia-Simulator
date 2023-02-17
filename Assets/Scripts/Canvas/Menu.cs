using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    public ScriptableBancoDeDados bancoDados;

    GeneratePersons generatePersons = new GeneratePersons();

    MouseController mouseCursor;
    Spam spamSalaDosArmarios;

    [HideInInspector] public bool escape;

    GameObject fundo;
    [SerializeField] string namePainelMenu;
    [Header("        Menu Inicial")]
    [SerializeField] GameObject spriteMenu;
    [SerializeField] GameObject painelInicial;
    [SerializeField] GameObject painelTitulo;
    [SerializeField] GameObject spriteGarota;
    [SerializeField] GameObject infoVersion;
    [SerializeField] GameObject panelSettings;
    public GameObject buttonsPersonPlayer;
    [SerializeField] GameObject returnPanelInicial;
    public GameObject returnPanelConfig;
    [SerializeField] GameObject fundoTrocaDeCabelos;
    [SerializeField] GameObject fundoActvCabelo;
    [SerializeField] GameObject blusaTrocaDeCor;
    public GameObject painelEscolhasCustom;
    [SerializeField] GameObject cursor;
    [Header("        Custon Personagens")]
    public Image preVill;

    [Space]

    public GameObject controlSettings;
    public List<Button> buttonsToDisable; // Variavel não atribuida

    [Space]

    [SerializeField] RawImage trocaDeCabelo;
    [SerializeField] RawImage trocaDeCorBlusa;

    [SerializeField] List<Texture2D> textureCabelo = new List<Texture2D>();
    [SerializeField] List<Texture2D> textureBlusa = new List<Texture2D>();

    [HideInInspector] public int indexCanelo;
    [HideInInspector] public int indexBlusa;
    [HideInInspector] public bool actvBlusa;
    [HideInInspector] public bool actvCabelo;
    [HideInInspector] public float sensibility;

    [HideInInspector] public bool espera;

    bool sceneArmarios;

    void Awake()
    {
        spamSalaDosArmarios = FindObjectOfType<Spam>();

        trocaDeCabelo.texture = textureCabelo[0];
        trocaDeCorBlusa.texture = textureBlusa[0];

        spriteMenu.SetActive(true);
        painelInicial.SetActive(true);
        painelTitulo.SetActive(true);
        spriteGarota.SetActive(true);
        infoVersion.SetActive(true);

        cursor.SetActive(true);

        panelSettings.SetActive(false);
        buttonsPersonPlayer.SetActive(false);
        returnPanelInicial.SetActive(false);
        returnPanelConfig.SetActive(false);
        controlSettings.SetActive(false);
        blusaTrocaDeCor.SetActive(false);
        painelEscolhasCustom.SetActive(false);

        generatePersons.AddSavesLists(bancoDados);
    }

    void Start()
    {
        mouseCursor = FindObjectOfType<MouseController>();

        mouseCursor.MouseLockedFalse();

        generatePersons.modelosIndex = 0;

        preVill.sprite = generatePersons.Modelos(generatePersons.modelosIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && sceneArmarios) {
            escape = !escape;

            if (fundo == null)
                fundo = GameObject.Find(namePainelMenu);

            if (sceneArmarios)
                fundo.GetComponent<Image>().enabled = escape;

            if (escape) { /* Cursor Mouse */
                mouseCursor.MouseNoneTrue();

                Time.timeScale = 0; // Pause
            }else {
                mouseCursor.MouseLockedFalse();

                Time.timeScale = 1; // UnPause
            }
        }

        if (espera)
        {
            spamSalaDosArmarios = GameObject.Find("Spam ID 001").GetComponent<Spam>();

            generatePersons.spamPosition.Add(GameObject.Find("Avatar Feminino 001").transform);

            if (spamSalaDosArmarios.spam)
            {
                generatePersons.SpamSalaDosArmarios(bancoDados);

                spamSalaDosArmarios.spam = false;
            }

            espera = false;
        }
    }

    public void LoadScene(string name) {
        SceneManager.LoadSceneAsync(name);
#pragma warning disable CS0618 // O tipo ou membro � obsoleto
        Application.LoadLevelAdditive("Essencial");
#pragma warning restore CS0618 // O tipo ou membro � obsoleto

        sceneArmarios = true;

        mouseCursor.MouseLockedFalse();
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
        controlSettings.SetActive(false);
        returnPanelInicial.SetActive(false);
        returnPanelConfig.SetActive(false);

        panelSettings.SetActive(true);

        mouseCursor.MouseLockedFalse();
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

        mouseCursor.MouseConfined();
    }

    public void ButtonTrocaDeCabelo()
    {
        fundoTrocaDeCabelos.SetActive(true);
        fundoActvCabelo.SetActive(true);

        buttonsPersonPlayer.SetActive(false);
        returnPanelConfig.SetActive(true);

        mouseCursor.MouseConfined();
    }

    public void ReturnPanelConfig()
    {
        buttonsPersonPlayer.SetActive(true);
        returnPanelInicial.SetActive(false);
        returnPanelConfig.SetActive(false);

        fundoTrocaDeCabelos.SetActive(false);
        fundoActvCabelo.SetActive(false);
        blusaTrocaDeCor.SetActive(false);
        controlSettings.SetActive(false);
        painelEscolhasCustom.SetActive(false);

        mouseCursor.MouseLockedFalse();
    }

    public void ControlSettings()
    {
        panelSettings.SetActive(false);

        returnPanelInicial.SetActive(true);
        controlSettings.SetActive(true);

        mouseCursor.MouseConfined();
    }

    public void ToggleBlusaDaSintura() =>
        actvBlusa = !actvBlusa;

    public void ToggleCabelo() =>
        actvCabelo = !actvCabelo;

    public void ButtonRetroceder() {
        indexCanelo--;

        if (indexCanelo < 0)
            indexCanelo = 7;

        trocaDeCabelo.texture = textureCabelo[indexCanelo];
    }

    public void ButtonAvansar() {
        indexCanelo++;

        if (indexCanelo > 7)
            indexCanelo = 0;

        trocaDeCabelo.texture = textureCabelo[indexCanelo];
    }

    public void ButtonRetrocederBlusa() {
        indexBlusa--;

        if (indexBlusa < 0)
            indexBlusa = 4;

        trocaDeCorBlusa.texture = textureBlusa[indexBlusa];
    }

    public void ButtonAvansarBlusa() {
        indexBlusa++;

        if (indexBlusa > 4)
            indexBlusa = 0;

        trocaDeCorBlusa.texture = textureBlusa[indexBlusa];
    }

    /* Parte de Customizar - Avatar, Mesh, Uniforme */

    public void UniformeCustons()
    {
        buttonsPersonPlayer.SetActive(false);

        mouseCursor.MouseConfined();

        returnPanelConfig.SetActive(true);
        painelEscolhasCustom.SetActive(true);
    }

    public void CustonButtonAvansar()
    {
        generatePersons.modelosIndex++;

        if (generatePersons.modelosIndex > 1)
            generatePersons.modelosIndex = 0;

        preVill.sprite = generatePersons.Modelos(generatePersons.modelosIndex);
    }

    public void CustonButtonRetroceder()
    {
        generatePersons.modelosIndex--;

        if (generatePersons.modelosIndex < 0)
            generatePersons.modelosIndex = 1;

        preVill.sprite = generatePersons.Modelos(generatePersons.modelosIndex);
    }

    public void SensibilitySlider(float value) => sensibility = value;

    public void ExitGame() =>
        Application.Quit();
}
