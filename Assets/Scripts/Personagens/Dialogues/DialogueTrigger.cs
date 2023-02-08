using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueContainer dialogueContainer;

    public void StartDialogue()
    {
        DialogueManager.instance.StartConversation(dialogueContainer);
    }
}
