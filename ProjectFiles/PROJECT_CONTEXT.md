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

## 📷 Camera System

### 1. Gameplay Camera (Main)

- Type: Isometric / Orthographic
- Controls: Pinch zoom, drag pan, optional rotate
- Player manages mall from this view

### 2. Cinematic Camera (Cinemachine)

- Triggers: New shop unlock, new floor, level complete, achievement, crowded mall, special events
- Flow: Gameplay → Cinematic (slow move + zoom to target) → "Unlocked!" text → back to Gameplay
- Virtual Camera list:
  - `VC_Gameplay` — default
  - `VC_ShopUnlock` — pan to new shop
  - `VC_NewFloor` — sweep new floor
  - `VC_MallOverview` — full mall bird's eye
  - `VC_Achievement` — zoom to achievement point
  - `VC_LevelComplete` — cinematic pan on complete

**Rule:** Cinemachine Brain on main camera. Switch virtual cameras via `CameraManager.cs`.

---

## 🧍 Character & Animation System

### Architecture (Modular)

- Each character = `CharacterBase` component
- Animator Controller: one shared `BaseAnimator` with sub-state machines per role
- Animation layers: Base layer (walk/idle) + Override layer (role-specific: shopping, cleaning, cashier, etc.)
- Avatar system: Humanoid rig — swap mesh, keep animator

### Rules

- Shared animations → common Animator Controller
- Custom animations per character type → Animation Override Controller
- New character types plug into same `CharacterBase` — no duplicate scripts
- Future-safe: custom characters with custom animations can override without breaking others

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
Level 4  → 1.73x income (1.2³)
Level N  → 1.0 × (1.2^(N-1)) income
```

Formula: `incomeMultiplier = Mathf.Pow(1.2f, levelIndex)`
Applied in `EconomyManager` when calculating customer spend.

---

## ⭐ Upgrade System

Each shop has 3 upgrade tiers:
| Tier | Capacity | Income | Speed |
|------|----------|--------|-------|
| 1 | 2 customers | $20 | 100% |
| 2 | 4 customers | $30 | 110% |
| 3 | 6 customers | $45 | 125% |

---

## 🎯 Mission System

Per-level missions tracked by `LevelObjectiveManager`:

- Build X shops
- Serve N customers
- Earn $X
- Reach X% satisfaction
- (Future) Hire staff, unlock area, reach customer count

---

## 🔄 Game Reset

Available on both PC and Android.

- Settings panel → "Reset Game" button
- Shows confirmation dialog
- On confirm: clears all `PlayerPrefs` + JSON save → reloads main menu
- Player goes through onboarding again (gender/age select)

---

## 😊 Satisfaction System

```
Satisfaction = BaseSatisfaction - WaitingPenalty + ShopUpgradeBonus + DecorationBonus
```

- > 80% → +10% customer rate
- < 30% → customers start leaving
- Tracked per level

---

## 🔊 Audio System

- **BGM:** Background music per scene/level, loop, fade in/out on scene change
- **SFX:** Button click, coin earn, customer arrive, customer pay, shop unlock, level complete
- Manager: `AudioManager.cs` — Singleton, DontDestroyOnLoad
- Two AudioSource components on AudioManager: one for BGM, one for SFX
- Volume control: separate BGM volume + SFX volume (saved to PlayerPrefs)

```
AudioManager
├── BGM AudioSource   → loop = true, plays level theme
└── SFX AudioSource   → loop = false, PlayOneShot()
```

- SFX keys: `sfx_click`, `sfx_coin`, `sfx_customer_arrive`, `sfx_customer_pay`, `sfx_shop_unlock`, `sfx_level_complete`

---

## ✨ Particle Effect System

Celebration effects only (no physics/weather):
| Effect | Trigger |
|--------|---------|
| `FX_CoinEarn` | Customer pays → money added |
| `FX_ShopUnlock` | New shop built/unlocked |
| `FX_LevelComplete` | Level complete panel shows |

- All effects: `ParticleSystem` prefabs under `Prefabs/FX/`
- Managed via `FXManager.cs` — `PlayFX(FXType type, Vector3 position)`
- Object pooling for FX prefabs (no instantiate/destroy per play)

---

## 📱 UI — Screen Auto-Fit

- Canvas Scaler: **Scale With Screen Size**
- Reference Resolution: **1080 × 1920** (portrait)
- Match: **0.5** (width + height balanced)
- Anchors: all UI elements use proper anchor presets (stretch/corner-based)
- Safe Area: handled via `SafeAreaHandler.cs` for notch/punch-hole devices

---

## 💾 Save System

- **Phase 1:** `PlayerPrefs` (MVP)
- **Phase 2:** JSON save file (after MVP)
- **Future:** Remote DB + user profile (optional, not mandatory for MVP)
- Saves: current level, coins, stars per level, unlocked shops, shop levels, satisfaction, character selection, income multiplier state, BGM/SFX volume

---

## 🏗️ Mall Layout

- Grid-based, tile size: 5×5 units
- Modular expansion — buy adjacent tiles
- Multi-floor support (Chapter 2+)
- Level 1 mall: 40×40 units, entrance gap 8 units at South wall (X -4 to +4)

---

## 🗺️ Level & Chapter Structure

| Chapter | Levels | Theme       | Key Feature Introduced               |
| ------- | ------ | ----------- | ------------------------------------ |
| 1       | 1–5    | Tiny Mall   | Core loop, 1 shop, basic customers   |
| 2       | 6–10   | City Mall   | Multiple shops, staff, 2nd floor     |
| 3       | 11–15  | Mega Mall   | Expansion, events, cinematic moments |
| 4       | 16–20  | Luxury Mall | All systems, premium shops           |

**MVP target: Level 1–3 only**

Each level: 2–5 min gameplay, introduces 1 new mechanic.

---

## 🛒 Shop Types (unlock order)

1. Mini Grocery (Level 1)
2. Clothing Store (Level 2)
3. Shoe Store (Level 3)
4. Coffee Shop (Level 4)
5. Food Court (Level 5)
6. Electronics (Level 6+)
7. Jewelry, Game Store, Gym, Cinema (later)

---

## 🧩 Key Scripts

| Script                     | Responsibility                                      |
| -------------------------- | --------------------------------------------------- |
| `GameManager.cs`           | Game state, scene transitions                       |
| `EconomyManager.cs`        | AddMoney, SpendMoney, GetBalance, income multiplier |
| `CameraManager.cs`         | Switch between Gameplay ↔ Cinematic VCs             |
| `AudioManager.cs`          | BGM + SFX playback, volume control                  |
| `FXManager.cs`             | Particle effect pool + trigger                      |
| `SafeAreaHandler.cs`       | Notch/punch-hole safe area UI fix                   |
| `CharacterBase.cs`         | Shared character logic (move, animate, state)       |
| `CustomerController.cs`    | Customer state machine                              |
| `CustomerSpawner.cs`       | Spawn timing and rate                               |
| `ShopController.cs`        | Build, upgrade, income per shop                     |
| `ShopData.cs`              | ScriptableObject — shop config                      |
| `LevelManager.cs`          | Load level, level complete, star calc               |
| `LevelObjectiveManager.cs` | Track objectives per level                          |
| `UIManager.cs`             | HUD, panels, events                                 |
| `SaveManager.cs`           | Save/load all game state                            |
| `OnboardingManager.cs`     | First-launch gender/age select                      |
| `ResetManager.cs`          | Full game reset logic                               |

---

## 📦 Full Folder Structure

```
Assets/
└── _Game/
    ├── Scripts/
    │   ├── Core/
    │   │   ├── GameManager.cs
    │   │   ├── AudioManager.cs
    │   │   ├── FXManager.cs
    │   │   ├── SafeAreaHandler.cs
    │   │   └── ResetManager.cs
    │   ├── Camera/
    │   │   └── CameraManager.cs
    │   ├── Character/
    │   │   ├── CharacterBase.cs
    │   │   ├── CustomerController.cs
    │   │   ├── CustomerSpawner.cs
    │   │   └── StaffController.cs
    │   ├── Shop/
    │   │   ├── ShopController.cs
    │   │   └── ShopData.cs (ScriptableObject)
    │   ├── Economy/
    │   │   └── EconomyManager.cs
    │   ├── Level/
    │   │   ├── LevelManager.cs
    │   │   └── LevelObjectiveManager.cs
    │   ├── UI/
    │   │   ├── UIManager.cs
    │   │   ├── BuildUI.cs
    │   │   ├── UpgradeUI.cs
    │   │   └── OnboardingManager.cs
    │   └── Save/
    │       └── SaveManager.cs
    ├── Animations/
    │   ├── Controllers/
    │   │   ├── BaseAnimator.controller
    │   │   └── Overrides/
    │   │       ├── Customer_Normal.overrideController
    │   │       └── Customer_Rich.overrideController
    │   └── Clips/
    │       ├── Shared/
    │       └── Custom/
    ├── Prefabs/
    │   ├── Characters/
    │   │   ├── Customer_Normal.prefab
    │   │   ├── Customer_Rich.prefab
    │   │   └── Customer_Impatient.prefab
    │   ├── Shops/
    │   │   └── GroceryShop.prefab
    │   ├── FX/
    │   │   ├── FX_CoinEarn.prefab
    │   │   ├── FX_ShopUnlock.prefab
    │   │   └── FX_LevelComplete.prefab
    │   └── Environment/
    ├── Scenes/
    │   ├── MainMenu
    │   ├── Onboarding
    │   └── Levels/
    │       ├── Level_01
    │       ├── Level_02
    │       └── Level_03
    ├── ScriptableObjects/
    │   ├── Shops/
    │   ├── Levels/
    │   └── Customers/
    ├── Audio/
    │   ├── BGM/
    │   └── SFX/
    └── Art/
        ├── Characters/
        ├── Environment/
        ├── Shops/
        └── Props/
```

---

## 🚫 MVP-তে নেই (পরে আসবে)

- Staff system
- Multiple floors / Elevator
- IAP / Ads
- Cloud save / User profile / Database
- Daily rewards
- VIP customers
- Parking
- Decoration system

---

## 🔮 Future Plans (not MVP)

- Remote database + user auth
- User profile with progress sync
- Multiplayer leaderboard
- Seasonal events
