using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePersons : MonoBehaviour
{
    [SerializeField] ScriptableBancoDeDados bancoDados;

    void Start()
    {
        
    }

    void SpamSalaDosArmarios() {
        for (int i = 0; i < bancoDados.listNames.Count; i++)
            switch (bancoDados.listNames[i])
            {
                case "Amai Odayaka":
                    Debug.Log("Amai");

                    break;
                case "Luana":
                    Debug.Log("Luana");

                    break;
            }
    }
}
