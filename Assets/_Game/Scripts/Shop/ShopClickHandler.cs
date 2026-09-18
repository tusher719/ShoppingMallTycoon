// Assets/_Game/Scripts/Shop/ShopClickHandler.cs
using UnityEngine;

public class ShopClickHandler : MonoBehaviour
{
    private ShopController _shopController;

    void Awake()
    {
        _shopController = GetComponentInParent<ShopController>();
    }

    void OnMouseDown()
    {
        if (_shopController == null || !_shopController.IsBuilt) return;
        UpgradeUI upgradeUI = FindFirstObjectByType<UpgradeUI>();
        if (upgradeUI != null)
            upgradeUI.OpenPanel(_shopController);
    }
}
