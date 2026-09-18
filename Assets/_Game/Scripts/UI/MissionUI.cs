// Assets/_Game/Scripts/UI/MissionUI.cs
using UnityEngine;
using TMPro;

public class MissionUI : MonoBehaviour
{
    [Header("Progress Text")]
    [SerializeField] private TextMeshProUGUI txtShops;
    [SerializeField] private TextMeshProUGUI txtCustomers;
    [SerializeField] private TextMeshProUGUI txtMoney;

    [Header("Level Complete")]
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private TextMeshProUGUI txtStars;

    void OnEnable()
    {
        LevelObjectiveManager.OnShopsProgress += UpdateShops;
        LevelObjectiveManager.OnCustomersProgress += UpdateCustomers;
        LevelObjectiveManager.OnMoneyProgress += UpdateMoney;
        LevelManager.OnLevelComplete += ShowLevelComplete;
    }

    void OnDisable()
    {
        LevelObjectiveManager.OnShopsProgress -= UpdateShops;
        LevelObjectiveManager.OnCustomersProgress -= UpdateCustomers;
        LevelObjectiveManager.OnMoneyProgress -= UpdateMoney;
        LevelManager.OnLevelComplete -= ShowLevelComplete;
    }

    void Start()
    {
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);

        // Initial values
        var obj = LevelObjectiveManager.Instance;
        if (obj == null) return;
        UpdateShops(obj.ShopsBuilt, obj.TargetShopsBuilt);
        UpdateCustomers(obj.CustomersServed, obj.TargetCustomersServed);
        UpdateMoney(obj.MoneyEarned, obj.TargetMoneyEarned);
    }

    private void UpdateShops(int current, int target)
    {
        if (txtShops != null)
            txtShops.text = $"Shops: {current}/{target}";
    }

    private void UpdateCustomers(int current, int target)
    {
        if (txtCustomers != null)
            txtCustomers.text = $"Customers: {current}/{target}";
    }

    private void UpdateMoney(float current, float target)
    {
        if (txtMoney != null)
            txtMoney.text = $"Earned: ${current:F0}/{target:F0}";
    }

    private void ShowLevelComplete(int stars)
    {
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);

        if (txtStars != null)
            txtStars.text = stars switch
            {
                3 => "Stars: 3/3",
                2 => "Stars: 2/3",
                _ => "Stars: 1/3"
            };

        Debug.Log($"[MissionUI] Level Complete! {stars} stars");
    }
}
