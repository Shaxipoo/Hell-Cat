using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public enum SectionType
{
    RandChat,
    MainCatChat,
    Tea,
    HeavenChoice
}

public class GameManager : MonoBehaviour
{
    [Header("Chat Phase")]
    [Tooltip("其他猫chapter")]
    public List<CatChapter> ranChatList;

    [Tooltip("主线猫")]
    public List<TextAsset> mainCatChatList;

    [Header("Process")]
    public List<SectionType> process;

    private int currentProcess;

    public SectionType currentSectionType;

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

    private List<CatChapter> storedCatChapter;
    private int currentainCatChat = 0;
    public void OnEnable()
    {
        Debug.Log("start game");
        storedCatChapter = new List<CatChapter>();

        currentProcess = 0;
        RunProcess();

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
        if (currentProcess >= process.Count)
        {
            EndScreen.SetActive(true);
            return;
        }
        switch (process[currentProcess])
        {
            case SectionType.RandChat:
                currentSectionType = SectionType.RandChat;
                ShowChatBackground();
                //musicManager.PlayNormalMusic();
                var availableCats = ranChatList.FindAll(cat => !cat.ifAppeared);

                if (availableCats.Count == 0)
                {
                    Debug.LogWarning("No unappeared cats available in ranChatList!");
                    return;
                }

                int ranIndex = UnityEngine.Random.Range(0, availableCats.Count);
                var selectedCat = availableCats[ranIndex];

                dialogueManager.dialogueLoader.LoadCSV(selectedCat.chatText);

                selectedCat.ifAppeared = true;
                storedCatChapter.Add(selectedCat);

                dialogueManager.StartDialogue();
                break;

            case SectionType.MainCatChat:
                currentSectionType = SectionType.MainCatChat;

                musicManager.PlayNormalMusic();

                dialogueManager.dialogueLoader.LoadCSV(mainCatChatList[currentainCatChat]);
                dialogueManager.StartDialogue();

                currentainCatChat += 1;

                ShowChatBackground();
                break;

            case SectionType.Tea:
                currentSectionType = SectionType.Tea;
                ShowTeaBackground();    
                TeaSelectionScreen.SetActive(true);
                TMPTeaOption1.text = storedCatChapter[0].CatName;
                if (storedCatChapter[1])
                {
                    TMPTeaOption2.text = storedCatChapter[1].CatName;
                }

                break;

            case SectionType.HeavenChoice:
                currentSectionType = SectionType.HeavenChoice;
                ShowHeavenBackground();
                HeavenSelectionScreen.SetActive(true);
                TMPHeavenOption1.text = storedCatChapter[0].CatName;
                if (storedCatChapter[1])
                {
                    TMPHeavenOption2.text = storedCatChapter[1].CatName;
                }

                break;
        }
    }


    public void ChooseTeaOption(int index)
    {
        TeaSelectionScreen.SetActive(false);
        dialogueManager.dialogueLoader.LoadCSV(storedCatChapter[index].teaText);
        dialogueManager.StartDialogue();
    }

    public void ChooseHeavenOption(int index)
    {
        HeavenSelectionScreen.SetActive(false);
        musicManager.PlayMusic(storedCatChapter[index].heavenMusic);
        dialogueManager.dialogueLoader.LoadCSV(storedCatChapter[index].heavenText);
        dialogueManager.StartDialogue();
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
