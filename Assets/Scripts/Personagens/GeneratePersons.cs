using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePersons : MonoBehaviour
{
    [SerializeField] ScriptableBancoDeDados bancoDados;

    List<Transform> spamPosition = new List<Transform>();
    List<GameObject> listSpawn = new List<GameObject>();

    Spam spamSalaDosArmarios;

    [HideInInspector] public bool espera;

    void Awake()
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
                CreateNewListGameObject();
                AddComponents();

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
                    
                    Instantiate(bancoDados.avatarFeminino, spamPosition[0].position, spamPosition[0].rotation);

                    break;
            }
    }

    void CreateNewListGameObject()
    {
        listSpawn.Add(new GameObject(bancoDados.listNames[0]));
        listSpawn.Add(new GameObject("filho"));
    }

    void AddComponents() {

        listSpawn[0].transform.position = new Vector3(-12, 0, -5);
        listSpawn[0].transform.rotation = Quaternion.Euler(0, -90, 0);

        listSpawn[1].GetComponent<Transform>().SetParent(listSpawn[0].transform, true);

        listSpawn[1].transform.position = listSpawn[0].transform.position;
        listSpawn[1].transform.rotation = listSpawn[0].transform.rotation;
        listSpawn[1].transform.localScale = listSpawn[0].transform.localScale;

        listSpawn[1].AddComponent<SkinnedMeshRenderer>();

        listSpawn[1].GetComponent<SkinnedMeshRenderer>().sharedMesh = bancoDados.mesh[0];
        listSpawn[1].GetComponent<SkinnedMeshRenderer>().materials[0] = bancoDados.material[2];
        //listSpawn[1].GetComponent<SkinnedMeshRenderer>().materials[1] = bancoDados.material[2];
        //listSpawn[1].GetComponent<SkinnedMeshRenderer>().materials[2] = bancoDados.material[12];
    }
}
