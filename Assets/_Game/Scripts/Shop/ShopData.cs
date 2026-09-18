// Assets/_Game/Scripts/Shop/ShopData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "ShopData", menuName = "MallTycoon/Shop Data")]
public class ShopData : ScriptableObject
{
    [Header("Basic Info")]
    public string shopName;
    public int unlockLevel;
    public Sprite shopIcon;
    public GameObject shopPrefab;

    [Header("Base Stats (Tier 1)")]
    public float baseCost;
    public float baseIncome;
    public int maxCustomers;

    [Header("Upgrade Tiers")]
    public UpgradeTier[] upgradeTiers;
}

[System.Serializable]
public class UpgradeTier
{
    public string tierName;
    public float upgradeCost;
    public float incomeMultiplier;
    public int maxCustomers;
}
