using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MapGame : MonoBehaviour
{
    private bool _isTutoria;   
    public void OpenShop()
    {
        
        gameObject.SetActive(false);
    }

    public void OpenSetting()
    {
        // Open setting
        
        gameObject.SetActive(false);
    }

    public void GoToLevel(int level)
    {
        //Tutoria
        if(!_isTutoria)
        {
            TutorialManager.Instance.StartTutorial();
            _isTutoria = true;
        }
        //Go to level
        XuanEventManager.OnStartLevel(level);
        UiManager.Instance.OpenGamePlay();
    }

    public void OutMapGame()
    {
        //Out Map
        
        gameObject.SetActive(false);
    }
}
