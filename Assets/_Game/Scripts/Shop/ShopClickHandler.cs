// Assets/_Game/Scripts/Shop/ShopClickHandler.cs
using UnityEngine;

public class ShopClickHandler : MonoBehaviour
{
    private ShopController _shopController;
    private Collider _collider;

    void Awake()
    {
        _shopController = GetComponentInParent<ShopController>();
        _collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // সব collider check করো, শুধু এই collider-এ hit হলে
        RaycastHit[] hits = Physics.RaycastAll(ray, 1000f);

        foreach (var hit in hits)
        {
            if (hit.collider != _collider) continue;

            if (_shopController == null || !_shopController.IsBuilt) return;

            UpgradeUI upgradeUI = FindFirstObjectByType<UpgradeUI>(FindObjectsInactive.Include);
            if (upgradeUI == null) return;

            Debug.Log($"[ShopClickHandler] Opening: {_shopController.DisplayName}");
            upgradeUI.OpenPanel(_shopController);
            return;
        }
    }
}
