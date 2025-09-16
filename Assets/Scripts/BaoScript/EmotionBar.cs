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
    [SerializeField] private Color sadColor = Color.red;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color happyColor = Color.green;
    [SerializeField] private Color superHappyColor = Color.yellow;

    [Header("Tween Settings")]
    [SerializeField] private float tweenDuration = 0.4f;
    [SerializeField] private float iconColorTween = 0.5f;


    [Header("Player Level")]
    [SerializeField] private PlayerLevelSystem playerLevelSystem;

    private float currentEmotion;
    private string currentState = ""; 

    private void Start()
    {
        currentEmotion = 0f;
        UpdateUIImmediate();
    }

    private void Update()
    {
        AddEmotion(_passiveGainPerSecond * Time.deltaTime);
    }

    public void AddEmotion(float amount)
    {
        currentEmotion = Mathf.Clamp(currentEmotion + amount, 0, _maxEmotion);
        UpdateUITween();

        if (currentEmotion >= _maxEmotion)
        {
            OnEmotionFull();
        }
    }

    private void UpdateUITween()
    {
        float t = currentEmotion / _maxEmotion;

        UpdateSliderValue(t);
        UpdateFillColor(t);
        UpdateEmotionIcon(t);
        //MoveEmotionIcon(t);
    }

    private void UpdateSliderValue(float t)
    {
        _emotionSlider.DOValue(t, tweenDuration).SetEase(Ease.OutCubic);
    }

    private void UpdateFillColor(float t)
    {
        if (_fillImage == null) return;

        Color targetColor = Color.white;

        if (t <= 0.3f)
            targetColor = sadColor;         
        else if (t <= 0.6f)
            targetColor = normalColor;      
        else if (t < 1f)
            targetColor = happyColor;       
        else
            targetColor = superHappyColor;  

        _fillImage.DOColor(targetColor, tweenDuration).SetEase(Ease.InOutSine);

        UpdateEmotionLevel(t);
    }

    private void UpdateEmotionIcon(float t)
    {
        if (_emotionIcon == null) return;

        if (t <= 0.3f)
            _emotionIcon.sprite = _sadIcon;
        else if (t <= 0.6f)
            _emotionIcon.sprite = _normalIcon;
        else if (t < 1f)
            _emotionIcon.sprite = _happyIcon;
        else
            _emotionIcon.sprite = _superHappyIcon;
    }

    private void UpdateUIImmediate()
    {
        float t = currentEmotion / _maxEmotion;
        _emotionSlider.value = t;

        if (_fillImage != null)
            _fillImage.color = _colorGradient.Evaluate(t);
    }

    private void OnEmotionFull()
    {
        Debug.Log(" Emotion Bar Full! Ultimate skill ready!");
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

        playerLevelSystem.LevelUp(newLevel);
    }
}
