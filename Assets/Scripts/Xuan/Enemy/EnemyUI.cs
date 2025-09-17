using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private GameObject _imotionBar;
    [SerializeField] private Image _imotionIcon;
    [SerializeField] private Image _imotion;
    [SerializeField] private Gradient _colorGradient; // chọn màu trong Inspector

    [Header("Imotion")]
    [SerializeField] private Sprite _imo1;
    [SerializeField] private Sprite _imo2;
    [SerializeField] private Sprite _imo3;
    [SerializeField] private Sprite _imo4;

    private float _maxEnergy;
    private float _targetFill;
    private float _currentFill;
    private float _lerpSpeed = 5f;

    public void SetImotionBar(float maxImotion)
    {
        _imotionBar.SetActive(false);
        _maxEnergy = maxImotion;
        _currentFill = 0f;
        _targetFill = 0f;
        _imotion.fillAmount = 0f;
        _imotion.color = _colorGradient.Evaluate(0f);
    }

    public void UpdateImotionBar(float currentEnergy)
    {
        if(!_imotionBar.activeSelf)
        {
            _imotionBar.SetActive(true);
        }
        _targetFill = Mathf.Clamp01(currentEnergy / _maxEnergy);
    }
    public void ChangeIcon()
    {
        if(_targetFill < 0.33f)
        {
            _imotionIcon.sprite = _imo1;
        }
        else if(_targetFill < 0.66f)
        {
            _imotionIcon.sprite = _imo2;
        }
        else if(_targetFill < 0.99f)
        {
            _imotionIcon.sprite = _imo3;
        }
        else
        {
            _imotionIcon.sprite = _imo4;
        }
    }

    private void Update()
    {
        // Fill mượt
        _currentFill = Mathf.Lerp(_currentFill, _targetFill, Time.deltaTime * _lerpSpeed);
        _imotion.fillAmount = _currentFill;
        ChangeIcon();
        // Đổi màu theo gradient
        _imotion.color = _colorGradient.Evaluate(_currentFill);
    }

}
