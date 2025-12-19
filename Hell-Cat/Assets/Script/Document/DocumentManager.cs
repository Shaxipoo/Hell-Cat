using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AppUI.UI;
using UnityEngine;

public class DocumentManager : MonoBehaviour
{
    public static DocumentManager instance;

    [Header("Document Set Generate")]
    [SerializeField] private Transform documentTrans;
    [SerializeField] private Transform documentParent;
    [SerializeField] private int offsetValue = 20;

    [Header("All Document Prefabs")]
    [SerializeField] private GameObject Prefab_ApplicationForm;
    [SerializeField] private GameObject Prefab_CatID;
    [SerializeField] private GameObject Prefab_BodyCheck;
    [SerializeField] private GameObject Prefab_CriminalRecord;
    [SerializeField] private GameObject Prefab_OwnerCard;
    [SerializeField] private GameObject Prefab_ItemApplication;
    [SerializeField] private GameObject Prefab_OtherDocument;


    [HideInInspector]
    public List<GameObject> docsOnTable;
    [HideInInspector]
    public DocumentSet currentDocumentSet;


    [Header("Interrogates")]
    // Interrogate Items in store
    [HideInInspector] public List<DocumentInteraction> storedInterrogateList;
    [SerializeField] private List<GameObject> interrogateBubbles;
    [SerializeField] private GameObject chatBubble;

    private void Awake()
    {
        instance = this;

        docsOnTable = new List<GameObject>();
        storedInterrogateList = new List<DocumentInteraction>();

        HideInterrogateBubbles();
        HideChatBubble();
    }

    public void AddDocumentSets(DocumentSet docSets)
    {
        currentDocumentSet = docSets;

        for (int i = 0; i < docSets.documentList.Count; i++)
        {
            int offsetX = 0 + i * offsetValue;
            int offsetY = 0 + i * offsetValue;
            Vector3 offset = new Vector3(offsetX, offsetY, 0);
            Vector3 newPos = documentTrans.position + offset;

            AddDocument(docSets.documentList[i],newPos,docSets);
        }
    }

    public void AddDocument(DocumentType dt, Vector3 pos, DocumentSet ds = null)
    {
        GameObject ng = null;
        switch (dt)
        {
            case DocumentType.ApplicationForm:
                ng = Instantiate(Prefab_ApplicationForm, documentParent);
                break;

            case DocumentType.CatIdCard:
                ng = Instantiate(Prefab_CatID, documentParent);
                break;

            case DocumentType.BodyCheck:
                ng = Instantiate(Prefab_BodyCheck, documentParent);
                break;

            case DocumentType.CriminalRecord:
                ng = Instantiate(Prefab_CriminalRecord, documentParent);
                break;

            case DocumentType.OwnerCard:
                ng = Instantiate(Prefab_OwnerCard, documentParent);
                break;

            case DocumentType.ItemApplicationForm:
                ng = Instantiate(Prefab_ItemApplication, documentParent);
                break;

            case DocumentType.Other:
                ng = Instantiate(Prefab_OtherDocument, documentParent);
                break;
        }
        ng.transform.position = pos;

        FillDocumentInfo(ng,ds);

        docsOnTable.Add(ng);
    }

    public void FillDocumentInfo(GameObject go,DocumentSet ds)
    {
        go.GetComponent<Document>().Fill(ds);
    }

    public void ClearDocument()
    {

    }
    public void ClearAllDocuments()
    {
        foreach (Transform child in documentParent)
        {
            GameObject.Destroy(child.gameObject);
        }
        docsOnTable.Clear();
        storedInterrogateList.Clear();
    }

    /*
     * Highlight Items on Doc Related
     */
    [HideInInspector]
    public bool isPen;
    public void OnClickPencilCase()
    {
        if(isPen){DropPen();}
        else{PickupPen();}
    }

    public void PickupPen()
    {
        CursorManager.Instance.ChangeCursorToHighlight();
        isPen = true;
        DisableDocDrag();  
    }

    public void DropPen()
    {
        CursorManager.Instance.ChangeCursorToNormal();
        isPen = false;
        EnableDocDrag();
    }

    public void EnableDocDrag()
    {
        foreach(GameObject g in docsOnTable)
        {
            g.GetComponent<DocumentUIHandler>().enabled = true;
        }
    }

    public void DisableDocDrag()
    {
        foreach (GameObject g in docsOnTable)
        {
            g.GetComponent<DocumentUIHandler>().enabled = false;
        }
    }

    /*
     * Interrogate Related
     */
    public void AddInterrogateItem(DocumentInteraction di)
    {
        storedInterrogateList.Add(di);
    }

    private bool isInterrogating = false;
    public void OnClickInterrogate()
    {
        if (isInterrogating) HideInterrogateBubbles();
        else ShowInterrogateBubbles();
    }
    public void ShowInterrogateBubbles()
    {
        isInterrogating = true;
        for (int i = 0;i < storedInterrogateList.Count; i++)
        {
            interrogateBubbles[i].SetActive(true);
            interrogateBubbles[i].GetComponentInChildren<TextMeshProUGUI>().text = storedInterrogateList[i].bubbleText;
        }

    }
    public void HideInterrogateBubbles()
    {
        isInterrogating = false;
        foreach (GameObject g in interrogateBubbles)
        {
            g.SetActive(false);
        }
        
    }

    private int currentChatAnswerIndex = 0;
    private int currentChatBubbleIndex = 0;
    public void LoadNextChat()
    {     
        // If reach the end
        if(currentChatAnswerIndex >= currentDocumentSet.documentInteractions[currentChatBubbleIndex].answer.Count)
        {
            Debug.Log("Bug haha");
            HideChatBubble();
            return;
        }
        // Set to next chat
        chatBubble.GetComponentInChildren<TextMeshProUGUI>().text = currentDocumentSet.documentInteractions[currentChatBubbleIndex].answer[currentChatAnswerIndex];

        currentChatAnswerIndex++;
    }
    public void ShowChatBubble()
    {
        Debug.Log("ShowChatBubble");
        chatBubble.SetActive(true);
        HideInterrogateBubbles();
    }
    public void HideChatBubble()
    {
        chatBubble.SetActive(false);
        ShowInterrogateBubbles();
        currentChatAnswerIndex = 0;
    }
    public void SetChatBubble(int bubbleIndex)
    {
        Debug.Log("Clicked bubble");
        ShowChatBubble();
        currentChatBubbleIndex = bubbleIndex;
        LoadNextChat();
    }


    /*
     * Application Result Paper
     */
    private bool isHoldPaper;
    private bool isGreenPaper;
    public void ClickOnWhitePaper()
    {
        if(isHoldPaper)
        {
            isHoldPaper = false;
            CursorManager.Instance.ChangeCursorToNormal();
        }
        else
        {
            isHoldPaper = true;
            isGreenPaper = false;
            CursorManager.Instance.ChangeCursorToWhite();
        }          
    }

    public void ClickOnGreenPaper()
    {
        if (isHoldPaper)
        {
            isHoldPaper = false;
            CursorManager.Instance.ChangeCursorToNormal();
        }
        else
        {
            isHoldPaper = true;
            isGreenPaper = true;
            CursorManager.Instance.ChangeCursorToGreen();
        }
    }

    public void GiveResultToCharacter()
    {
        if(isHoldPaper)
        {
            CursorManager.Instance.ChangeCursorToNormal();
            ClearAllDocuments();
            // if send the cat to heaven
            if (isGreenPaper)
            {    
                GameManager.Instance.GetCurrentCatChapter().isHeaven = true;
                GameManager.Instance.SwitchBackToDialogueWithAnswer(true);
            }

            // if send the cat to heaven
            else
            {
                GameManager.Instance.SwitchBackToDialogueWithAnswer(false);
            }           
        }
    }




}
