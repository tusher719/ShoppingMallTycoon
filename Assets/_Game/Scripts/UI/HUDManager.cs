// Assets/_Game/Scripts/UI/HUDManager.cs
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("Top Bar")]
    [SerializeField] private TextMeshProUGUI txtCoins;
    [SerializeField] private TextMeshProUGUI txtIncomeRate;

    [Header("Bottom Bar")]
    [SerializeField] private TextMeshProUGUI txtShopCount;
    [SerializeField] private TextMeshProUGUI txtCustomerCount;

    private int _shopCount = 0;
    private int _customerCount = 0;
    private float _rateTimer = 0f;
    private float _rateUpdateInterval = 1f;

    void OnEnable()
    {
        EconomyManager.OnMoneyChanged += UpdateCoins;
        ShopController.OnShopBuilt += HandleShopBuilt;
        ShopController.OnShopUpgraded += HandleShopUpgraded;
        CustomerController.OnCustomerServed += HandleCustomerServed;
        CustomerController.OnCustomerLeft += HandleCustomerLeft;
    }

    void OnDisable()
    {
        EconomyManager.OnMoneyChanged -= UpdateCoins;
        ShopController.OnShopBuilt -= HandleShopBuilt;
        ShopController.OnShopUpgraded -= HandleShopUpgraded;
        CustomerController.OnCustomerServed -= HandleCustomerServed;
        CustomerController.OnCustomerLeft -= HandleCustomerLeft;
    }

    void Start()
    {
        UpdateCoins(EconomyManager.Instance.CurrentCoins);
        UpdateShopCount();
        UpdateCustomerCount();
        UpdateIncomeRate();
    }

    void Update()
    {
        _rateTimer += Time.deltaTime;
        if (_rateTimer >= _rateUpdateInterval)
        {
            _rateTimer = 0f;
            UpdateIncomeRate();
        }
    }

    private void UpdateIncomeRate()
    {
        if (txtIncomeRate == null) return;
        float perMin = CalculateIncomePerMin();
        txtIncomeRate.text = perMin > 0 ? $"+{perMin:F0}/min" : "+0/min";
    }

    private float CalculateIncomePerMin()
    {
        ShopController[] shops = FindObjectsByType<ShopController>(FindObjectsSortMode.None);
        float totalPerMin = 0f;
        foreach (var shop in shops)
        {
            if (!shop.IsBuilt) continue;
            float incomePerInterval = shop.Data.baseIncome * shop.GetCurrentMultiplier();
            if (LevelManager.Instance != null)
                incomePerInterval *= LevelManager.Instance.GetIncomeMultiplier();
            totalPerMin += (incomePerInterval / 5f) * 60f;
        }
        return totalPerMin;
    }

    private void UpdateCoins(float coins)
    {
        if (txtCoins != null)
            txtCoins.text = $"Coins: {coins:F0}";
    }

    private void HandleShopBuilt(ShopController shop)
    {
        _shopCount++;
        UpdateShopCount();
        UpdateIncomeRate();
    }

    private void HandleShopUpgraded(ShopController shop)
    {
        UpdateIncomeRate();
    }

    private void HandleCustomerServed()
    {
        _customerCount = Mathf.Max(0, _customerCount - 1);
        UpdateCustomerCount();
    }

    private void HandleCustomerLeft()
    {
        _customerCount = Mathf.Max(0, _customerCount - 1);
        UpdateCustomerCount();
    }

    public void OnCustomerSpawned()
    {
        _customerCount++;
        UpdateCustomerCount();
    }

    private void UpdateShopCount()
    {
        if (txtShopCount != null)
            txtShopCount.text = $"Shops: {_shopCount}";
    }

    private void UpdateCustomerCount()
    {
        if (txtCustomerCount != null)
            txtCustomerCount.text = $"Customers: {_customerCount}";
    }
}
