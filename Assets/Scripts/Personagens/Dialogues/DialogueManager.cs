using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public static event Action ResetText;
    public static event Action ResetNameText;
    public static event Action<string> ShowName;
    public static event Action<string> ShowMessage;
    public static event Action<bool> UIState;

    DialogueContainer currentDialogue;
    MouseController   mouse;
    SystemPersonagens systemPersonagens;
    CanvasManager canvasManager;

    string names;

    bool endCurrentTalk = true;
    bool buttonClicked = false;

    void Awake()
    {
        instance = this;

        mouse = FindObjectOfType<MouseController>();
        systemPersonagens = FindObjectOfType<SystemPersonagens>();
        canvasManager = FindObjectOfType<CanvasManager>();
    }

    public void StartConversation(DialogueContainer container, int indice) {
        currentDialogue = container;
        StartCoroutine(StartDialogue(indice));
        UIState?.Invoke(true);
    }

    IEnumerator StartDialogue(int indice) {
        for (int i = 0; i < currentDialogue._talker[indice].dialogos.Count; i++) {
            ResetText?.Invoke();
            ResetNameText?.Invoke();
            names = currentDialogue._talker[indice].dialogos[i].name;
            StartCoroutine(ShowDialogue(currentDialogue._talker[indice].dialogos[i].mensagens));

            yield return new WaitUntil(() => endCurrentTalk);
        }

        canvasManager.componentsInteracoes[1].SetActive(true); // Caixa de Escolhas Assunto
        mouse.MouseLockedFalse();

        UIState?.Invoke(false);
    }

    IEnumerator ShowDialogue(List<string> messages) {
        endCurrentTalk = false;

        foreach(var message in messages)
        {
            ShowAllMessage(message, names);
            yield return new WaitUntil(() => buttonClicked);
        }

        endCurrentTalk = true;
    }

    void ShowAllMessage(string message, string name) {
        ShowName?.Invoke(name);
        ShowMessage?.Invoke(message);
        buttonClicked = false;
    }

    public void DialogueFim() 
    { /* Encerrar Comversa */
        mouse.MouseLockedFalse();

        systemPersonagens.trava = false;
        systemPersonagens.atvCaixaEscolhas = false;
        canvasManager.componentsInteracoes[1].SetActive(false); // Caixa de Escolhas Assunto
    }

    public void ButtonWasClicked() => // Button Avansar na Comversa
        buttonClicked = true;
}
