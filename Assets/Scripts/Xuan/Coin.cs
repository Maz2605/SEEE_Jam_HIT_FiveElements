using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float _speed = 20f;
    private void Start()
    {
        
    }

    public void StartCoin(Vector3 pos)
    {
        transform.DOMove(pos, _speed).SetEase(Ease.Linear).OnComplete(() => 
        {
            //PoolingManager.Despawn(gameObject);
        });
    }
}
