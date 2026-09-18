// Assets/_Game/Scripts/Level/LevelObjectiveManager.cs
using UnityEngine;
using System;

public class LevelObjectiveManager : MonoBehaviour
{
    public static LevelObjectiveManager Instance { get; private set; }

    [Header("Level Targets")]
    [SerializeField] private int targetShopsBuilt = 3;
    [SerializeField] private int targetCustomersServed = 20;
    [SerializeField] private float targetMoneyEarned = 500f;

    private int _shopsBuilt = 0;
    private int _customersServed = 0;
    private float _moneyEarned = 0f;
    private bool _objectivesComplete = false;

    public static event Action<int, int> OnShopsProgress;
    public static event Action<int, int> OnCustomersProgress;
    public static event Action<float, float> OnMoneyProgress;
    public static event Action OnAllObjectivesComplete;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    void OnEnable()
    {
        ShopController.OnShopBuilt += HandleShopBuilt;
        CustomerController.OnCustomerServed += HandleCustomerServed;
        EconomyManager.OnMoneyAdded += AddMoneyEarned;   // ← updated
    }

    void OnDisable()
    {
        ShopController.OnShopBuilt -= HandleShopBuilt;
        CustomerController.OnCustomerServed -= HandleCustomerServed;
        EconomyManager.OnMoneyAdded -= AddMoneyEarned;   // ← updated
    }

    private void HandleShopBuilt(ShopController shop)
    {
        _shopsBuilt++;
        OnShopsProgress?.Invoke(_shopsBuilt, targetShopsBuilt);
        Debug.Log($"[Objective] Shops Built: {_shopsBuilt}/{targetShopsBuilt}");
        CheckAllObjectives();
    }

    private void HandleCustomerServed()
    {
        _customersServed++;
        OnCustomersProgress?.Invoke(_customersServed, targetCustomersServed);
        Debug.Log($"[Objective] Customers Served: {_customersServed}/{targetCustomersServed}");
        CheckAllObjectives();
    }

    public void AddMoneyEarned(float amount)
    {
        if (amount <= 0f) return;
        _moneyEarned += amount;
        OnMoneyProgress?.Invoke(_moneyEarned, targetMoneyEarned);
        Debug.Log($"[Objective] Money Earned: {_moneyEarned}/{targetMoneyEarned}");
        CheckAllObjectives();
    }

    private void CheckAllObjectives()
    {
        if (_objectivesComplete) return;

        if (_shopsBuilt >= targetShopsBuilt &&
            _customersServed >= targetCustomersServed &&
            _moneyEarned >= targetMoneyEarned)
        {
            _objectivesComplete = true;
            Debug.Log("[Objective] ✅ All objectives complete!");
            OnAllObjectivesComplete?.Invoke();
        }
    }

    public int ShopsBuilt => _shopsBuilt;
    public int CustomersServed => _customersServed;
    public float MoneyEarned => _moneyEarned;
    public int TargetShopsBuilt => targetShopsBuilt;
    public int TargetCustomersServed => targetCustomersServed;
    public float TargetMoneyEarned => targetMoneyEarned;
    public bool IsComplete => _objectivesComplete;
}
