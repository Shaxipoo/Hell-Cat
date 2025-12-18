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
    public List<DaySchedule> dayList;

    private int currentDay;
    private int currentProcess;


    [Header("UI Gears")]
    [SerializeField, HideInInspector] public DialogueManager dialogueManager;

    [SerializeField, HideInInspector] private GameObject EndScreen;

    [Header("Backgrounds")]
    [SerializeField, HideInInspector] private GameObject chatBackground;
    [SerializeField, HideInInspector] private GameObject heavenBackground;

    [Header("Audio")]
    [SerializeField, HideInInspector] private MusicManager musicManager;

    private int currentainCatChat = 0;
    public void OnEnable()
    {
        currentDay = 0;
        currentProcess = 0;

        RunProcess();

        // Set Initial UI

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

        ShowChatBackground();

        if (!dayList[currentDay].chapterList[currentProcess].isHeaven)
        {
            // Load Story
            dialogueManager.dialogueLoader.LoadCSV(dayList[currentDay].chapterList[currentProcess].chatText);

            foreach (var v in dialogueManager.dialogueLoader.dialogueDict)
            {
                dayList[currentDay].chapterList[currentProcess].currentChapter += 1;
                if (v.Value.chapterId == dayList[currentDay].chapterList[currentProcess].currentChapter)
                {
                    // Start Chapter
                    dialogueManager.StartDialogue(dayList[currentDay].chapterList[currentProcess].currentChapter);                 
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

    public DocumentSet GetCurrentDocumentSet()
    {
        ShowDayProcess();
        
        
        return dayList[currentDay].chapterList[currentProcess].documentSetList[dayList[currentDay].chapterList[currentProcess].currentChapter-1];
    }

    public void ShowChatBackground()
    {
        chatBackground.SetActive(true);
        heavenBackground.SetActive(false);
    }

    public void ShowHeavenBackground()
    {
        chatBackground.SetActive(false);
        heavenBackground.SetActive(true);
    }


    //Debug

    private void ShowDayProcess()
    {
        Debug.Log("Day:" + currentDay + "   Process" + currentProcess);
        Debug.Log("Current Chapter:" + dayList[currentDay].chapterList[currentProcess].currentChapter);
    }

}
