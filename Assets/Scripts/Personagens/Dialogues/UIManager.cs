using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject UIConteiner;
    public Image _image;
    public TMP_Text _talkerName;
    public TMP_Text _dialogue;

    public Animator anim;

    void Awake()
    {
        DialogueManager.NewTalker += NewTalker;
        DialogueManager.ShowMessage += ShowText;
        DialogueManager.ResetText += ResetText;
        DialogueManager.UIState += UIContainerState;
    }

    void OnDestroy()
    {
        DialogueManager.NewTalker -= NewTalker;
        DialogueManager.ShowMessage -= ShowText;
        DialogueManager.ResetText -= ResetText;
        DialogueManager.UIState -= UIContainerState;
    }

    void Start() =>
        anim.GetComponent<Animator>();

    void NewTalker(Dialogue talkerInformations) {
        _image.sprite = talkerInformations._talker._sprite;
        _talkerName.text = talkerInformations._talker.name;
        anim.Play("animation");
    }

    void ShowText(string message) =>
        _dialogue.text = message;

    void ResetText() =>
        _dialogue.text = string.Empty;

    void UIContainerState(bool state) =>
        UIConteiner.SetActive(state);
}
