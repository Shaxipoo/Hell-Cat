using UnityEngine;

[CreateAssetMenu(fileName = "TutorialPageAssets", menuName = "Scriptable Objects/Page", order = 1)]
public class TutorialPageAsset : ScriptableObject
{
    public Sprite image;
    [TextArea] public string text;
}