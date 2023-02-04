using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemPersonagens : MonoBehaviour
{
    RectTransform telaIndicativa;

    [SerializeField] Detecta localPlayer;

    GameObject localDestination;

    float time;

    void Awake()
    {
        localPlayer.GetComponent<Detecta>();
    }

    void Start()
    {
        telaIndicativa = GameObject.Find("Indicador de Tecla para das Interações").GetComponent<RectTransform>();
        localDestination = GameObject.Find("Pai do Indicador");

        time = 150;

        telaIndicativa.SetParent(localDestination.transform, true);
        telaIndicativa.anchoredPosition = new Vector2(0, time);
    }

    void Update()
    {
        if (time > 0 && localPlayer.local) {
            time -= Time.deltaTime * 100;

            telaIndicativa.anchoredPosition = new Vector2(0, time);

            if (time < 0)
                time = 0;
        }
        
        if (!localPlayer.local) {
            time += Time.deltaTime * 150;

            telaIndicativa.anchoredPosition = new Vector2(0, time);

            if (time > 150)
                time = 150;
        }
    }
}
