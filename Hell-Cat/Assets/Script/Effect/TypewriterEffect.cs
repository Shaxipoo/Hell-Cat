using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private string fullText;
    public float delay = 0.05f;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void StartTypeWriter(string newText)
    {
        fullText = newText;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(RevealText());
    }

    IEnumerator RevealText()
    {
        isTyping = true;
        textComponent.text = fullText;
        textComponent.maxVisibleCharacters = 0;

        for (int i = 0; i <= fullText.Length; i++)
        {
            textComponent.maxVisibleCharacters = i;
            yield return new WaitForSeconds(delay);
        }

        isTyping = false;
    }

    public void ShowAllText()
    {
        if (isTyping && typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            isTyping = false;
        }
        textComponent.maxVisibleCharacters = fullText.Length;
    }

    public bool IsAllText()
    {
        return textComponent.maxVisibleCharacters >= fullText.Length;
    }

    public bool IsTyping()
    {
        return isTyping;
    }
}