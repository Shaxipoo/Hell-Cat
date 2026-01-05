using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private const string NO_VALUE = "-1";

    [Header("Set Up")]
    [SerializeField] private int startTextId = 1;

    private int currentTextId;

    [HideInInspector]
    public DialogueLoader dialogueLoader;

    [Header("UI: Dialogue")]
    [SerializeField] private TextMeshProUGUI speakerNameTMPro;
    [SerializeField] private TextMeshProUGUI speakerTextTMPro;
    [SerializeField] private Image speakerImage;
    [SerializeField] private TypewriterEffect typewriterEffect;

    //[SerializeField] private Button textClickPad;

    [Header("UI: Options")]
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private List<GameObject> optionButtonList;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void StartDialogue(int i)
    {
        optionPanel.SetActive(false);
        GameManager.Instance.storyTransitionScreen.SetActive(false);
        GameManager.Instance.HideDocumentScreen();
        
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
        if(GameManager.Instance.isInDoc)
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
                    GameManager.Instance.SwitchToDocumentCheck();
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


    public void SwitchToHeavenOrNot(bool isHeaven)
    {
        if (isHeaven)
        {
            currentTextId = dialogueLoader.dialogueDict[currentTextId].heavenID;
            Debug.Log("Switch to heaven text");
        }
        else
        {
            currentTextId = dialogueLoader.dialogueDict[currentTextId].notApproveId;
            Debug.Log("Switch to not approved text");
        }
        // 切换到指定分支后直接更新对话，不调用 OnClick()，避免触发点击逻辑导致多跳一句
        UpdateDialogue();
    }

    public void UpdateDialogue()
    {

        ApplySpeakerVisuals();

        if (dialogueLoader.dialogueDict[currentTextId].characterName == "Player")
        {
            speakerNameTMPro.text = PlayerData.playername;
            Darken(speakerImage);
        }

        typewriterEffect.StartTypeWriter(dialogueLoader.dialogueDict[currentTextId].text);
    }

    private bool HasValue(string s)
    {
        return !string.IsNullOrEmpty(s) && s != NO_VALUE;
    }

    private void ApplySpeakerVisuals()
    {
        var line = dialogueLoader.dialogueDict[currentTextId];

        // default: reset name then image state
        speakerNameTMPro.text = line.characterName == NO_VALUE ? "" : line.characterName;
        UnDarken(speakerImage);

        if (!HasValue(line.characterImage))
        {
            speakerImage.gameObject.SetActive(false);
            if (!HasValue(line.characterName)) speakerNameTMPro.text = "";
            return;
        }

        // have image
        speakerImage.gameObject.SetActive(true);
        ChangeSpeakerImage(line.characterImage);

        if (!HasValue(line.characterName))
        {
            speakerNameTMPro.text = "";
            Darken(speakerImage);
        }
    }

    public void EndStory()
    {
        GameManager.Instance.storyTransitionScreen.SetActive(true);
    }


    /* 
     * Independent Performance
     */
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
        GameManager.Instance.DisableDialogueClick();

        foreach (GameObject button in optionButtonList) button.SetActive(false);

        var line = dialogueLoader.dialogueDict[currentTextId];
        List<string> options = new List<string>();
        if (HasValue(line.option1)) options.Add(line.option1);
        if (HasValue(line.option2)) options.Add(line.option2);
        if (HasValue(line.option3)) options.Add(line.option3);
        if (HasValue(line.option4)) options.Add(line.option4);
        if (HasValue(line.option5)) options.Add(line.option5);

        optionPanel.SetActive(true);
        int showCount = Mathf.Min(options.Count, optionButtonList.Count);
        for (int i = 0; i < showCount; i++)
        {
            optionButtonList[i].SetActive(true);
            optionButtonList[i].GetComponentInChildren<TextMeshProUGUI>().text = options[i];
        }
    }

    public void ClickOption(int no)
    {
        GameManager.Instance.EnableDialogueClick();

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

    public void FastSkip()
    {
        if(GameManager.Instance.isFastSkipEnable)
        {
            OnClick();
            typewriterEffect.ShowAllText();
        }
        
    }
}
