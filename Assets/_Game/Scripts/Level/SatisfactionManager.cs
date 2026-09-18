// Assets/_Game/Scripts/Level/SatisfactionManager.cs
using UnityEngine;
using System;

public class SatisfactionManager : MonoBehaviour
{
    public static SatisfactionManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private float baseSatisfaction = 70f;
    [SerializeField] private float waitingPenaltyPerCustomer = 2f;
    [SerializeField] private float servedBonusPerCustomer = 1.5f;

    [Header("Thresholds")]
    [SerializeField] private float highThreshold = 80f;   // spawn rate boost
    [SerializeField] private float lowThreshold = 30f;    // customers leave

    private float _satisfaction;
    private int _waitingCustomers = 0;

    public float Satisfaction => _satisfaction;

    public static event Action<float> OnSatisfactionChanged;
    public static event Action OnHighSatisfaction;   // > 80 → spawn rate boost
    public static event Action OnLowSatisfaction;    // < 30 → customers leave

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    void Start()
    {
        _satisfaction = baseSatisfaction;
        OnSatisfactionChanged?.Invoke(_satisfaction);
    }

    void OnEnable()
    {
        CustomerController.OnCustomerServed += HandleCustomerServed;
        CustomerController.OnCustomerWaiting += HandleCustomerWaiting;
        CustomerController.OnCustomerLeft += HandleCustomerLeft;
    }

    void OnDisable()
    {
        CustomerController.OnCustomerServed -= HandleCustomerServed;
        CustomerController.OnCustomerWaiting -= HandleCustomerWaiting;
        CustomerController.OnCustomerLeft -= HandleCustomerLeft;
    }

    private void HandleCustomerServed()
    {
        _satisfaction = Mathf.Clamp(_satisfaction + servedBonusPerCustomer, 0f, 100f);
        if (_waitingCustomers > 0) _waitingCustomers--;
        Evaluate();
    }

    private void HandleCustomerWaiting()
    {
        _waitingCustomers++;
        _satisfaction = Mathf.Clamp(_satisfaction - waitingPenaltyPerCustomer, 0f, 100f);
        Evaluate();
    }

    private void HandleCustomerLeft()
    {
        _waitingCustomers = Mathf.Max(0, _waitingCustomers - 1);
    }

    private void Evaluate()
    {
        OnSatisfactionChanged?.Invoke(_satisfaction);
        Debug.Log($"[Satisfaction] {_satisfaction:F1}%");

        if (_satisfaction >= highThreshold)
            OnHighSatisfaction?.Invoke();
        else if (_satisfaction <= lowThreshold)
            OnLowSatisfaction?.Invoke();
    }
}
