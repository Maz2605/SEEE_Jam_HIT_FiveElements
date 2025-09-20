using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnPause;

    private void Start()
    {
        btn.onClick.AddListener(() =>
        {
            btn.gameObject.SetActive(false);
            Debug.Log("Start Level 1");
            XuanEventManager.OnStartLevel(1);
        });
        btnContinue.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            btnContinue.gameObject.SetActive(false);
        });
        btnPause.onClick.AddListener(() =>
        {
            Time.timeScale = 0;
            btnContinue.gameObject.SetActive(true);
        });
    }

}
