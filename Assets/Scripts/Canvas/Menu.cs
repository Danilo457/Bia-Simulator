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

    [HideInInspector] public int indexCabelo;
    [HideInInspector] public int indexBlusa;
    [HideInInspector] public bool actvBlusa, actvPuseira;
    [HideInInspector] public bool actvCabelo;
    [HideInInspector] public float sensibility;

    [HideInInspector] public bool espera;

    bool sceneArmarios;

    [HideInInspector] public int indexUniforme; // Material Estilo de Roura Padrão da Escola
    [HideInInspector] public int indexMesh; // Mesh do Player

    /* Sistema de Save "Configurações de Usuario" */
    public Slider sensibilitySlider;
    public Toggle fullscreenTogglePuseiras;
    public Toggle fullscreenToggleBlusa;
    public Toggle fullscreenToggleCabeloPlayer;

    int indexModelo;
    int indiceCabelo, indiceBlusa;
    bool fullscreenPuseiras;
    bool fullscreenBlusa;
    bool fullscreenCabeloPlayer;

    void Awake()
    {
        spamSalaDosArmarios = FindObjectOfType<Spam>();

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
        fundoTrocaDeCabelos.SetActive(false);
        fundoActvCabelo.SetActive(false);

        generatePersons.AddSavesLists(bancoDados);
    }

    void Start()
    {
        mouseCursor = FindObjectOfType<MouseController>();

        LoadConfig();
        UpdateUI();

        mouseCursor.MouseLockedFalse();

        indexModelo = 0;

        preVill.sprite = bancoDados.spritsModelos.modelos[indexModelo];
    }

    void SaveConfig()
    { /* Sistema de Save Simples */
        indexCabelo = indiceCabelo;
        indexBlusa = indiceBlusa;
        sensibility = sensibilitySlider.value;

        fullscreenPuseiras = fullscreenTogglePuseiras.isOn;
        fullscreenBlusa = fullscreenToggleBlusa.isOn;
        fullscreenCabeloPlayer = fullscreenToggleCabeloPlayer.isOn;

        SaveSystem.SaveConfig(indexCabelo, indexBlusa, sensibility, fullscreenPuseiras, 
            fullscreenBlusa, fullscreenCabeloPlayer);
    }

    void LoadConfig()
    {
        SaveSystem.LoadConfig(out indexCabelo, out indexBlusa, out sensibility, out fullscreenPuseiras, 
            out fullscreenBlusa, out fullscreenCabeloPlayer);
    }

    void UpdateUI()
    { /* Seta todos os Valores Salvos */
        indiceCabelo = indexCabelo;
        indiceBlusa = indexBlusa;
        sensibilitySlider.value = sensibility;
        fullscreenTogglePuseiras.isOn = fullscreenPuseiras;
        fullscreenToggleBlusa.isOn = fullscreenBlusa;
        fullscreenToggleCabeloPlayer.isOn = fullscreenCabeloPlayer;

        trocaDeCabelo.texture = textureCabelo[indiceCabelo]; // Carrega o Cabelo "Visual"
        trocaDeCorBlusa.texture = textureBlusa[indiceBlusa]; // Carrega a Cor Blusa "Visual"
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

        SaveConfig(); // Save
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

        SaveConfig(); // Save
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

        SaveConfig(); // Save
    }

    public void ControlSettings()
    {
        panelSettings.SetActive(false);

        returnPanelInicial.SetActive(true);
        controlSettings.SetActive(true);

        mouseCursor.MouseConfined();
    }

    public void ToggleBlusaDaSintura() => // Assesorio do Player "Blusa Amarada a Sintura"
        actvBlusa = !actvBlusa;

    public void TogglePuseiras() => // Assesorio do Player "Puseiras"
        actvPuseira = !actvPuseira;

    public void ToggleCabelo() =>
        actvCabelo = !actvCabelo;

    public void ButtonRetroceder() {
        indexCabelo--;

        if (indexCabelo < 0)
            indexCabelo = 7;

        indiceCabelo = indexCabelo; // save o Indice

        trocaDeCabelo.texture = textureCabelo[indexCabelo];
    }

    public void ButtonAvansar() {
        indexCabelo++;

        if (indexCabelo > 7)
            indexCabelo = 0;

        indiceCabelo = indexCabelo; // save o Indice

        trocaDeCabelo.texture = textureCabelo[indexCabelo];
    }

    public void ButtonRetrocederBlusa() {
        indexBlusa--;

        if (indexBlusa < 0)
            indexBlusa = 4;

        indiceBlusa = indexBlusa; // save o Indice

        trocaDeCorBlusa.texture = textureBlusa[indexBlusa];
    }

    public void ButtonAvansarBlusa() {
        indexBlusa++;

        if (indexBlusa > 4)
            indexBlusa = 0;

        indiceBlusa = indexBlusa; // save o Indice

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
        indexModelo++;

        if (indexModelo > 1)
            indexModelo = 0;

        generatePersons.IndexSaia = 0;

        Modelos();

        indexMesh = indexModelo; // Quantidade de Mesh do Player

        ValorMesh(indexModelo); // Quantidade de Mesh
    }

    public void CustonButtonRetroceder() // Escolha dos Modelos
    {
        indexModelo--;

        if (indexModelo < 0)
            indexModelo = 1;

        generatePersons.IndexSaia = 0;

        Modelos();

        indexMesh = indexModelo; // Quantidade de Mesh do Player

        ValorMesh(indexModelo); // Quantidade de Mesh
    }

    public void CustonSaiaButtonAvansar() // Button Troca de Cor da Saia (++)
    {
        int saiasCount = bancoDados.spritsModelos.personalizar[indexModelo].saias.Count;
        generatePersons.IndexSaia = (generatePersons.IndexSaia + 1) % saiasCount;

        Personalizacao();
    }

    public void CustonSaiaButtonRetroceder() // Button Troca de Cor da Saia (--)
    {
        int saiasCount = bancoDados.spritsModelos.personalizar[indexModelo].saias.Count;
        generatePersons.IndexSaia = (generatePersons.IndexSaia + saiasCount - 1) % saiasCount;

        Personalizacao();
    }

    void Modelos()
    {
        indexUniforme = indexModelo; // Material Estilo de Roura Padrão da Escola

        preVill.sprite = bancoDados.spritsModelos.modelos[indexModelo];
        generatePersons.IndexSaia = 0;
        preVill.sprite = bancoDados.spritsModelos.personalizar[indexModelo].saias[0];
    }

    void Personalizacao()
    {
        preVill.sprite = bancoDados.spritsModelos.personalizar[indexModelo].saias[generatePersons.IndexSaia];
        indexUniforme = indexModelo + 2; // Material Estilo de Roura Padrão da Escola
    }

    void ValorMesh(int num) => // Quantidade de Mesh
        generatePersons.indexMesh = num;

    public void SensibilitySlider(float value) => sensibility = value; // mudar valor - Mouse sensibility


    public void ExitGame() =>
        Application.Quit();
}
