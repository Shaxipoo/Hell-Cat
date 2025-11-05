using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

[CreateAssetMenu(fileName = "CatChapter", menuName = "Scriptable Objects/Cat")]
public class CatChapter : ScriptableObject
{
    public int Id;
    public string CatName;

    public TextAsset chatText;

    public TextAsset teaText;

    public TextAsset heavenText;

    public AudioClip heavenMusic;

    public int currentChapter = 1;

    public bool isHeaven = false;

    public void OnEnable()
    {


        currentChapter = 1;

        isHeaven = false;
    }


}
