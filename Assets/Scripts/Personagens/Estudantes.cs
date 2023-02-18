using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estudantes : MonoBehaviour
{
    Dictionary<string, AnimationClip> myListClips = new Dictionary<string, AnimationClip>();
    Dictionary<string, AudioClip> myListAudios = new Dictionary<string, AudioClip>();

    Transform targetArmario;

    Animation anim;
    AudioSource audioToks;

    AudioClip playAudio;

    Rigidbody rb;

    public void ListsAnimClips(ScriptableBancoDeDados bancoDados)
    {
    //    myListClips.Add("Anim Parada Normal", dadosPer.alunos.clip[0]);
    //    myListClips.Add("Anim Parada Estilo 02", dadosPer.alunos.clip[1]);
    //    myListClips.Add("Anim Parada Abrir Armario", dadosPer.alunos.clip[2]);

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

    //    myListClips.TryGetValue("Anim Parada Abrir Armario", out AnimationClip a);

        anim.Play("Juntar npc");
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
