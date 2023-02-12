using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePersons : MonoBehaviour
{
    [SerializeField] ScriptableBancoDeDados bancoDados;

    List<Transform> spamPosition = new List<Transform>();

    Spam spamSalaDosArmarios;

    [HideInInspector] public bool espera;

    [HideInInspector] public int modelosIndex;

    List<Sprite> saveModelosSprites = new List<Sprite>();
    List<Mesh> saveMeshAvatar = new List<Mesh>();
    List<Material> saveMaterialCorpo = new List<Material>();
    List<Material> saveMaterialFace = new List<Material>();

    public Sprite Modelos(int num) { return saveModelosSprites[num]; }
    Mesh AvatarMesh(int num) { return saveMeshAvatar[num]; }
    Material AvatarMaterialCorpo(int num) { return saveMaterialCorpo[num]; }
    Material AvatarMaterialFace(int num) { return saveMaterialFace[num]; }

    private void Awake()
    {
        saveModelosSprites.Add(bancoDados.sprites[0]);
        saveModelosSprites.Add(bancoDados.sprites[2]);

        saveMeshAvatar.Add(bancoDados.mesh[0]);
        saveMeshAvatar.Add(bancoDados.mesh[3]);

        saveMaterialCorpo.Add(bancoDados.material[3]);
        saveMaterialCorpo.Add(bancoDados.material[4]);

        saveMaterialFace.Add(bancoDados.material[6]);
    }

    void Start()
    {

    }

    void Update()
    {
        if (espera)
        {
            spamSalaDosArmarios = GameObject.Find("Spam ID 001").GetComponent<Spam>();

            spamPosition.Add(GameObject.Find("Avatar Feminino 001").transform);

            if (spamSalaDosArmarios.spam)
            {
                SpamSalaDosArmarios();
                MeshAvatar();
                MaterialAvatar();
                AddGameObject();

                spamSalaDosArmarios.spam = false;
            }

            espera = false;
        }
    }

    void SpamSalaDosArmarios() {
        for (int i = 0; i < bancoDados.listNames.Count; i++)
            switch (bancoDados.listNames[i])
            {
                case "Amai Odayaka":
                    
                    Instantiate(bancoDados.avatar, spamPosition[0].position, spamPosition[0].rotation);

                    break;
            }
    }

    void AddGameObject()
    {
        bancoDados.addGameObject.AddMesh("RightIris - Nemesis", "LeftIris - Nemesis");

        bancoDados.components.MeshIris("RightIris - Nemesis").mesh = bancoDados.mesh[4];
        bancoDados.components.MeshIris("LeftIris - Nemesis").mesh = bancoDados.mesh[4];

        bancoDados.components.MaterialIris("RightIris - Nemesis").material = bancoDados.material[6];
        bancoDados.components.MaterialIris("LeftIris - Nemesis").material = bancoDados.material[6];
    }

    void MeshAvatar()
    {
        bancoDados.components.AvatarCuston("CorpoNemesis - Nemesis").sharedMesh = AvatarMesh(modelosIndex);
    }

    void MaterialAvatar()
    {
        bancoDados.components.AvatarCuston("CorpoNemesis - Nemesis").materials[0].shader = AvatarMaterialCorpo(modelosIndex).shader;
        bancoDados.components.AvatarCuston("CorpoNemesis - Nemesis").materials[1].shader = AvatarMaterialCorpo(modelosIndex).shader;
        bancoDados.components.AvatarCuston("CorpoNemesis - Nemesis").materials[2].shader = AvatarMaterialFace(0).shader;

        bancoDados.components.AvatarCuston("CorpoNemesis - Nemesis").materials[0].mainTexture = AvatarMaterialCorpo(modelosIndex).mainTexture;
        bancoDados.components.AvatarCuston("CorpoNemesis - Nemesis").materials[1].mainTexture = AvatarMaterialCorpo(modelosIndex).mainTexture;
        bancoDados.components.AvatarCuston("CorpoNemesis - Nemesis").materials[2].mainTexture = AvatarMaterialFace(0).mainTexture;
    }
}
