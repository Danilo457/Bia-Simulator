using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CircleEscolhas : MonoBehaviour
{
    public int id;

    Animator anim;

    public string itemName;

    public TextMeshProUGUI itemText;

    //public Image selectedItem;

    //public Sprite icon;

    bool selected;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (selected) {
            //selectedItem.sprite = icon;
            itemText.text = itemName;
        }   
    }

    public void Selected()
    {
        selected = true;
    }

    public void Deselected()
    {
        selected = false;
    }

    public void HoverEnter()
    {
        anim.SetBool("Hover", true);
        itemText.text = itemName;
    }

    public void HoverExit()
    {
        anim.SetBool("Hover", false);
        itemText.text = string.Empty;
    }
}
