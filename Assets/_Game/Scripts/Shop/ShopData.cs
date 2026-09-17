// Assets/_Game/Scripts/Shop/ShopData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewShop_Data", menuName = "MallTycoon/Shop Data")]
public class ShopData : ScriptableObject
{
    [Header("Identity")]
    public string shopName;
    public int unlockLevel = 1;

    [Header("Economy")]
    public float baseCost = 100f;
    public float baseIncome = 20f;

    [Header("Capacity")]
    public int maxCustomers = 2;

    [Header("Visuals")]
    public GameObject shopPrefab;
    public Sprite shopIcon;
}
