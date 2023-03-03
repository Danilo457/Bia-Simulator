using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
/* Bibliotecas Bia-Simulator */
using ListasNames;

public class SystemPersonagens : MonoBehaviour
{
    RectTransform telaIndicativa;

    List<DetectaNPCs> detecta = new List<DetectaNPCs>();

    MouseController cursor;
    CanvasManager canvasManager;
    Menu menu;

    GameObject localDestination;
    GameObject caixaEscolhas;

    Image imageTime;

    [HideInInspector] public List<string> namesPersonagens = new List<string>(); // List de todos os Names
    [HideInInspector] public List<string> namesAnim = new List<string>(); // List das Animações
    [HideInInspector] public List<string> corPele = new List<string>(); // List das Etinias dos NPCs

    float time;
    float timeImage;

    bool playerInsideCollider;
    bool collidersDisabled;

    [HideInInspector] public bool trava;
    [HideInInspector] public bool atvCaixaEscolhas;

    void Awake() =>
        timeImage = 1.0f;

    public void GetIndice(string name) =>
        detecta.Add(GameObject.Find("DetectorCaixaConversar - " + name).GetComponent<DetectaNPCs>());

    void Start()
    {
        cursor = FindObjectOfType<MouseController>();
        canvasManager = FindObjectOfType<CanvasManager>();
        menu = FindObjectOfType<Menu>();

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

    public void UpdateInteracoes(int index)
    {
        if (detecta[index].local)
        {
            // desativar todos os outros colliders e atualizar a UI
            if (!collidersDisabled)
            {
                foreach (var detectaObj in detecta)
                {
                    if (detectaObj != detecta[index])
                    {
                        detectaObj.gameObject.SetActive(false);
                    }
                }

                collidersDisabled = true;
            }

            if (!menu.escape)
                UpdateUI(index);

            playerInsideCollider = true;
            Debug.Log($"index: {index}, entrou: {playerInsideCollider}");
        }
        else if (playerInsideCollider && detecta[index].saiu)
        {
            // reativar todos os colliders desativados
            if (collidersDisabled)
            {
                foreach (var detectaObj in detecta)
                {
                    detectaObj.gameObject.SetActive(true);
                }
                collidersDisabled = false;
            }

            time += Time.deltaTime * 150;
            telaIndicativa.anchoredPosition = new Vector2(0, time);

            if (time > 150)
                time = 150;

            if (time == 150)
                playerInsideCollider = false;

            Debug.Log($"index: {index}, saiu: {playerInsideCollider}");
        }
    }

    void UpdateUI(int index)
    {
        UpdateTime(index);
        CheckInput(index);
        UpdateChoices();
        UpdateCursor();
        CheckLoading();
    }

    void UpdateTime(int index)
    {
        if (time > 0 && detecta[index].local && !atvCaixaEscolhas)
        {
            time -= Time.deltaTime * 100;

            telaIndicativa.anchoredPosition = new Vector2(0, time);

            if (time < 0)
                time = 0;
        }
    }

    void CheckInput(int index)
    {
        if (Input.GetKeyDown(KeyCode.E) && timeImage > 0 && detecta[index].local && !atvCaixaEscolhas)
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
