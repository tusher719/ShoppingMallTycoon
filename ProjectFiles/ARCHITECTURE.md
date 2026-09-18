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
       │
  CustomerSpawner
       │
  CustomerController (per instance)
       │
  CharacterBase (shared)
```

## 📐 Project Rules

### 1. Singleton Pattern

`GameManager`, `EconomyManager`, `CameraManager`, `AudioManager`, `FXManager`, `SaveManager`, `UIManager` — all Singletons.

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
// LevelManager wired in Phase 6 (Step 35)
// Placeholder multiplier = 1f until then
float multiplier = Mathf.Pow(1.2f, LevelManager.Instance.CurrentLevelIndex);
float income = baseIncome * multiplier;
```

### 7. Dynamic Shop Cost (Phase 7)

```csharp
// প্রতিটা নতুন shop build করলে দাম বাড়বে
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
public static event Action OnCustomerServed;
public static event Action OnLevelComplete;
public static event Action<ShopController> OnShopBuilt;
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

---

## 🎥 Camera Architecture (Cinemachine 3.x)

```
Main Camera
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
public void AddMoney(float amount)    // fires OnMoneyChanged
public bool SpendMoney(float amount)  // returns false if insufficient
// Starting coins: 500
```

## 🏪 ShopData ScriptableObject ✅

```
[CreateAssetMenu] → MallTycoon/Shop Data
Fields: shopName, unlockLevel, baseCost, baseIncome, maxCustomers, shopPrefab, shopIcon
Asset: GroceryShop_Data (Cost:100, Income:20/5s, MaxCustomers:2)
⚠️ Always use: public ShopData Data => shopData; — never expose field directly
```

## 🏗️ ShopController ✅

```csharp
public void Build()           // isBuilt=true, fires OnShopBuilt
void GenerateIncome()         // every 5s → EconomyManager.AddMoney
public static event Action<ShopController> OnShopBuilt;
public ShopData Data => shopData;  // always use this property
public bool IsBuilt => isBuilt;
```

## 🖥️ BuildUI ✅

```
BuildPanel — Bottom Center, 400×120, #1A1A2E
BtnBuild   — #4A90D9, grey+disabled when coins < baseCost
TxtCoins   — Top Center, #F5A623, updates via OnMoneyChanged
Shop spawns at (0,0,5) fixed — grid placement Phase 7
```

## 🧍 CustomerController ✅

```csharp
// States: Spawned → Walking → Shopping → Paying → Leaving
// shoppingDuration: 5s, payingDuration: 2s
// Payment: baseIncome * multiplier (multiplier=1f until Phase 6 Step 35)
// Exit target: (0, 0, -18) then Destroy
public ShopController targetShop;
public static event Action OnCustomerServed;
```

## 👥 CustomerSpawner ✅

```csharp
// ⚠️ Event-driven: ShopController.OnShopBuilt → sets _targetShop automatically
// ⚠️ Never assign Target Shop in Inspector — BuildUI instantiates clones, not original
// spawnInterval: 8s, maxCustomers: 5
// SpawnPoint position: (0, 0, -15)
```

---

## 🎨 HUD Plan (Phase 7 — Step 39)

```
TopBar:
├── 💰 Coins + per-min income rate    e.g. "💰 1,250  (+$48/min)"
├── 📊 Level progress bar + % + goal  e.g. "67% (2/3 goals)"
└── 💎 Gems                           e.g. "💎 12"

BottomBar:
├── 🏪 Shop count                     e.g. "Shops: 3"
├── 👥 Active customer count          e.g. "Customers: 7"
└── [Build] [Upgrade] [Missions] buttons

MissionPanel: task list + reward + per-task progress bar
LevelCompletePanel: ⭐⭐⭐ + coins earned + next level
```

---

## 🧍 Character Architecture

```
CharacterBase (abstract)
├── CustomerController ✅
│   ├── States: Spawned→Walking→Shopping→Paying→Leaving
│   └── AnimatorOverrideController (per customer type)
└── StaffController (Phase 2+, not MVP)
```

## 🔊 Audio Architecture

```
AudioManager (DontDestroyOnLoad)
├── BGM AudioSource → PlayBGM(), StopBGM(), FadeBGM()
└── SFX AudioSource → PlaySFX(string key) → PlayOneShot()

Audio/
├── BGM/ bgm_menu.mp3, bgm_mall.mp3
└── SFX/ sfx_click, sfx_coin, sfx_customer_arrive,
         sfx_customer_pay, sfx_shop_unlock, sfx_level_complete
```

## ✨ FX Architecture

```
FXManager (Singleton) — Object Pool per FX type
├── Pool: FX_CoinEarn     (size: 5)
├── Pool: FX_ShopUnlock   (size: 3)
├── Pool: FX_LevelComplete (size: 1)
└── Pool: FX_GemCollect   (size: 5)

Trigger points:
- EconomyManager.AddMoney()    → FX_CoinEarn
- ShopController.Build()       → FX_ShopUnlock
- LevelManager.CompleteLevel() → FX_LevelComplete
- Milestone reached            → FX_GemCollect
```

## 📱 UI Architecture

```
Canvas (Screen Space - Overlay)
└── Canvas Scaler: 1080×1920, match 0.5
    ├── SafeArea (SafeAreaHandler.cs)
    │   ├── TopBar (Phase 7)
    │   ├── BottomBar (Phase 7)
    │   ├── BuildPanel ✅
    │   ├── UpgradePanel (Phase 7)
    │   ├── MissionPanel (Phase 6)
    │   └── LevelCompletePanel (Phase 6)
    └── TutorialPanel ✅
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
- BuildUI instantiates shop clones — original never gets Build() called
