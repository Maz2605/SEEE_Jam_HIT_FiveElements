using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListOfBuff : MonoBehaviour
{
    [SerializeField] private Image _currentImage;
    [SerializeField] private TextMeshProUGUI _currentName;
    [SerializeField] private TextMeshProUGUI _currentDescription;
    [SerializeField] private int _currentBuffChoice;
    
    [SerializeField] private List<BuffChoosing> _choosingObjs = new List<BuffChoosing>();

    private void Awake()
    {
        
    }

}
