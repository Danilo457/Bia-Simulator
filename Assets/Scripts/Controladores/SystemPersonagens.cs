using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SystemPersonagens : MonoBehaviour
{
    RectTransform telaIndicativa;

    List<Detecta> detecta = new List<Detecta>();

    MouseController cursor;
    CanvasManager canvasManager;

    GameObject localDestination;
    GameObject caixaEscolhas;

    Image imageTime;

    [HideInInspector] public bool trava;

    float time;
    float timeImage;

    [HideInInspector] public bool atvCaixaEscolhas;

    void Awake() =>
        timeImage = 1.0f;

    public void GetIndice(string name) =>
        detecta.Add(GameObject.Find("DetectorCaixaConversar - " + name).GetComponent<Detecta>());

    void Start()
    {
        cursor = FindObjectOfType<MouseController>();
        canvasManager = FindObjectOfType<CanvasManager>();

        telaIndicativa = GameObject.Find("Indicador de Tecla para das Interações").GetComponent<RectTransform>();
        localDestination = GameObject.Find("Pai do Indicador");

        caixaEscolhas = GameObject.Find("Caixa de Escolhas");
        imageTime = GameObject.Find("CarregamentoTime").GetComponent<Image>();

        time = 150;

        telaIndicativa.SetParent(localDestination.transform, true);
        telaIndicativa.anchoredPosition = new Vector2(0, time);

        caixaEscolhas.SetActive(false); // O Circulo de 8 Escolhas
    }

    public void UpdateInteracoes(string name, int index)
    {
        if (detecta[index].local)
        {
            if (time > 0 && detecta[index].local && !atvCaixaEscolhas)
            {
                time -= Time.deltaTime * 100;

                telaIndicativa.anchoredPosition = new Vector2(0, time);

                if (time < 0)
                    time = 0;
            }

            if (Input.GetKeyDown(KeyCode.E) && timeImage > 0 && detecta[index].local && !atvCaixaEscolhas)
            {
                atvCaixaEscolhas = !atvCaixaEscolhas;

                caixaEscolhas.SetActive(true); // O Circulo de 8 Escolhas
            }

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

            if (!atvCaixaEscolhas && time < 150)
                cursor.MouseLockedFalse();

            if (canvasManager.carregamento)
            {
                timeImage = 1.0f;

                cursor.MouseLockedFalse();

                trava = true;
                canvasManager.carregamento = false;
            }

            Debug.Log($"local: {index}");
        }
        else if (!detecta[2].local && detecta[0].local && detecta[1].local 
            && detecta[3].local && detecta[4].local)
        {
            time += Time.deltaTime * 150;

            telaIndicativa.anchoredPosition = new Vector2(0, time);

            if (time > 150)
                time = 150;

            Debug.Log($"saiu: {name}");
        }
    }
}
