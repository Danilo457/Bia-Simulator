using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Scriptable/Banco de Dados")]
public class ScriptableBancoDeDados : ScriptableObject
{
    public List<string> listNames = new List<string>();

    public List<Material> material = new List<Material>();
    public List<Mesh> mesh = new List<Mesh>();

    public GameObject avatarFeminino;
}
