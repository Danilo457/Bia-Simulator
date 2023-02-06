using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemPersonagens : MonoBehaviour
{
    RectTransform telaIndicativa;

    [SerializeField] Detecta localPlayer;

    GameObject localDestination;
    GameObject caixaEscolhas;

    float time;

    [HideInInspector] public bool atvCaixaEscolhas;

    void Awake()
    {
        localPlayer.GetComponent<Detecta>();
    }

    void Start()
    {
        telaIndicativa = GameObject.Find("Indicador de Tecla para das Intera��es").GetComponent<RectTransform>();
        localDestination = GameObject.Find("Pai do Indicador");

        caixaEscolhas = GameObject.Find("Caixa de Escolhas");

        time = 150;

        telaIndicativa.SetParent(localDestination.transform, true);
        telaIndicativa.anchoredPosition = new Vector2(0, time);

        caixaEscolhas.SetActive(false);
    }

    void Update()
    {
        if (time > 0 && localPlayer.local) {
            time -= Time.deltaTime * 100;

            telaIndicativa.anchoredPosition = new Vector2(0, time);

            if (time < 0)
                time = 0;
        }

        if (Input.GetKeyDown(KeyCode.E) && localPlayer.local) {
            atvCaixaEscolhas = !atvCaixaEscolhas;

            caixaEscolhas.SetActive(atvCaixaEscolhas);
        }

        if (!localPlayer.local) {
            time += Time.deltaTime * 150;

            telaIndicativa.anchoredPosition = new Vector2(0, time);

            if (time > 150)
                time = 150;
        }
    }
}
