using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnboardingManager : MonoBehaviour
{
    [Header("Gender Buttons")]
    [SerializeField] private Button btnMale;
    [SerializeField] private Button btnFemale;

    [Header("Age Buttons")]
    [SerializeField] private Button btnYoung;
    [SerializeField] private Button btnAdult;
    [SerializeField] private Button btnSenior;

    [Header("Start Button")]
    [SerializeField] private Button btnStart;

    [Header("Button Colors")]
    [SerializeField] private Color colorDefault = new Color(0.267f, 0.267f, 0.4f);
    [SerializeField] private Color colorSelected = new Color(0.29f, 0.565f, 0.851f);

    private string selectedGender = "";
    private string selectedAge = "";

    private const string KEY_GENDER = "save_char_gender";
    private const string KEY_AGE = "save_char_age";
    private const string KEY_ONBOARDED = "save_onboarded";
    private const string SCENE_LEVEL = "Level_01";

    void Start()
    {
        // Start button শুরুতে disabled
        btnStart.interactable = false;

        // Gender button listeners
        btnMale.onClick.AddListener(() => SelectGender("male", btnMale, btnFemale));
        btnFemale.onClick.AddListener(() => SelectGender("female", btnFemale, btnMale));

        // Age button listeners
        btnYoung.onClick.AddListener(() => SelectAge("young", btnYoung));
        btnAdult.onClick.AddListener(() => SelectAge("adult", btnAdult));
        btnSenior.onClick.AddListener(() => SelectAge("senior", btnSenior));

        // Start button listener
        btnStart.onClick.AddListener(OnStartGame);
    }

    private void SelectGender(string gender, Button selected, Button other)
    {
        selectedGender = gender;
        SetButtonColor(selected, colorSelected);
        SetButtonColor(other, colorDefault);
        CheckStartButton();
    }

    private void SelectAge(string age, Button selected)
    {
        selectedAge = age;
        SetButtonColor(btnYoung, colorDefault);
        SetButtonColor(btnAdult, colorDefault);
        SetButtonColor(btnSenior, colorDefault);
        SetButtonColor(selected, colorSelected);
        CheckStartButton();
    }

    private void SetButtonColor(Button btn, Color color)
    {
        btn.GetComponent<Image>().color = color;
    }

    private void CheckStartButton()
    {
        btnStart.interactable = selectedGender != "" && selectedAge != "";
    }

    private void OnStartGame()
    {
        PlayerPrefs.SetString(KEY_GENDER, selectedGender);
        PlayerPrefs.SetString(KEY_AGE, selectedAge);
        PlayerPrefs.SetInt(KEY_ONBOARDED, 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(SCENE_LEVEL);
    }
}
