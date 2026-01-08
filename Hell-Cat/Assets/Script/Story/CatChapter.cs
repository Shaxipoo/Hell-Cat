using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

[CreateAssetMenu(fileName = "CatChapter", menuName = "Scriptable Objects/CatChapter")]
public class CatChapter : ScriptableObject
{
    public int Id;
    public string CatName;

    public TextAsset chatText;

    public AudioClip heavenMusic;

    public List<DocumentSet> documentSetList;
}



