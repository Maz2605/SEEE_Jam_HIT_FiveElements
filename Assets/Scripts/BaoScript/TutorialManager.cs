using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class TutorialStep
{
    [TextArea]
    public string instructionText;     // Nội dung hướng dẫn
    public Transform spawnPoint;       // Điểm spawn highlight (do bạn đặt sẵn trong scene)
    public Vector3 rotationEuler;
    public GameObject targetToHide;
    public bool hideOnNext;
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private GameObject highlightPrefab;

    [Header("Steps")]
    [SerializeField] private List<TutorialStep> steps = new List<TutorialStep>();
    [SerializeField] private UnityEngine.UI.Button nextButton;

    private int currentStep = -1;
    private GameObject currentHighlight;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        nextButton.onClick.AddListener(NextStep);
        StartTutorial();
    }

    public void StartTutorial()
    {
        currentStep = -1;
        tutorialPanel.SetActive(true);
        nextButton.gameObject.SetActive(true);
        NextStep();
    }

    public void NextStep()
    {
        if (currentStep >= 0 && currentStep < steps.Count)
        {
            var prevStep = steps[currentStep];
            if (prevStep.hideOnNext && prevStep.targetToHide != null)
            {
                prevStep.targetToHide.SetActive(false);
            }
        }

        currentStep++;

        if (currentStep >= steps.Count)
        {
            EndTutorial();
            return;
        }

        var step = steps[currentStep];
        instructionText.text = step.instructionText;

        if (currentHighlight != null) Destroy(currentHighlight);

        if (step.spawnPoint != null)
        {
            // Tạo highlight tại spawnPoint + xoay theo rotationEuler
            currentHighlight = Instantiate(
                highlightPrefab,
                step.spawnPoint.position,
                Quaternion.Euler(step.rotationEuler),
                tutorialPanel.transform
            );
            currentHighlight.transform.SetAsLastSibling();
        }


        Time.timeScale = 0f;
    }


    private void EndTutorial()
    {
        tutorialPanel.SetActive(false);
        instructionText.gameObject.SetActive(false);
        if (currentHighlight != null) Destroy(currentHighlight);

        nextButton.gameObject.SetActive(false);

        Time.timeScale = 1f;
        Debug.Log("Tutorial finished!");
    }
}
