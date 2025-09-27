using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI nameText;
    public void LoadLevel(string levelname)
    {
        SceneManager.LoadScene(levelname);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveName()
    {
        PlayerData.playername = nameText.text;
        
    }

}
