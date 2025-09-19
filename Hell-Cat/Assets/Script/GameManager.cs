using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
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




    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] private GameObject TeaSelectionScreen;
    [SerializeField] private GameObject HeavenSelectionScreen;
    [SerializeField] private TextMeshProUGUI TMPTeaOption1;
    [SerializeField] private TextMeshProUGUI TMPTeaOption2;
    [SerializeField] private TextMeshProUGUI TMPHeavenOption1;
    [SerializeField] private TextMeshProUGUI TMPHeavenOption2;

    private List<CatChapter> storedCatChapter;
    public void Start()
    {
        storedCatChapter = new List<CatChapter>();

        currentProcess = 0;
        RunProcess();

        TeaSelectionScreen.SetActive(false);
        HeavenSelectionScreen.SetActive(false);
        
    }

    public void NextProcess()
    {
        currentProcess += 1;
        RunProcess();
    }

    public void RunProcess()
    {
        switch (process[currentProcess])
        {
            case SectionType.RandChat:
                while (true)
                {
                    int ran = UnityEngine.Random.Range(0, ranChatList.Count - 1);
                    if (ranChatList[ran].ifAppeared == false)
                    {
                        dialogueManager.dialogueLoader.LoadCSV(ranChatList[ran].chatText);
                        ranChatList[ran].ifAppeared = true;
                        storedCatChapter.Add(ranChatList[ran]);

                        dialogueManager.StartDialogue();
                        break;
                    }
                }
                break;

            case SectionType.MainCatChat:
                break;

            case SectionType.Tea:
                TeaSelectionScreen.SetActive(true);
                TMPTeaOption1.text = storedCatChapter[0].CatName;
                TMPTeaOption2.text = storedCatChapter[1].CatName;
                break;

            case SectionType.HeavenChoice:
                HeavenSelectionScreen.SetActive(true);
                TMPHeavenOption1.text = storedCatChapter[0].CatName;
                TMPHeavenOption2.text = storedCatChapter[1].CatName;
                break;
        }
    }


    public void ChooseTeaOption(int index)
    {
        TeaSelectionScreen.SetActive(false);
        dialogueManager.dialogueLoader.LoadCSV(storedCatChapter[index].teaText);
    }

        public void ChooseHeavenOption(int index)
    {
        HeavenSelectionScreen.SetActive(false);
        dialogueManager.dialogueLoader.LoadCSV(storedCatChapter[index].heavenText);
    }

}
