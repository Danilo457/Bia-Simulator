using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePersons : MonoBehaviour
{
    [SerializeField] ScriptableBancoDeDados bancoDados;

    List<Transform> spamPosition = new List<Transform>();

    Spam spamSalaDosArmarios;

    [HideInInspector] public bool espera;

    [HideInInspector] public int indexAvatar;

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
                SetMatirials();

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
                    
                    Instantiate(bancoDados.avatar[indexAvatar], spamPosition[0].position, spamPosition[0].rotation);

                    break;
            }
    }

    void MeshAvatar()
    {
        
    }

    void SetMatirials()
    {
        
    }
}
