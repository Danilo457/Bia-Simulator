using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject UIConteiner;
    public TMP_Text _dialogue;

    public Animator anim;

    void Awake()
    {
        DialogueManager.ShowMessage += ShowText;
        DialogueManager.ResetText += ResetText;
        DialogueManager.UIState += UIContainerState;
    }

    void OnDestroy()
    {
        DialogueManager.ShowMessage -= ShowText;
        DialogueManager.ResetText -= ResetText;
        DialogueManager.UIState -= UIContainerState;
    }

    void Start() =>
        anim.GetComponent<Animator>();

    //void NewTalker(DialogueContainer talkerInformations) => // Animação do Button
    //    anim.Play("animation");

    void ShowText(string message) =>
        _dialogue.text = message;

    void ResetText() =>
        _dialogue.text = string.Empty;

    void UIContainerState(bool state) =>
        UIConteiner.SetActive(state);
}
