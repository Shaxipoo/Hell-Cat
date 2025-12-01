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

    public DialogueLoader dialogueLoader;
    public GameManager gameManager;

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
    [SerializeField] private GameObject storyTransitionScreen;

    [Header("UI: Archive")]
    [SerializeField] private GameObject archiveScreen;

    [Header("UI: Document Check")]
    [SerializeField] private GameObject documentCheckScreen;
    [SerializeField] private DocumentManager documentManager;

    private bool isFastSkipEnable = true;

    private bool isInDoc = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void StartDialogue(int i)
    {
        optionPanel.SetActive(false);
        storyTransitionScreen.SetActive(false);
        documentCheckScreen.SetActive(false);
        archiveScreen.SetActive(false);
        
        foreach(var v in dialogueLoader.dialogueDict)
        {
            if(v.Value.chapterId == i)
            {
                currentTextId = v.Key;
                UpdateDialogue();
                break;
            }
        }    
    }

    public void OnClick()
    {
        if(isInDoc)
        {
            return;
        }
        // is all text
        if (typewriterEffect.IsAllText())
        {
            switch (dialogueLoader.dialogueDict[currentTextId].specialActionId)
            {
                // Options
                case 1:
                    ShowOption();
                    break;
                // End Story
                case 2:
                    EndStory();
                    break;
                // Show Document
                case 3:
                    documentCheckScreen.SetActive(true);
                    documentManager.AddDocumentSets(gameManager.GetCurrentDocumentSet());

                    textClickPad.gameObject.SetActive(false);
                    isFastSkipEnable = false;
                    isInDoc = true;
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
            speakerImage.gameObject.SetActive(false);
            if (dialogueLoader.dialogueDict[currentTextId].characterName == "-1")
            {
                speakerNameTMPro.text = "";
            }
        }
        //have image
        else
        {
            if (dialogueLoader.dialogueDict[currentTextId].characterName == "-1")
            {
                speakerNameTMPro.text = "";
                speakerImage.gameObject.SetActive(true);
                ChangeSpeakerImage(dialogueLoader.dialogueDict[currentTextId].characterImage);
                Darken(speakerImage);
            }
            //有名字，有立绘
            else
            {
                speakerImage.gameObject.SetActive(true);
                ChangeSpeakerImage(dialogueLoader.dialogueDict[currentTextId].characterImage);
            }
        }


        if (dialogueLoader.dialogueDict[currentTextId].characterName == "Player")
        {
            speakerNameTMPro.text = PlayerData.playername;
            Darken(speakerImage);
            //Debug.Log("playername == " + PlayerData.playername);
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
            //Debug.LogError("Rin: cannot find image");
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
        storyTransitionScreen.SetActive(true);
    }

    public void FastSkip()
    {
        if(isFastSkipEnable)
        {
            OnClick();
            typewriterEffect.ShowAllText();
        }
        
    }
}
