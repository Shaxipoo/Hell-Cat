using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class TutorialPageView : MonoBehaviour
{
    [SerializeField] private List<Image> imageSlots;
    [SerializeField] private List<TMP_Text> textSlots;

    public void SetContent(TutorialPageAsset pageAsset)
    {
        for (int i = 0; i < imageSlots.Count; i++)
        {
            imageSlots[i].gameObject.SetActive(i < pageAsset.images.Count);
            if (i < pageAsset.images.Count)
                imageSlots[i].sprite = pageAsset.images[i];
        }

        for (int i = 0; i < textSlots.Count; i++)
        {
            textSlots[i].gameObject.SetActive(i < pageAsset.texts.Count);
            if (i < pageAsset.texts.Count)
                textSlots[i].text = pageAsset.texts[i];
        }
    }

}