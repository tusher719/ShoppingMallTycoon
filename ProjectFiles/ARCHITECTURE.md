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

---

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
// Priority-based switching via Output Channel
// VC_Gameplay: Output Channel = Default
// Cinematic VCs: Output Channel = Channel02 (inactive until triggered)
// CinemachineBrain: Channel Mask = Default only

public void PlayShopUnlock(float duration = 3f, Action onComplete = null) { ... }
public void PlayLevelComplete(float duration = 5f, Action onComplete = null) { ... }
```

**Cinemachine 3.x Notes:**

- `using Unity.Cinemachine;` (namespace changed from Cinemachine 2.x)
- Priority checkbox does not work — use Output Channel to isolate VCs
- CinemachineBrain Channel Mask = Default (cinematic VCs on separate channel)
- Input System: `EnhancedTouchSupport.Enable()` must be called in OnEnable

### 6. Income Formula

```csharp
// Per-interval income (every 5s per shop, Phase 4)
// LevelManager wired in Phase 6 (Step 35)
float multiplier = Mathf.Pow(1.2f, LevelManager.Instance.CurrentLevelIndex);
float income = baseIncome * multiplier;
```

### 7. AudioManager — BGM + SFX

```csharp
AudioManager.Instance.PlayBGM("bgm_mall");
AudioManager.Instance.StopBGM();
AudioManager.Instance.PlaySFX("sfx_coin");
AudioManager.Instance.PlaySFX("sfx_click");
```

- Two AudioSources: one BGM (loop=true), one SFX (PlayOneShot)
- Volume saved to PlayerPrefs: `"vol_bgm"`, `"vol_sfx"`

### 8. FXManager — object pool

```csharp
FXManager.Instance.PlayFX(FXType.CoinEarn, transform.position);
FXManager.Instance.PlayFX(FXType.ShopUnlock, shopPosition);
FXManager.Instance.PlayFX(FXType.LevelComplete, Vector3.zero);
```

- No Instantiate/Destroy per play — pooled prefabs

### 9. Canvas Scaler — mobile auto-fit

```
Canvas Scaler Settings:
- UI Scale Mode: Scale With Screen Size
- Reference Resolution: 1080 × 1920
- Match: 0.5
```

- `SafeAreaHandler.cs` adjusts RectTransform for notch/punch-hole

### 10. Event System — C# events, no direct references

```csharp
public static event Action<float> OnMoneyChanged;
public static event Action<int> OnCustomerServed;
public static event Action OnLevelComplete;
public static event Action<string> OnShopUnlocked;
```

### 11. Save Key Convention

```
PlayerPrefs keys:
"save_coins"         → float
"save_level"         → int
"save_stars_X"       → int (X = level index)
"save_shop_X_level"  → int
"save_char_gender"   → string ("male"/"female")
"save_char_age"      → string ("young"/"adult"/"senior")
"save_onboarded"     → int (0/1)
"save_tutorial_done" → int (0/1)
"vol_bgm"            → float (0.0 - 1.0)
"vol_sfx"            → float (0.0 - 1.0)
```

### 12. Game Reset

```csharp
PlayerPrefs.DeleteAll();
PlayerPrefs.Save();
SceneManager.LoadScene("Onboarding");
```

### 13. Naming Convention

| Type                   | Convention      | Example                |
| ---------------------- | --------------- | ---------------------- |
| Class                  | PascalCase      | `CustomerController`   |
| Method                 | PascalCase      | `MoveTo()`             |
| Variable               | camelCase       | `currentIncome`        |
| Const                  | UPPER_SNAKE     | `MAX_CUSTOMERS`        |
| ScriptableObject asset | PascalCase_Data | `GroceryShop_Data`     |
| Scene                  | PascalCase      | `Level_01`             |
| Prefab                 | PascalCase      | `Customer_Normal`      |
| Anim clip              | Type_Action     | `Customer_Walk`        |
| FX Prefab              | FX_Name         | `FX_CoinEarn`          |
| Audio clip             | type_name       | `sfx_coin`, `bgm_mall` |

---

## 🎥 Camera Architecture (Cinemachine 3.x)

```
Main Camera
└── CinemachineBrain (Channel Mask: Default only)
    ├── VC_Gameplay      (Output: Default, always live)
    ├── VC_ShopUnlock    (Output: Channel02, triggered via CameraManager)
    ├── VC_NewFloor      (Output: Channel02, triggered via CameraManager)
    ├── VC_MallOverview  (Output: Channel02, triggered via CameraManager)
    └── VC_LevelComplete (Output: Channel02, triggered via CameraManager)
```

---

## 💰 EconomyManager (Phase 4 — Step 22) ✅

```csharp
public static event Action<float> OnMoneyChanged;
public void AddMoney(float amount)    // fires OnMoneyChanged
public bool SpendMoney(float amount)  // returns false if insufficient
// Starting coins: 500
// LevelManager multiplier wired in Phase 6 (Step 35)
```

## 🏪 ShopData ScriptableObject (Phase 4 — Step 23) ✅

```
[CreateAssetMenu] ShopData : ScriptableObject
- shopName, unlockLevel
- baseCost, baseIncome
- maxCustomers
- shopPrefab, shopIcon
Asset: GroceryShop_Data (Cost: 100, Income: 20, MaxCustomers: 2)
```

## 🏗️ ShopController (Phase 4 — Step 25) ✅

```csharp
public void Build()              // sets isBuilt=true, fires OnShopBuilt
void GenerateIncome()            // called every incomeInterval (5s)
public static event Action<ShopController> OnShopBuilt;
```

## 🖥️ BuildUI (Phase 4 — Step 26) ✅

```csharp
// BuildPanel — Bottom Center, 400×120, #1A1A2E
// BtnBuild — #4A90D9, disabled+grey when coins insufficient
// TxtCoins — Top Center, #F5A623
// Button disable logic: btnBuild.interactable = canAfford
```

---

## 🎨 HUD Design Plan (Phase 7 — Step 39)

> Planned — number-driven, fun, always readable

```
TopBar (always visible):
├── 💰 Coins          — current coins + per-min income rate  e.g. "💰 1,250  (+$48/min)"
├── ⭐ Stars           — current level stars earned
├── 📊 Level Progress — % bar + number  e.g. "67% (2/3 goals)"
└── 💎 Gems           — gem count (fast upgrade currency)

BottomBar:
├── 🏪 Shops Built    — e.g. "Shops: 3"
├── 👥 Customers      — active customer count  e.g. "Customers: 7"
├── [Build]  [Upgrade]  [Missions]  buttons

MissionPanel:
├── Task list with reward (coins/gems)
└── Progress bar per task  e.g. "Serve 10 customers  7/10"

LevelCompletePanel:
├── ⭐⭐⭐ star display
├── Coins earned this level
└── Next level button
```

**Gem System (Phase 7):**

- Gems earned via: completing missions, level complete bonus, daily reward
- Gems used for: instant shop upgrade, speed boost
- Gem collect: tap floating gem prefab spawned on milestone

---

## 🧍 Character Architecture

```
CharacterBase (abstract)
├── CustomerController
│   ├── States: Spawned → Walking → Shopping → Queuing → Paying → Leaving
│   └── AnimatorOverrideController (per customer type)
└── StaffController (Phase 2+)
    └── AnimatorOverrideController (per staff type)
```

---

## 🔊 Audio Architecture

```
AudioManager (DontDestroyOnLoad)
├── BGM AudioSource   → PlayBGM(), StopBGM(), FadeBGM()
└── SFX AudioSource   → PlaySFX(string key) → PlayOneShot()

Audio/
├── BGM/  bgm_menu.mp3, bgm_mall.mp3
└── SFX/  sfx_click, sfx_coin, sfx_customer_arrive,
          sfx_customer_pay, sfx_shop_unlock, sfx_level_complete
```

---

## ✨ FX Architecture

```
FXManager (Singleton) — Object Pool per FX type
├── Pool: FX_CoinEarn    (size: 5)
├── Pool: FX_ShopUnlock  (size: 3)
├── Pool: FX_LevelComplete (size: 1)
└── Pool: FX_GemCollect  (size: 5)  ← NEW (Phase 7)

Trigger points:
- EconomyManager.AddMoney()    → FX_CoinEarn
- ShopController.Build()       → FX_ShopUnlock
- LevelManager.CompleteLevel() → FX_LevelComplete
- Milestone reached            → FX_GemCollect (tap to collect)
```

---

## 📱 UI Architecture

```
Canvas (Screen Space - Overlay)
└── Canvas Scaler: Scale With Screen Size, 1080×1920, match 0.5
    ├── SafeArea (SafeAreaHandler.cs)
    │   ├── TopBar
    │   │   ├── TxtCoins + TxtIncomeRate   ← NEW
    │   │   ├── TxtStars
    │   │   ├── LevelProgressBar + TxtProgress  ← NEW
    │   │   └── TxtGems                    ← NEW
    │   ├── BottomBar
    │   │   ├── TxtShopCount               ← NEW
    │   │   ├── TxtCustomerCount           ← NEW
    │   │   └── [Build] [Upgrade] [Missions] buttons
    │   ├── BuildPanel ✅ DONE
    │   ├── UpgradePanel
    │   ├── MissionPanel
    │   └── LevelCompletePanel
    └── TutorialPanel ✅ DONE
```

---

## 💾 Save Architecture

```
Phase 1 (MVP): SaveManager → PlayerPrefs
Phase 2 (post-MVP): SaveManager → JSON → persistentDataPath/save.json
Future: SaveManager → REST API → Remote DB + User Auth
```
