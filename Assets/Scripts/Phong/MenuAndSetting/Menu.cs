using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }
    
    public void ResumeGame()
    {
        // Resume game
        
        gameObject.SetActive(false);
    }

    public void SettingsGame()
    {
        gameObject.SetActive(false);
        GameEventPhong.OpenSettings();
    }

    public void BackHome()
    {
        gameObject.SetActive(false);
        GameEventPhong.OpenMap();
    }
}
