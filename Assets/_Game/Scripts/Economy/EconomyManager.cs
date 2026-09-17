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
    // LevelManager not yet implemented (Step 35)
    // Multiplier will be wired in Phase 6
    CurrentCoins += amount;
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
