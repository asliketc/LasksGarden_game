using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoader : MonoBehaviour
{
    public void LoadDialogScene()
    {
        SceneManager.LoadScene("DialogScene");
    }
}
