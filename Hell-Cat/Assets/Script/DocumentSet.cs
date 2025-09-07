using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Document", menuName = "Scriptable Objects/Document")]
public class DocumentSet : ScriptableObject
{
    public List<GameObject> DocumentList;
}
