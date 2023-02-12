using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Scriptable/Banco de Dados")]
public class ScriptableBancoDeDados : ScriptableObject
{
    public Components components = new Components();
    public AddGameObject addGameObject = new AddGameObject();

    public GameObject avatar;

    public List<string> listNames = new List<string>();

    public List<Material> material = new List<Material>();
    public List<Mesh> mesh = new List<Mesh>();

    public List<Sprite> sprites = new List<Sprite>();
}

public class Components
{
    public SkinnedMeshRenderer AvatarCuston(string name) { 
        return GameObject.Find(name).GetComponent<SkinnedMeshRenderer>(); }

    public MeshFilter MeshIris(string name) { 
        return GameObject.Find(name).GetComponent<MeshFilter>(); }

    public MeshRenderer MaterialIris(string name) { 
        return GameObject.Find(name).GetComponent<MeshRenderer>(); }
}

public class AddGameObject
{
    public void AddMesh(string nameTipo1, string nameTipo2) {
        GameObject.Find(nameTipo1).AddComponent<MeshFilter>();
        GameObject.Find(nameTipo2).AddComponent<MeshFilter>();

        GameObject.Find(nameTipo1).AddComponent<MeshRenderer>();
        GameObject.Find(nameTipo2).AddComponent<MeshRenderer>();
    }
}
