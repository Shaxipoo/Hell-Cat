using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class TutorialBook : MonoBehaviour
{
    [Header("Tutorial Interaction")]
    [SerializeField] private Button TutorialButton;
    [SerializeField] private GameObject FlipBook;

    [Header("Page Containers")]
    [SerializeField] private Transform LeftPageContainer;
    [SerializeField] private Transform RightPageContainer;

    [Header("Page Content")]
    [SerializeField] private List<TutorialPageAsset> pages = new List<TutorialPageAsset>();
    [SerializeField] private TutorialPageAsset EmptyPageAsset;

    private int currentLeftPageIndex = 0;

    private GameObject LeftPageInstance;
    private GameObject RightPageInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FlipBook.SetActive(false);


        if(pages.Count % 2 != 0) // 当总页数是奇数时，加empty page
        {
            pages.Add(EmptyPageAsset);
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

        // 清除旧的页面
        if (LeftPageInstance) Destroy(LeftPageInstance);
        if (RightPageInstance) Destroy(RightPageInstance);

        // 实例化新的页面
        LeftPageInstance = Instantiate(pages[currentLeftPageIndex].pagePrefab, LeftPageContainer);
        RightPageInstance = Instantiate(pages[currentRightPageIndex].pagePrefab, RightPageContainer);

        // 传递数据给页面组件（下一步实现）
        LeftPageInstance.GetComponent<TutorialPageView>().SetContent(pages[currentLeftPageIndex]);
        RightPageInstance.GetComponent<TutorialPageView>().SetContent(pages[currentRightPageIndex]);


    }

    public void OnClickNextPageButton()
    {

        if(currentLeftPageIndex + 2 < pages.Count)
        {
            currentLeftPageIndex += 2;
            ShowPage();
        }
        
    }

    public void OnClickPrePageButton()
    {
        if (currentLeftPageIndex - 2 >= 0)
        {
            currentLeftPageIndex -= 2;
            ShowPage();
        }
    }

    public void OnCloseBookButton()
    {
        TutorialButton.gameObject.SetActive(true); 
        FlipBook.gameObject.SetActive(false);
    }
}
