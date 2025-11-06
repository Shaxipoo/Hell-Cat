using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("Chat Phase")]

    [Header("Story Process")]
    public List<CatChapter> storyList;

    private int currentProcess;

    [SerializeField] public DialogueManager dialogueManager;

    [SerializeField] private GameObject TeaSelectionScreen;
    [SerializeField] private GameObject HeavenSelectionScreen;
    [SerializeField] private GameObject EndScreen;
    [SerializeField] private TextMeshProUGUI TMPTeaOption1;
    [SerializeField] private TextMeshProUGUI TMPTeaOption2;
    [SerializeField] private TextMeshProUGUI TMPHeavenOption1;
    [SerializeField] private TextMeshProUGUI TMPHeavenOption2;

    [Header("Backgrounds")]
    [SerializeField] private GameObject chatBackground;
    [SerializeField] private GameObject teaBackground;
    [SerializeField] private GameObject heavenBackground;

    [Header("audio")]
    [SerializeField] private MusicManager musicManager;

    private int currentainCatChat = 0;
    public void OnEnable()
    {
        currentProcess = 0;
        RunProcess();

        // Set Initial UI
        TeaSelectionScreen.SetActive(false);
        HeavenSelectionScreen.SetActive(false);
        EndScreen.SetActive(false);
    }

    public void NextProcess()
    {
        currentProcess += 1;
        RunProcess();
    }

    public void RunProcess()
    {
        // If run out of story, quit
        if (currentProcess >= storyList.Count)
        {
            EndScreen.SetActive(true);
            return;
        }

        ShowChatBackground();


        if (!storyList[currentProcess].isHeaven)
        {
            // Load Story
            dialogueManager.dialogueLoader.LoadCSV(storyList[currentProcess].chatText);

            foreach (var v in dialogueManager.dialogueLoader.dialogueDict)
            {
                if (v.Value.chapterId == storyList[currentProcess].currentChapter)
                {
                    // Start Chapter
                    dialogueManager.StartDialogue(storyList[currentProcess].currentChapter);

                    storyList[currentProcess].currentChapter += 1;
                    break;
                }
                NextProcess();
                return;
            }
      
        }
        else
        {
            Debug.Log("Cat already in heaven");
            NextProcess();
            return;
        }
        
    }


    public void ShowChatBackground()
    {
        chatBackground.SetActive(true);
        teaBackground.SetActive(false);
        heavenBackground.SetActive(false);
    }

    public void ShowTeaBackground()
    {
        chatBackground.SetActive(false);
        teaBackground.SetActive(true);
        heavenBackground.SetActive(false);
    }
    public void ShowHeavenBackground()
    {
        chatBackground.SetActive(false);
        teaBackground.SetActive(false);
        heavenBackground.SetActive(true);
    }

}
