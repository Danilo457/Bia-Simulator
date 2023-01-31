using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject UIConteiner;
    public Image _image;
    public Text _talkerName;
    public TMP_Text _dialogue;

    void Awake()
    {
        
    }

    void NewTalker(Dialogue talkerInformations) {
        _image.sprite = talkerInformations._talker._sprite;
        _talkerName.text = talkerInformations._talker.name;
        _image.GetComponent<Animator>().SetTrigger("animation");
    }

    void ShowText(string message) =>
        _dialogue.text = message;

    void ResetText() =>
        _dialogue.text = string.Empty;

    void UIContainerState(bool state) =>
        UIConteiner.SetActive(state);
}
