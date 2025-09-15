using UnityEngine;


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


    public void OnClickInfo()
    {
        GameObject.Find("Document Manager").GetComponent<DocumentManager>().InputInfo(infoType, infoText);
    }
}
