// Assets/_Game/Scripts/UI/BuildUI.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button btnBuild;
    [SerializeField] private TextMeshProUGUI txtCoins;

    [Header("Shop Settings")]
    [SerializeField] private ShopData shopData;
    [SerializeField] private GameObject shopPrefab;
    [SerializeField] private Vector3 spawnPosition = new Vector3(0, 0, 0);

    void OnEnable()
    {
        EconomyManager.OnMoneyChanged += UpdateCoinsUI;
    }

    void OnDisable()
    {
        EconomyManager.OnMoneyChanged -= UpdateCoinsUI;
    }

    void Start()
    {
        btnBuild.onClick.AddListener(OnBuildClicked);
        UpdateCoinsUI(500f); // starting coins
    }

    void OnBuildClicked()
    {
        if (EconomyManager.Instance == null) return;

        bool success = EconomyManager.Instance.SpendMoney(shopData.baseCost);

        if (success)
        {
            GameObject shop = Instantiate(shopPrefab, spawnPosition, Quaternion.identity);
            shop.GetComponent<ShopController>().Build();
            Debug.Log($"[BuildUI] {shopData.shopName} built at {spawnPosition}");
        }
        else
        {
            Debug.Log("[BuildUI] Not enough coins!");
        }
    }

    void UpdateCoinsUI(float amount)
    {
        txtCoins.text = $"Coins: {amount:F0}";

        // Button disable if not enough coins
        bool canAfford = amount >= shopData.baseCost;
        btnBuild.interactable = canAfford;
        btnBuild.image.color = canAfford ? new Color(0.29f, 0.565f, 0.851f) : new Color(0.4f, 0.4f, 0.4f);
    }
}
