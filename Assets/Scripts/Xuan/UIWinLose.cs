using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIWinLose : Singleton<UIWinLose>
{
    [SerializeField] private GameObject _backGround;
    [SerializeField] private GameObject _win;
    [SerializeField] private GameObject _lose;

    [Header("Button")]
    [SerializeField] private Button btnBackWin;
    [SerializeField] private Button btnBackLose;
    [SerializeField] private Button btnNext;
    [SerializeField] private Button btnRetryWin;
    [SerializeField] private Button btnRetryLose;

    private void Start()
    {
        btnBackLose.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            BackLose();
            XuanEventManager.OnBackLevel();
            UiManager.Instance.OpenMap();
        });
        btnBackWin.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            BackWin();
            XuanEventManager.OnBackLevel();
            UiManager.Instance.OpenMap();
        });
        btnNext.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            BackWin();
            XuanEventManager.OnNextLevel();
        });
        btnRetryLose.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            BackLose();
            XuanEventManager.OnRetryLevel();
        });
        btnRetryWin.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            BackWin();
            XuanEventManager.OnRetryLevel();
        });

    }
    public void ShowWin()
    {
        AudioManager.Instance.PlayWinGame();
        DOVirtual.DelayedCall(1f, () =>
        {
            Time.timeScale = 0f;
            _backGround.SetActive(true);
            _win.SetActive(true);
        });
       
    }
    public void BackWin()
    {
        _backGround.SetActive(false);
        _win.SetActive(false);
    }

    public void ShowLose()
    {
        AudioManager.Instance.PlayLoseGame();
        DOVirtual.DelayedCall(1f, () =>
        {
            Time.timeScale = 0f;
            _backGround.SetActive(true);
            _lose.SetActive(true);
        });
        
    }
    public void BackLose()
    {
        _backGround.SetActive(false);
        _lose.SetActive(false);
    }
}
