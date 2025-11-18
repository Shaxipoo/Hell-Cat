using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AppUI.UI;
using UnityEngine;

public class DocumentManager : MonoBehaviour
{

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

    public bool IfSkipDoc = false;

    public void OnEnable()
    {
    }
    //Load document data
    public void SetDocuments()
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
        for (int i = 0; i < docSets.documentList.Count; i++)
        {
            int offsetX = 0 + i * offsetValue;
            int offsetY = 0 + i * offsetValue;
            Vector3 offset = new Vector3(offsetX, offsetY, 0);
            Vector3 newPos = documentTrans.position + offset;

            AddDocument(docSets.documentList[i],newPos);

        }
        Debug.Log("add doc");

    }

    public void AddDocument(DocumentType dt, Vector3 pos)
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

/*        //Set Position
        float randomRange = 50f;

        float offsetX = Random.Range(-randomRange, randomRange);
        float offsetY = Random.Range(-randomRange, randomRange);

        Vector3 offset = new Vector3(offsetX, offsetY, 0);
        Vector3 newPos = documentTrans.position + offset;

        ng.transform.position = newPos;*/


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
    }



}
