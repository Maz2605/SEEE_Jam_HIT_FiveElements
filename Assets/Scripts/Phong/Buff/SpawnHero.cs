using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpawnHero : Buff
{
    public List<GameObject> heroPrefabs = new List<GameObject>();
    public GameObject exChangePre;
    public Vector3 heroPos1 = new Vector3(-3, -2, 0);
    public Vector3 heroPos2 = new Vector3(0, 3.54f, 0);

    private GameObject currentHero;
    private GameObject currentExchange;
    private void OnEnable()
    {
        GameEventPhong.SpawnHero += SpawnHero1;
        GameManager.OnWaveCompleted += DespawnHero;
    }

    private void OnDisable()
    {
        GameEventPhong.SpawnHero -= SpawnHero1;
        GameManager.OnWaveCompleted -= DespawnHero;
    }

    private void SpawnHero1()
    {
        if (currentHero != null)
        {
            Destroy(currentHero);
            currentHero = null;
        }
        if (currentExchange != null)
        {
            Destroy(currentExchange);
            currentExchange = null;
        }

        int chosenHero = UnityEngine.Random.Range(0, heroPrefabs.Count);
            if (chosenHero == 0)
            {
                Instantiate(exChangePre, heroPos1, Quaternion.identity);
                DOVirtual.DelayedCall(0.3f,
                    () => { Instantiate(heroPrefabs[chosenHero], heroPos1, Quaternion.identity); });
            }
            else
            {
                Instantiate(heroPrefabs[chosenHero], heroPos2, Quaternion.identity);
            }
      
    }

    private void DespawnHero()
    {
        if (currentHero != null)
        {
            Destroy(currentHero);
            currentHero = null;
        }
        if (currentExchange != null)
        {
            Destroy(currentExchange);
            currentExchange = null;
        }
    }
}
