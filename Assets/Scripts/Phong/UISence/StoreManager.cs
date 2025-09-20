using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // cần DOTween

public class StoreManager : MonoBehaviour
{
    public static Action OnStoreClosed;

    public GameObject panelRoot;
    public Image backGround;
    public RectTransform start;
    public RectTransform end;
   
    public List<GameObject> storePrefabs = new List<GameObject>();

    private void OnEnable()
    {
        GameEventPhong.AppearAward += AppearStore;
        GameEventPhong.DisAppearAward += DisappearStore; // thêm event mới
    }

    private void OnDisable()
    {
        GameEventPhong.AppearAward -= AppearStore;
        GameEventPhong.DisAppearAward -= DisappearStore;
    }

    private void Start()
    {
        //AppearStore();
    }

    private void MovebackGround()
    {
        gameObject.SetActive(true);
        backGround.rectTransform.position = start.position;
        backGround.rectTransform.DOMove(end.position, 1.5f).SetEase(Ease.InOutQuad);
    }

    private void MovebackGroundReverse()
    {
        backGround.rectTransform.DOMove(start.position, 1.5f).SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                foreach (var part in storePrefabs)
                {
                    panelRoot.SetActive(false);
                    part.SetActive(false);
                }
            });
    }

    private void AppearStore()
    {
        panelRoot.SetActive(true);
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

            part.transform.localScale = Vector3.zero;
            CanvasGroup cg = part.GetComponent<CanvasGroup>();
            if (cg == null) cg = part.AddComponent<CanvasGroup>();
            cg.alpha = 0;

            seq.Append(part.transform.DOScale(0.8f, 0.5f).SetEase(Ease.OutBack));
            seq.Join(cg.DOFade(1f, 0.5f));

            seq.AppendInterval(0.2f);
        }
        
    }

    private void ShowButtonWithFade(Button btn)
    {
        btn.gameObject.SetActive(true);

        CanvasGroup cg = btn.GetComponent<CanvasGroup>();
        if (cg == null) cg = btn.gameObject.AddComponent<CanvasGroup>();

        cg.alpha = 0;
        cg.DOFade(1f, 0.5f);
    }

    // -----------------------------
    // Hàm Disappear
    // -----------------------------
    private void DisappearStore()
    {
        Sequence seq = DOTween.Sequence();

        // Ẩn các item trước
        for (int i = storePrefabs.Count - 1; i >= 0; i--) // ẩn ngược lại
        {
            GameObject part = storePrefabs[i];
            if (part.activeSelf)
            {
                CanvasGroup cg = part.GetComponent<CanvasGroup>();
                if (cg == null) cg = part.AddComponent<CanvasGroup>();

                seq.Append(part.transform.DOScale(0f, 0.4f).SetEase(Ease.InBack));
                seq.Join(cg.DOFade(0f, 0.4f));
                seq.AppendInterval(0.1f);

                seq.AppendCallback(() => part.SetActive(false));
            }
            
        }

        // Ẩn button

        // Sau cùng cho background trượt về
        seq.AppendCallback(() =>
        {
            MovebackGroundReverse();
            OnStoreClosed?.Invoke();
        });
    }

    private void HideButtonWithFade(Button btn)
    {
        if (btn.gameObject.activeSelf)
        {
            CanvasGroup cg = btn.GetComponent<CanvasGroup>();
            if (cg == null) cg = btn.gameObject.AddComponent<CanvasGroup>();

            cg.DOFade(0f, 0.5f).OnComplete(() =>
            {
                btn.gameObject.SetActive(false);
            });
        }
    }
}
