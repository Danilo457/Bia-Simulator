using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
/* Bibliotecas Bia-Simulator */
using ListasNames;

public class SystemPersonagens : MonoBehaviour
{
    RectTransform telaIndicativa;

    DetectaNPCs detecta;
    MouseController cursor;
    CanvasManager canvasManager;
    Estudantes estudantes;
    Menu menu;

    GameObject localDestination;
    GameObject caixaEscolhas;

    Image imageTime;

    [HideInInspector] public List<string> namesPersonagens = new List<string>(); // List de todos os Names
    [HideInInspector] public List<string> namesAnim = new List<string>(); // List das Animações
    [HideInInspector] public List<string> corPele = new List<string>(); // List das Etinias dos NPCs

    float time;
    float timeImage;
    int indice;

    bool playerInsideCollider;

    [HideInInspector] public bool trava;
    [HideInInspector] public bool atvCaixaEscolhas;

    void Awake()
    {
        menu = FindObjectOfType<Menu>();
        cursor = FindObjectOfType<MouseController>();

        timeImage = 1.0f;

        Scene("Menu");
    }

    void Scene(string name)
    {
        if (menu == null) /* Verifica se a Scene não é a do Menu e Loga nela */
            SceneManager.LoadSceneAsync(name);
    }

    void Start()
    {
        canvasManager = FindObjectOfType<CanvasManager>();

        telaIndicativa = GameObject.Find("Indicador de Tecla para das Interações").GetComponent<RectTransform>();
        localDestination = GameObject.Find("Pai do Indicador");

        caixaEscolhas = GameObject.Find("Caixa de Escolhas");
        imageTime = GameObject.Find("CarregamentoTime").GetComponent<Image>();

        time = 150;

        telaIndicativa.SetParent(localDestination.transform, true);
        telaIndicativa.anchoredPosition = new Vector2(0, time);

        caixaEscolhas.SetActive(false); // O Circulo de 8 Escolhas

        foreach (Personagem namesPers in NamesList.personagensList) // Percorre todos os names dos Personagens
            namesPersonagens.Add(namesPers.name);

        foreach (Personagem animNames in NamesList.personagensList) // Percorre todos os names das Animações
            namesAnim.Add(animNames.anim);

        foreach (Personagem corPele in NamesList.personagensList) // Percorre os Tipos de tons de Pele dos NPCs
            this.corPele.Add(corPele.cor);
    }

    void Update()
    {
        if (detecta != null && estudantes.indice < 2) // indice < 3 como Interropitor "Temporario"
            UpdateInteracoes(detecta, indice);
    }

    public void EntrouTrigger(DetectaNPCs detecta, Estudantes estudantes, int indice)
    {
        this.detecta = detecta;
        this.indice = indice;
        this.estudantes = estudantes;
    }

    void UpdateInteracoes(DetectaNPCs detecta, int indice)
    {
        if (detecta.local)
        {
            if (!menu.escape)
                UpdateUI(detecta, indice);

            playerInsideCollider = true;
        }
        else if (playerInsideCollider && detecta.saiu)
        {
            time += Time.deltaTime * 150;
            telaIndicativa.anchoredPosition = new Vector2(0, time);

            if (time > 150)
                time = 150;

            if (time == 150)
                playerInsideCollider = false;
        }
    }

    void UpdateUI(DetectaNPCs detecta, int indice)
    {
        UpdateTime(detecta);
        CheckInput(detecta, indice);
        UpdateChoices();
        UpdateCursor();
        CheckLoading();
    }

    void UpdateTime(DetectaNPCs detecta)
    {
        if (time > 0 && detecta.local && !atvCaixaEscolhas)
        {
            time -= Time.deltaTime * 100;

            telaIndicativa.anchoredPosition = new Vector2(0, time);

            if (time < 0)
                time = 0;
        }
        else if (atvCaixaEscolhas)
        {
            time += Time.deltaTime * 150;
            telaIndicativa.anchoredPosition = new Vector2(0, time);

            if (time > 150)
                time = 150;
        }
    }

    void CheckInput(DetectaNPCs detecta, int indice)
    {
        if (Input.GetKeyDown(KeyCode.E) && timeImage > 0 && detecta.local && !atvCaixaEscolhas)
        {
            atvCaixaEscolhas = !atvCaixaEscolhas;

            caixaEscolhas.SetActive(true); // O Circulo de 8 Escolhas
        }
    }

    void UpdateChoices()
    {
        if (atvCaixaEscolhas && !trava)
        {
            timeImage -= Time.deltaTime / 10.0f;

            imageTime.fillAmount = timeImage;

            if (timeImage <= 0)
            {
                timeImage = 1.0f;
                atvCaixaEscolhas = false;

                caixaEscolhas.SetActive(false); // O Circulo de 8 Escolhas
            }

            cursor.MouseConfined();
        }
    }

    void UpdateCursor()
    {
        if (!atvCaixaEscolhas && time < 150)
            cursor.MouseLockedFalse();
    }

    void CheckLoading()
    {
        if (canvasManager.carregamento)
        {
            timeImage = 1.0f;

            cursor.MouseLockedFalse();

            trava = true;
            canvasManager.carregamento = false;
        }
    }
}
