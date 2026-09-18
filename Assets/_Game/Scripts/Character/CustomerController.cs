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

    public override void Initialize()
    {
        _state = CustomerState.Spawned;
    }

    void Update()
    {
        switch (_state)
        {
            case CustomerState.Spawned:
                HandleSpawned();
                break;
            case CustomerState.Walking:
                HandleWalking();
                break;
            case CustomerState.Shopping:
                HandleShopping();
                break;
            case CustomerState.Paying:
                HandlePaying();
                break;
            case CustomerState.Leaving:
                HandleLeaving();
                break;
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
            // LevelManager Phase 6-এ wire হবে, এখন multiplier = 1
            float multiplier = 1f;
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
        Destroy(gameObject);
    }

    void SetState(CustomerState newState)
    {
        _state = newState;
    }
}
