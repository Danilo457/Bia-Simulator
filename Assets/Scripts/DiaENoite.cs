using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigsDia
{
    public bool trava;
    public float rotationDoSol;

    public float duracaoDiaMin = 60;

    [Range(0, 8)] public float intensidadeLuzDia = 1;
    [Range(0, 8)] public float intensidadeLuzAmbienteDia = 1;
    [Range(0.1f, 100)] public float velocidadeInteracao = 1;
    [Range(0.2f, 1)] public float horaLuzesNoite = 0.4f;
    [Range(0, 8)] public float reflexosLuzDia = 1;
}

public class DiaENoite : MonoBehaviour
{
    [SerializeField] ConfigsDia configsDoDia;
    [SerializeField] Light[] luzesNoite;

    Light componenteLuz;

    bool ativou1x;

    float intensidadeCor;

    void Awake()
    {
        if (!configsDoDia.trava)
            transform.Rotate(configsDoDia.rotationDoSol, 0, 0);
    }

    void Start()
    {
        AtivarLuzes(false);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        componenteLuz = GetComponent<Light>();
        componenteLuz.renderMode = LightRenderMode.ForcePixel;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientSkyColor = componenteLuz.color;
        intensidadeCor = Mathf.Clamp((transform.eulerAngles.x / 90), 0, 1);
    }

    void Update()
    {
        if (configsDoDia.duracaoDiaMin > 0 && configsDoDia.trava)
            transform.Rotate((6.0f / configsDoDia.duracaoDiaMin) * Time.deltaTime, 0, 0);

        if (transform.eulerAngles.x > 0 && transform.eulerAngles.x < 180) {
            intensidadeCor = Mathf.Lerp(intensidadeCor, Mathf.Abs(transform.eulerAngles.x / 90), 
                Time.deltaTime * configsDoDia.velocidadeInteracao);

            RenderSettings.ambientSkyColor = componenteLuz.color * Mathf.Clamp(intensidadeCor, 0.0f, 1.0f);

            RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, (0.15f + 
                configsDoDia.intensidadeLuzAmbienteDia * Mathf.Abs(transform.eulerAngles.x / 90)), 
                Time.deltaTime * configsDoDia.velocidadeInteracao);

            RenderSettings.reflectionIntensity = Mathf.Lerp(RenderSettings.reflectionIntensity, 
                (0.1f + configsDoDia.reflexosLuzDia * Mathf.Abs(transform.eulerAngles.x / 90)), 
                Time.deltaTime * configsDoDia.velocidadeInteracao);

            componenteLuz.intensity = Mathf.Lerp(componenteLuz.intensity, (configsDoDia.intensidadeLuzDia / 2 + 
                configsDoDia.intensidadeLuzDia * Mathf.Abs(transform.eulerAngles.x / 360)), 
                Time.deltaTime * configsDoDia.velocidadeInteracao);
        }else {
            intensidadeCor = Mathf.Lerp(intensidadeCor, 0.0f, Time.deltaTime * configsDoDia.velocidadeInteracao * 0.4f);
            RenderSettings.ambientSkyColor = componenteLuz.color * Mathf.Clamp(intensidadeCor, 0.0f, 1.0f);

            RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, 0.0f, 
                Time.deltaTime * configsDoDia.velocidadeInteracao);

            RenderSettings.reflectionIntensity = Mathf.Lerp(RenderSettings.reflectionIntensity, 0.0f, 
                Time.deltaTime * configsDoDia.velocidadeInteracao);

            componenteLuz.intensity = Mathf.Lerp(componenteLuz.intensity, 0.01f, Time.deltaTime * configsDoDia.velocidadeInteracao);
        }

        if (RenderSettings.ambientIntensity < configsDoDia.horaLuzesNoite && ativou1x == false) {
            AtivarLuzes(true);
            ativou1x = true;
        }else if (RenderSettings.ambientIntensity >= configsDoDia.horaLuzesNoite) {
            AtivarLuzes(false);
            ativou1x = false;
        }
    }

    void AtivarLuzes(bool condic)
    {
        for (int x = 0; x < luzesNoite.Length; x++)
            luzesNoite[x].enabled = condic;
    }
}
