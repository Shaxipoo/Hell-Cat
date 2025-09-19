using System.Collections.Generic;
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


    public void Start()
    {
        currentProcess = 0;
        RunProcess();
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
                        break;
                    }
                }
                break;

            case SectionType.MainCatChat:
                break;

            case SectionType.Tea:
                break;

            case SectionType.HeavenChoice:
                break;
        }
    }

}
