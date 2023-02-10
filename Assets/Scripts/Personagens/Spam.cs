using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spam : MonoBehaviour
{
    [SerializeField] bool atvAvatar;
    [SerializeField, Range(0, 2)] int modeloAvatar;

    GeneratePersons generatePersons;

    [HideInInspector] public bool spam;

    void Start()
    {
        generatePersons = FindObjectOfType<GeneratePersons>();

        generatePersons.indexAvatar = modeloAvatar;

        if (atvAvatar)
            generatePersons.espera = true;

        spam = true;
    }
}
