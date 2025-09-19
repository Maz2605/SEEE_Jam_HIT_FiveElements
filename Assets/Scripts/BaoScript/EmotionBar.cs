using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EmotionBar : Singleton<EmotionBar>
{
    [Header("Emotion Settings")]
    [SerializeField] private Slider _emotionSlider;
    [SerializeField] private float _maxEmotion = 100f;
    [SerializeField] private float _passiveGainPerSecond = 2f;

    [Header("Color Settings")]
    [SerializeField] private Image _fillImage;
    [SerializeField] private Gradient _colorGradient;

    [Header("Icon Settings")]
    [SerializeField] private Image _emotionIcon;
    [SerializeField] private Sprite _sadIcon;
    [SerializeField] private Sprite _normalIcon;
    [SerializeField] private Sprite _happyIcon;
    [SerializeField] private Sprite _superHappyIcon;

    [Header("Icon Colors")]
    [SerializeField] private Color _sadColor = Color.red;
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _happyColor = Color.green;
    [SerializeField] private Color _superHappyColor = Color.yellow;

    [Header("Tween Settings")]
    [SerializeField] private float _tweenDuration = 0.4f;
    [SerializeField] private float _iconColorTween = 0.5f;


    [Header("Player Level")]
    [SerializeField] private PlayerLevelSystem _playerLevelSystem;

    private float _currentEmotion;

    private bool _isDraining = false;

    #region GETTER SETTER
    public float MaxEmotion => _maxEmotion; 
    public float PassiveGainPerSecond => _passiveGainPerSecond;
    public float TweenDuration => _tweenDuration;    
    public float IconColorTween => _iconColorTween;  
    public float CurrentEmotion => _currentEmotion;

    public float EmotionPercent => _maxEmotion > 0 ? _currentEmotion / _maxEmotion : 0f;
    public bool IsDraining => _isDraining;

    public void SetMaxEmotion(float value)
    {
        _maxEmotion += value;
        _maxEmotion = Mathf.Max(1f, _maxEmotion); // tránh 0 hoặc âm
    }

    public void SetPassiveGainPerSecond(float value)
    {
        _passiveGainPerSecond += value;
        _passiveGainPerSecond = Mathf.Max(0f, _passiveGainPerSecond);
    }

    public void SetTweenDuration(float value)
    {
        _tweenDuration += value;
        _tweenDuration = Mathf.Max(0f, _tweenDuration);
    }

    public void SetIconColorTween(float value)
    {
        _iconColorTween += value;
        _iconColorTween = Mathf.Max(0f, _iconColorTween);
    }

    public void SetCurrentEmotion(float value)
    {
        _currentEmotion += value;
        _currentEmotion = Mathf.Clamp(_currentEmotion, 0f, _maxEmotion);
        UpdateUITween();
    }

    public void SetEmotionPercent(float percent)
    {
        percent = Mathf.Clamp01(percent); // 0..1
        _currentEmotion = percent * _maxEmotion;
        UpdateUITween();
    }

    public void SetIsDraining(bool value)
    {
        _isDraining = value;
    }
    #endregion

    private void Start()
    {
        _currentEmotion = 0f;
        UpdateUIImmediate();
    }

    private void Update()
    {
        AddEmotion(_passiveGainPerSecond * Time.deltaTime);
    }

   public void AddEmotion(float amount)
   {
        if (_isDraining) return; 

        _currentEmotion = Mathf.Clamp(_currentEmotion + amount, 0, _maxEmotion);
        UpdateUITween();

        if (_currentEmotion >= _maxEmotion)
        {
            OnEmotionFull();
        }
   }

    private void UpdateUITween()
    {
        float t = _currentEmotion / _maxEmotion;

        UpdateSliderValue(t);

        if (_isDraining && _currentEmotion > 0f)
        {
            _fillImage.DOColor(_superHappyColor, _tweenDuration).SetEase(Ease.InOutSine);
            if (_emotionIcon != null)
                _emotionIcon.sprite = _superHappyIcon;

            _playerLevelSystem.LevelUp(3);
        }
        else
        {
            UpdateFillColor(t);
            UpdateEmotionIcon(t);
        }
    }

    private void UpdateSliderValue(float t)
    {
        _emotionSlider.DOValue(t, _tweenDuration).SetEase(Ease.OutCubic);
    }

    private void UpdateFillColor(float t)
    {
        if (_fillImage == null) return;

        Color targetColor = Color.white;

        if (t <= 0.3f)
            targetColor = _sadColor;         
        else if (t <= 0.6f)
            targetColor = _normalColor;      
        else if (t < 1f)
            targetColor = _happyColor;       
        else
            targetColor = _superHappyColor;  

        _fillImage.DOColor(targetColor, _tweenDuration).SetEase(Ease.InOutSine);

        UpdateEmotionLevel(t);
    }

    private void UpdateEmotionIcon(float t)
    {
        if (_emotionIcon == null) return;

        if (t <= 0.3f)
        {
            _emotionIcon.sprite = _sadIcon;
            GameEventPhong.LookSkill2();
        }
           
        else if (t <= 0.6f)
        {
            _emotionIcon.sprite = _normalIcon;
            GameEventPhong.UnLookSkill2();
        }
           
        else if (t < 1f)
        {
            _emotionIcon.sprite = _happyIcon;
            GameEventPhong.UnLookSkill3();
        }
           
        else
        {
            _emotionIcon.sprite = _superHappyIcon;
            GameEventPhong.UnLookUltimate();
        }
          
    }

    private void UpdateUIImmediate()
    {
        float t = _currentEmotion / _maxEmotion;
        _emotionSlider.value = t;

        if (_fillImage != null)
            _fillImage.color = _colorGradient.Evaluate(t);
    }

    private void OnEmotionFull()
    {
        if (_isDraining) return;

        _isDraining = true;

        float endEmotion = 0f;
        float duration = 3f;

        DOTween.To(() => _currentEmotion,
                   x => { _currentEmotion = x; UpdateUITween(); },
                   endEmotion,
                   duration)
               .SetEase(Ease.Linear)
               .OnComplete(() => { _isDraining = false; });
    }

    private void UpdateEmotionLevel(float t)
    {
        int newLevel = 0;

        if (t <= 0.3f)
            newLevel = 0;
        else if (t <= 0.6f)
            newLevel = 1;
        else if (t < 1f)
            newLevel = 2;
        else
            newLevel = 3;

        _playerLevelSystem.LevelUp(newLevel);
    }


}
