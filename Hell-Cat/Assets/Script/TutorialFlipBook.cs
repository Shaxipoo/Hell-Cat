using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TutorialFlipBook : MonoBehaviour
{
    [SerializeField] private Button TutorialButton;
    [SerializeField] private GameObject FlipBook;
    [SerializeField] private Image LeftPage;
    [SerializeField] private Image RightPage;
    [SerializeField] private Button nextPageButton;
    [SerializeField] private Image prevPageButton;
    [SerializeField] private TMP_Text LeftPageText;
    [SerializeField] private TMP_Text RightPageText;

    [SerializeField] private Transform DragArea;

    private int currentLeftPage = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FlipBook.SetActive(false);
    }


    public void OnClickTutorial()
    {
        TutorialButton.gameObject.SetActive(false); //Hide the button
        FlipBook.gameObject.SetActive(true);//Show Book

    }

    public void OnClickNextPageButton()
    {

    }

    public void OnClickPrePageButton()
    {

    }
}
