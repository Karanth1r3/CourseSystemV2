using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    Canvas mainCanvas;
    [SerializeField] Canvas[] pages;
    [SerializeField] Canvas mainPage;
    // Start is called before the first frame update
    void Start()
    {
        mainCanvas = GetComponent<Canvas>();
    }

    void HideAllPages()
    {
        foreach (Canvas page in pages)
        {
            page.enabled = false;
        }
    }

    // Update is called once per frame
   public void OpenPageWithIndex(int index)
    {
        HideAllPages();
        pages[index].enabled = true;
    }

    public void ShowMainPage()
    {
        HideAllPages();
        mainPage.enabled = true;
    }
}
