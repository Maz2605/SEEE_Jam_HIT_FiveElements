using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{
    [SerializeField] private Button btn;

    private void Start()
    {
        btn.onClick.AddListener(() =>
        {
            btn.gameObject.SetActive(false);
            Debug.Log("Start Level 1");
            XuanEventManager.OnStartLevel(1);
        });
    }
}
