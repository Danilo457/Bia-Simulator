using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] List<DialogueContainer> dialogueContainer = new List<DialogueContainer>();

    public void StartDialogue(int indice, int num) =>
        DialogueManager.instance.StartConversation(dialogueContainer[num], indice);
}
