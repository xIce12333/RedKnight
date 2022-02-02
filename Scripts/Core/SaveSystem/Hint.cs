using UnityEngine;

public class Hint : MonoBehaviour
{
    [Header("Text Content")]

    [TextArea(3, 10)]
    [SerializeField] private string popUpBoxText;



    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            FindObjectOfType<PopUpBox>().DisplayText(popUpBoxText);
        }
            
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            FindObjectOfType<PopUpBox>().EndText();
    }
}
