using TMPro;
using UnityEngine;

public class DocApplicationForm : MonoBehaviour
{
    public TextMeshProUGUI ApplicationNo;
    public TextMeshProUGUI ID;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Age;
    public TextMeshProUGUI Gender;
    public TextMeshProUGUI Breed;
    public TextMeshProUGUI COD;
    public TextMeshProUGUI Job;
    public TextMeshProUGUI Reason;

    public void Fill(DocumentSet ds)
    {
        ApplicationNo.text = ds.ApplicationNo.ToString();
        ID.text = ds.CardId.ToString();
        Name.text = ds.Name;
        Age.text = ds.Age.ToString();
        Gender.text = ds.Gender;
        Breed.text = ds.Breed;
        COD.text = ds.CauseOfDeath;
        Job.text = ds.Job;
        Reason.text = ds.ApplicationReason;

    }

}
