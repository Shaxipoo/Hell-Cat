using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadLevel(string levelname)
    {
        SceneManager.LoadScene(levelname);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
