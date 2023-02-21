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

    public int indexUniforme; // Material Estilo de Roura Padrão da Escola
    public int indexMesh; // Mesh do Player

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

        generatePersons.IndexModelo = 0;

        preVill.sprite = bancoDados.spritsModelos.modelos[generatePersons.IndexModelo];
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

    public void CustonButtonAvansar() // Escolha dos Modelos
    {
        generatePersons.IndexModelo++;

        if (generatePersons.IndexModelo > 1)
            generatePersons.IndexModelo = 0;

        generatePersons.IndexSaia = 0;

        Modelos();

        indexMesh = generatePersons.IndexModelo; // Quantidade de Mesh do Player

        ValorMesh(generatePersons.IndexModelo); // Quantidade de Mesh
    }

    public void CustonButtonRetroceder() // Escolha dos Modelos
    {
        generatePersons.IndexModelo--;

        if (generatePersons.IndexModelo < 0)
            generatePersons.IndexModelo = 1;

        generatePersons.IndexSaia = 0;

        Modelos();

        indexMesh = generatePersons.IndexModelo; // Quantidade de Mesh do Player

        ValorMesh(generatePersons.IndexModelo); // Quantidade de Mesh
    }

    public void CustonSaiaButtonAvansar() // Button Troca de Cor da Saia (++)
    {
        switch (generatePersons.IndexModelo)
        {
            case 0:
                if (generatePersons.IndexSaia < bancoDados.spritsModelos.personalizar[0].saias.Count - 1)
                {
                    generatePersons.IndexSaia++;
                }
                else
                {
                    generatePersons.IndexSaia = 0;
                }
                break;
            case 1:
                if (generatePersons.IndexSaia < bancoDados.spritsModelos.personalizar[1].saias.Count - 1)
                {
                    generatePersons.IndexSaia++;
                }
                else
                {
                    generatePersons.IndexSaia = 0;
                }
                break;
        }

        Personalizacao();
    }

    public void CustonSaiaButtonRetroceder() // Button Troca de Cor da Saia (--)
    {
        switch (generatePersons.IndexModelo)
        {
            case 0:
                if (generatePersons.IndexSaia > 0)
                {
                    generatePersons.IndexSaia--;
                }
                else
                {
                    generatePersons.IndexSaia = bancoDados.spritsModelos.personalizar[0].saias.Count - 1;
                }
                break;
            case 1:
                if (generatePersons.IndexSaia > 0)
                {
                    generatePersons.IndexSaia--;
                }
                else
                {
                    generatePersons.IndexSaia = bancoDados.spritsModelos.personalizar[1].saias.Count - 1;
                }
                break;
        }

        Personalizacao();
    }

    void Modelos()
    {
        indexUniforme = generatePersons.IndexModelo; // Material Estilo de Roura Padrão da Escola

        switch (generatePersons.IndexModelo)
        {
            case 0:
                preVill.sprite = bancoDados.spritsModelos.modelos[0];
                generatePersons.IndexSaia = 0;
                preVill.sprite = bancoDados.spritsModelos.personalizar[0].saias[0];
                break;
            case 1:
                preVill.sprite = bancoDados.spritsModelos.modelos[0];
                generatePersons.IndexSaia = 0;
                preVill.sprite = bancoDados.spritsModelos.personalizar[1].saias[0];
                break;
        }
    }

    void Personalizacao()
    {
        switch (generatePersons.IndexModelo)
        {
            case 0:
                preVill.sprite = bancoDados.spritsModelos.personalizar[0].saias[generatePersons.IndexSaia];
                indexUniforme = 2; // Material Estilo de Roura Padrão da Escola
                break;
            case 1:
                preVill.sprite = bancoDados.spritsModelos.personalizar[1].saias[generatePersons.IndexSaia];
                indexUniforme = 3; // Material Estilo de Roura Padrão da Escola
                break;
        }
    }

    void ValorMesh(int num) => // Quantidade de Mesh
        generatePersons.indexMesh = num;

    public void SensibilitySlider(float value) => sensibility = value;

    public void ExitGame() =>
        Application.Quit();
}
