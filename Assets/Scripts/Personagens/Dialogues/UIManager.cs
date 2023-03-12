using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject UIConteiner;
    public TMP_Text _name;
    public TMP_Text _dialogue;

    public Animator anim;

    void Awake()
    {
        DialogueManager.ShowName += ShowNameText;
        DialogueManager.ShowMessage += ShowText;
        DialogueManager.ResetText += ResetText;
        DialogueManager.ResetNameText += ResetNameText;
        DialogueManager.UIState += UIContainerState;
    }

    void OnDestroy()
    {
        DialogueManager.ShowName -= ShowNameText;
        DialogueManager.ShowMessage -= ShowText;
        DialogueManager.ResetText -= ResetText;
        DialogueManager.ResetNameText -= ResetNameText;
        DialogueManager.UIState -= UIContainerState;
    }

    void Start() =>
        anim.GetComponent<Animator>();

    //void NewTalker(DialogueContainer talkerInformations) => // Animação do Button
    //    anim.Play("animation");

    void ShowText(string message) =>
        _dialogue.text = message;

    void ShowNameText(string name) =>
        _name.text = name;

    void ResetText() =>
        _dialogue.text = string.Empty;

    void ResetNameText() =>
        _name.text = string.Empty;

    void UIContainerState(bool state) =>
        UIConteiner.SetActive(state);
}
