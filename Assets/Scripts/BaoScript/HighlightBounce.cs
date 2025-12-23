using UnityEngine;
using DG.Tweening;

public class HighlightBounce : MonoBehaviour
{
    [SerializeField] private float scaleAmount = 1.2f;  // mức phóng to
    [SerializeField] private float duration = 0.5f;     // thời gian 1 lần

    private void OnEnable()
    {
        // Reset scale
        transform.localScale = Vector3.one;

        // Nhún nhún vô hạn
        transform.DOScale(scaleAmount, duration)
                 .SetEase(Ease.InOutSine)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetUpdate(true); // chạy kể cả khi Time.timeScale = 0
    }

    private void OnDisable()
    {
        // Khi disable thì clear tween để không bị leak
        transform.DOKill();
    }
}
