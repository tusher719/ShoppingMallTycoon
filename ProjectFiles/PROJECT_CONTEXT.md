# 🏬 Shopping Mall Tycoon — Project Context

> Paste this file into a new chat to give Claude full project context instantly.

---

## 👤 Developer Info

- **Name:** Mehedi (Tusher)
- **OS:** Windows, Git Bash terminal
- **Unity Version:** Unity 6 (6000.0.47f1) — URP template
- **Platform:** Android (primary), PC (secondary)
- **Repo:** https://github.com/tusher719/ShoppingMallTycoon

---

## 🎮 Game Overview

- **Genre:** Mobile Tycoon / Incremental Management
- **Engine:** Unity 6 (URP)
- **Core Loop:** Build Shop → Customers Arrive → Shop → Checkout → Earn → Upgrade → Expand → Level Complete
- **Internet:** Offline-first. Optional user profile + remote DB planned for future (not MVP)
- **Graphics:** 3D Low-Poly, Isometric view

---

## 📷 Camera System ✅ DONE

### Setup (Cinemachine 3.1.7)

- `using Unity.Cinemachine;` — namespace is different from Cinemachine 2.x
- **CinemachineBrain:** on Main Camera, Channel Mask = Default only
- **VC_Gameplay:** Output Channel = Default, Orthographic size 10, Position (0,20,-15), Rotation (45,0,0) — always live
- **Cinematic VCs:** Output Channel = Channel02 (Default unchecked) — triggered via CameraManager script

### Virtual Cameras

| VC               | Position   | Rotation | Ortho Size | Channel   |
| ---------------- | ---------- | -------- | ---------- | --------- |
| VC_Gameplay      | (0,20,-15) | (45,0,0) | 10         | Default   |
| VC_ShopUnlock    | (0,15,-10) | (45,0,0) | 7          | Channel02 |
| VC_NewFloor      | (0,25,-20) | (50,0,0) | 15         | Channel02 |
| VC_MallOverview  | (0,35,-25) | (55,0,0) | 20         | Channel02 |
| VC_LevelComplete | (0,18,-12) | (45,0,0) | 8          | Channel02 |

### Camera Controls (New Input System)

- `EnhancedTouchSupport.Enable()` must be called in OnEnable
- PC: Mouse drag pan + scroll wheel zoom
- Android: Single finger drag pan + pinch zoom
- Pan bounds: X(-15,15), Z(-15,15) | Zoom: OrthographicSize(5,20)

---

## 🧍 Character & Animation System ✅ DONE

### Architecture (Modular)

- Each character has a `CharacterBase` component
- Shared `BaseAnimator` controller with states per role
- Custom animations per type via `AnimatorOverrideController`
- Humanoid rig — swap mesh, keep animator

### Character Types

| Type               | Animations                      |
| ------------------ | ------------------------------- |
| Normal Customer    | Walk, Idle, Browse, Pay         |
| Rich Customer      | Walk (fancy), Idle, Pay         |
| Impatient Customer | Walk (fast), Look around, Leave |
| Cashier Staff      | Idle, Scan, Talk                |
| Cleaner Staff      | Walk, Sweep, Mop                |
| Manager            | Walk, Talk, Inspect             |

---

## 🎬 Onboarding System ✅ DONE

### Flow

1. **LoadingScreen** — progress bar, routes to Onboarding or Level_01 based on `save_onboarded`
2. **Onboarding scene** — Gender (Male/Female) + Age (Young/Adult/Senior) selection
3. Saves to PlayerPrefs → loads Level_01
4. **Tutorial** — 4-step arrow + message overlay inside Level_01 (Step 21, next)

### Tutorial Steps (Level_01)

| Step | Target Area           | Message                                 |
| ---- | --------------------- | --------------------------------------- |
| 1    | Build button (bottom) | "Tap here to build your first shop!"    |
| 2    | South wall entrance   | "Customers will enter through here"     |
| 3    | Mall floor center     | "Place your shop anywhere on the floor" |
| 4    | HUD top area          | "Earn money and upgrade your shops!"    |

---

## 💰 Income Multiplier (Level Scaling)

```
Level 1  → 1.0x income
Level 2  → 1.2x income
Level 3  → 1.44x income (1.2²)
Level N  → 1.0 × (1.2^(N-1)) income
```

Formula: `incomeMultiplier = Mathf.Pow(1.2f, levelIndex)`

---

## ⭐ Upgrade System

| Tier | Capacity    | Income | Speed |
| ---- | ----------- | ------ | ----- |
| 1    | 2 customers | $20    | 100%  |
| 2    | 4 customers | $30    | 110%  |
| 3    | 6 customers | $45    | 125%  |

---

## 🎯 Mission System

- Build X shops
- Serve N customers
- Earn $X
- Reach X% satisfaction

---

## 🔄 Game Reset

- Settings panel → "Reset Game" → confirmation dialog
- `PlayerPrefs.DeleteAll()` + JSON wipe → reload Onboarding scene

---

## 😊 Satisfaction System

```
Satisfaction = BaseSatisfaction - WaitingPenalty + ShopUpgradeBonus + DecorationBonus
```

- > 80% → +10% customer arrival rate
- < 30% → customers start leaving

---

## 🔊 Audio System

- `AudioManager.cs` — Singleton, DontDestroyOnLoad
- BGM AudioSource (loop=true) + SFX AudioSource (PlayOneShot)
- SFX keys: `sfx_click`, `sfx_coin`, `sfx_customer_arrive`, `sfx_customer_pay`, `sfx_shop_unlock`, `sfx_level_complete`

---

## ✨ Particle Effect System

| Effect             | Trigger        |
| ------------------ | -------------- |
| `FX_CoinEarn`      | Customer pays  |
| `FX_ShopUnlock`    | New shop built |
| `FX_LevelComplete` | Level complete |

- Object pooling via `FXManager.cs`

---

## 📱 UI — Screen Auto-Fit

- Canvas Scaler: Scale With Screen Size, 1080×1920, Match 0.5
- `SafeAreaHandler.cs` handles notch and punch-hole displays

---

## 🎨 Material & Lighting (URP)

### Critical Rules

- **All materials must use URP/Lit or URP/Simple Lit** — Standard shader turns pink in URP
- Characters: URP/Simple Lit (mobile performance)
- FX particles: URP/Particles/Unlit (additive blend)

### Directional Light

```
Rotation: (50, -30, 0)
Intensity: 1.0
Color: #FFF5E0 (warm white)
Shadow: Soft, Strength 0.5
```

### URP Asset (mobile-optimized)

```
Additional Lights: Disabled
Post Processing: Off (MVP)
Anti Aliasing: 2x or Off
Cast Shadows: Main light only
```

### Color Palette (Low-Poly)

```
Floor:   #E8DCC8  |  Walls:  #F5F0E8
Accent:  #4A90D9  |  Gold:   #F5A623
Success: #7ED321
```

---

## 💾 Save System

- Phase 1: PlayerPrefs | Phase 2: JSON | Future: Remote DB

### PlayerPrefs Keys

```
"save_coins"         → float
"save_level"         → int
"save_stars_X"       → int (X = level index)
"save_shop_X_level"  → int
"save_char_gender"   → string ("male"/"female")
"save_char_age"      → string ("young"/"adult"/"senior")
"save_onboarded"     → int (0/1)
"vol_bgm"            → float (0.0–1.0)
"vol_sfx"            → float (0.0–1.0)
```

---

## 🏗️ Mall Layout

- Grid-based, tile size: 5×5 units
- Level 1: 40×40 units floor, 8-unit entrance gap at South wall (X -4 to +4)

---

## 🗺️ Level & Chapter Structure

| Chapter | Levels | Theme       |
| ------- | ------ | ----------- |
| 1       | 1–5    | Tiny Mall   |
| 2       | 6–10   | City Mall   |
| 3       | 11–15  | Mega Mall   |
| 4       | 16–20  | Luxury Mall |

**MVP target: Level 1–3 only**

---

## 🛒 Shop Types (unlock order)

Mini Grocery (L1) → Clothing (L2) → Shoe (L3) → Coffee (L4) → Food Court (L5) → Electronics (L6+)

---

## 🧩 Key Scripts

| Script                     | Responsibility                           |
| -------------------------- | ---------------------------------------- |
| `GameManager.cs`           | Game state, scene transitions            |
| `EconomyManager.cs`        | AddMoney, SpendMoney, income multiplier  |
| `CameraManager.cs`         | Switch Gameplay ↔ Cinematic VCs          |
| `CameraInputHandler.cs`    | Drag pan + pinch zoom (New Input System) |
| `AudioManager.cs`          | BGM + SFX                                |
| `FXManager.cs`             | Particle pool                            |
| `SafeAreaHandler.cs`       | Notch fix                                |
| `CharacterBase.cs`         | Shared character logic                   |
| `CustomerController.cs`    | Customer state machine                   |
| `CustomerSpawner.cs`       | Spawn timing                             |
| `ShopController.cs`        | Build, upgrade, income                   |
| `ShopData.cs`              | ScriptableObject                         |
| `LevelManager.cs`          | Level complete, star calc                |
| `LevelObjectiveManager.cs` | Track objectives                         |
| `UIManager.cs`             | HUD, panels                              |
| `SaveManager.cs`           | Save/load                                |
| `OnboardingManager.cs`     | First-launch flow                        |
| `LoadingManager.cs`        | Loading screen + scene routing           |
| `TutorialManager.cs`       | In-game tutorial arrows + messages       |
| `ResetManager.cs`          | Full game reset                          |

---

## 📦 Folder Structure

```
Assets/
└── _Game/
    ├── Scripts/
    │   ├── Core/          GameManager, AudioManager, FXManager, SafeAreaHandler, ResetManager, LoadingManager
    │   ├── Camera/        CameraManager, CameraInputHandler
    │   ├── Character/     CharacterBase, CustomerController, CustomerSpawner, StaffController
    │   ├── Shop/          ShopController, ShopData
    │   ├── Economy/       EconomyManager
    │   ├── Level/         LevelManager, LevelObjectiveManager, TutorialManager
    │   ├── UI/            UIManager, BuildUI, UpgradeUI, OnboardingManager
    │   └── Save/          SaveManager
    ├── Animations/
    ├── Prefabs/           Characters/, Shops/, FX/, Environment/
    ├── Scenes/            LoadingScreen, Onboarding, Levels/Level_01~03
    ├── ScriptableObjects/ Shops/, Levels/, Customers/
    ├── Audio/             BGM/, SFX/
    └── Art/               Characters/, Environment/, Shops/, Props/
```

### Build Settings Order

```
0 - _Game/Scenes/LoadingScreen
1 - _Game/Scenes/Onboarding
2 - _Game/Scenes/Level_01
```

---

## 🚫 Not in MVP

Staff system, Multiple floors, IAP/Ads, Cloud save, Daily rewards, VIP customers, Decoration system

---

## ⚠️ Known Issues / Lessons Learned

- Cinemachine 3.x Priority checkbox does not work → solved with Output Channel isolation
- `EnhancedTouchSupport.Enable()` must be called in OnEnable for touch input to work
- Drag-dropping prefab resets Y to 0 → always spawn via CustomerSpawner in code
- `Image Type: Filled` requires a Source Image (UISprite) assigned before the option appears
- Canvas Match must be 0.5 for correct scaling on both portrait and landscape displays
- Wire buttons via `AddListener` in script, not via Inspector OnClick events
