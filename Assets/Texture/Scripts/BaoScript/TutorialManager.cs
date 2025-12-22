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

public class TutorialManager : Singleton<TutorialManager>
{
    [Header("UI")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private GameObject highlightPrefab;
    [SerializeField] private GameObject tutorialImage;

    [Header("Steps")]
    [SerializeField] private List<TutorialStep> steps = new List<TutorialStep>();
    [SerializeField] private UnityEngine.UI.Button nextButton;

    private int currentStep = -1;
    
    private void Start()
    {
        nextButton.onClick.AddListener(NextStep);
        //StartTutorial();
    }

    public void StartTutorial()
    {
        currentStep = -1;
        tutorialPanel.SetActive(true);
        nextButton.gameObject.SetActive(true);
        instructionText.gameObject.SetActive(true);
        NextStep();
    }

    public void NextStep()
    {
        if (currentStep >= 0 && currentStep < steps.Count)
        {
            TutorialStep prevStep = steps[currentStep];
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

        if (currentStep == 0)
        {
            tutorialImage.SetActive(true);
        }
        else
        {
            tutorialImage.SetActive(false);
        }

        TutorialStep step = steps[currentStep];
        instructionText.text = step.instructionText;
        
        highlightPrefab.transform.position = step.spawnPoint.position;
        highlightPrefab.transform.rotation = Quaternion.Euler(step.rotationEuler);
        /*if (step.spawnPoint != null)
        {
            // Tạo highlight tại spawnPoint + xoay theo rotationEuler
            currentHighlight = Instantiate(
                highlightPrefab,
                step.spawnPoint.position,
                Quaternion.Euler(step.rotationEuler)
                
            );
            currentHighlight.transform.SetAsLastSibling();
        }*/


        Time.timeScale = 0f;
    }


    private void EndTutorial()
    {
        tutorialPanel.SetActive(false);
        instructionText.gameObject.SetActive(false);
        /*if (currentHighlight != null) Destroy(currentHighlight);*/
        highlightPrefab.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        Time.timeScale = 1f;
        Debug.Log("Tutorial finished!");
    }
}
