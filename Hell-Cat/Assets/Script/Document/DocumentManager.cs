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
    [SerializeField] private TypewriterEffect typewriterEffect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

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
            Vector3 offset = new Vector3(i * offsetValue, i * offsetValue, 0);
            Vector3 newPos = documentTrans.position + offset;
            AddDocument(docSets.documentList[i], newPos, docSets);
        }
    }

    public void AddDocument(DocumentType dt, Vector3 pos, DocumentSet ds = null)
    {
        GameObject prefab = GetPrefabForType(dt);
        if (prefab == null)
        {
            Debug.LogWarning($"DocumentManager: No prefab for DocumentType {dt}");
            return;
        }

        GameObject ng = Instantiate(prefab, documentParent);
        if (ng != null)
        {
            ng.transform.position = pos;
            FillDocumentInfo(ng, ds);
            docsOnTable.Add(ng);
        }
    }

    private GameObject GetPrefabForType(DocumentType dt)
    {
        switch (dt)
        {
            case DocumentType.ApplicationForm: return Prefab_ApplicationForm;
            case DocumentType.CatIdCard: return Prefab_CatID;
            case DocumentType.BodyCheck: return Prefab_BodyCheck;
            case DocumentType.CriminalRecord: return Prefab_CriminalRecord;
            case DocumentType.OwnerCard: return Prefab_OwnerCard;
            case DocumentType.ItemApplicationForm: return Prefab_ItemApplication;
            case DocumentType.Other: return Prefab_OtherDocument;
            default: return null;
        }
    }

    public void FillDocumentInfo(GameObject go,DocumentSet ds)
    {
        go.GetComponent<Document>().Fill(ds);
    }

    public void ClearDocuments()
    {
        // Destroy all documents except those whose DocumentType is listed in currentDocumentSet.keepList
        var keepList = currentDocumentSet != null ? currentDocumentSet.keepList : null;
        HashSet<DocumentType> keepSet = null;
        if (keepList != null) keepSet = new HashSet<DocumentType>(keepList);

        List<GameObject> toDestroy = new List<GameObject>();

        foreach (Transform child in documentParent)
        {
            var go = child.gameObject;

            // Try to infer DocumentType by comparing with known prefabs' names
            bool matched = false;
            DocumentType matchedType = default;

            if (Prefab_ApplicationForm != null && go.name.StartsWith(Prefab_ApplicationForm.name)) { matched = true; matchedType = DocumentType.ApplicationForm; }
            else if (Prefab_CatID != null && go.name.StartsWith(Prefab_CatID.name)) { matched = true; matchedType = DocumentType.CatIdCard; }
            else if (Prefab_BodyCheck != null && go.name.StartsWith(Prefab_BodyCheck.name)) { matched = true; matchedType = DocumentType.BodyCheck; }
            else if (Prefab_CriminalRecord != null && go.name.StartsWith(Prefab_CriminalRecord.name)) { matched = true; matchedType = DocumentType.CriminalRecord; }
            else if (Prefab_OwnerCard != null && go.name.StartsWith(Prefab_OwnerCard.name)) { matched = true; matchedType = DocumentType.OwnerCard; }
            else if (Prefab_ItemApplication != null && go.name.StartsWith(Prefab_ItemApplication.name)) { matched = true; matchedType = DocumentType.ItemApplicationForm; }
            else if (Prefab_OtherDocument != null && go.name.StartsWith(Prefab_OtherDocument.name)) { matched = true; matchedType = DocumentType.Other; }

            // If we couldn't match a type, assume it should be destroyed (not preserved)
            bool keep = false;
            if (keepSet != null && matched)
            {
                keep = keepSet.Contains(matchedType);
            }

            if (!keep)
            {
                toDestroy.Add(go);
            }
        }

        foreach (var go in toDestroy)
        {
            GameObject.Destroy(go);
        }

        // Rebuild docsOnTable from remaining children
        docsOnTable.Clear();
        foreach (Transform child in documentParent)
        {
            docsOnTable.Add(child.gameObject);
        }

        storedInterrogateList.Clear();
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
        foreach (GameObject g in docsOnTable)
        {
            var handler = g.GetComponent<DocumentUIHandler>();
            if (handler != null) handler.enabled = true;
        }
    }

    public void DisableDocDrag()
    {
        foreach (GameObject g in docsOnTable)
        {
            var handler = g.GetComponent<DocumentUIHandler>();
            if (handler != null) handler.enabled = false;
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
        int count = Math.Min(storedInterrogateList.Count, interrogateBubbles.Count);
        for (int i = 0; i < count; i++)
        {
            interrogateBubbles[i].SetActive(true);
            var text = interrogateBubbles[i].GetComponentInChildren<TextMeshProUGUI>();
            if (text != null) text.text = storedInterrogateList[i].bubbleText;
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
        if (typewriterEffect == null) return;

        if (!typewriterEffect.IsAllText())
        {
            typewriterEffect.ShowAllText();
            return;
        }

        if (currentDocumentSet == null || currentDocumentSet.documentInteractions == null) return;
        if (currentChatBubbleIndex < 0 || currentChatBubbleIndex >= currentDocumentSet.documentInteractions.Count) return;

        var interaction = currentDocumentSet.documentInteractions[currentChatBubbleIndex];
        if (interaction == null || interaction.answer == null) { HideChatBubble(); return; }

        // If reach the end
        if (currentChatAnswerIndex >= interaction.answer.Count)
        {
            HideChatBubble();
            return;
        }

        // Set to next chat
        typewriterEffect.StartTypeWriter(interaction.answer[currentChatAnswerIndex]);
        currentChatAnswerIndex++;
    }
    public void ShowChatBubble()
    {
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
        ShowChatBubble();
        currentChatBubbleIndex = bubbleIndex;
        LoadNextChat();
    }

    public void FastSkip()
    {
        
        LoadNextChat();
    }

    /*
     * Application Result Paper
     */
    private bool isHoldPaper;
    private bool isGreenPaper;

    // Decline
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

    // Accept
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
            ClearDocuments();
            // if send the cat to heaven
            if (isGreenPaper)
            {    
                var currentCat = GameManager.Instance.GetCurrentCatChapter();
                if (currentCat != null)
                {
                    PlayerData.SetIsHeaven(currentCat.Id, true);
                }
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
