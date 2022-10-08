using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupPagesController : MonoBehaviour
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private UIPage[] pages;
    [SerializeField] private UIPage defaultPage;
    [SerializeField] private UIManager uiManager;

    private void Awake()
    {
        //pages = GetComponentsInChildren<UIPage>(true);
        uiManager = FindObjectOfType<UIManager>();
        foreach (UIPage page in pages)
        {
            if (page != defaultPage)
            page.OnOpenpage?.AddListener(() =>
            {
                closeBtn.gameObject.SetActive(true);
                uiManager.ClosePage(defaultPage);
                foreach (UIPage p in pages)
                {
                    if(p != page)
                    uiManager.ClosePage(p);
                }
                closeBtn.onClick.AddListener(() =>
                {
                    uiManager.ClosePage(page);
                });
            });

            //if (page != defaultPage)
            //{
            //    page.OnOpenpage.AddListener(() =>
            //    {

            //        foreach (UIPage p in pages)
            //        {
            //            if (p != page && p != defaultPage)
            //            {
            //                uiManager.ClosePage(p);
            //            }
            //        }
            //        ///closeBtn.onClick.RemoveAllListeners();
            //        closeBtn.onClick.AddListener(() =>
            //        {
            //            uiManager.ClosePage(page);
            //        });

            //        //uiManager.ClosePage(defaultPage);
            //    });
            //}
        }
        uiManager.OpenPage(defaultPage);
        //defaultPage.OnOpenpage?.AddListener(() =>
        //{
        //    closeBtn.gameObject.SetActive(false);
        //});
        //defaultPage.OnClosepage?.AddListener(() =>
        //{
        //    closeBtn.gameObject.SetActive(true);
        //});
    }



}
