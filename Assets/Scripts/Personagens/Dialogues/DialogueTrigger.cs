using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueContainer dialogueContainer;

    void Start()
    {
        DialogueManager.instance.StartConversation(dialogueContainer);
    }
}
