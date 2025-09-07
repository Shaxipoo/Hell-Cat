using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DocumentManager : MonoBehaviour
{
    [Header("Require Info Text")]
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Age;
    [SerializeField] private TextMeshProUGUI Breed;

    [Header("Document Set List")]
    [SerializeField] private List<DocumentSet> documentSets;

    [Header("Document Set Generate")]
    [SerializeField] private Transform documentTrans;
    [SerializeField] private Transform documentParent;
    [SerializeField] private int offsetValue = 20;

    [Header("Submit Check List")]
    [SerializeField] private List<TextMeshProUGUI> requiredInfoList;

    //Load document data
    public void SetDocuments(int setId)
    {
        ClearDocuments();
        // Show new Doc
        for (int i = 0; i < documentSets[setId].DocumentList.Count;i++)
        {
            int offsetX = 0 + i * offsetValue;
            int offsetY = 0 + i * offsetValue;
            Vector3 offset = new Vector3(offsetX, offsetY, 0);
            Vector3 newPos = documentTrans.position + offset;

            GameObject ng = Instantiate(documentSets[setId].DocumentList[i], documentParent);
            ng.transform.position = newPos;
        }
    }

    public void ClearDocuments()
    {
        foreach (Transform child in documentParent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void InputInfo(InfoType it,string s)
    {
        switch(it)
        {
            case InfoType.Name:
                Name.text = s;
                break;
            case InfoType.Age:
                Age.text = s;
                break;
            case InfoType.Breed:
                Breed.text = s;
                break;
        }
    }


    public bool CheckAllItems()
    {
        foreach(TextMeshProUGUI t in requiredInfoList)
        {
            if(t.text == "")
            {
                return false;
            }
        }
        return true;
    }




    

}
