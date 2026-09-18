using UnityEngine;
using UnityEngine.AI;

public class CustomerController : CharacterBase
{
    public enum CustomerState
    {
        Spawned,
        Walking,
        Shopping,
        Queuing,
        Paying,
        Leaving
    }

    [Header("References")]
    public ShopController targetShop;

    [Header("Settings")]
    public float shoppingDuration = 5f;
    public float payingDuration = 2f;

    private CustomerState _state;
    private float _stateTimer;

    public static event System.Action OnCustomerServed;
    public static event System.Action OnCustomerWaiting;  // ← NEW
    public static event System.Action OnCustomerLeft;     // ← NEW

    public override void Initialize()
    {
        _state = CustomerState.Spawned;
    }

    void Update()
    {
        switch (_state)
        {
            case CustomerState.Spawned: HandleSpawned(); break;
            case CustomerState.Walking: HandleWalking(); break;
            case CustomerState.Shopping: HandleShopping(); break;
            case CustomerState.Paying: HandlePaying(); break;
            case CustomerState.Leaving: HandleLeaving(); break;
        }
    }

    void HandleSpawned()
    {
        if (targetShop == null) return;
        MoveTo(targetShop.transform.position);
        PlayAnim("Walk");
        SetState(CustomerState.Walking);
    }

    void HandleWalking()
    {
        if (!HasReachedDestination()) return;
        OnCustomerWaiting?.Invoke();   // ← NEW: shop-এ পৌঁছে waiting শুরু
        PlayAnim("Browse");
        _stateTimer = shoppingDuration;
        SetState(CustomerState.Shopping);
    }

    void HandleShopping()
    {
        _stateTimer -= Time.deltaTime;
        if (_stateTimer > 0) return;
        PlayAnim("Pay");
        _stateTimer = payingDuration;
        SetState(CustomerState.Paying);
    }

    void HandlePaying()
    {
        _stateTimer -= Time.deltaTime;
        if (_stateTimer > 0) return;

        if (targetShop != null)
        {
            float multiplier = LevelManager.Instance != null ? LevelManager.Instance.GetIncomeMultiplier() : 1f;
            float income = targetShop.Data.baseIncome * multiplier;
            EconomyManager.Instance.AddMoney(income);
            Debug.Log($"[Customer] Paid: +{income} coins");
        }

        OnCustomerServed?.Invoke();
        PlayAnim("Walk");
        MoveTo(new Vector3(0, 0, -18f));
        SetState(CustomerState.Leaving);
    }

    void HandleLeaving()
    {
        if (!HasReachedDestination()) return;
        OnCustomerLeft?.Invoke();   // ← NEW: exit করার আগে fire
        Destroy(gameObject);
    }

    void SetState(CustomerState newState)
    {
        _state = newState;
    }
}
