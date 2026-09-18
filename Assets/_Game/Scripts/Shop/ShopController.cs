// Assets/_Game/Scripts/Shop/ShopController.cs
using UnityEngine;
using System;

public class ShopController : MonoBehaviour
{
    [SerializeField] private ShopData shopData;

    private bool _isBuilt = false;
    private int _currentTier = 0;        // 0 = Tier 1
    private float _incomeTimer = 0f;
    private const float INCOME_INTERVAL = 5f;

    public ShopData Data => shopData;
    public bool IsBuilt => _isBuilt;
    public int CurrentTier => _currentTier;

    public static event Action<ShopController> OnShopBuilt;
    public static event Action<ShopController> OnShopUpgraded;

    public void Build()
    {
        _isBuilt = true;
        OnShopBuilt?.Invoke(this);
        Debug.Log($"[ShopController] Built: {shopData.shopName}");
    }

    void Update()
    {
        if (!_isBuilt) return;
        _incomeTimer += Time.deltaTime;
        if (_incomeTimer < INCOME_INTERVAL) return;
        _incomeTimer = 0f;
        GenerateIncome();
    }

    void GenerateIncome()
    {
        float multiplier = GetCurrentMultiplier();
        float income = shopData.baseIncome * multiplier;

        // LevelManager multiplier (Phase 6 Step 35)
        if (LevelManager.Instance != null)
            income *= LevelManager.Instance.GetIncomeMultiplier();

        EconomyManager.Instance.AddMoney(income);
        Debug.Log($"[ShopController] Income: +{income} from {shopData.shopName} ({GetTierName()})");
    }

    // --- Upgrade ---
    public bool CanUpgrade()
    {
        if (shopData.upgradeTiers == null) return false;
        return _currentTier < shopData.upgradeTiers.Length - 1;
    }

    public float GetUpgradeCost()
    {
        if (!CanUpgrade()) return 0f;
        return shopData.upgradeTiers[_currentTier + 1].upgradeCost;
    }

    public bool Upgrade()
    {
        if (!CanUpgrade()) return false;

        float cost = GetUpgradeCost();
        if (!EconomyManager.Instance.SpendMoney(cost)) return false;

        _currentTier++;
        OnShopUpgraded?.Invoke(this);
        Debug.Log($"[ShopController] Upgraded to {GetTierName()}");
        return true;
    }

    // --- Helpers ---
    public float GetCurrentMultiplier()
    {
        if (shopData.upgradeTiers == null || shopData.upgradeTiers.Length == 0)
            return 1f;
        return shopData.upgradeTiers[_currentTier].incomeMultiplier;
    }

    public string GetTierName()
    {
        if (shopData.upgradeTiers == null || shopData.upgradeTiers.Length == 0)
            return "Tier 1";
        return shopData.upgradeTiers[_currentTier].tierName;
    }

    public int GetMaxCustomers()
    {
        if (shopData.upgradeTiers == null || shopData.upgradeTiers.Length == 0)
            return shopData.maxCustomers;
        return shopData.upgradeTiers[_currentTier].maxCustomers;
    }
}
