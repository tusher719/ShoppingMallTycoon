# 🏬 Shopping Mall Tycoon — Architecture & Rules

## 🏗️ Architecture Overview

```
┌─────────────────────────────────────────────┐
│                 GameManager                  │
│         (singleton, scene control)           │
└──────┬──────────┬───────────┬───────────────┘
       │          │           │
  EconomyMgr  LevelMgr   SaveManager
       │          │
  ShopCtrl   ObjectiveMgr
       │
  CustomerSpawner
       │
  CustomerController (per instance)
       │
  CharacterBase (shared)
```

## 📐 Project Rules

### 1. Singleton Pattern
`GameManager`, `EconomyManager`, `CameraManager`, `SaveManager`, `UIManager` — সব Singleton।
```csharp
public static GameManager Instance { get; private set; }
void Awake() { if (Instance == null) Instance = this; else Destroy(gameObject); }
```

### 2. ScriptableObject for Data
Shop config, level config, customer config — সব ScriptableObject।
Script-এ hardcode করবে না।

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

### 4. AnimatorOverrideController — custom animation
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

### 7. Event System — UnityEvents বা C# events
Manager-to-Manager কথা বলবে events দিয়ে, সরাসরি reference নয়।
```csharp
public static event Action<float> OnMoneyChanged;
public static event Action<int> OnCustomerServed;
public static event Action OnLevelComplete;
```

### 8. Save Key Convention
```
PlayerPrefs keys:
"save_coins"         → float
"save_level"         → int
"save_stars_X"       → int (X = level index)
"save_shop_X_level"  → int
"save_char_gender"   → string ("male"/"female")
"save_char_age"      → string ("young"/"adult"/"senior")
"save_onboarded"     → int (0/1)
```

### 9. Reset — সব key একসাথে clear
```csharp
PlayerPrefs.DeleteAll();
PlayerPrefs.Save();
SceneManager.LoadScene("Onboarding");
```

### 10. Naming Convention
| Type | Convention | Example |
|------|-----------|---------|
| Class | PascalCase | `CustomerController` |
| Method | PascalCase | `MoveTo()` |
| Variable | camelCase | `currentIncome` |
| Const | UPPER_SNAKE | `MAX_CUSTOMERS` |
| ScriptableObject | PascalCase_Data | `GroceryShop_Data` |
| Scene | PascalCase | `Level_01` |
| Prefab | PascalCase | `Customer_Normal` |
| Anim clip | Type_Action | `Customer_Walk` |

---

## 🎥 Camera Architecture

```
Main Camera
└── Cinemachine Brain
    ├── VC_Gameplay      (priority 10, default)
    ├── VC_ShopUnlock    (priority 20, triggered)
    ├── VC_NewFloor      (priority 20, triggered)
    ├── VC_MallOverview  (priority 20, triggered)
    ├── VC_Achievement   (priority 20, triggered)
    └── VC_LevelComplete (priority 20, triggered)
```
Gameplay camera always priority 10. Cinematic VC bumped to 20 on trigger, then back to 0 after.

---

## 🧍 Character Architecture

```
CharacterBase (abstract)
├── CustomerController
│   ├── State: Spawned → Walking → Shopping → Queuing → Paying → Leaving
│   └── Uses: AnimatorOverrideController
└── StaffController (Phase 2+)
    └── Uses: AnimatorOverrideController
```

---

## 💾 Save Architecture

```
Phase 1 (MVP):
SaveManager → PlayerPrefs → string/int/float keys

Phase 2 (post-MVP):
SaveManager → JSON → Application.persistentDataPath/save.json

Future:
SaveManager → REST API → Remote DB
```
