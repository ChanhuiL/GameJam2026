using UnityEngine;

public class PageScript : MonoBehaviour
{
    public GameObject[] page;
    private int currentPageIndex = 0;

    public void OnPreviousPage()
    {
        if (currentPageIndex == 0) return;
        page[currentPageIndex].SetActive(false);        
        currentPageIndex--;
        page[currentPageIndex].SetActive(true);
    }
    
    public void OnNextPage()
    {
        if (currentPageIndex == page.Length-1) return;
        page[currentPageIndex].SetActive(false);     
        currentPageIndex++;
        page[currentPageIndex].SetActive(true);
        
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        page[currentPageIndex].SetActive(true);
    }

    
}
