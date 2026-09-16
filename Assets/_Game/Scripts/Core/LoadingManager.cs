using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image progressBarFill;
    [SerializeField] private Text loadingText;

    private const string KEY_ONBOARDED = "save_onboarded";
    private const string SCENE_ONBOARDING = "Onboarding";
    private const string SCENE_LEVEL = "Level_01";

    void Start()
    {
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        // Determine target scene
        string targetScene = PlayerPrefs.GetInt(KEY_ONBOARDED, 0) == 1
            ? SCENE_LEVEL
            : SCENE_ONBOARDING;

        AsyncOperation op = SceneManager.LoadSceneAsync(targetScene);
        op.allowSceneActivation = false;

        float displayProgress = 0f;

        while (!op.isDone)
        {
            // Unity caps at 0.9 until allowSceneActivation = true
            float targetProgress = Mathf.Clamp01(op.progress / 0.9f);
            displayProgress = Mathf.MoveTowards(displayProgress, targetProgress, Time.deltaTime * 0.8f);

            progressBarFill.fillAmount = displayProgress;

            if (loadingText != null)
                loadingText.text = $"Loading... {Mathf.RoundToInt(displayProgress * 100)}%";

            if (op.progress >= 0.9f && displayProgress >= 0.99f)
            {
                progressBarFill.fillAmount = 1f;
                yield return new WaitForSeconds(0.3f);
                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
