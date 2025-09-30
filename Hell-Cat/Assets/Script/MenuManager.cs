using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI nameText;
    [SerializeField] public TextMeshProUGUI inputText;

    public void Start()
    {
        if(inputText != null)
        {
            inputText.text = PlayerData.playername;
        }
        
    }
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
