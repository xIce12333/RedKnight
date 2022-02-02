using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    private DialogueManager dialogueManager;
    private float dialogueDelay = 2f;       

    private void Awake()
    {
        SoundManager.instance.PlaySound("BGM");
        StartCoroutine(DialogueDelay());
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.StopPlayerInDialogue();
    }

    private IEnumerator DialogueDelay()     // wait "dialougeDelay" of time before starting the dialogue    少し間を置いてから会話を始めます
    {
        yield return new WaitForSeconds(dialogueDelay);
        dialogueManager.StartDialogue(dialogue);
    }
}
