using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePersons : MonoBehaviour
{
    [SerializeField] ScriptableBancoDeDados bancoDados;

    List<Transform> spamPosition = new List<Transform>();

    Spam spamSalaDosArmarios;
    Menu menu;
    MouseController mouseCursor;

    [HideInInspector] public bool espera;

    void Start()
    {
        menu = FindObjectOfType<Menu>();
        mouseCursor = FindObjectOfType<MouseController>();
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
        bancoDados.components.AvatarCuston().sharedMesh = bancoDados.mesh[1];
    }

    void MaterialAvatar()
    {
        bancoDados.components.AvatarCuston().materials[0].shader = bancoDados.material[15].shader;
        bancoDados.components.AvatarCuston().materials[1].shader = bancoDados.material[15].shader;
        bancoDados.components.AvatarCuston().materials[2].shader = bancoDados.material[1].shader;

        bancoDados.components.AvatarCuston().materials[0].mainTexture = bancoDados.material[15].mainTexture;
        bancoDados.components.AvatarCuston().materials[1].mainTexture = bancoDados.material[15].mainTexture;
        bancoDados.components.AvatarCuston().materials[2].mainTexture = bancoDados.material[1].mainTexture;
    }

    public void UniformeCustons()
    {
        menu.buttonsPersonPlayer.SetActive(false);

        mouseCursor.MouseNoneTrue();

        menu.returnPanelConfig.SetActive(true);
    }
}
