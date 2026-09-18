// Assets/_Game/Scripts/Level/LevelManager.cs
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private int currentLevelIndex = 0;  // Level 1 = index 0

    public int CurrentLevelIndex => currentLevelIndex;

    public static event Action<int> OnLevelComplete;  // passes star count

    private bool _levelComplete = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    void OnEnable()
    {
        LevelObjectiveManager.OnAllObjectivesComplete += HandleObjectivesComplete;
    }

    void OnDisable()
    {
        LevelObjectiveManager.OnAllObjectivesComplete -= HandleObjectivesComplete;
    }

    private void HandleObjectivesComplete()
    {
        if (_levelComplete) return;
        _levelComplete = true;

        int stars = CalculateStars();
        Debug.Log($"[LevelManager] Level Complete! Stars: {stars}");

        PlayerPrefs.SetInt($"save_stars_{currentLevelIndex}", stars);
        PlayerPrefs.SetInt("save_level", currentLevelIndex + 1);
        PlayerPrefs.Save();

        OnLevelComplete?.Invoke(stars);
    }

    private int CalculateStars()
    {
        float satisfaction = SatisfactionManager.Instance.Satisfaction;

        if (satisfaction >= 80f) return 3;
        if (satisfaction >= 50f) return 2;
        return 1;
    }

    // Income multiplier — CustomerController-এ wire করবো
    public float GetIncomeMultiplier()
    {
        return Mathf.Pow(1.2f, currentLevelIndex);
    }
}
