using UnityEngine;
using TMPro;

public class CircleEscolhas : MonoBehaviour
{
    Animator anim;

    [SerializeField] string itemName;

    [SerializeField] TextMeshProUGUI itemText;

    bool selected;

    void Start() =>
        anim = GetComponent<Animator>();

    void Update()
    {
        if (selected) 
            itemText.text = itemName;
    }

    public void Selected() =>
        selected = true;

    public void Deselected() =>
        selected = false;

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
