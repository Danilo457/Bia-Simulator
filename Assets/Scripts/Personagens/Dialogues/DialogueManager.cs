using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public static event Action ResetText;
    public static event Action<string> ShowMessage;
    public static event Action<bool> UIState;

    DialogueContainer currentDialogue;
    MouseController mouse;
    SystemPersonagens systemPersonagens;

    bool endCurrentTalk = true;
    bool buttonClicked = false;

    void Awake()
    {
        instance = this;

        mouse = FindObjectOfType<MouseController>();
        systemPersonagens = FindObjectOfType<SystemPersonagens>();
    }

    public void StartConversation(DialogueContainer container) {
        currentDialogue = container;
        StartCoroutine(StartDialogue());
        UIState?.Invoke(true);
    }

    IEnumerator StartDialogue() {
        for (int i = 0; i < currentDialogue._talker[0].dialogos.Count; i++) {
            ResetText?.Invoke();
            StartCoroutine(ShowDialogue(currentDialogue._talker[0].dialogos[i].mensagens));

            yield return new WaitUntil(() => endCurrentTalk);
        }

        DialogueFim();

        UIState?.Invoke(false);
    }

    IEnumerator ShowDialogue(List<string> messages) {
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

    void DialogueFim() {
        mouse.MouseLockedFalse();

        systemPersonagens.trava = false;
        systemPersonagens.atvCaixaEscolhas = false;
    }

    public void ButtonWasClicked() =>
        buttonClicked = true;
}
