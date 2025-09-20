using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class TutorialStep
{
    public string instructionText;
    public GameObject highlightTarget;
    public bool blockInput;
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

    private int currentStep = -1;
    private GameObject currentHighlight;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartTutorial();
    }

    public void StartTutorial()
    {
        currentStep = -1;
        tutorialPanel.SetActive(true);
        NextStep();
    }

    public void NextStep()
    {
        currentStep++;
        if (currentStep >= steps.Count)
        {
            EndTutorial();
            return;
        }

        var step = steps[currentStep];
        instructionText.text = step.instructionText;

        if (currentHighlight != null) Destroy(currentHighlight);
        if (step.highlightTarget != null)
        {
            currentHighlight = Instantiate(highlightPrefab, step.highlightTarget.transform.position, Quaternion.identity, tutorialPanel.transform);
            currentHighlight.transform.SetAsLastSibling(); // đảm bảo ở trên cùng
        }
    }

    private void EndTutorial()
    {
        tutorialPanel.SetActive(false);
        if (currentHighlight != null) Destroy(currentHighlight);
        Debug.Log("Tutorial finished!");
    }
}
