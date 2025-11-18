using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

[CreateAssetMenu(fileName = "CatChapter", menuName = "Scriptable Objects/CatChapter")]
public class CatChapter : ScriptableObject
{
    public int Id;
    public string CatName;

    public TextAsset chatText;

    public TextAsset heavenText;

    public AudioClip heavenMusic;

    public int currentChapter = 0;

    public List<DocumentSet> documentSetList;

    public bool isHeaven = false;

    public void OnEnable()
    {

        currentChapter = 0;
    }

}



