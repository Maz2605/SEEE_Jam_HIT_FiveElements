using UnityEngine;
using DG.Tweening;

public class ShakeCamera : Singleton<ShakeCamera>
{
    [SerializeField] private float duration = 0.5f;   // thời gian rung
    [SerializeField] private float strength = 0.5f;   // độ mạnh
    [SerializeField] private int vibrato = 10;        // số lần rung
    [SerializeField] private float randomness = 90f;  // độ ngẫu nhiên

    private Transform cam;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    public void Shake()
    {
        if (cam != null)
        {
            cam.DOShakePosition(
                duration,   // thời gian
                strength,   // độ mạnh
                vibrato,    // số lần rung
                randomness, // độ ngẫu nhiên
                false,      // không dùng snapping
                true        // rung tương đối (không thay đổi vị trí gốc)
            );
        }
    }
}