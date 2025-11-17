using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AppUI.UI;
using UnityEngine;

public class DocumentManager : MonoBehaviour
{

    [Header("Document Set List")]
    [SerializeField] private List<DocumentSet> documentSets;

    [Header("Document Set Generate")]
    [SerializeField] private Transform documentTrans;
    [SerializeField] private Transform documentParent;
    [SerializeField] private int offsetValue = 20;


    public bool IfSkipDoc = false;

    public void OnEnable()
    {
    }
    //Load document data
    public void SetDocuments(int setId)
    {
/*        ClearDocuments();
        // Show new Doc
        for (int i = 0; i < documentSets[setId].DocumentList.Count; i++)
        {
            int offsetX = 0 + i * offsetValue;
            int offsetY = 0 + i * offsetValue;
            Vector3 offset = new Vector3(offsetX, offsetY, 0);
            Vector3 newPos = documentTrans.position + offset;

            GameObject ng = Instantiate(documentSets[setId].DocumentList[i], documentParent);
            ng.transform.position = newPos;
        }*/
    }

    public void AddDocumentSets(DocumentSet docSets)
    {

    }

    public void ClearDocuments()
    {
        foreach (Transform child in documentParent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }



}
