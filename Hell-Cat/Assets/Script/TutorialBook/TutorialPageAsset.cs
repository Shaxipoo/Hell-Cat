using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialPageAssets", menuName = "Scriptable Objects/Page")]
public class TutorialPageAsset : ScriptableObject
{
    public GameObject pagePrefab; // page layouts
    public List<Sprite> images = new List<Sprite>();
    [TextArea] public List<string> texts = new List<string>();
}