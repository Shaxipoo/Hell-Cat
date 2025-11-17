using UnityEngine;
using TMPro;



public class ItemOnDoc : MonoBehaviour
{
    public string infoText;
    public TMPro.TextMeshProUGUI ButtonTextBox;

    public void OnClickInfo()
    {
        
    }
    void OnValidate()
    {
        ButtonTextBox.text = infoText;
    }
}
