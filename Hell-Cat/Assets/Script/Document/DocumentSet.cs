using System.Collections.Generic;
using UnityEngine;

public enum DocumentType
{
    ApplicationForm,
    CatIdCard,
    BodyCheck,
    CriminalRecord,
    OwnerCard,
    ItemApplicationForm,
    Other,
}
public enum InteractionType
{
    NoCard,
    AskBreed,
    AskCOD,
    AskJob,
    AskApplicationReason,
    AskGeneralAppearance,
    AskImaging,
    AskMedicalHistory,
    AskCriminalItem1,
    AskCriminalItem2,
    AskItemReason,
    AskOther,
}
[System.Serializable]
public class DocumentInteraction
{
    public DocumentType docType;  
    public InteractionType interactType;
    public InteractionType bubbleText;
    public List<string> answer;
}

[CreateAssetMenu(fileName = "Document", menuName = "Scriptable Objects/Document")]

public class DocumentSet : ScriptableObject
{
    public string DocumentId;

    [Header("Document List")]
    public List<DocumentType> documentList;

    public List<DocumentInteraction> documentInteractions;

    [Header("What to keep after the cat left")]
    public List<DocumentType> keepList;

    [Header("Basic Info")]
    public string Name;
    public int Age;
    public string Breed;

    [Header("Application Form")]
    public int ApplicationNo;
    public string Gender;
    public string CauseOfDeath;
    public string Job;
    public string ApplicationReason;

    [Header("Cat ID Card")]
    public string CardId;
    public int Weight;

    [Header("Body Check")]
    public string GeneralAppearance;
    public string Imaging;
    public string MedicalHistory;

    [Header("CriminalRecord")]
    public string CriminalItem1;
    public string CriminalItem1Extent;
    public string CriminalItem2;
    public string CriminalItem2Extent;

    [Header("Owner Card")]
    public string OwnerName;
    public string OwnerJob;
    public string Background;


    [Header("Item Application")]
    public string ItemName;
    public string ItemReason;

    [Header("OtherItem")]
    public GameObject OtherItem; 

}
