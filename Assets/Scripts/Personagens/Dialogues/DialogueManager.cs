using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public static event Action resetText;

    DialogueContainer currentDialogue;

    bool endCurrentTalk;

    void Awake()
    {
        instance = this;

        endCurrentTalk = true;
    }

    void StartConversation(DialogueContainer container) {
        currentDialogue = container;
    }

    void StartDialogue() {
        //for (int i = 0; i < currentDialogue.dialogues.Length; i++) {
        //    resetText?.Invoke();
            //ShowDialogue(currentDialogue.dialogues[i].dialogos);
        //}
    }

    void ShowDialogue(List<string> messages) {
        endCurrentTalk = false;
    }
}
