using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SystemPersonagens : MonoBehaviour
{
    RectTransform telaIndicativa;

    [SerializeField] Detecta localPlayer;
    MouseController cursor;
    CanvasManager canvasManager;

    GameObject localDestination;
    GameObject caixaEscolhas;

    Image imageTime;

    bool trava;

    float time;
    float timeImage;

    [HideInInspector] public bool atvCaixaEscolhas;

    void Awake()
    {
        localPlayer.GetComponent<Detecta>();

        timeImage = 1.0f;
    }

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

        caixaEscolhas.SetActive(false);
    }

    void Update()
    {
        if (time > 0 && localPlayer.local && !atvCaixaEscolhas) {
            time -= Time.deltaTime * 100;

            telaIndicativa.anchoredPosition = new Vector2(0, time);

            if (time < 0)
                time = 0;
        }

        if (Input.GetKeyDown(KeyCode.E) && timeImage > 0 && localPlayer.local && !atvCaixaEscolhas) {
            atvCaixaEscolhas = !atvCaixaEscolhas;

            caixaEscolhas.SetActive(atvCaixaEscolhas);
        }

        if (atvCaixaEscolhas && !trava) {
            timeImage -= Time.deltaTime / 10.0f;

            imageTime.fillAmount = timeImage;

            if (timeImage <= 0) {
                timeImage = 1.0f;
                atvCaixaEscolhas = false;
            }

            cursor.MouseConfined();
        }

        if (!atvCaixaEscolhas && time < 150)
            cursor.MouseLockedFalse();

        if (!localPlayer.local || atvCaixaEscolhas) {
            time += Time.deltaTime * 150;

            telaIndicativa.anchoredPosition = new Vector2(0, time);

            if (time > 150)
                time = 150;
        }

        if (canvasManager.carregamento) {
            timeImage = 1.0f;

            cursor.MouseLockedFalse();

            trava = true;
            canvasManager.carregamento = false;
        }
    }
}
