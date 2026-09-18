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

        // DisplayName দিয়ে দেখাও — Shop #1, Shop #2 etc.
        txtShopName.text = _selectedShop.DisplayName;
        txtCurrentTier.text = $"Current: {_selectedShop.GetTierName()}";

        if (_selectedShop.CanUpgrade())
        {
            float cost = _selectedShop.GetUpgradeCost();
            bool canAfford = EconomyManager.Instance.CurrentCoins >= cost;
            txtUpgradeCost.text = $"Upgrade: ${cost}";
            btnUpgrade.interactable = canAfford;
            SetButtonColor(canAfford
                ? new Color(0.29f, 0.56f, 0.85f)
                : new Color(0.4f, 0.4f, 0.4f));
        }
        else
        {
            txtUpgradeCost.text = "Max Level!";
            btnUpgrade.interactable = false;
            SetButtonColor(new Color(0.4f, 0.4f, 0.4f));
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

    private void SetButtonColor(Color color)
    {
        var colors = btnUpgrade.colors;
        colors.normalColor = color;
        colors.disabledColor = color;
        btnUpgrade.colors = colors;
    }
}
