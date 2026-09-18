// Assets/_Game/Scripts/Character/CustomerSpawner.cs
using UnityEngine;
using System.Collections.Generic;

public class CustomerSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnInterval = 8f;
    [SerializeField] private int maxCustomers = 5;

    private List<ShopController> _builtShops = new();
    private float _spawnTimer = 0f;
    private int _activeCustomers = 0;

    void OnEnable()
    {
        ShopController.OnShopBuilt += HandleShopBuilt;
        CustomerController.OnCustomerServed += HandleCustomerLeft;
        CustomerController.OnCustomerLeft += HandleCustomerLeft;
    }

    void OnDisable()
    {
        ShopController.OnShopBuilt -= HandleShopBuilt;
        CustomerController.OnCustomerServed -= HandleCustomerLeft;
        CustomerController.OnCustomerLeft -= HandleCustomerLeft;
    }

    private void HandleShopBuilt(ShopController shop)
    {
        if (!_builtShops.Contains(shop))
            _builtShops.Add(shop);
        Debug.Log($"[CustomerSpawner] Shop registered: {shop.Data.shopName}. Total: {_builtShops.Count}");
    }

    private void HandleCustomerLeft()
    {
        _activeCustomers = Mathf.Max(0, _activeCustomers - 1);
    }

    void Update()
    {
        if (_builtShops.Count == 0) return;
        if (_activeCustomers >= maxCustomers) return;

        _spawnTimer += Time.deltaTime;
        if (_spawnTimer < spawnInterval) return;
        _spawnTimer = 0f;

        SpawnCustomer();
    }

    void SpawnCustomer()
    {
        // Random shop select
        ShopController target = _builtShops[Random.Range(0, _builtShops.Count)];

        GameObject go = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        CustomerController ctrl = go.GetComponent<CustomerController>();
        ctrl.targetShop = target;
        ctrl.Initialize();
        _activeCustomers++;
        Debug.Log($"[CustomerSpawner] Customer spawned → {target.Data.shopName}");

        HUDManager hud = FindFirstObjectByType<HUDManager>();
        if (hud != null) hud.OnCustomerSpawned();
    }
}
