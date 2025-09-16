using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerSkill : MonoBehaviour
{
   
  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("-50 mau ");
        }
    }
}
