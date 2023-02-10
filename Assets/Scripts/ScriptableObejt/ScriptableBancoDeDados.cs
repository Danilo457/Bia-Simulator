using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Scriptable/Banco de Dados")]
public class ScriptableBancoDeDados : ScriptableObject
{
    Variaveis variaveis = new Variaveis();

    public List<string> listNames = new List<string>();

    public List<Material> material = new List<Material>();
    public List<Mesh> mesh = new List<Mesh>();

    public List<GameObject> avatar = new List<GameObject>();

    public MeshRenderer MeshAdd() { return variaveis.meshadd = 
            GameObject.Find("LeftEmptyEyeNemesis").GetComponent<MeshRenderer>(); }
}

public class Variaveis
{
    public MeshRenderer meshadd;
}
