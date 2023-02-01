using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public static event Action<Dialogue> NewTalker;
    public static event Action ResetText;
    public static event Action<string> ShowMessage;
    public static event Action<bool> UIState;

    DialogueContainer currentDialogue;

    bool endCurrentTalk = true;
    bool buttonClicked = false;

    void Awake() =>
        instance = this;

    public void StartConversation(DialogueContainer container) {
        currentDialogue = container;
        StartCoroutine(StartDialogue());
        UIState?.Invoke(true);
    }

    IEnumerator StartDialogue() {
        for (int i = 0; i < currentDialogue._dialogues.Length; i++) {
            ResetText?.Invoke();
            NewTalker?.Invoke(currentDialogue._dialogues[i]);
            StartCoroutine(ShowDialogue(currentDialogue._dialogues[i].messages));

            yield return new WaitUntil(() => endCurrentTalk);
        }

        UIState?.Invoke(false);
    }

    IEnumerator ShowDialogue(string[] messages) {
        endCurrentTalk = false;

        foreach(var message in messages)
        {
            ShowAllMessage(message);
            yield return new WaitUntil(() => buttonClicked);
        }

        endCurrentTalk = true;
    }

    void ShowAllMessage(string message) {
        ShowMessage?.Invoke(message);
        buttonClicked = false;
    }

    public void ButtonWasClicked() =>
        buttonClicked = true;
}
