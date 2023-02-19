using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePersons
{
    Estudantes estudantes;

    public int indexMesh;

    int modelosIndex;
    int saiaIndex;

    public int IndexValue {
        get { return modelosIndex; }
        set { modelosIndex = value; }
    }
    public int IndexSaia
    {
        get { return saiaIndex; }
        set { saiaIndex = value; }
    }

    public List<Transform> spamPosition = new List<Transform>();

    public List<Sprite> saveModelosSprites = new List<Sprite>();
    public List<Mesh> saveMeshAvatar = new List<Mesh>();
    public List<Material> saveMaterialCorpo = new List<Material>();
    public List<Material> saveMaterialFace = new List<Material>();

    public Sprite Modelos(int num) { return saveModelosSprites[num]; }
    public Mesh AvatarMesh(int num) { return saveMeshAvatar[num]; }
    public Material AvatarMaterialCorpo(int num) { return saveMaterialCorpo[num]; }
    public Material AvatarMaterialFace(int num) { return saveMaterialFace[num]; }

    public void AddSavesLists(ScriptableBancoDeDados bancoDados)
    {
        saveModelosSprites.Add(bancoDados.sprites[0]);
        saveModelosSprites.Add(bancoDados.sprites[2]);
        saveModelosSprites.Add(bancoDados.sprites[3]);
        saveModelosSprites.Add(bancoDados.sprites[1]);

        saveMeshAvatar.Add(bancoDados.mesh[0]);
        saveMeshAvatar.Add(bancoDados.mesh[3]);

        saveMaterialCorpo.Add(bancoDados.material[3]);
        saveMaterialCorpo.Add(bancoDados.material[4]);
        saveMaterialCorpo.Add(bancoDados.material[5]);
        saveMaterialCorpo.Add(bancoDados.material[22]);

        saveMaterialFace.Add(bancoDados.material[6]);
    }

    void MeshAvatar(ScriptableBancoDeDados bancoDados, int num)
    {
        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis[num]).sharedMesh =
            AvatarMesh(indexMesh);
    }

    public void SpamSalaDosArmarios(ScriptableBancoDeDados bancoDados)
    {
        for (int i = 0; i < bancoDados.listNames.Count; i++)
            switch (bancoDados.listNames[i])
            {
                case "Amai Odayaka":
                    Object obj = Object.Instantiate(bancoDados.avatar, spamPosition[0].position, spamPosition[0].rotation);
                    obj.name = bancoDados.listNames[i];

                    RenomeCorpo(obj.name);
                    RenomeCabelo(obj.name);
                    MeshAvatar(bancoDados, 0);
                    MaterialAvatar(bancoDados, 0);
                    AddCobelos(bancoDados, 0, obj.name);
                    Olhos(bancoDados, obj.name, 0);
                    AddComponents(bancoDados, obj.name);

                    break;
                case "Alícia":
                    Object obj2 = Object.Instantiate(bancoDados.avatar, spamPosition[0].position, spamPosition[0].rotation);
                    obj2.name = bancoDados.listNames[i];

                    RenomeCorpo(obj2.name);
                    RenomeCabelo(obj2.name);
                    MeshAvatar(bancoDados, 1);
                    MaterialAvatar(bancoDados, 1);
                    AddCobelos(bancoDados, 1, obj2.name);
                    Olhos(bancoDados, obj2.name, 1);
                    AddComponents(bancoDados, obj2.name);

                    break;
            }
    }

    void MaterialAvatar(ScriptableBancoDeDados bancoDados, int num)
    {
        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis[num]).materials[0].shader =
            AvatarMaterialCorpo(modelosIndex).shader;
        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis[num]).materials[1].shader =
            AvatarMaterialCorpo(modelosIndex).shader;
        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis[num]).materials[2].shader =
            AvatarMaterialFace(0).shader;

        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis[num]).materials[0].mainTexture =
            AvatarMaterialCorpo(modelosIndex).mainTexture;
        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis[num]).materials[1].mainTexture =
            AvatarMaterialCorpo(modelosIndex).mainTexture;
        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis[num]).materials[2].mainTexture =
            AvatarMaterialFace(0).mainTexture;        
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
