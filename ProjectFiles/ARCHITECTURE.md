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

- Cinematic VCs live on Channel02, triggered via CameraManager script
- VC_Gameplay is always live on the Default channel

### Camera Controls

- Input System: New Input System Package (Unity.InputSystem)
- PC: Mouse drag pan + scroll wheel zoom
- Android: Single finger drag pan + pinch zoom
- `EnhancedTouchSupport.Enable()` must be called in OnEnable
- Pan bounds: X(-15 to 15), Z(-15 to 15)
- Zoom bounds: OrthographicSize(5 to 20)

---

## 🎨 Material & Lighting Setup (URP)

### Lighting Rules

- **Render Pipeline:** URP (Universal Render Pipeline)
- All materials must use **URP/Lit** or **URP/Simple Lit** shader
- Never use Standard shader — it turns pink in URP

### Scene Lighting

```
Directional Light:
- Rotation: (50, -30, 0)
- Intensity: 1.0
- Color: Warm white (#FFF5E0)
- Shadow Type: Soft Shadows
- Shadow Strength: 0.5

Environment:
- Skybox: URP default or solid color (mobile performance)
- Ambient Mode: Flat
- Ambient Color: soft blue-grey (#8AA0B0)
- Fog: disabled (mobile performance)
```

### URP Asset Settings (mobile-optimized)

```
Rendering:
- Depth Texture: off
- Opaque Texture: off

Quality:
- Anti Aliasing: 2x (or off for low-end)
- Render Scale: 1.0

Lighting:
- Main Light: Per Pixel
- Additional Lights: Disabled (mobile performance)
- Cast Shadows: on (main light only)

Post Processing: off (Phase 1 MVP)
```

### Material Conventions

| Object Type  | Shader              | Notes                   |
| ------------ | ------------------- | ----------------------- |
| Mall floor   | URP/Lit             | Albedo only, smooth=0   |
| Walls        | URP/Lit             | Albedo only             |
| Shop prefabs | URP/Lit             | Low-poly, bright colors |
| Characters   | URP/Simple Lit      | Mobile-friendly         |
| FX particles | URP/Particles/Unlit | Additive blend          |

### Low-Poly Color Palette

```
Floor:    #E8DCC8  (warm beige)
Walls:    #F5F0E8  (off-white)
Accent:   #4A90D9  (blue — shop highlights)
Income:   #F5A623  (gold — coin/money UI)
Success:  #7ED321  (green — objectives)
```

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
- EconomyManager.AddMoney()   → FXManager.PlayFX(CoinEarn, pos)
- ShopController.BuildShop()  → FXManager.PlayFX(ShopUnlock, pos)
- LevelManager.CompleteLevel()→ FXManager.PlayFX(LevelComplete, center)
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
    └── TutorialPanel
        ├── Overlay (black, alpha 0.4)
        ├── Arrow (Image, #F5A623, rotated per step)
        └── MessageBox (#1A1A2E)
            └── MessageText (TMP, white, size 36)
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
