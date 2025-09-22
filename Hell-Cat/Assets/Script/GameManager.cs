using System.Collections.Generic;
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

    [SerializeField] public DialogueManager dialogueManager;

    [SerializeField] private GameObject TeaSelectionScreen;
    [SerializeField] private GameObject HeavenSelectionScreen;
    [SerializeField] private GameObject EndScreen;
    [SerializeField] private TextMeshProUGUI TMPTeaOption1;
    [SerializeField] private TextMeshProUGUI TMPTeaOption2;
    [SerializeField] private TextMeshProUGUI TMPHeavenOption1;
    [SerializeField] private TextMeshProUGUI TMPHeavenOption2;

    [Header("audio")]
    [SerializeField] private MusicManager musicManager;

    private List<CatChapter> storedCatChapter;
    public void Start()
    {
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
        if(currentProcess >= process.Count)
        {
            EndScreen.SetActive(true);
            return;
        }
        switch (process[currentProcess])
        {
            case SectionType.RandChat:
                musicManager.PlayNormalMusic();
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
                break;

            case SectionType.Tea:
                TeaSelectionScreen.SetActive(true);
                TMPTeaOption1.text = storedCatChapter[0].CatName;
                if (storedCatChapter[1])
                {
                    TMPTeaOption2.text = storedCatChapter[1].CatName;
                }

                break;

            case SectionType.HeavenChoice:
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

}
