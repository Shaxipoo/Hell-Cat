using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Document : MonoBehaviour
{
    public DocumentType documentType;

    [Header("Basic Info")]
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Age;
    public TextMeshProUGUI Breed;

    [Header("Application Form")]
    public TextMeshProUGUI ApplicationNo;
    public TextMeshProUGUI Gender;
    public TextMeshProUGUI CauseOfDeath;
    public TextMeshProUGUI Job;
    public TextMeshProUGUI ApplicationReason;

    [Header("Cat ID Card")]
    public TextMeshProUGUI CardId;
    public TextMeshProUGUI Weight;

    [Header("Body Check")]
    public TextMeshProUGUI GeneralAppearance;
    public TextMeshProUGUI Imaging;
    public TextMeshProUGUI MedicalHistory;

    [Header("Criminal Record")]
    public TextMeshProUGUI CriminalItem1;
    public TextMeshProUGUI CriminalItem1Extent;
    public TextMeshProUGUI CriminalItem2;
    public TextMeshProUGUI CriminalItem2Extent;

    [Header("Owner Card")]
    public TextMeshProUGUI OwnerName;
    public TextMeshProUGUI OwnerJob;
    public TextMeshProUGUI Background;

    [Header("Item Application")]
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemReason;

    [Header("Other")]
    public Image otherItemImage;

    public void Fill(DocumentSet ds)
    {
        SetText(ApplicationNo, ds.ApplicationNo.ToString());
        SetText(CardId, ds.CardId);
        SetText(Name, ds.Name);
        SetText(Age, ds.Age.ToString());
        SetText(Gender, ds.Gender);
        SetText(Breed, ds.Breed);

        SetText(CauseOfDeath, ds.CauseOfDeath);
        SetText(Job, ds.Job);
        SetText(ApplicationReason, ds.ApplicationReason);

        SetText(Weight, ds.Weight.ToString());

        SetText(GeneralAppearance, ds.GeneralAppearance);
        SetText(Imaging, ds.Imaging);
        SetText(MedicalHistory, ds.MedicalHistory);

        SetText(CriminalItem1, ds.CriminalItem1);
        SetText(CriminalItem1Extent, ds.CriminalItem1Extent);
        SetText(CriminalItem2, ds.CriminalItem2);
        SetText(CriminalItem2Extent, ds.CriminalItem2Extent);

        SetText(OwnerName, ds.OwnerName);
        SetText(OwnerJob, ds.OwnerJob);
        SetText(Background, ds.Background);

        SetText(ItemName, ds.ItemName);
        SetText(ItemReason, ds.ItemReason);

        SetImage(otherItemImage, ds.OtherItem);
    }

    private void SetText(TextMeshProUGUI tmp, string value)
    {
        if (tmp != null) tmp.text = value;
    }

    private void SetImage(Image ima, Sprite s)
    {
        if (ima != null) ima.sprite = s;
    }
}
