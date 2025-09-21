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
    public GameObject tutorialUI;
    private bool _isSkip;

    public void OpenMenu()
    {
        AudioManager.Instance.PlaySoundClickButton();
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
        AudioManager.Instance.PlaySoundClickButton();
    }

    public void CloseSetting()
    {
        settingUI.SetActive(false);
        AudioManager.Instance.PlaySoundClickButton();
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
        AudioManager.Instance.PlaySoundClickButton();
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
        AudioManager.Instance.PlaySoundClickButton();
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
        AudioManager.Instance.PlaySoundClickButton();
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
        AudioManager.Instance.PlaySoundClickButton();
    }
    
    public void OpenSence()
    {
        AudioManager.Instance.PlaySoundClickButton();
        if(_isSkip)
        {
            introUI.SetActive(false);
            OpenMap();
            return;
        }
        // 
        _isSkip = true;
        menuUI.SetActive(false);
        settingUI.SetActive(false);
        mapUI.SetActive(false);
        openSenceUI.SetActive(true);
        shopUI.SetActive(false);
        gamePlayUI.SetActive(false);
        introUI.SetActive(false);
    }

    public void OpenTutorial()
    {
        /*menuUI.SetActive(false);
        settingUI.SetActive(false);
        mapUI.SetActive(false);
        openSenceUI.SetActive(false);
        shopUI.SetActive(false);
        gamePlayUI.SetActive(false);
        introUI.SetActive(false);
        
        tutorialUI.SetActive(true);
        
        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.StartTutorial();
        }*/
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySoundClickButton();
        Application.Quit();
    }
    
}
