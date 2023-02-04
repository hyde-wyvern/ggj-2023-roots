using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public void ActiveDialogue()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }
}
