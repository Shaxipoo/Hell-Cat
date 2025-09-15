using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DocumentManager : MonoBehaviour
{
    [Header("Require Info Text")]
    [SerializeField] public TextMeshProUGUI Name;
    [SerializeField] public TextMeshProUGUI Age;
    [SerializeField] public TextMeshProUGUI Breed;
    [SerializeField] public TextMeshProUGUI CauseOfDeath;
    [SerializeField] public TextMeshProUGUI CriminalRecord;
    [SerializeField] public TextMeshProUGUI CriminalDegree;
    [SerializeField] public TextMeshProUGUI Contraband;


    [Header("Document Set List")]
    [SerializeField] private List<DocumentSet> documentSets;

    [Header("Document Set Generate")]
    [SerializeField] private Transform documentTrans;
    [SerializeField] private Transform documentParent;
    [SerializeField] private int offsetValue = 20;

    [Header("Submit Check List")]
    [SerializeField] private List<TextMeshProUGUI> requiredInfoList;
    [SerializeField] private GameObject moreInfoTips;


    //Load document data
    public void SetDocuments(int setId)
    {
        moreInfoTips.SetActive(false);
        ClearDocuments();
        // Show new Doc
        for (int i = 0; i < documentSets[setId].DocumentList.Count; i++)
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
    public void InputInfo(InfoType it, string s)
    {
        switch (it)
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
            case InfoType.CauseOfDeath:
                CauseOfDeath.text = s;
                break;
            case InfoType.CriminalRecord:
                CriminalRecord.text = s;
                break;
            case InfoType.CriminalDegree:
                CriminalDegree.text = s;
                break;
            case InfoType.Contraband:
                Contraband.text = s;
                break;

        }
    }


    public bool CheckAllItems(int documentSetId)
    {
        foreach (InfoType i in documentSets[documentSetId].requiredInfo)
        {
            foreach (TextMeshProUGUI tmp in requiredInfoList)
            {
                if (tmp.gameObject.name == i.ToString())
                {
                    if (tmp.text == "")
                    {
                        StartCoroutine(ShowMoreInfoTips(2f));
                        return false;
                    }
                }
            }

        }



        return true;
    }

    IEnumerator ShowMoreInfoTips(float t)
    {
        moreInfoTips.SetActive(true);
        yield return new WaitForSeconds(t);
        moreInfoTips.SetActive(false);

    }

}
