using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float _speed = 2f;

    public void StartCoin(Vector3 pos)
    {
        DOVirtual.DelayedCall(0.2f, () =>
        {
            transform.DOMove(pos, _speed).SetEase(Ease.Linear).OnComplete(() =>
            {
                PoolingManager.Despawn(gameObject);
            });
        });
    }
}
