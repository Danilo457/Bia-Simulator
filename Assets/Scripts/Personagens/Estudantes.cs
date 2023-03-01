using System.Collections.Generic;
using UnityEngine;
/* Bibliotecas Bia-Simulator */
using Conversor;

public class Estudantes : MonoBehaviour
{
    Dictionary<string, AudioClip> myListAudios = new Dictionary<string, AudioClip>();

    SystemPersonagens systemPersonagens;

    Transform targetArmario;

    Animation anim;
    AudioSource audioToks;

    AudioClip playAudio;

    Rigidbody rb;

    int index;

    public void ListsAnimClips(ScriptableBancoDeDados bancoDados)
    {
        myListAudios.Add("AudioClip Abrir tranca Armaio", bancoDados.audio[0]);

        myListAudios.TryGetValue("AudioClip Abrir tranca Armaio", out playAudio);
    }

    public void StartEsts(ScriptableBancoDeDados bancoDados, string name, int indice, int uniformeIndice)
    {
        systemPersonagens = FindAnyObjectByType<SystemPersonagens>();

        anim = GameObject.Find(name).GetComponent<Animation>();
        audioToks = GameObject.Find(name).GetComponent<AudioSource>();
        rb = GameObject.Find(name).GetComponent<Rigidbody>();

        targetArmario = GameObject.Find("Target Position - " + name).transform;

        transform.position = targetArmario.position;
        transform.rotation = targetArmario.rotation;

        index = indice;

        rb.mass = 500;

        anim.Play(systemPersonagens.namesAnim[indice]); // Adiciona as Anima��es de todos os NPCs

        if (indice == 4)
            NPCOlivia(bancoDados, name, uniformeIndice);
    }

    void NPCOlivia(ScriptableBancoDeDados bancoDados, string name, int uniformeIndice)
    {
        /* Bolsa de Colocar o Viol�o */
        GameObject.Find("GuitarCase - " + name).GetComponent<MeshFilter>().mesh = bancoDados.mesh[10];
        GameObject.Find("GuitarCase - " + name).GetComponent<MeshRenderer>().material = bancoDados.material[28];

        GameObject objRight = GameObject.Find("RightMeia - " + name); // Meia Right , Pad�o
        GameObject objLeft = GameObject.Find("LeftMeia - " + name); // Meia Left , Pad�o

        objRight.SetActive(false); // Desativar a Pad�o
        objLeft.SetActive(false); // Desativar a Pad�o

        Valores.IndiceOlivia(uniformeIndice);

        Corpo(bancoDados, name, 0);
        Corpo(bancoDados, name, 1);
    }

    void Corpo(ScriptableBancoDeDados bancoDados, string name, int num)
    {
        GameObject.Find("CorpoNemesis - " + name).GetComponent<SkinnedMeshRenderer>()
            .materials[num].shader = bancoDados.material[Valores.index].shader;

        GameObject.Find("CorpoNemesis - " + name).GetComponent<SkinnedMeshRenderer>()
            .materials[num].mainTexture = bancoDados.material[Valores.index].mainTexture;
    }

    void Update() =>
        systemPersonagens.UpdateInteracoes(index);

    public void EventAudioArmarioTranca() { /* Evento "Acontece na Anima��o Abrir Armario" */
        audioToks.clip = playAudio;
        audioToks.loop = false;
        audioToks.volume = 0.3f;
        audioToks.minDistance = 0.5f;
        audioToks.maxDistance = 1.22f;

        audioToks.Play();
    }
}
