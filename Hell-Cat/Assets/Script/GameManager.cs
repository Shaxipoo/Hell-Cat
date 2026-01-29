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

    [Header("Scroller")]
    [SerializeField]
    private ScrollController scrollController;
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
        DisableScroller();
    }

    public void NextProcess()
    {
        // Debug info
        if (dayList == null)
        {
            Debug.Log("NextProcess: dayList is null.");
            return;
        }
        Debug.Log("NextProcess called: currentDay=" + currentDay + " currentProcess=" + currentProcess + " dayChapterCount=" + (dayList.Count > currentDay && currentDay >= 0 ? dayList[currentDay].chapterList.Count : -1));

        // Guard advancing process so we don't run past today's chapters
        if (currentDay < 0 || currentDay >= dayList.Count)
        {
            Debug.Log("NextProcess: currentDay out of range: " + currentDay);
            return;
        }

        int max = dayList[currentDay].chapterList.Count;
        if (currentProcess + 1 >= max)
        {
            Debug.Log("NextProcess: reached end of day's chapters — calling EndDay().");
            EndDay();
            return;
        }

        currentProcess += 1;
        RunProcess();
    }
    public void EndDay()
    {
        EndScreen.SetActive(true);      
        DisableScroller();
    }

    public void OnClickStartNextDay()
    {
         currentDay += 1;
         ResetForNewDay();
    }

    private void ResetForNewDay()
    {
        currentProcess = 0;
        EndScreen.SetActive(false);
        HideDialogueScreen();
        HideCharacterProfile();
        DocumentManager.instance.HideInterrogateButton();
        storyTransitionScreen.SetActive(false);
        OpenSignButton.interactable = true;   
    }

    public void RunProcess()
    {
        // If run out of story, DAY ENDS
        if (currentProcess >= dayList[currentDay].chapterList.Count)
        {
            Debug.Log("Day " + currentDay + " ends.");
            EndDay();
            return;
        }

        ShowDayProcess();
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

            bool found = false;
            foreach (var kv in dialogueManager.dialogueLoader.dialogueDict)
            {
                if (kv.Value.chapterId == newChapter)
                {
                    // Start Chapter and only then update saved player chapter
                    dialogueManager.StartDialogue(newChapter);
                    PlayerData.SetCurrentChapter(catId, newChapter);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Debug.Log("RunProcess: No dialogue for cat " + catId + " chapter " + newChapter + ". dayListCount=" + dayList.Count + " currentDay=" + currentDay + " currentProcess=" + currentProcess + ". Not auto-advancing; please press Next to continue.");
                // Do not auto-advance here — let the player press NextProcess() to go to the next chapter/process.
                return;
            }
        }
        else
        {
            Debug.Log("RunProcess: cat " + catId + " is heaven; not auto-advancing. Press Next to continue.");
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

    public void EnableScroller()
    {
        scrollController.enabled = true;
    }
    public void DisableScroller()
    {
        scrollController.enabled = false;
    }
    public void ShowDialogueScreen()
    {
        dialogueScreen.SetActive(true);
        DisableScroller();
    }
    public void HideDialogueScreen()
    {
        dialogueScreen.SetActive(false);
        EnableScroller();
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
