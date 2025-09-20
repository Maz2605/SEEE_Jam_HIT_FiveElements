using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGame : MonoBehaviour
{
    [SerializeField] private GameObject _shop;

    public void OpenShop()
    {
        _shop.SetActive(true);
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
    }

    public void OutMapGame()
    {
        //Out Map
        
        gameObject.SetActive(false);
    }
}
