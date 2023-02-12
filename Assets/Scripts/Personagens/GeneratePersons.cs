using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePersons
{
    public int modelosIndex;

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

        saveMeshAvatar.Add(bancoDados.mesh[0]);
        saveMeshAvatar.Add(bancoDados.mesh[3]);

        saveMaterialCorpo.Add(bancoDados.material[3]);
        saveMaterialCorpo.Add(bancoDados.material[4]);

        saveMaterialFace.Add(bancoDados.material[6]);
    }

    public void AddGameObject(ScriptableBancoDeDados bancoDados)
    {
        bancoDados.addGameObject.AddMesh("RightIris - Nemesis", "LeftIris - Nemesis");

        bancoDados.components.MeshIris("RightIris - Nemesis").mesh = bancoDados.mesh[4];
        bancoDados.components.MeshIris("LeftIris - Nemesis").mesh = bancoDados.mesh[4];

        bancoDados.components.MaterialIris("RightIris - Nemesis").material = bancoDados.material[6];
        bancoDados.components.MaterialIris("LeftIris - Nemesis").material = bancoDados.material[6];
    }

    public void MeshAvatar(ScriptableBancoDeDados bancoDados)
    {
        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis).sharedMesh =
            AvatarMesh(modelosIndex);
    }

    public void SpamSalaDosArmarios(ScriptableBancoDeDados bancoDados)
    {
        for (int i = 0; i < bancoDados.listNames.Count; i++)
            switch (bancoDados.listNames[i])
            {
                case "Amai Odayaka":

                    Object.Instantiate(bancoDados.avatar, spamPosition[0].position, spamPosition[0].rotation);

                    break;
            }
    }

    public void MaterialAvatar(ScriptableBancoDeDados bancoDados)
    {
        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis).materials[0].shader =
            AvatarMaterialCorpo(modelosIndex).shader;
        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis).materials[1].shader =
            AvatarMaterialCorpo(modelosIndex).shader;
        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis).materials[2].shader =
            AvatarMaterialFace(0).shader;

        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis).materials[0].mainTexture =
            AvatarMaterialCorpo(modelosIndex).mainTexture;
        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis).materials[1].mainTexture =
            AvatarMaterialCorpo(modelosIndex).mainTexture;
        bancoDados.components.AvatarCuston(bancoDados.namesHierarchy.nameCorpoNemesis).materials[2].mainTexture =
            AvatarMaterialFace(0).mainTexture;
    }
}
