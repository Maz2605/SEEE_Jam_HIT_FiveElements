using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public List<PageManager> pages = new List<PageManager>();
    public Button clickToContinueButton; // gắn button full screen trong Canvas

    private int currentPage = 0;

    private void Start()
    {
        // Ẩn hết các page
        foreach (var page in pages)
        {
            page.gameObject.SetActive(false);
        }

        // Ẩn nút bấm chờ (nút full screen)
        if (clickToContinueButton != null)
            clickToContinueButton.gameObject.SetActive(false);

        if (pages.Count > 0)
        {
            ShowNextPage();
        }
    }

    private void ShowNextPage()
    {
        if (currentPage < pages.Count)
        {
            if (currentPage < pages.Count - 1)
            {
                // Trang thường: Fade in rồi tự tắt
                pages[currentPage].ShowPage(() =>
                {
                    currentPage++;
                    ShowNextPage();
                }, autoHide: true);
            }
            else
            {
                // Trang cuối: Fade in rồi chờ click
                pages[currentPage].ShowPage(() =>
                {
                    if (clickToContinueButton != null)
                    {
                        clickToContinueButton.gameObject.SetActive(true);
                        clickToContinueButton.onClick.RemoveAllListeners();
                        clickToContinueButton.onClick.AddListener(OnLastPageClicked);
                    }
                }, autoHide: false);
            }
        }
    }

    private void OnLastPageClicked()
    {
        clickToContinueButton.gameObject.SetActive(false); // tắt nút click để không spam

        PageManager lastPage = pages[currentPage];
        lastPage.HidePage(() =>
        {
            Debug.Log("Chuyển sang cái khác (ví dụ scene mới)");
            // Ví dụ load scene mới:
            // SceneManager.LoadScene("NextScene");
        });
    }
}
