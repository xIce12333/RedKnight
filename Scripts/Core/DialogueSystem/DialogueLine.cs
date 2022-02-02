using UnityEngine;

[System.Serializable]
public class DialogueLine : MonoBehaviour
{
    [Header("Character Image")]
    public Sprite characterSprite;

    [Header("Dialogue Text")]
    [TextArea(3, 10)] public string sentence;
}
