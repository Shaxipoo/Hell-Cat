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

    [Header("UI: Document Check")]
    [SerializeField] private GameObject documentCheckScreen;
    [SerializeField] private DocumentManager documentManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        optionPanel.SetActive(false);
        endStoryScreen.SetActive(false);
        documentCheckScreen.SetActive(false);

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
        typewriterEffect.StartTypeWriter(dialogueLoader.dialogueDict[currentTextId].text);
    }


    public void ShowOption()
    {
        textClickPad.interactable = false;

        foreach(GameObject button in optionButtonList)
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

    public void OnClickSubmitFile()
    {
        if(documentManager.CheckAllItems())
        {
            textClickPad.gameObject.SetActive(true);
            documentManager.ClearDocuments();
            documentCheckScreen.SetActive(false);
            currentTextId = dialogueLoader.dialogueDict[currentTextId].nextId;
            UpdateDialogue();
        }
    }

}
