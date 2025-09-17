using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // cần DOTween

public class StoreManager : MonoBehaviour
{
    public Image backGround;
    public RectTransform start;
    public RectTransform end;
    public Button btn1;
    public Button btn2;
    public List<GameObject> storePrefabs = new List<GameObject>();

    private void OnEnable()
    {
        GameEventPhong.AppearStore += AppearStore;
    }

    private void OnDisable()
    {
        GameEventPhong.AppearStore -= AppearStore;
    }

    private void Start()
    {
        AppearStore();
    }

    private void MovebackGround()
    {
        backGround.rectTransform.position = start.position;
        backGround.rectTransform.DOMove(end.position, 1.5f).SetEase(Ease.InOutQuad);
    }

    private void AppearStore()
    {
        MovebackGround();

        DOVirtual.DelayedCall(1.5f, () =>
        {
            AppearItemBoard();
        });
    }

    private void AppearItemBoard()
    {
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < storePrefabs.Count; i++)
        {
            GameObject part = storePrefabs[i];
            part.SetActive(true);

            // reset trạng thái trước khi tween
            part.transform.localScale = Vector3.zero;
            CanvasGroup cg = part.GetComponent<CanvasGroup>();
            if (cg == null) cg = part.AddComponent<CanvasGroup>();
            cg.alpha = 0;

            // tạo tween cho từng item
            seq.Append(part.transform.DOScale(0.8f, 0.5f).SetEase(Ease.OutBack));
            seq.Join(cg.DOFade(1f, 0.5f));

            // delay 0.2s giữa các item
            seq.AppendInterval(0.2f);
        }

        seq.OnComplete(() =>
        {
            ShowButtonWithFade(btn1);
            ShowButtonWithFade(btn2);
        });
    }

    private void ShowButtonWithFade(Button btn)
    {
        btn.gameObject.SetActive(true);

        CanvasGroup cg = btn.GetComponent<CanvasGroup>();
        if (cg == null) cg = btn.gameObject.AddComponent<CanvasGroup>();

        cg.alpha = 0; // reset alpha
        cg.DOFade(1f, 0.5f); // fade-in 0.5s
    }
    
}