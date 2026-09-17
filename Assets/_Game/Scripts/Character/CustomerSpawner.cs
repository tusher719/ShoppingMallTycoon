using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject customerPrefab;
    public Transform spawnPoint;

    [Header("Spawn Settings")]
    public float spawnInterval = 8f;
    public int maxCustomers = 5;

    private ShopController _targetShop;
    private float _spawnTimer;
    private int _activeCustomers;

    void OnEnable()
    {
        CustomerController.OnCustomerServed += OnCustomerLeft;
        ShopController.OnShopBuilt += OnShopBuilt;
    }

    void OnDisable()
    {
        CustomerController.OnCustomerServed -= OnCustomerLeft;
        ShopController.OnShopBuilt -= OnShopBuilt;
    }

    void OnShopBuilt(ShopController shop)
    {
        _targetShop = shop;
        Debug.Log($"[CustomerSpawner] Target shop set: {shop.Data.shopName}");
    }

    void Update()
    {
        if (_targetShop == null || !_targetShop.IsBuilt) return;
        if (_activeCustomers >= maxCustomers) return;

        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= spawnInterval)
        {
            _spawnTimer = 0f;
            SpawnCustomer();
        }
    }

    void SpawnCustomer()
    {
        if (customerPrefab == null || spawnPoint == null) return;

        GameObject obj = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        CustomerController customer = obj.GetComponent<CustomerController>();

        if (customer != null)
        {
            customer.targetShop = _targetShop;
            customer.Initialize();
            _activeCustomers++;
            Debug.Log("[CustomerSpawner] Customer spawned!");
        }
    }

    void OnCustomerLeft()
    {
        _activeCustomers = Mathf.Max(0, _activeCustomers - 1);
    }
}
