using UnityEngine;
using TMPro;


public enum InfoType
{
    Name,
    Age,
    Breed,
    CauseOfDeath,
    CriminalRecord,
    CriminalDegree,
    Contraband,
}

public class ItemOnDoc : MonoBehaviour
{
    public InfoType infoType;
    public string infoText;
    public TMPro.TextMeshProUGUI ButtonTextBox;

    public void OnClickInfo()
    {
        GameObject.Find("Document Manager").GetComponent<DocumentManager>().InputInfo(infoType, infoText);
    }
    public void Start()
    {
        ButtonTextBox.text = infoText;
    }
}
