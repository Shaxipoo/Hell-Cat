using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Story Process")]
    public List<DaySchedule> dayList;

    private int currentDay;
    private int currentProcess;

    [Header("UI Gears")]
    [SerializeField] public DialogueManager dialogueManager;

    [SerializeField] private GameObject EndScreen;

    [Header("UI: Story Ending")]
    [SerializeField] public GameObject storyTransitionScreen;

    [Header("UI: Document Check")]
    [SerializeField] private GameObject documentCheckScreen;

    [Header("UI: Dialogue")]
    [SerializeField] private GameObject dialogueScreen;


    [Header("Backgrounds")]
    [SerializeField] private GameObject chatBackground;
    [SerializeField] private GameObject heavenBackground;

    [Header("Audio")]
    [SerializeField] private MusicManager musicManager;

    private int currentainCatChat = 0;

    [HideInInspector]
    public bool isFastSkipEnable = true;
    [HideInInspector]
    public bool isInDoc = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        currentDay = 0;
        currentProcess = 0;
        RunProcess();
        EndScreen.SetActive(false);
    }

    public void NextProcess()
    {
        currentProcess += 1;
        RunProcess();
    }
    public void EndDay()
    {
        // Reset Everything
        currentDay += 1;
        currentProcess = 0;

        EndScreen.SetActive(true);
    }
    public void RunProcess()
    {
        // If run out of story, DAY ENDS
        if (currentProcess >= dayList[currentDay].chapterList.Count)
        {
            EndDay();
            return;
        }

        if (!GetCurrentCatChapter().isHeaven)
        {
            // Load Story
            dialogueManager.dialogueLoader.LoadCSV(GetCurrentCatChapter().chatText);

            foreach (var v in dialogueManager.dialogueLoader.dialogueDict)
            {
                GetCurrentCatChapter().currentChapter += 1;
                if (v.Value.chapterId == GetCurrentCatChapter().currentChapter)
                {
                    // Start Chapter
                    dialogueManager.StartDialogue(GetCurrentCatChapter().currentChapter);                 
                    break;
                }
                NextProcess();
                return;
            }   
        }
        else
        {
            NextProcess();
            return;
        }   
    }
    public void SwitchBackToDialogueWithAnswer(bool isHeaven)
    {
        HideDocumentScreen();

        ShowDialogueScreen();
        isFastSkipEnable = true;
        isInDoc = false;

        dialogueManager.SwitchToHeavenOrNot(isHeaven);

        // Prevent the click that closed the document from being received
        // by the dialogue click handler in the same frame.
        StartCoroutine(EnableDialogueClickNextFrame());
    }

    private IEnumerator EnableDialogueClickNextFrame()
    {
        DisableDialogueClick();
        yield return new WaitForEndOfFrame();
        EnableDialogueClick();
    }
    public void SwitchToDocumentCheck()
    {
        ShowDocumentScreen();
        DocumentManager.instance.AddDocumentSets(GetCurrentDocumentSet());

        HideDialogueScreen();
        isFastSkipEnable = false;
        isInDoc = true;
    }

    public DocumentSet GetCurrentDocumentSet()
    {
        ShowDayProcess();
        
        
        return GetCurrentCatChapter().documentSetList[dayList[currentDay].chapterList[currentProcess].currentChapter-1];
    }
    public CatChapter GetCurrentCatChapter()
    {
        return dayList[currentDay].chapterList[currentProcess];
    }
    public void ShowDocumentScreen()
    {
        documentCheckScreen.SetActive(true);
    }
    public void HideDocumentScreen()
    {
        documentCheckScreen.SetActive(false);
    }
    public void ShowDialogueScreen()
    {
        dialogueScreen.SetActive(true);
    }
    public void HideDialogueScreen()
    {
        dialogueScreen.SetActive(false);
    }
    public void EnableDialogueClick()
    {
        dialogueScreen.GetComponent<Button>().interactable = true;
    }
    public void DisableDialogueClick()
    {
        dialogueScreen.GetComponent<Button>().interactable = false;
    }
    //Debug
    private void ShowDayProcess()
    {
        Debug.Log("Day:" + currentDay + "   Process" + currentProcess);
        Debug.Log("Current Chapter:" + dayList[currentDay].chapterList[currentProcess].currentChapter);
    }

}
