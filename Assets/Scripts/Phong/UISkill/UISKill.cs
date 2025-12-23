using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkill : MonoBehaviour
{
    public Image loadingImage;
    public TextMeshProUGUI loadingText;
    

    private float duration;
    private float remaining;
    private System.Action onFinish;

    public void StartLoading(float time, System.Action onFinishCallback = null)
    {
       
        duration = time;
        remaining = time;
        onFinish = onFinishCallback;

        loadingImage.gameObject.SetActive(true);
        
        loadingImage.fillAmount = 1f;
        if (loadingText != null)
            loadingText.text = Mathf.CeilToInt(remaining).ToString();

        // gọi hàm UpdateLoading mỗi 0.1s
        InvokeRepeating(nameof(UpdateLoading), 0f, 0.1f);
    }

    private void UpdateLoading()
    {
        remaining -= 0.1f;

        float progress = Mathf.Clamp01(remaining / duration);
        loadingImage.fillAmount = progress;

        if (loadingText != null)
            loadingText.text = Mathf.CeilToInt(remaining).ToString();

        if (remaining <= 0f)
        {
            CancelInvoke(nameof(UpdateLoading));

            loadingImage.fillAmount = 0f;
            if (loadingText != null)
                loadingText.text = "";

            loadingImage.gameObject.SetActive(false);

            onFinish?.Invoke();
        }
    }
}