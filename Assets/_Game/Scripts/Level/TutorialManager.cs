using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private RectTransform arrowRect;
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Arrow Positions (anchored to Canvas)")]
    [SerializeField] private Vector2 step1ArrowPos = new Vector2(0, -750);    // Build button area
    [SerializeField] private Vector2 step2ArrowPos = new Vector2(0, 200);     // South entrance (center)
    [SerializeField] private Vector2 step3ArrowPos = new Vector2(0, 0);       // Mall floor center
    [SerializeField] private Vector2 step4ArrowPos = new Vector2(0, 800);     // HUD top area

    [Header("Arrow Rotations (degrees)")]
    [SerializeField] private float step1ArrowRot = 180f;  // pointing down toward button
    [SerializeField] private float step2ArrowRot = 90f;
    [SerializeField] private float step3ArrowRot = 180f;
    [SerializeField] private float step4ArrowRot = 180f;

    private readonly string[] messages = new string[]
    {
        "Tap here to build your first shop!",
        "Customers will enter through here",
        "Place your shop anywhere on the floor",
        "Earn money and upgrade your shops!"
    };

    private int currentStep = 0;
    private bool tutorialActive = false;
    private bool inputBlocked = false;

    private const string TUTORIAL_KEY = "save_onboarded";

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    void Start()
    {
        // TEMP: Reset for testing — remove before release
        PlayerPrefs.SetInt("save_tutorial_done", 0);
        PlayerPrefs.SetInt("save_onboarded", 1);  // manually set করো
        PlayerPrefs.Save();

        if (PlayerPrefs.GetInt(TUTORIAL_KEY, 0) == 1)
        {
            if (PlayerPrefs.GetInt("save_tutorial_done", 0) == 0)
            {
                StartTutorial();
            }
            else
            {
                tutorialPanel.SetActive(false);
            }
        }
        else
        {
            tutorialPanel.SetActive(false);
        }
    }

    public void StartTutorial()
    {
        currentStep = 0;
        tutorialActive = true;
        tutorialPanel.SetActive(true);
        ShowStep(currentStep);
    }

    void ShowStep(int step)
    {
        messageText.text = messages[step];

        Vector2 targetPos = step switch
        {
            0 => step1ArrowPos,
            1 => step2ArrowPos,
            2 => step3ArrowPos,
            3 => step4ArrowPos,
            _ => Vector2.zero
        };

        float targetRot = step switch
        {
            0 => step1ArrowRot,
            1 => step2ArrowRot,
            2 => step3ArrowRot,
            3 => step4ArrowRot,
            _ => 0f
        };

        arrowRect.anchoredPosition = targetPos;
        arrowRect.localRotation = Quaternion.Euler(0, 0, targetRot);

        // Block input briefly to prevent accidental skip
        StartCoroutine(UnblockInputAfterDelay(0.5f));
    }

    void Update()
    {
        if (!tutorialActive || inputBlocked) return;

        bool tapped = false;

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            tapped = true;
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            tapped = true;
#endif

        if (tapped) NextStep();
    }

    void NextStep()
    {
        currentStep++;

        if (currentStep >= messages.Length)
        {
            CompleteTutorial();
        }
        else
        {
            inputBlocked = true;
            ShowStep(currentStep);
        }
    }

    void CompleteTutorial()
    {
        tutorialActive = false;
        tutorialPanel.SetActive(false);
        PlayerPrefs.SetInt("save_tutorial_done", 1);
        PlayerPrefs.Save();
    }

    IEnumerator UnblockInputAfterDelay(float delay)
    {
        inputBlocked = true;
        yield return new WaitForSeconds(delay);
        inputBlocked = false;
    }

    // Call this from other scripts if needed
    public void SkipTutorial()
    {
        CompleteTutorial();
    }
}
