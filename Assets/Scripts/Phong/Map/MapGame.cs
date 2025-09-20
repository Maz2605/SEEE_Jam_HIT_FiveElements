using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGame : MonoBehaviour
{
    
    
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
