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

    public bool ifAppeared;

    public AudioClip heavenMusic;
    public void OnEnable()
    {
        ifAppeared = false;
    }
}
