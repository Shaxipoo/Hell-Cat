using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class TutorialFlipBook : MonoBehaviour
{
    [Header("Tutorial Interaction")]
    [SerializeField] private Button TutorialButton;
    [SerializeField] private GameObject FlipBook;
    [SerializeField] private Button nextPageButton;
    [SerializeField] private Button prevPageButton;

    [Header("Book Content")]
    [SerializeField] private Image LeftPage;
    [SerializeField] private Image RightPage;
    [SerializeField] private TMP_Text LeftPageText;
    [SerializeField] private TMP_Text RightPageText;

    [Header("Page Content")]
    [SerializeField] private List<TutorialPageAsset> pages = new List<TutorialPageAsset>();
    [SerializeField] private TutorialPageAsset EmptyPageAsset;
    private int currentLeftPageIndex = 0;
    private int MaxPageIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FlipBook.SetActive(false);

        MaxPageIndex = pages.Count-1;

        if(MaxPageIndex % 2 == 0) // 当Index是偶数，总页数是奇数时，加empty page, 保障maxPageIndex应总为奇数
        {
            //add empty pages at the end
            pages.Add(EmptyPageAsset);
            MaxPageIndex = pages.Count;
        }

        ShowPage();
        
    }

    public void OnClickTutorial()
    {
        TutorialButton.gameObject.SetActive(false); //Hide the button
        FlipBook.gameObject.SetActive(true);//Show Book

    }

    public void ShowPage()
    {
        if (pages == null || pages.Count == 0) return;
        
        int currentRightPageIndex = currentLeftPageIndex+1;

        LeftPage.sprite = pages[currentLeftPageIndex].image;
        LeftPageText.text = pages[currentLeftPageIndex].text;
        RightPage.sprite = pages[currentRightPageIndex].image;
        RightPageText.text = pages[currentRightPageIndex].text;

    }

    public void OnClickNextPageButton()
    {
        currentLeftPageIndex += 2;

        if(currentLeftPageIndex<= MaxPageIndex-1)
        {
            ShowPage();
        }
        else
        {
            Debug.Log("Page doesn't exist");
        }
        
    }

    public void OnClickPrePageButton()
    {
        currentLeftPageIndex -= 2;

        if (currentLeftPageIndex < 0)
        {
            Debug.Log("Page doesn't exist");
        }
        else
        {
            ShowPage();
        }
        
    }
}
