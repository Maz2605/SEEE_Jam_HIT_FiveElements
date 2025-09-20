using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    public GameObject menuUI;
    public GameObject settingUI;
    public GameObject openSenceUI;
    public GameObject mapUI;
    public GameObject shopUI;
    public GameObject gamePlayUI;
    public GameObject introUI;
   

    public void OpenMenu()
    {
        menuUI.SetActive(true);
        settingUI.SetActive(false);
        mapUI.SetActive(false);
        openSenceUI.SetActive(false);
        shopUI.SetActive(false);
        gamePlayUI.SetActive(false);
        introUI.SetActive(false);
    }

    public void OpenSetting()
    {
        settingUI.SetActive(true);
    }

    public void CloseSetting()
    {
        settingUI.SetActive(false);
    }

    public void OpenShop()
    {
        menuUI.SetActive(false);
        settingUI.SetActive(false);
        openSenceUI.SetActive(false);
        mapUI.SetActive(false);
        shopUI.SetActive(true);
        gamePlayUI.SetActive(false);
        introUI.SetActive(false);
    }

    public void OpenGamePlay()
    {
        AudioManager.Instance.PlayMusicInGame();
        menuUI.SetActive(false);
        settingUI.SetActive(false);
        openSenceUI.SetActive(false);
        mapUI.SetActive(false);
        shopUI.SetActive(false);
        gamePlayUI.SetActive(true);
        introUI.SetActive(false);
    }

    public void OpenMap()
    {
        AudioManager.Instance.PlayMusicSelectLevel();
        menuUI.SetActive(false);
        settingUI.SetActive(false);
        openSenceUI.SetActive(false);
        mapUI.SetActive(true);
        shopUI.SetActive(false);
        gamePlayUI.SetActive(false);
        introUI.SetActive(false);
    }

    

    public void OpenIntro()
    {
        menuUI.SetActive(false);
        settingUI.SetActive(false);
        mapUI.SetActive(false);
        openSenceUI.SetActive(false);
        shopUI.SetActive(false);
        gamePlayUI.SetActive(false);
        introUI.SetActive(true);
    }
    
    public void OpenSence()
    {
        // 
        menuUI.SetActive(false);
        settingUI.SetActive(false);
        mapUI.SetActive(false);
        openSenceUI.SetActive(true);
        shopUI.SetActive(false);
        gamePlayUI.SetActive(false);
        introUI.SetActive(false);
    }



    public void QuitGame()
    {
        Application.Quit();
    }
    
}
