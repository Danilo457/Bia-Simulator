using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spam : MonoBehaviour
{
    GeneratePersons generatePersons;

    [HideInInspector] public bool spam;

    void Start()
    {
        generatePersons = FindObjectOfType<GeneratePersons>();

        generatePersons.espera = true;
        spam = true;
    }
}
