using System;
using System.Collections.Generic;
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
        if (dialogueManager != null && PlayerData.fileList.Count > 0)
        {
            var keys = new List<int>(PlayerData.fileList.Keys);
            keys.Sort();
            currentFileId = keys[0];
            UpdateFile();
        }
        else
        {
            Debug.LogWarning("fileList is empty, cannot initialize currentFileId!");
        }
    }


    public void ShowNextFile()
    {
        var keys = new List<int>(PlayerData.fileList.Keys);
        keys.Sort();

        int index = keys.IndexOf(currentFileId);
        if (index == -1) index = 0;

        index = (index + 1) % keys.Count;
        currentFileId = keys[index];

        UpdateFile();
        Debug.Log("show next");
    }

    public void ShowPreviousFile()
    {
        var keys = new List<int>(PlayerData.fileList.Keys);
        keys.Sort();

        int index = keys.IndexOf(currentFileId);
        if (index == -1) index = 0;

        index = (index - 1 + keys.Count) % keys.Count;
        currentFileId = keys[index];

        UpdateFile();
        Debug.Log("show previous");
    }

    private void UpdateFile()
    {
        if (PlayerData.fileList.ContainsKey(currentFileId))
        {
        TMPName.text = PlayerData.fileList[currentFileId].Name;
        TMPAge.text = PlayerData.fileList[currentFileId].Age;
        TMPBreed.text = PlayerData.fileList[currentFileId].Breed;
        TMPCauseOfDeath.text = PlayerData.fileList[currentFileId].CauseOfDeath;
        TMPCriminalRecord.text = PlayerData.fileList[currentFileId].CriminalRecord;
        TMPCriminalDegree.text = PlayerData.fileList[currentFileId].CriminalDegree;
        TMPContraband.text = PlayerData.fileList[currentFileId].Contraband;
        }
        
    }
}
