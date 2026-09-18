// Assets/_Game/Scripts/Economy/EconomyManager.cs
using UnityEngine;
using System;

public class EconomyManager : MonoBehaviour
{
  public static EconomyManager Instance { get; private set; }

  [Header("Settings")]
  [SerializeField] private float startingCoins = 500f;

  public float CurrentCoins { get; private set; }

  public static event Action<float> OnMoneyChanged;
  public static event Action<float> OnMoneyAdded;

  void Awake()
  {
    if (Instance == null) Instance = this;
    else { Destroy(gameObject); return; }
  }

  void Start()
  {
    CurrentCoins = startingCoins;
    OnMoneyChanged?.Invoke(CurrentCoins);
  }

  public void AddMoney(float amount)
  {
    if (amount <= 0f) return;
    CurrentCoins += amount;
    OnMoneyAdded?.Invoke(amount);
    OnMoneyChanged?.Invoke(CurrentCoins);
  }

  public bool SpendMoney(float amount)
  {
    if (CurrentCoins < amount) return false;
    CurrentCoins -= amount;
    OnMoneyChanged?.Invoke(CurrentCoins);
    return true;
  }
}
