using UnityEngine;
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;

    private void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)     //　trigger dialogue when player enters a specific region
    {                                                       //  プレイヤーが特定の場所（ボス部屋など）に入ったら、会話を始めます。
        if(collision.tag == "Player") {
            TriggerDialogue();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
