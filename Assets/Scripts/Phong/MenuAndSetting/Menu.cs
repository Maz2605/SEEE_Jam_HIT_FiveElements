using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    private void OnEnable()
    {
        GameEventPhong.OpenMenu += OpenMenu;
        GameEventPhong.CloseMenu += CloseMenu;
    }

    private void OnDisable()
    {
        GameEventPhong.OpenMenu -= OpenMenu;
        GameEventPhong.CloseMenu -= CloseMenu;
    }

    public void OpenMenu()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    private void CloseMenu()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    
    public void ResumeGame()
    {
        // Resume game
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void SettingsGame()
    {
        gameObject.SetActive(false);
        GameEventPhong.OpenSettings();
    }

    public void BackHome()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        XuanEventManager.OnBackLevel();
        GameEventPhong.OpenMap();
    }
}
