using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePersons
{
    Estudantes estudantes;
    Menu menu;

    Dictionary<string, int> personagensIndices = new Dictionary<string, int>()
    {
        { "Amai Odayaka", 0 }, { "Alícia", 1 }, { "Carolina", 2 },
    };

    public int indexMesh; // Quantidade de Mesh

    int modelosIndex; // Quantidade de Modelos
    int saiaIndex;

    public int IndexModelo { // Quantidade de Modelos
        get { return modelosIndex; }
        set { modelosIndex = value; }
    }
    public int IndexSaia {
        get { return saiaIndex; }
        set { saiaIndex = value; }
    }

    public List<Transform> spamPosition = new List<Transform>();

    public List<Mesh> saveMeshAvatar = new List<Mesh>();
    public List<Material> saveMaterialCorpo = new List<Material>();
    public List<Material> saveMaterialFace = new List<Material>();

    public Mesh AvatarMesh(int num) { return saveMeshAvatar[num]; }
    public Material AvatarMaterialCorpo(int num) { return saveMaterialCorpo[num]; }
    public Material AvatarMaterialFace(int num) { return saveMaterialFace[num]; }

    public void AddSavesLists(ScriptableBancoDeDados bancoDados)
    {
        saveMeshAvatar.Add(bancoDados.mesh[0]);
        saveMeshAvatar.Add(bancoDados.mesh[3]);

        saveMaterialCorpo.Add(bancoDados.material[3]);
        saveMaterialCorpo.Add(bancoDados.material[4]);
        saveMaterialCorpo.Add(bancoDados.material[22]);
        saveMaterialCorpo.Add(bancoDados.material[5]);

        saveMaterialFace.Add(bancoDados.material[6]);
    }

    void MeshAvatar(ScriptableBancoDeDados bancoDados, int num) // Mesh dos NPCs
    {
        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis[num]).sharedMesh =
            AvatarMesh(indexMesh);
    }

    public void SpamSalaDosArmarios(ScriptableBancoDeDados bancoDados)
    {
        for (int i = 0; i < bancoDados.listNames.Count; i++)
        {
            string nome = bancoDados.listNames[i];

            if (personagensIndices.ContainsKey(nome))
            {
                SpawnAvatar(nome, bancoDados);
            }
        }
    }

    private void SpawnAvatar(string nomePersonagem, ScriptableBancoDeDados bancoDados)
    {
        int indice = personagensIndices[nomePersonagem];
        Object obj = Object.Instantiate(bancoDados.avatar, spamPosition[0].position, spamPosition[0].rotation);
        obj.name = nomePersonagem;

        RenomeCorpo(obj.name);
        RenomeCabelo(obj.name);
        MeshAvatar(bancoDados, indice);
        MaterialAvatar(bancoDados, indice);
        AddCobelos(bancoDados, indice, obj.name);
        Olhos(bancoDados, obj.name, indice);
        AddComponents(bancoDados, obj.name);
    }

    void MaterialAvatar(ScriptableBancoDeDados bancoDados, int num)
    {
        menu = Object.FindObjectOfType<Menu>();

        Material materialCorpo = AvatarMaterialCorpo(menu.indexUniforme);
        Material materialFace = AvatarMaterialFace(0);

        SetMaterialShader(bancoDados, num, 0, materialCorpo.shader);
        SetMaterialShader(bancoDados, num, 1, materialCorpo.shader);
        SetMaterialShader(bancoDados, num, 2, materialFace.shader);

        SetMaterialTexture(bancoDados, num, 0, materialCorpo.mainTexture);
        SetMaterialTexture(bancoDados, num, 1, materialCorpo.mainTexture);
        SetMaterialTexture(bancoDados, num, 2, materialFace.mainTexture);
    }

    void SetMaterialShader(ScriptableBancoDeDados bancoDados, int num, int materialIndex, Shader shader)
    {
        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis[num]).materials
            [materialIndex].shader = shader;
    }

    void SetMaterialTexture(ScriptableBancoDeDados bancoDados, int num, int materialIndex, Texture texture)
    {
        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis[num]).materials
            [materialIndex].mainTexture = texture;
    }

    void AddComponents(ScriptableBancoDeDados bancoDados, string name)
    {
        GameObject.Find(name).AddComponent<Estudantes>();

        estudantes = GameObject.Find(name).GetComponent<Estudantes>();
        estudantes.StartEsts(name);

        estudantes.ListsAnimClips(bancoDados);
    }

    void AddCobelos(ScriptableBancoDeDados bancoDados, int num, string name)
    {
        GameObject obj = Object.Instantiate(bancoDados.cabelos[num]);
        obj.transform.SetParent(GameObject.Find("CabelosPer - " + name).transform, false);
    }

    void Olhos(ScriptableBancoDeDados bancoDados, string name, int num)
    {
        Object obj = GameObject.Find("RightIris - Nemesis");
        obj.name = "RightIris - " + name + num;

        Object obj2 = GameObject.Find("LeftIris - Nemesis");
        obj2.name = "LeftIris - " + name + num;

        bancoDados.addGameObject.AddMesh("RightIris - " + name + num, "LeftIris - " + name + num);

        bancoDados.components.MeshIris("RightIris - " + name + num).mesh = bancoDados.mesh[4];
        bancoDados.components.MeshIris("LeftIris - " + name + num).mesh = bancoDados.mesh[4];

        bancoDados.components.MaterialIris("RightIris - " + name + num).material = bancoDados.material[7];
        bancoDados.components.MaterialIris("LeftIris - " + name + num).material = bancoDados.material[7];
    }

    void RenomeCorpo(string name)
    {
        Object obj = GameObject.Find("CorpoNemesis - Nemesis");
        obj.name = "CorpoNemesis - " + name;
    }

    void RenomeCabelo(string name)
    {
        Object obj = GameObject.Find("CabelosPer - Nemesis");
        obj.name = "CabelosPer - " + name;
    }
}
