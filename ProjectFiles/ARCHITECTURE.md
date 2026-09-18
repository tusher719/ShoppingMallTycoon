# 🏬 Shopping Mall Tycoon — Architecture & Rules

## 🏗️ Architecture Overview

```
┌─────────────────────────────────────────────────────┐
│                    GameManager                       │
│            (singleton, scene control)                │
└──────┬──────────┬───────────┬────────┬──────────────┘
       │          │           │        │
  EconomyMgr  LevelMgr   SaveMgr  AudioMgr
       │          │                    │
  ShopCtrl   ObjectiveMgr          FXManager
       │       SatisfactionMgr
  CustomerSpawner
       │
  CustomerController (per instance)
       │
  CharacterBase (shared)
```

## 📐 Project Rules

### 1. Singleton Pattern

`GameManager`, `EconomyManager`, `CameraManager`, `AudioManager`, `FXManager`, `SaveManager`, `UIManager`, `GridManager`, `LevelObjectiveManager`, `SatisfactionManager`, `LevelManager` — all Singletons.

```csharp
public static T Instance { get; private set; }
void Awake() { if (Instance == null) Instance = this; else Destroy(gameObject); }
```

### 2. ScriptableObject for Data

Shop config, level config, customer config — all ScriptableObjects. Never hardcode in scripts.

### 3. CharacterBase — all characters extend this class

```csharp
public abstract class CharacterBase : MonoBehaviour {
    protected Animator animator;
    protected NavMeshAgent agent;
    public abstract void Initialize();
    public void MoveTo(Vector3 target) { agent.SetDestination(target); }
    public void PlayAnim(string trigger) { animator.SetTrigger(trigger); }
}
```

### 4. AnimatorOverrideController — custom animation per character

```csharp
var overrideCtrl = new AnimatorOverrideController(baseAnimator.runtimeAnimatorController);
overrideCtrl["Walk"] = customWalkClip;
animator.runtimeAnimatorController = overrideCtrl;
```

### 5. CameraManager — event-driven switch (Cinemachine 3.x)

```csharp
public void PlayShopUnlock(float duration = 3f, Action onComplete = null) { ... }
public void PlayLevelComplete(float duration = 5f, Action onComplete = null) { ... }
```

**Cinemachine 3.x Notes:**

- `using Unity.Cinemachine;`
- Priority checkbox does not work — use Output Channel to isolate VCs
- CinemachineBrain Channel Mask = Default only
- `EnhancedTouchSupport.Enable()` must be called in OnEnable

### 6. Income Formula

```csharp
float multiplier = Mathf.Pow(1.2f, LevelManager.Instance.CurrentLevelIndex);
float income = baseIncome * tierMultiplier * levelMultiplier;
```

### 7. Dynamic Shop Cost (Phase 7 — pending)

```csharp
float cost = baseCost * Mathf.Pow(1.5f, shopsBuilt);
```

### 8. AudioManager — BGM + SFX

```csharp
AudioManager.Instance.PlayBGM("bgm_mall");
AudioManager.Instance.StopBGM();
AudioManager.Instance.PlaySFX("sfx_coin");
```

- Two AudioSources: BGM (loop=true), SFX (PlayOneShot)
- Volume saved: `"vol_bgm"`, `"vol_sfx"`

### 9. FXManager — object pool

```csharp
FXManager.Instance.PlayFX(FXType.CoinEarn, transform.position);
FXManager.Instance.PlayFX(FXType.ShopUnlock, shopPosition);
FXManager.Instance.PlayFX(FXType.LevelComplete, Vector3.zero);
```

- No Instantiate/Destroy per play — pooled prefabs

### 10. Canvas Scaler — mobile auto-fit

```
Scale With Screen Size | 1080×1920 | Match: 0.5
```

`SafeAreaHandler.cs` adjusts RectTransform for notch/punch-hole

### 11. Event System — C# events, no direct references

```csharp
public static event Action<float> OnMoneyChanged;
public static event Action<float> OnMoneyAdded;
public static event Action OnCustomerServed;
public static event Action OnCustomerWaiting;
public static event Action OnCustomerLeft;
public static event Action OnLevelComplete;
public static event Action<ShopController> OnShopBuilt;
public static event Action<ShopController> OnShopUpgraded;
public static event Action<int, int> OnShopsProgress;
public static event Action<int, int> OnCustomersProgress;
public static event Action<float, float> OnMoneyProgress;
public static event Action OnAllObjectivesComplete;
public static event Action<float> OnSatisfactionChanged;
public static event Action OnHighSatisfaction;
public static event Action OnLowSatisfaction;
```

### 12. Save Key Convention

```
"save_coins"         → float
"save_level"         → int
"save_stars_X"       → int
"save_shop_X_level"  → int
"save_char_gender"   → string ("male"/"female")
"save_char_age"      → string ("young"/"adult"/"senior")
"save_onboarded"     → int (0/1)
"save_tutorial_done" → int (0/1)
"vol_bgm"            → float (0.0-1.0)
"vol_sfx"            → float (0.0-1.0)
```

### 13. Game Reset

```csharp
PlayerPrefs.DeleteAll();
PlayerPrefs.Save();
SceneManager.LoadScene("Onboarding");
```

### 14. Naming Convention

| Type             | Convention      | Example                |
| ---------------- | --------------- | ---------------------- |
| Class            | PascalCase      | `CustomerController`   |
| Method           | PascalCase      | `MoveTo()`             |
| Variable         | camelCase       | `currentIncome`        |
| Const            | UPPER_SNAKE     | `MAX_CUSTOMERS`        |
| ScriptableObject | PascalCase_Data | `GroceryShop_Data`     |
| Scene            | PascalCase      | `Level_01`             |
| Prefab           | PascalCase      | `Customer_Normal`      |
| Anim clip        | Type_Action     | `Customer_Walk`        |
| FX Prefab        | FX_Name         | `FX_CoinEarn`          |
| Audio clip       | type_name       | `sfx_coin`, `bgm_mall` |

### 15. Shop Unique ID

```csharp
private static int _shopCounter = 0;
public int ShopID { get; private set; }
public string DisplayName { get; private set; }
// DisplayName = $"{shopData.shopName} #{ShopID}"
// gameObject.name = DisplayName on Build()
```

### 16. Grid System

```csharp
GridManager.Instance.TryGetNextTile(out Vector3 worldPos);
// Grid: 3×3, tileSize 5, origin (-5, 0, 0)
// Max shops = columns × rows = 9
```

### 17. Shop Click Detection

```csharp
// ShopClickHandler on ShopBody
// Physics.RaycastAll — handles overlapping colliders
// Main Camera needs PhysicsRaycaster component
// Finds UpgradeUI with FindFirstObjectByType(FindObjectsInactive.Include)
```

### 18. Satisfaction System

```
Satisfaction = BaseSatisfaction(70) - WaitingPenalty(2/customer) + ServedBonus(1.5/customer)
> 80% → OnHighSatisfaction (spawn rate boost — pending)
< 30% → OnLowSatisfaction (customers leave — pending)
```

---

## 🎥 Camera Architecture (Cinemachine 3.x)

```
Main Camera (+ PhysicsRaycaster)
└── CinemachineBrain (Channel Mask: Default only)
    ├── VC_Gameplay      (Output: Default, always live)
    ├── VC_ShopUnlock    (Output: Channel02)
    ├── VC_NewFloor      (Output: Channel02)
    ├── VC_MallOverview  (Output: Channel02)
    └── VC_LevelComplete (Output: Channel02)
```

| VC               | Position   | Rotation | Ortho Size | Channel   |
| ---------------- | ---------- | -------- | ---------- | --------- |
| VC_Gameplay      | (0,20,-15) | (45,0,0) | 10         | Default   |
| VC_ShopUnlock    | (0,15,-10) | (45,0,0) | 7          | Channel02 |
| VC_NewFloor      | (0,25,-20) | (50,0,0) | 15         | Channel02 |
| VC_MallOverview  | (0,35,-25) | (55,0,0) | 20         | Channel02 |
| VC_LevelComplete | (0,18,-12) | (45,0,0) | 8          | Channel02 |

---

## 💰 EconomyManager ✅

```csharp
public static event Action<float> OnMoneyChanged;
public static event Action<float> OnMoneyAdded;   // earned amount (not balance)
public void AddMoney(float amount)    // fires both events
public bool SpendMoney(float amount)  // returns false if insufficient
// Starting coins: 500
```

## 🏪 ShopData ScriptableObject ✅

```
[CreateAssetMenu] → MallTycoon/Shop Data
Fields: shopName, unlockLevel, baseCost, baseIncome, maxCustomers, shopPrefab, shopIcon
       upgradeTiers: UpgradeTier[] (tierName, upgradeCost, incomeMultiplier, maxCustomers)
Asset: GroceryShop_Data
  Base: Cost:100, Income:20/5s, MaxCustomers:2
  Tier 1: multiplier 1.0, maxCustomers 2, upgradeCost 0
  Tier 2: multiplier 1.5, maxCustomers 4, upgradeCost 150
  Tier 3: multiplier 2.25, maxCustomers 6, upgradeCost 300
⚠️ Always use: public ShopData Data => shopData; — never expose field directly
```

## 🏗️ ShopController ✅

```csharp
public void Build()           // isBuilt=true, sets DisplayName, fires OnShopBuilt
void GenerateIncome()         // every 5s → baseIncome * tierMultiplier * levelMultiplier
public bool Upgrade()         // spends money, _currentTier++, fires OnShopUpgraded
public bool CanUpgrade()
public float GetUpgradeCost()
public float GetCurrentMultiplier()
public string GetTierName()
public int GetMaxCustomers()
public int ShopID             // unique, static counter
public string DisplayName     // "Grocery Shop #1"
public static event Action<ShopController> OnShopBuilt;
public static event Action<ShopController> OnShopUpgraded;
public ShopData Data => shopData;
public bool IsBuilt => _isBuilt;
public int CurrentTier => _currentTier;
```

## 🖥️ BuildUI ✅

```
BuildPanel — Bottom Center, #1A1A2E
BtnBuild   — #4A90D9, grey+disabled when coins < baseCost, "Max" when grid full
TxtBtnLabel — updates dynamically
Max Shops: 9 (GridManager 3×3)
Shops spawn in --- Mall --- parent via GridManager.TryGetNextTile()
```

## 🧍 CustomerController ✅

```csharp
// States: Spawned → Walking → Shopping → Paying → Leaving
// shoppingDuration: 5s, payingDuration: 2s
// Payment: baseIncome * tierMultiplier * levelMultiplier
// OnCustomerWaiting fires on shop arrival
// OnCustomerLeft fires before Destroy
// Exit target: (0, 0, -18) then Destroy
public ShopController targetShop;
public static event Action OnCustomerServed;
public static event Action OnCustomerWaiting;
public static event Action OnCustomerLeft;
```

## 👥 CustomerSpawner ✅

```csharp
// Event-driven: ShopController.OnShopBuilt → adds to _builtShops list
// Random shop selection from _builtShops on each spawn
// spawnInterval: 8s, maxCustomers: 5
// SpawnPoint position: (0, 0, -15)
// Notifies HUDManager.OnCustomerSpawned()
// ⚠️ Never assign Target Shop in Inspector
```

## 🎯 LevelObjectiveManager ✅

```csharp
// Tracks: shopsBuilt, customersServed, moneyEarned
// Targets (Inspector): 3 shops, 20 customers, $500 earned
// Events: OnShopsProgress, OnCustomersProgress, OnMoneyProgress, OnAllObjectivesComplete
// Listens: ShopController.OnShopBuilt, CustomerController.OnCustomerServed, EconomyManager.OnMoneyAdded
```

## 😊 SatisfactionManager ✅

```csharp
// Base: 70, High threshold: 80, Low threshold: 30
// +1.5 per customer served, -2 per customer waiting
// Events: OnSatisfactionChanged, OnHighSatisfaction, OnLowSatisfaction
```

## ⭐ LevelManager ✅

```csharp
// Listens: LevelObjectiveManager.OnAllObjectivesComplete
// Star calc: satisfaction >= 80 → 3★, >= 50 → 2★, else 1★
// Saves: save_stars_X, save_level
// GetIncomeMultiplier(): Mathf.Pow(1.2f, currentLevelIndex)
// Event: OnLevelComplete(int stars)
```

## 🗺️ GridManager ✅

```csharp
// 3×3 grid, tileSize 5, origin (-5, 0, 0)
// TryGetNextTile(out Vector3 worldPos) — row-major order
// MaxShops = 9
```

## 🖥️ MissionUI ✅

```csharp
// Shows: Shops X/3, Customers X/20, Earned $X/500
// LevelCompletePanel: activates on OnLevelComplete, shows Stars X/3
// References: TxtShops, TxtCustomers, TxtMoney, LevelCompletePanel, TxtStars
```

## 🖥️ UpgradeUI ✅

```csharp
// Opens on ShopClickHandler → ShopBody click (Physics.RaycastAll)
// Shows: DisplayName, CurrentTier, Cost: -$X, Income: $X → $Y/5s
// Max Level: shows current income only
// BtnUpgrade: disabled when can't afford or max tier
```

## 🖥️ HUDManager ✅

```csharp
// TopBar (left box): Coins, +X/min, Shops: X, Customers: X
// Income rate: recalculates every 1s from all built shops
// Upgrade event → rate updates instantly
// OnCustomerSpawned() called by CustomerSpawner
```

## 🖥️ UI Architecture (Canvas)

```
Canvas (Screen Space - Overlay, 1080×1920, match 0.5)
├── TutorialPanel ✅
├── BuildPanel ✅
├── TxtCoins (disabled — HUD handles this) ✅
├── MissionPanel ✅ (Top Right)
│   ├── TxtTitle
│   └── Content (Vertical Layout Group)
│       ├── TxtShops
│       ├── TxtCustomers
│       └── TxtMoney
├── UpgradePanel ✅ (Center)
│   ├── TxtShopName
│   ├── TxtCurrentTier
│   ├── TxtUpgradeCost
│   ├── TxtIncomeInfo
│   ├── BtnUpgrade
│   └── BtnClose
├── LevelCompletePanel ✅ (Center, starts inactive)
│   ├── TxtTitle
│   └── TxtStars
└── HUDPanel ✅
    └── TopBar (Top Left box)
        ├── TxtCoinsHUD
        ├── TxtIncomeRate
        ├── TxtShopCount
        └── TxtCustomerCount
```

## 🧍 Character Architecture

```
CharacterBase (abstract)
├── CustomerController ✅
│   ├── States: Spawned→Walking→Shopping→Paying→Leaving
│   └── AnimatorOverrideController (per customer type)
└── StaffController (Phase 2+, not MVP)
```

## 🔊 Audio Architecture (Phase 7B — pending)

```
AudioManager (DontDestroyOnLoad)
├── BGM AudioSource → PlayBGM(), StopBGM(), FadeBGM()
└── SFX AudioSource → PlaySFX(string key) → PlayOneShot()
```

## ✨ FX Architecture (Phase 7B — pending)

```
FXManager (Singleton) — Object Pool per FX type
├── Pool: FX_CoinEarn     (size: 5)
├── Pool: FX_ShopUnlock   (size: 3)
├── Pool: FX_LevelComplete (size: 1)
└── Pool: FX_GemCollect   (size: 5)
```

## 💾 Save Architecture

```
Phase 1 (MVP): PlayerPrefs
Phase 2: JSON → persistentDataPath/save.json
Future: REST API → Remote DB
```

## ⚠️ Key Lessons

- Cinemachine 3.x Priority → use Output Channel instead
- `EnhancedTouchSupport.Enable()` in OnEnable
- Buttons: AddListener in script, not Inspector OnClick
- Materials: URP/Lit or URP/Simple Lit only
- CustomerSpawner Target Shop — event-driven, never Inspector assign
- ShopController.Data property — never .shopData direct
- BuildUI instantiates shop clones — use event to track, not original prefab
- Orthographic camera: OnMouseDown broken → use Physics.RaycastAll in Update()
- FindFirstObjectByType(FindObjectsInactive.Include) — finds inactive panels
- Shop clone parent: GameObject.Find("--- Mall ---") for hierarchy organization
- \_shopCounter is static — resets only on domain reload (not scene reload)
- TMP emoji (Unicode > U+FFFF) not supported by LiberationSans SDF — use plain text
