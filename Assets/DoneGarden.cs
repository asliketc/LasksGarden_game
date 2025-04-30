using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DoneGarden : MonoBehaviour
{
    public GameObject dialogUI;
    public TMPro.TextMeshProUGUI dialogText;
    public GameObject characterImage;
    public Button continueButton;

    private int dialogIndex = 0;
    private string[] lines = {
        "Wow, the garden looks amazing!",
        "You really helped me finish this in time for my assignment!"
    };

    public void ShowEnding()
    {
        dialogUI.SetActive(true);
        characterImage.SetActive(true);
        continueButton.gameObject.SetActive(true);
        dialogText.text = lines[dialogIndex];
    }

    public void Continue()
    {
        dialogIndex++;
        if (dialogIndex < lines.Length)
        {
            dialogText.text = lines[dialogIndex];
        }
        else
        {
            Application.Quit();
        }
    }
}
