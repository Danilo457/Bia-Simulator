using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estudantes : MonoBehaviour
{
    Dictionary<string, AudioClip> myListAudios = new Dictionary<string, AudioClip>();

    Dictionary<string, string> animations = new Dictionary<string, string>
    {
        {  "Amai Odayaka"  , "Juntar npc"         },
        {  "Alícia"        ,  "Juntar npc"        },
        {  "Carolina"      ,  "ParadaNormal"      },
        {  "Alana"         ,  "ParadaNormal"      },
        {  "Olivia"        ,  "Parada Estolo 02"  }
    };

    SystemPersonagens systemPersonagens;
    MouseController cursor;

    Transform targetArmario;

    Animation anim;
    AudioSource audioToks;

    AudioClip playAudio;

    Rigidbody rb;

    string nome;

    int index;

    public void ListsAnimClips(ScriptableBancoDeDados bancoDados)
    {
        myListAudios.Add("AudioClip Abrir tranca Armaio", bancoDados.audio[0]);

        myListAudios.TryGetValue("AudioClip Abrir tranca Armaio", out playAudio);
    }

    public void StartEsts(string name, int indice)
    {
        systemPersonagens = FindAnyObjectByType<SystemPersonagens>();
        cursor = FindObjectOfType<MouseController>();

        anim = GameObject.Find(name).GetComponent<Animation>();
        audioToks = GameObject.Find(name).GetComponent<AudioSource>();
        rb = GameObject.Find(name).GetComponent<Rigidbody>();

        targetArmario = GameObject.Find("Target Position - " + name).transform;

        transform.position = targetArmario.position;
        transform.rotation = targetArmario.rotation;

        nome = name;
        index = indice;

        rb.mass = 500;

        if (animations.ContainsKey(name))
            anim.Play(animations[name]);
    }

    void Update() =>
        systemPersonagens.UpdateInteracoes(nome, index);

    public void EventAudioArmarioTranca() {
        audioToks.clip = playAudio;
        audioToks.loop = false;
        audioToks.volume = 0.3f;
        audioToks.minDistance = 0.5f;
        audioToks.maxDistance = 1.22f;

        audioToks.Play();
    }
}
