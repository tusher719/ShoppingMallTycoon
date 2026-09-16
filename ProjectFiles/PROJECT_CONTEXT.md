# 🏬 Shopping Mall Tycoon — Project Context

> New chat-এ এই file paste করলেই Claude পুরো project বুঝবে।

---

## 👤 Developer Info

- **Name:** Mehedi (Tusher)
- **OS:** Windows, Git Bash terminal
- **Unity Version:** Unity 6 (6000.0.47f1) — URP template
- **Platform:** Android (primary), PC (secondary)

---

## 🎮 Game Overview

- **Genre:** Mobile Tycoon / Incremental Management
- **Engine:** Unity 6 (URP)
- **Core Loop:** Build Shop → Customers Arrive → Shop → Checkout → Earn → Upgrade → Expand → Level Complete
- **Internet:** Offline-first. Future plan: optional user profile + database (not mandatory)
- **Graphics:** 3D Low-Poly, Isometric view

---

## 📷 Camera System ✅ DONE

### Setup (Cinemachine 3.1.7)

- `using Unity.Cinemachine;` — namespace Cinemachine 2.x থেকে আলাদা
- **CinemachineBrain:** Main Camera-তে, Channel Mask = Default only
- **VC_Gameplay:** Output Channel = Default, Orthographic size 10, Position (0,20,-15), Rotation (45,0,0) — always live
- **Cinematic VCs:** Output Channel = Channel02 (Default uncheck) — CameraManager script দিয়ে trigger হয়

### Virtual Cameras

| VC               | Position   | Rotation | Ortho Size | Channel   |
| ---------------- | ---------- | -------- | ---------- | --------- |
| VC_Gameplay      | (0,20,-15) | (45,0,0) | 10         | Default   |
| VC_ShopUnlock    | (0,15,-10) | (45,0,0) | 7          | Channel02 |
| VC_NewFloor      | (0,25,-20) | (50,0,0) | 15         | Channel02 |
| VC_MallOverview  | (0,35,-25) | (55,0,0) | 20         | Channel02 |
| VC_LevelComplete | (0,18,-12) | (45,0,0) | 8          | Channel02 |

### Camera Controls (New Input System)

- `EnhancedTouchSupport.Enable()` OnEnable-এ call করতে হয়
- PC: Mouse drag pan + scroll wheel zoom
- Android: Single finger drag pan + pinch zoom
- Pan bounds: X(-15,15), Z(-15,15) | Zoom: OrthographicSize(5,20)

---

## 🧍 Character & Animation System

### Architecture (Modular)

- Each character = `CharacterBase` component
- Animator Controller: one shared `BaseAnimator` with sub-state machines per role
- Animation layers: Base layer (walk/idle) + Override layer (role-specific)
- Avatar system: Humanoid rig — swap mesh, keep animator

### Rules

- Shared animations → common Animator Controller
- Custom animations per character type → Animation Override Controller
- New character types plug into same `CharacterBase` — no duplicate scripts

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

## 🎮 First Launch — Onboarding

On first install, player sees:

1. **Character Select Screen**
   - Gender: Male / Female
   - Age group: Young / Adult / Senior
   - (Cosmetic only — affects player avatar/icon in UI)
2. Tutorial starts after selection
3. Selection saved to `PlayerPrefs` / Save file

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
- `PlayerPrefs.DeleteAll()` + JSON save → reload Onboarding

---

## 😊 Satisfaction System

```
Satisfaction = BaseSatisfaction - WaitingPenalty + ShopUpgradeBonus + DecorationBonus
```

- > 80% → +10% customer rate
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

- Canvas Scaler: Scale With Screen Size, 1080×1920, match 0.5
- `SafeAreaHandler.cs` for notch/punch-hole

---

## 🎨 Material & Lighting (URP)

### Critical Rules

- **সব Material: URP/Lit বা URP/Simple Lit shader** — Standard shader ব্যবহার করলে pink হবে
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
Anti Aliasing: 2x বা Off
Cast Shadows: Main light only
```

### Color Palette (Low-Poly)

```
Floor:   #E8DCC8  Walls:  #F5F0E8
Accent:  #4A90D9  Gold:   #F5A623
Success: #7ED321
```

---

## 💾 Save System

- Phase 1: PlayerPrefs | Phase 2: JSON | Future: Remote DB

---

## 🏗️ Mall Layout

- Grid-based, tile size: 5×5 units
- Level 1: 40×40 units, entrance gap 8 units South wall (X -4 to +4)

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

1. Mini Grocery (L1) → Clothing (L2) → Shoe (L3) → Coffee (L4) → Food Court (L5) → Electronics (L6+)

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
| `ResetManager.cs`          | Full reset                               |

---

## 📦 Folder Structure

```
Assets/
└── _Game/
    ├── Scripts/
    │   ├── Core/          GameManager, AudioManager, FXManager, SafeAreaHandler, ResetManager
    │   ├── Camera/        CameraManager, CameraInputHandler
    │   ├── Character/     CharacterBase, CustomerController, CustomerSpawner, StaffController
    │   ├── Shop/          ShopController, ShopData
    │   ├── Economy/       EconomyManager
    │   ├── Level/         LevelManager, LevelObjectiveManager
    │   ├── UI/            UIManager, BuildUI, UpgradeUI, OnboardingManager
    │   └── Save/          SaveManager
    ├── Animations/
    ├── Prefabs/           Characters/, Shops/, FX/, Environment/
    ├── Scenes/            MainMenu, Onboarding, Levels/Level_01~03
    ├── ScriptableObjects/ Shops/, Levels/, Customers/
    ├── Audio/             BGM/, SFX/
    └── Art/               Characters/, Environment/, Shops/, Props/
```

---

## 🚫 MVP-তে নেই

- Staff system, Multiple floors, IAP/Ads, Cloud save, Daily rewards, VIP customers, Decoration system
