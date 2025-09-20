using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWinLose : Singleton<UIWinLose>
{
    [SerializeField] private GameObject _backGround;
    [SerializeField] private GameObject _win;
    [SerializeField] private GameObject _lose;

    public void ShowWin()
    {
        _backGround.SetActive(true);
        _win.SetActive(true);
    }
    public void BackWin()
    {
        _backGround.SetActive(false);
        _win.SetActive(false);
    }

    public void ShowLose()
    {
        _backGround.SetActive(true);
        _lose.SetActive(true);
    }
    public void BackLose()
    {
        _backGround.SetActive(false);
        _lose.SetActive(false);
    }
}
