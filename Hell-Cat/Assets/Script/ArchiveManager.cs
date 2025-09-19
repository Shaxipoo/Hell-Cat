using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ArchiveManager : MonoBehaviour
{
    private int currentFileId;

    [SerializeField] private DialogueManager dialogueManager;

    [Header("TextMeshProUGUI")]
    [SerializeField] private TextMeshProUGUI TMPName;
    [SerializeField] private TextMeshProUGUI TMPAge;
    [SerializeField] private TextMeshProUGUI TMPBreed;
    [SerializeField] private TextMeshProUGUI TMPCauseOfDeath;
    [SerializeField] private TextMeshProUGUI TMPCriminalRecord;
    [SerializeField] private TextMeshProUGUI TMPCriminalDegree;
    [SerializeField] private TextMeshProUGUI TMPContraband;


    void OnEnable()
    {
        currentFileId = 1;

        UpdateFile();
    }


    public void ShowNextFile()
    {
        while(true)
        {
            if (currentFileId > dialogueManager.fileList.Count)
            {
                currentFileId = 1;
            }
            if (dialogueManager.fileList[currentFileId].FileId == currentFileId)
                {
                    UpdateFile();
                    return;
                }
                else
                {
                    currentFileId += 1;
                }
        }

    }

    public void ShowPreviousFile()
    {
        while(true)
        {
            if (currentFileId < 1)
            {
                currentFileId = dialogueManager.fileList.Count - 1;
            }
            if (dialogueManager.fileList[currentFileId].FileId == currentFileId)
                {
                    UpdateFile();
                    return;
                }
                else
                {
                    currentFileId -= 1;
                }
        }

    }

    private void UpdateFile()
    {
        if (dialogueManager.fileList.ContainsKey(currentFileId))
        {
        TMPName.text = dialogueManager.fileList[currentFileId].Name;
        TMPAge.text = dialogueManager.fileList[currentFileId].Age;
        TMPBreed.text = dialogueManager.fileList[currentFileId].Breed;
        TMPCauseOfDeath.text = dialogueManager.fileList[currentFileId].CauseOfDeath;
        TMPCriminalRecord.text = dialogueManager.fileList[currentFileId].CriminalRecord;
        TMPCriminalDegree.text = dialogueManager.fileList[currentFileId].CriminalDegree;
        TMPContraband.text = dialogueManager.fileList[currentFileId].Contraband;
        }
        
    }
}
