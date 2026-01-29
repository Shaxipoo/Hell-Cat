using System.Collections.Generic;
using System.Collections;
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

    [SerializeField] private Button OpenSignButton;
    [SerializeField] public DialogueManager dialogueManager;

    [SerializeField] private GameObject EndScreen;

    [Header("UI: Story Ending")]
    [SerializeField] public GameObject storyTransitionScreen;

    [Header("UI: Document Check")]
    [SerializeField] private GameObject documentCheckScreen;

    [Header("UI: Dialogue")]
    [SerializeField] private GameObject dialogueScreen;


    [Header("Backgrounds")]
    [SerializeField] private GameObject characterProfile;

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
        EndScreen.SetActive(false);
        HideDialogueScreen();
        HideCharacterProfile();
        DocumentManager.instance.HideInterrogateButton();
    }

    public void OnClickStartTheDay()
    {
        ShowDialogueScreen();
        ShowCharacterProfile();
        RunProcess();
        OpenSignButton.interactable = false;
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
        OpenSignButton.interactable = true;    
    }
    public void RunProcess()
    {
        // If run out of story, DAY ENDS
        if (currentProcess >= dayList[currentDay].chapterList.Count)
        {
            EndDay();
            return;
        }
        // If the first process of the day, sent mail
        if(currentProcess == 0)
        {
            if(dayList[currentDay].mails != null && dayList[currentDay].mails.Count > 0)
            {
                MailController.SendMails(dayList[currentDay].mails);
            }      
        }
        var currentCat = GetCurrentCatChapter();
        int catId = currentCat.Id;

        if (!PlayerData.GetIsHeaven(catId))
        {
            // Load Story
            dialogueManager.dialogueLoader.LoadCSV(currentCat.chatText);

            int newChapter = PlayerData.GetCurrentChapter(catId) + 1;
            PlayerData.SetCurrentChapter(catId, newChapter);

            bool found = false;
            foreach (var kv in dialogueManager.dialogueLoader.dialogueDict)
            {
                if (kv.Value.chapterId == newChapter)
                {
                    // Start Chapter
                    dialogueManager.StartDialogue(newChapter);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                // No dialogue for this chapter in current cat, advance process
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
        DocumentManager.instance.HideInterrogateButton();
        ShowDialogueScreen();
        isFastSkipEnable = true;
        isInDoc = false;

        dialogueManager.SwitchToHeavenOrNot(isHeaven);

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

        DocumentManager.instance.AddDocumentSets(GetCurrentDocumentSet());
        DocumentManager.instance.ShowInterrogateButton();


        HideDialogueScreen();
        isFastSkipEnable = false;
        isInDoc = true;
    }
    public DocumentSet GetCurrentDocumentSet()
    {
        ShowDayProcess();
        var currentCat = GetCurrentCatChapter();
        int catId = currentCat.Id;
        int curChapter = PlayerData.GetCurrentChapter(catId);

        int idx = Mathf.Clamp(curChapter - 1, 0, currentCat.documentSetList.Count - 1);
        return currentCat.documentSetList[idx];
    }
    public CatChapter GetCurrentCatChapter()
    {
        return dayList[currentDay].chapterList[currentProcess];
    }
    public void ShowDialogueScreen()
    {
        dialogueScreen.SetActive(true);
    }
    public void HideDialogueScreen()
    {
        dialogueScreen.SetActive(false);
    }
    public void ShowCharacterProfile()
    {
        characterProfile.SetActive(true);
    }
    public void HideCharacterProfile()
    {
        characterProfile.SetActive(false);
    }
    public void EnableDialogueClick()
    {
        dialogueScreen.GetComponent<Button>().interactable = true;
    }
    public void DisableDialogueClick()
    {
        dialogueScreen.GetComponent<Button>().interactable = false;
    }
    
    private bool isSignOpen = false;
    public void FlipSign()
    {
        if(isSignOpen)
        {
            isSignOpen = false;
        }
        else
        {
            isSignOpen = true;
        }
    }
    //Debug
    private void ShowDayProcess()
    {
        Debug.Log("Day:" + currentDay + "   Process" + currentProcess);
        var currentCat = GetCurrentCatChapter();
        Debug.Log("Current Chapter:" + PlayerData.GetCurrentChapter(currentCat.Id));
    }

}
