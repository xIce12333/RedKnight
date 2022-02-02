using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region Variables   変数

    private Queue<DialogueLine> sentences;
    [Header("Delay between each letter")]
    private float textDelay = 0.02f;
    [System.NonSerialized] public bool inDialogue = false;
    private float dialogueDelay = 1f;

    [Header("References")]
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private GameObject boss;
    [SerializeField] private Image characterImage;
    [SerializeField] private Text dialogueText;
    [SerializeField] private GameObject player;
    private Dialogue dialogue;
    
    #endregion

    #region  Functions  関数

    private void Awake()
    {
        sentences = new Queue<DialogueLine>();
        dialogueUI.SetActive(false);
    }

    void Update()
    {
        if (inDialogue) {
            if (Input.GetKeyDown(KeyCode.L))    // skip to next line    次の会話に進めます
                DisplaySentence();
            
        }
    }    

    public void StartDialogue(Dialogue d)
    {
        dialogue = d;
        dialogueText.text = "";
        dialogueUI.SetActive(true);
        StopPlayerInDialogue();
        EnqueueDialogue();
        StartCoroutine(DialogueCoroutine());
    }

    private void EnqueueDialogue()      // put all sentences into the queue     
    {
        sentences.Clear();
        foreach(DialogueLine sentence in dialogue.sentences)        
            sentences.Enqueue(sentence);
    }
    private IEnumerator DialogueCoroutine()         // Show Image of the first character of the dialogue, wait a certain period of time before starting dialogue
    {                                               // 会話が始まったら、少し間を置いてから一行目の会話を表示します。（このほうがプレイヤーが会話に集中できるため）
        DisplayCharacterImage();
        yield return new WaitForSeconds(dialogueDelay);
        inDialogue = true;
        DisplaySentence();
    }
    public void DisplayCharacterImage()
    {
        characterImage.sprite = dialogue.sentences[0].characterSprite;
        characterImage.preserveAspect = true;
    }

    public void DisplaySentence()
    {
        dialogueText.text = "";
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }
        DialogueLine line = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(WriteText(line));
    }

    private IEnumerator WriteText(DialogueLine line)
    {
        characterImage.sprite = line.characterSprite;
        foreach (char letter in line.sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textDelay);
        }
    }

    private void EndDialogue()
    {
        dialogueUI.SetActive(false);
        inDialogue = false;
        if (boss == null)           // for prologue     プロローグ用
            StartEvent();
        else if(!boss.GetComponent<EnemyHealth>().isDead)       // start battle     ボスの会話が終わった時用
            StartBattle();   
        else               // boss is dead, transit to next scene       ボスが　ﾁ───(´-ω-｀)───ﾝ　した時用
            StartEndBattleEvent();
        
        
    }

    private void StartEvent()       // for prologue プロローグが終わったら、次のシーンに移ります
    {
        Invoke("EndOfScene", 1f);
    }

    private void StartBattle()      //　ボスの会話が終わったら、プレイヤーとボスを動けるようにします
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerAnimation>().enabled = true;
        player.GetComponent<PlayerAttack>().enabled = true;
        dialogueUI.SetActive(false);
        dialogue.animator.SetTrigger("startBattle");
        SoundManager.instance.StopSound("BGM");
        SoundManager.instance.PlaySoundIfNotPlaying("Boss");
        FindObjectOfType<HealthSpawner>().StartSpawn();
    }

    private void StartEndBattleEvent()      //　ボスが死んで、次のシーンに移ります
    {
        player.GetComponent<PlayerAnimation>().ChangeAnimationState(AnimationScript.State.Win);
        SoundManager.instance.PlaySound("Win");
        SoundManager.instance.StopSound("Boss");
        Invoke("EndOfScene", 2f);
    }
    private void EndOfScene()
    {
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }


    public void StopPlayerInDialogue()          // Don't let player move during dialogue    会話中、プレイヤーキャラクターの動きを止めます
    {
        player.GetComponent<PlayerMovement>().StopPlayerMovement();
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerAttack>().enabled = false;
        player.GetComponent<PlayerAnimation>().ChangeAnimationState(AnimationScript.State.Idle);
        player.GetComponent<PlayerAnimation>().enabled = false;
    }
    
    #endregion
}

