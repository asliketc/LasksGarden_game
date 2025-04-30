using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public Button continueButton;

    //all dialog strings, loaded by "continue" button
    private string[] lines = {
        "Hi! I am Lask.",
        "I have to deal with the garden. But..",
        "..I have a composition assignment due tonight",
        "Can you do the gardening while I compose my music!"
    };

    private int index = 0;

    void Start()
    {
        continueButton.onClick.AddListener(NextLine);
        dialogText.text = lines[0];
    }

    void NextLine()
    {
        index++;
        if (index < lines.Length)
        {
            dialogText.text = lines[index];
        }
        else
        {
            SceneManager.LoadScene("GardenScene");
        }
    }
}
