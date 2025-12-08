using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemOnDoc : MonoBehaviour
{
    public InteractionType interactionType;

    private Color origColor;
    private Image image;

    private void OnEnable()
    {
        image = GetComponent<Image>();
        origColor = image.color;
        
        HideHighlight();

    }

    public void OnClickItem()
    {
        Debug.Log("On click");
        if (DocumentManager.instance.isPen)
        {
            foreach (DocumentInteraction di in DocumentManager.instance.currentDocumentSet.documentInteractions)
            {
                if (di.interactType == interactionType)
                {
                    ShowHightlight();

                    DocumentManager.instance.AddInterrogateItem(di);
                    GetComponent<Button>().enabled = false;

                }
            }
        }

    }
    public void HideHighlight()
    {
        Color c = image.color;
        c.a = 0f;
        image.color = c;
    }
    public void ShowHightlight()
    {
        Color c = origColor;
        image.color = c;
    }
}
