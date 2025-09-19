using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Set Up")]
    [SerializeField] private int startTextId = 1;

    private int currentTextId;

    [SerializeField] private InputSystem_Actions inputSystem_Actions;
    [SerializeField] private DialogueLoader dialogueLoader;

    [Header("UI: Dialogue")]
    [SerializeField] private TextMeshProUGUI speakerNameTMPro;
    [SerializeField] private TextMeshProUGUI speakerTextTMPro;
    [SerializeField] private Image speakerImage;
    [SerializeField] private TypewriterEffect typewriterEffect;

    [SerializeField] private Button textClickPad;

    [Header("UI: Options")]
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private List<GameObject> optionButtonList;

    [Header("UI: Story Ending")]
    [SerializeField] private GameObject endStoryScreen;

    [Header("UI: Archive")]
    [SerializeField] private GameObject archiveScreen;

    [Header("UI: Document Check")]
    [SerializeField] private GameObject documentCheckScreen;
    [SerializeField] private DocumentManager documentManager;


    public Dictionary<int, FileInfo> fileList = new Dictionary<int, FileInfo>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        optionPanel.SetActive(false);
        endStoryScreen.SetActive(false);
        documentCheckScreen.SetActive(false);
        archiveScreen.SetActive(false);

        currentTextId = startTextId;
        UpdateDialogue();
    }

    public void OnClick()
    {
        // is all text
        if(typewriterEffect.IsAllText())
        {

            switch(dialogueLoader.dialogueDict[currentTextId].specialActionId)
            {
                // Options
                case 1:
                    ShowOption();
                    break;
                // End Story
                case 2:
                    EndStory();
                    Debug.Log("End sTORY");
                    break;
                // Show Document
                case 3:
                    documentCheckScreen.SetActive(true);
                    documentManager.SetDocuments(dialogueLoader.dialogueDict[currentTextId].documentSetId);
                    textClickPad.gameObject.SetActive(false);
                    break;
                // No Events
                default:
                    currentTextId = dialogueLoader.dialogueDict[currentTextId].nextId;
                    UpdateDialogue();
                    break;
            }
        }
        else
        {
            typewriterEffect.ShowAllText();

        }
        
    }

    public void UpdateDialogue()
    {
        speakerNameTMPro.text = dialogueLoader.dialogueDict[currentTextId].characterName;
        UnDarken(speakerImage);

        // if no image, no speaker, hide image
        // if have image, no speaker, darkern
        // if have image, have speaker, 

        if (dialogueLoader.dialogueDict[currentTextId].characterImage == "-1")
        {
            if (dialogueLoader.dialogueDict[currentTextId].characterName == "-1")
            {
                speakerNameTMPro.text = "";
                speakerImage.gameObject.SetActive(false);
            }
        }
        else
        {
            if (dialogueLoader.dialogueDict[currentTextId].characterName == "-1")
            {
                speakerNameTMPro.text = "";
                speakerImage.gameObject.SetActive(true);
                ChangeSpeakerImage(dialogueLoader.dialogueDict[currentTextId].characterImage);
                Darken(speakerImage);
                Debug.Log("dark");
            }
            else
            {
                speakerImage.gameObject.SetActive(true);
                ChangeSpeakerImage(dialogueLoader.dialogueDict[currentTextId].characterImage);
            }
        }

        typewriterEffect.StartTypeWriter(dialogueLoader.dialogueDict[currentTextId].text);
    }

    public void ChangeSpeakerImage(string imageAddress)
    {
        Sprite newSprite = Resources.Load<Sprite>("Characters/" + imageAddress);
        if (newSprite != null)
        {
            speakerImage.sprite = newSprite;
        }
        else
        {
            Debug.LogError("Rin: cannot find image");
        }
    }
    
    public void Darken(Image im)
    {
        Color c = im.color;

        c.r = 0.5f;
        c.g = 0.5f;
        c.b = 0.5f;
        im.color = c;
    }

    public void UnDarken(Image im)
    {
        Color c = im.color;

        c.r = 1f;
        c.g = 1f;
        c.b = 1f;
        im.color = c;
    }


    public void ShowOption()
    {
        textClickPad.interactable = false;

        foreach (GameObject button in optionButtonList)
        {
            button.SetActive(false);
        }

        List<string> optionStringList = new List<string>();
        optionStringList.Add(dialogueLoader.dialogueDict[currentTextId].option1);
        optionStringList.Add(dialogueLoader.dialogueDict[currentTextId].option2);
        optionStringList.Add(dialogueLoader.dialogueDict[currentTextId].option3);

        optionPanel.SetActive(true);
        if (dialogueLoader.dialogueDict[currentTextId].option5 != "-1")
        {
            for (int i = 0; i < 3; i++)
            {
                optionButtonList[i].SetActive(true);
                optionButtonList[i].GetComponentInChildren<TextMeshProUGUI>().text = optionStringList[i];
            }
        }
        else if (dialogueLoader.dialogueDict[currentTextId].option4 != "-1")
        {
            for (int i = 0; i < 3; i++)
            {
                optionButtonList[i].SetActive(true);
                optionButtonList[i].GetComponentInChildren<TextMeshProUGUI>().text = optionStringList[i];
            }
        }
        else if (dialogueLoader.dialogueDict[currentTextId].option3 != "-1")
        {
            for (int i = 0; i < 3; i++)
            {
                optionButtonList[i].SetActive(true);
                optionButtonList[i].GetComponentInChildren<TextMeshProUGUI>().text = optionStringList[i];
            }
        }
        else if (dialogueLoader.dialogueDict[currentTextId].option2 != "-1")
        {
            for (int i = 0; i < 2; i++)
            {
                optionButtonList[i].SetActive(true);
                optionButtonList[i].GetComponentInChildren<TextMeshProUGUI>().text = optionStringList[i];
            }
        }
        else
        {
            optionButtonList[0].SetActive(true);
            optionButtonList[0].GetComponentInChildren<TextMeshProUGUI>().text = optionStringList[0];
        }
    }

    public void ClickOption(int no)
    {
        textClickPad.interactable = true;
        optionPanel.SetActive(false);

        switch (no)
        {
            case 1:
                currentTextId = dialogueLoader.dialogueDict[currentTextId].option1NextId;
                UpdateDialogue();
                break;
            case 2:
                currentTextId = dialogueLoader.dialogueDict[currentTextId].option2NextId;
                UpdateDialogue();
                break;
            case 3:
                currentTextId = dialogueLoader.dialogueDict[currentTextId].option3NextId;
                UpdateDialogue();
                break;
        }
    }

    public void EndStory()
    {
        endStoryScreen.SetActive(true);
    }


    public void OnClickArhive()
    {
        archiveScreen.SetActive(true);

        PrintFileList();
    }

    public void OnQuitArchive()
    {
        archiveScreen.SetActive(false);
    }

    // Test
    private void PrintFileList()
    {
        if (fileList == null || fileList.Count == 0)
        {
            Debug.Log("fileList is null");
            return;
        }

        foreach (var kvp in fileList)
        {
            int id = kvp.Key;
            FileInfo file = kvp.Value;

            Debug.Log($"FileId: {id}, Name: {file.Name}, Age: {file.Age}, Breed: {file.Breed}, CauseOfDeath: {file.CauseOfDeath}, CriminalRecord: {file.CriminalRecord}, CriminalDegree: {file.CriminalDegree}, Contraband: {file.Contraband}");
        }
    }

    public void OnClickSubmitFile()
    {
        if (documentManager.CheckAllItems(dialogueLoader.dialogueDict[currentTextId].documentSetId))
        {
            textClickPad.gameObject.SetActive(true);
            documentManager.ClearDocuments();
            documentCheckScreen.SetActive(false);

            // Save in archive
            SubmitFile(dialogueLoader.dialogueDict[currentTextId].fileId);


            currentTextId = dialogueLoader.dialogueDict[currentTextId].nextId;
            UpdateDialogue();
        }
    }

    private void SubmitFile(int fileId)
    {
        if (fileList.ContainsKey(fileId))
        {
            fileList[fileId].Name = documentManager.Name.text;
            fileList[fileId].Age = documentManager.Age.text;
            fileList[fileId].Breed = documentManager.Breed.text;
            fileList[fileId].CauseOfDeath = documentManager.CauseOfDeath.text;
            fileList[fileId].CriminalRecord = documentManager.CriminalRecord.text;
            fileList[fileId].CriminalDegree = documentManager.CriminalDegree.text;
            fileList[fileId].Contraband = documentManager.Contraband.text;
        }
        else
        {
            FileInfo fi = new FileInfo();
            fileList.Add(fileId, fi);
            fileList[fileId].FileId = fileId;
            fileList[fileId].Name = documentManager.Name.text;
            fileList[fileId].Age = documentManager.Age.text;
            fileList[fileId].Breed = documentManager.Breed.text;
            fileList[fileId].CauseOfDeath = documentManager.CauseOfDeath.text;
            fileList[fileId].CriminalRecord = documentManager.CriminalRecord.text;
            fileList[fileId].CriminalDegree = documentManager.CriminalDegree.text;
            fileList[fileId].Contraband = documentManager.Contraband.text;
        }
    }
}
