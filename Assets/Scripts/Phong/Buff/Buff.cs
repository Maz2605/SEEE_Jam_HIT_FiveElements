using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    [SerializeField] private bool _isUseBuff = false;
    [SerializeField] private Image _image;

    private Color _originalColor;

    public bool IsUseBuff
    {
        get => _isUseBuff;
        set => _isUseBuff = value;
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
        if (_image != null)
            _originalColor = _image.color ;

        if (IsUseBuff)
        {
            _image.color = _originalColor; 
        }
        else
        {
            // màu xám đen (50,50,50)
            _image.color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f); 
        }
    }

    public void UnLockBuff()
    {
        _isUseBuff = true;
        if (_image != null)
        {
            _image.color = _originalColor; 
        }
    }

    public void LockBuff()
    {
        _isUseBuff = false;
        if (_image != null)
        {
            // đổi sang màu xám đen
            _image.color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f); 
        }
    }
}