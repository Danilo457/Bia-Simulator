using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estudantes : MonoBehaviour
{
    Dictionary<string, AudioClip> myListAudios = new Dictionary<string, AudioClip>();

    Transform targetArmario;

    Animation anim;
    AudioSource audioToks;

    AudioClip playAudio;

    Rigidbody rb;

    public void ListsAnimClips(ScriptableBancoDeDados bancoDados)
    {
        myListAudios.Add("AudioClip Abrir tranca Armaio", bancoDados.audio[0]);

        myListAudios.TryGetValue("AudioClip Abrir tranca Armaio", out playAudio);
    }

    public void StartEsts(string name)
    {
        anim = GameObject.Find(name).GetComponent<Animation>();
        audioToks = GameObject.Find(name).GetComponent<AudioSource>();
        rb = GameObject.Find(name).GetComponent<Rigidbody>();

        targetArmario = GameObject.Find("Target Position - " + name).transform;

        transform.position = targetArmario.position;
        transform.rotation = targetArmario.rotation;

        rb.mass = 500;

        switch (name)
        {
            case "Amai Odayaka":
                anim.Play("Juntar npc");
                break;
            case "Al�cia":
                anim.Play("Juntar npc");
                break;
            case "Carolina":
                anim.Play("ParadaNormal");
                break;
        }
    }

    public void EventAudioArmarioTranca() {
        audioToks.clip = playAudio;
        audioToks.loop = false;
        audioToks.volume = 0.3f;
        audioToks.minDistance = 0.5f;
        audioToks.maxDistance = 1.22f;

        audioToks.Play();
    }
}
