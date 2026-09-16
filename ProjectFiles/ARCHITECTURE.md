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

`GameManager`, `EconomyManager`, `CameraManager`, `AudioManager`, `FXManager`, `SaveManager`, `UIManager` — সব Singleton।

```csharp
public static T Instance { get; private set; }
void Awake() { if (Instance == null) Instance = this; else Destroy(gameObject); }
```

### 2. ScriptableObject for Data

Shop config, level config, customer config — সব ScriptableObject। Script-এ hardcode করবে না।

### 3. CharacterBase — সব character এই class extend করবে

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

### 5. CameraManager — event-driven switch

```csharp
public void SwitchToGameplay() { ... }
public void PlayCinematic(CinematicType type, Vector3 focusPoint, Action onComplete) { ... }
```

### 6. Income Formula

```csharp
float multiplier = Mathf.Pow(1.2f, LevelManager.Instance.CurrentLevelIndex);
float income = baseIncome * multiplier;
```

### 7. AudioManager — BGM + SFX

```csharp
// BGM
AudioManager.Instance.PlayBGM("bgm_mall");
AudioManager.Instance.StopBGM();

// SFX
AudioManager.Instance.PlaySFX("sfx_coin");
AudioManager.Instance.PlaySFX("sfx_click");
```

- Two AudioSource: one BGM (loop=true), one SFX (PlayOneShot)
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

## 🎥 Camera Architecture

```
Main Camera
└── Cinemachine Brain
    ├── VC_Gameplay      (priority 10, default, always active)
    ├── VC_ShopUnlock    (priority 20, triggered on shop build)
    ├── VC_NewFloor      (priority 20, triggered on floor unlock)
    ├── VC_MallOverview  (priority 20, triggered on overview)
    ├── VC_Achievement   (priority 20, triggered on achievement)
    └── VC_LevelComplete (priority 20, triggered on level end)
```

Cinematic VC bumped to priority 20 on trigger → plays → returns to 0. Gameplay always at 10.

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
├── BGM/
│   ├── bgm_menu.mp3
│   └── bgm_mall.mp3
└── SFX/
    ├── sfx_click.wav
    ├── sfx_coin.wav
    ├── sfx_customer_arrive.wav
    ├── sfx_customer_pay.wav
    ├── sfx_shop_unlock.wav
    └── sfx_level_complete.wav
```

---

## ✨ FX Architecture

```
FXManager (Singleton)
└── Object Pool per FX type
    ├── Pool: FX_CoinEarn    (size: 5)
    ├── Pool: FX_ShopUnlock  (size: 3)
    └── Pool: FX_LevelComplete (size: 1)

Trigger points:
- EconomyManager.AddMoney() → FXManager.PlayFX(CoinEarn, pos)
- ShopController.BuildShop() → FXManager.PlayFX(ShopUnlock, pos)
- LevelManager.CompleteLevel() → FXManager.PlayFX(LevelComplete, center)
```

---

## 📱 UI Architecture

```
Canvas (Screen Space - Overlay)
└── Canvas Scaler: Scale With Screen Size, 1080×1920, match 0.5
    ├── SafeArea (SafeAreaHandler.cs adjusts this)
    │   ├── TopBar (money, stars, level)
    │   ├── BottomBar (build, upgrade, missions buttons)
    │   ├── BuildPanel
    │   ├── UpgradePanel
    │   ├── MissionPanel
    │   └── LevelCompletePanel
    └── (outside SafeArea — decorative bg elements)
```

---

## 💾 Save Architecture

```
Phase 1 (MVP):
SaveManager → PlayerPrefs → string/int/float keys

Phase 2 (post-MVP):
SaveManager → JSON → Application.persistentDataPath/save.json

Future:
SaveManager → REST API → Remote DB + User Auth
```
