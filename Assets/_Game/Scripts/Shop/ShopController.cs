// Assets/_Game/Scripts/Shop/ShopController.cs
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private ShopData shopData;

    [Header("Settings")]
    [SerializeField] private float incomeInterval = 5f; // প্রতি 5 সেকেন্ডে income

    private bool isBuilt = false;
    private float incomeTimer = 0f;

    public ShopData Data => shopData;
    public bool IsBuilt => isBuilt;

    public static event System.Action<ShopController> OnShopBuilt;

    public void Build()
    {
        if (isBuilt) return;
        isBuilt = true;
        OnShopBuilt?.Invoke(this);
        Debug.Log($"[ShopController] {shopData.shopName} built!");
    }

    void Update()
    {
        if (!isBuilt) return;

        incomeTimer += Time.deltaTime;
        if (incomeTimer >= incomeInterval)
        {
            incomeTimer = 0f;
            GenerateIncome();
        }
    }

    void GenerateIncome()
    {
        if (EconomyManager.Instance == null) return;
        EconomyManager.Instance.AddMoney(shopData.baseIncome);
        Debug.Log($"[ShopController] Income: +{shopData.baseIncome} from {shopData.shopName}");
    }
}
