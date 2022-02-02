using UnityEngine;
using UnityEngine.UI;
public class PopUpBox : MonoBehaviour
{
    [SerializeField] private GameObject textUI;




    public void DisplayText(string text)
    {
        textUI.SetActive(true);
        textUI.GetComponentInChildren<Text>().text = text;
    }

    public void EndText()
    {
        textUI.GetComponentInChildren<Text>().text = "";
        textUI.SetActive(false);
    }
}   
