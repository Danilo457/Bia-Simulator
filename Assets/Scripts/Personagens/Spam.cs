using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spam : MonoBehaviour
{
    [SerializeField] bool atvAvatar;

    Menu menu;

    [HideInInspector] public bool spam;

    void Start()
    {
        menu = FindObjectOfType<Menu>();

        if (atvAvatar)
            menu.espera = true;

        spam = true;
    }
}
