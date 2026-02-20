using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PageScript : MonoBehaviour
{
    private List<GameObject> page = new List<GameObject>();
    private int currentPageIndex = 0;

    public GameObject pagePrefab;

    public void OnPreviousPage()
    {
        if (currentPageIndex == 0) return;
        page[currentPageIndex].SetActive(false);        
        currentPageIndex--;
        page[currentPageIndex].SetActive(true);
    }
    
    public void OnNextPage()
    {
        if (currentPageIndex == page.Count-1) return;
        page[currentPageIndex].SetActive(false);     
        currentPageIndex++;
        page[currentPageIndex].SetActive(true);
        
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewPage("11111111111111");
        NewPage("222222222222222222222222222");
        NewPage("333333333333333333333");
        NewPage("444444444444444444444");
        page[currentPageIndex].SetActive(true);
    }

    void NewPage(string str)
    {
        var tmpPage = Instantiate(pagePrefab, transform);
        tmpPage.transform.SetParent(transform);
        tmpPage.GetComponentInChildren<TextMeshProUGUI>().text = str;
        page.Add(tmpPage);
    }
}
