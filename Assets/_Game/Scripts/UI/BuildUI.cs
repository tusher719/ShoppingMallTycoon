// Assets/_Game/Scripts/UI/BuildUI.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button btnBuild;
    [SerializeField] private TextMeshProUGUI txtBtnLabel;
    [SerializeField] private TextMeshProUGUI txtCoins;
    [SerializeField] private GameObject shopPrefab;
    [SerializeField] private ShopData shopData;

    [Header("Settings")]
    [SerializeField] private int maxShops = 9;

    private int _shopsBuilt = 0;

    void OnEnable()
    {
        EconomyManager.OnMoneyChanged += UpdateCoinDisplay;
        ShopController.OnShopBuilt += HandleShopBuilt;
    }

    void OnDisable()
    {
        EconomyManager.OnMoneyChanged -= UpdateCoinDisplay;
        ShopController.OnShopBuilt -= HandleShopBuilt;
    }

    void Start()
    {
        btnBuild.onClick.AddListener(OnBuildClicked);
        UpdateCoinDisplay(EconomyManager.Instance.CurrentCoins);
        UpdateButton();
    }

    private void HandleShopBuilt(ShopController shop)
    {
        _shopsBuilt++;
        UpdateButton();
    }

    void OnBuildClicked()
    {
        if (_shopsBuilt >= maxShops) return;
        if (!EconomyManager.Instance.SpendMoney(shopData.baseCost)) return;

        if (!GridManager.Instance.TryGetNextTile(out Vector3 spawnPos))
        {
            Debug.LogWarning("[BuildUI] Grid full!");
            return;
        }

        // Mall parent-এর ভেতরে spawn
        GameObject mallParent = GameObject.Find("--- Mall ---");
        GameObject shopGO = Instantiate(shopPrefab, spawnPos, Quaternion.identity);
        if (mallParent != null)
            shopGO.transform.SetParent(mallParent.transform);

        ShopController ctrl = shopGO.GetComponent<ShopController>();
        ctrl.Build();
        Debug.Log($"[BuildUI] {shopData.shopName} built at {spawnPos}");
    }

    private void UpdateCoinDisplay(float coins)
    {
        if (txtCoins != null)
            txtCoins.text = $"Coins: {coins:F0}";
        UpdateButton();
    }

    private void UpdateButton()
    {
        if (txtBtnLabel == null || shopData == null) return;

        if (_shopsBuilt >= maxShops)
        {
            btnBuild.interactable = false;
            txtBtnLabel.text = "Max";
            SetButtonColor(new Color(0.4f, 0.4f, 0.4f));
            return;
        }

        bool canAfford = EconomyManager.Instance != null &&
                         EconomyManager.Instance.CurrentCoins >= shopData.baseCost;

        btnBuild.interactable = canAfford;
        txtBtnLabel.text = $"Build {shopData.shopName}";
        SetButtonColor(canAfford
            ? new Color(0.29f, 0.56f, 0.85f)
            : new Color(0.4f, 0.4f, 0.4f));
    }

    private void SetButtonColor(Color color)
    {
        var colors = btnBuild.colors;
        colors.normalColor = color;
        colors.disabledColor = color;
        btnBuild.colors = colors;
    }
}
