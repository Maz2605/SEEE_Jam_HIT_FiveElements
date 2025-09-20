using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    [SerializeField] private bool _isUseBuff = false;

    private Color _originalColor;

    public bool IsUseBuff
    {
        get => _isUseBuff;
        set => _isUseBuff = value;
    }


}