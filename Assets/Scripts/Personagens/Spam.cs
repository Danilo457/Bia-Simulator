using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spam : MonoBehaviour
{
    [SerializeField] bool atvAvatar;

    GeneratePersons generatePersons;

    [HideInInspector] public bool spam;

    void Start()
    {
        generatePersons = FindObjectOfType<GeneratePersons>();

        if (atvAvatar)
            generatePersons.espera = true;

        spam = true;
    }
}
