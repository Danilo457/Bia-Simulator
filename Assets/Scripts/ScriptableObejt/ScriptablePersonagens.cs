using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Scriptable/Dados dos Personagens")]
public class ScriptablePersonagens : ScriptableObject
{
    public Alunos alunos = new Alunos();
}

[Serializable]
public class Alunos
{
    public string name;

    public List<AnimationClip> clip = new List<AnimationClip>();
    public List<AudioClip> audio = new List<AudioClip>();
    public List<Material> material = new List<Material>();
}
