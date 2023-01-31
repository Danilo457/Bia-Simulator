using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Scriptable/Dialogos")]
public class ScriptableDialogos : ScriptableObject
{
    public List<Dialogos> dialogos = new List<Dialogos>();
}

[Serializable]
public class Dialogos
{
    public string assunto;
    [TextArea(5,10)] public List<string> mensagens = new List<string>();
}
