using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptinBehaviourColor : MonoBehaviour, IOptionBehaviour
{
    [SerializeField] TMP_Text _text;

    [SerializeField] Color cor;

    public void OnStateChange(bool state)
    {
        _text.color = state ? cor : Color.white;
    }
}
