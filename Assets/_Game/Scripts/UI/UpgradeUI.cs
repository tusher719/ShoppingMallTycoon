// Assets/_Game/Scripts/UI/UpgradeUI.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject upgradePanel;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI txtShopName;
    [SerializeField] private TextMeshProUGUI txtCurrentTier;
    [SerializeField] private TextMeshProUGUI txtUpgradeCost;
    [SerializeField] private Button btnUpgrade;
    [SerializeField] private Button btnClose;

    private ShopController _selectedShop;

    void Start()
    {
        btnUpgrade.onClick.AddListener(OnUpgradeClicked);
        btnClose.onClick.AddListener(ClosePanel);
        upgradePanel.SetActive(false);
    }

    void OnEnable()
    {
        ShopController.OnShopUpgraded += HandleShopUpgraded;
    }

    void OnDisable()
    {
        ShopController.OnShopUpgraded -= HandleShopUpgraded;
    }

    // Shop click হলে এই method call হবে
    public void OpenPanel(ShopController shop)
    {
        _selectedShop = shop;
        upgradePanel.SetActive(true);
        RefreshUI();
    }

    public void ClosePanel()
    {
        upgradePanel.SetActive(false);
        _selectedShop = null;
    }

    private void RefreshUI()
    {
        if (_selectedShop == null) return;

        txtShopName.text = _selectedShop.Data.shopName;
        txtCurrentTier.text = $"Current: {_selectedShop.GetTierName()}";

        if (_selectedShop.CanUpgrade())
        {
            float cost = _selectedShop.GetUpgradeCost();
            bool canAfford = EconomyManager.Instance.CurrentCoins >= cost;

            txtUpgradeCost.text = $"Upgrade: ${cost}";
            btnUpgrade.interactable = canAfford;

            var colors = btnUpgrade.colors;
            colors.normalColor = canAfford
                ? new Color(0.29f, 0.56f, 0.85f)
                : new Color(0.4f, 0.4f, 0.4f);
            btnUpgrade.colors = colors;
        }
        else
        {
            txtUpgradeCost.text = "Max Level!";
            btnUpgrade.interactable = false;
        }
    }

    private void OnUpgradeClicked()
    {
        if (_selectedShop == null) return;
        _selectedShop.Upgrade();
        RefreshUI();
    }

    private void HandleShopUpgraded(ShopController shop)
    {
        if (_selectedShop == shop)
            RefreshUI();
    }
}
