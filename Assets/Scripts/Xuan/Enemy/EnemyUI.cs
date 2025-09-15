using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private GameObject _imotionBar;
    [SerializeField] private Image _imotion;
    [SerializeField] private Gradient _colorGradient; // chọn màu trong Inspector

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

    private void Update()
    {
        // Fill mượt
        _currentFill = Mathf.Lerp(_currentFill, _targetFill, Time.deltaTime * _lerpSpeed);
        _imotion.fillAmount = _currentFill;

        // Đổi màu theo gradient
        _imotion.color = _colorGradient.Evaluate(_currentFill);
    }

}
