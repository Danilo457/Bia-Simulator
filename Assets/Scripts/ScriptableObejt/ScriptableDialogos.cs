using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Scriptable/Dialogos")]
public class ScriptableDialogos : ScriptableObject
{
    public List<Dialogos> dialogos = new List<Dialogos>();
}

[System.Serializable]
public class Dialogos
{
    public string assunto;

    [TextArea(5,10)] public List<string> mensagens = new List<string>();
}
