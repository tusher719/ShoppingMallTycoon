// Assets/_Game/Scripts/Shop/GridManager.cs
using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [Header("Grid Settings")]
    [SerializeField] private int columns = 3;
    [SerializeField] private int rows = 3;
    [SerializeField] private float tileSize = 5f;
    [SerializeField] private Vector3 gridOrigin = new Vector3(-5f, 0f, 0f);

    private Dictionary<Vector2Int, bool> _occupiedTiles = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    // পরবর্তী ফাঁকা tile-এর world position return করে
    public bool TryGetNextTile(out Vector3 worldPos)
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                var key = new Vector2Int(col, row);
                if (!_occupiedTiles.ContainsKey(key) || !_occupiedTiles[key])
                {
                    _occupiedTiles[key] = true;
                    worldPos = GridToWorld(col, row);
                    return true;
                }
            }
        }
        worldPos = Vector3.zero;
        return false;  // grid full
    }

    private Vector3 GridToWorld(int col, int row)
    {
        return gridOrigin + new Vector3(col * tileSize, 0f, row * tileSize);
    }

    public int MaxShops => columns * rows;
}
