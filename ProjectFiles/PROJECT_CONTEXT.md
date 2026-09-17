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

- `using Unity.Cinemachine;` — namespace different from Cinemachine 2.x
- **CinemachineBrain:** Channel Mask = Default only
- **VC_Gameplay:** Output Channel = Default, Orthographic size 10, Position (0,20,-15), Rotation (45,0,0)
- **Cinematic VCs:** Output Channel = Channel02 — triggered via CameraManager

| VC               | Position   | Rotation | Ortho Size | Channel   |
| ---------------- | ---------- | -------- | ---------- | --------- |
| VC_Gameplay      | (0,20,-15) | (45,0,0) | 10         | Default   |
| VC_ShopUnlock    | (0,15,-10) | (45,0,0) | 7          | Channel02 |
| VC_NewFloor      | (0,25,-20) | (50,0,0) | 15         | Channel02 |
| VC_MallOverview  | (0,35,-25) | (55,0,0) | 20         | Channel02 |
| VC_LevelComplete | (0,18,-12) | (45,0,0) | 8          | Channel02 |

- `EnhancedTouchSupport.Enable()` must be called in OnEnable
- Pan bounds: X(-15,15), Z(-15,15) | Zoom: OrthographicSize(5,20)

---

## 🧍 Character & Animation System ✅ DONE

- `CharacterBase.cs` — abstract (MoveTo, PlayAnim, StopMoving, HasReachedDestination)
- `BaseAnimator` controller — Idle/Walk/Browse/Pay states, Any State transitions, Trigger params
- `Customer_Normal` prefab — Capsule, URP/Simple Lit #4A90D9, NavMeshAgent (Speed 3.5, StopDist 1.5)
- `Customer_Normal_Override.overrideController` → BaseAnimator
- Step 15 (animation clips) skipped — wire when real 3D model arrives
- **NavMeshAgent disabled** until Step 27 (NavMesh bake)

---

## 🎬 Onboarding System ✅ DONE

1. **LoadingScreen** — progress bar, routes based on `save_onboarded`
2. **Onboarding scene** — Gender (Male/Female) + Age (Young/Adult/Senior)
3. Saves to PlayerPrefs → loads Level_01
4. **Tutorial** — 4-step arrow + message overlay in Level_01

| Step | Target Area           | Message                                 | Arrow Y | Arrow Rot |
| ---- | --------------------- | --------------------------------------- | ------- | --------- |
| 1    | Build button (bottom) | "Tap here to build your first shop!"    | -750    | 180°      |
| 2    | South wall entrance   | "Customers will enter through here"     | 200     | 90°       |
| 3    | Mall floor center     | "Place your shop anywhere on the floor" | 0       | 180°      |
| 4    | HUD top area          | "Earn money and upgrade your shops!"    | 800     | 180°      |

---

## 💰 Economy System ✅ DONE (Phase 4)

### EconomyManager.cs

```csharp
public static event Action<float> OnMoneyChanged;
public void AddMoney(float amount)    // fires OnMoneyChanged
public bool SpendMoney(float amount)  // returns false if insufficient
// Starting coins: 500
// LevelManager multiplier wired in Phase 6 (Step 35)
```

### Income Formula (Phase 6)

```
Level 1 → 1.0x | Level 2 → 1.2x | Level N → 1.2^(N-1)x
Formula: Mathf.Pow(1.2f, levelIndex)
```

### ShopData ScriptableObject

```
[CreateAssetMenu] → MallTycoon/Shop Data
Fields: shopName, unlockLevel, baseCost, baseIncome, maxCustomers, shopPrefab, shopIcon
Asset: GroceryShop_Data — Cost:100, Income:20/5s, MaxCustomers:2
```

### ShopController.cs

```csharp
public void Build()          // isBuilt=true, fires OnShopBuilt
void GenerateIncome()        // every 5s → EconomyManager.AddMoney(baseIncome)
public static event Action<ShopController> OnShopBuilt;
```

### BuildUI.cs

```
BuildPanel — Bottom Center, 400×120, #1A1A2E
BtnBuild   — #4A90D9, grey+disabled when coins < baseCost, text always white
TxtCoins   — Top Center, #F5A623, updates via OnMoneyChanged
Shop spawns at (0,0,5) fixed — grid placement in Phase 7
```

---

## ⭐ Upgrade System (Phase 7)

| Tier | Capacity    | Income | Speed |
| ---- | ----------- | ------ | ----- |
| 1    | 2 customers | $20    | 100%  |
| 2    | 4 customers | $30    | 110%  |
| 3    | 6 customers | $45    | 125%  |

---

## 🎯 Mission System (Phase 6)

- Build X shops
- Serve N customers
- Earn $X
- Reach X% satisfaction

---

## 📊 HUD Plan (Phase 7 — Step 39)

> Number-driven, always readable, fun

```
TopBar:
├── 💰 Coins + per-min income rate    e.g. "💰 1,250  (+$48/min)"
├── 📊 Level progress bar + % + goal  e.g. "67% (2/3 goals)"
└── 💎 Gems                           e.g. "💎 12"

BottomBar:
├── 🏪 Shop count                     e.g. "Shops: 3"
├── 👥 Active customer count          e.g. "Customers: 7"
└── [Build] [Upgrade] [Missions] buttons

MissionPanel:
├── Task list + reward (coins/gems)
└── Per-task progress bar             e.g. "Serve 10 customers  7/10"

LevelCompletePanel:
├── ⭐⭐⭐ star display
├── Coins earned this level
└── Next level button
```

---

## 💎 Gem System (Phase 7)

- **Earn:** mission complete / level complete bonus / daily reward
- **Spend:** instant shop upgrade / speed boost
- **Collect:** tap floating `FX_GemCollect` prefab spawned on milestone

---

## 😊 Satisfaction System (Phase 6)

```
Satisfaction = BaseSatisfaction - WaitingPenalty + ShopUpgradeBonus + DecorationBonus
> 80% → +10% customer arrival rate
< 30% → customers start leaving
```

---

## 🔊 Audio System (Phase 7B)

- `AudioManager.cs` — Singleton, DontDestroyOnLoad
- BGM AudioSource (loop=true) + SFX AudioSource (PlayOneShot)
- SFX keys: `sfx_click`, `sfx_coin`, `sfx_customer_arrive`, `sfx_customer_pay`, `sfx_shop_unlock`, `sfx_level_complete`

---

## ✨ Particle Effect System (Phase 7B)

| Effect             | Trigger           |
| ------------------ | ----------------- |
| `FX_CoinEarn`      | Customer pays     |
| `FX_ShopUnlock`    | New shop built    |
| `FX_LevelComplete` | Level complete    |
| `FX_GemCollect`    | Milestone reached |

- Object pooling via `FXManager.cs`

---

## 📱 UI — Screen Auto-Fit

- Canvas Scaler: Scale With Screen Size, 1080×1920, Match 0.5
- `SafeAreaHandler.cs` handles notch and punch-hole displays

---

## 🎨 Material & Lighting (URP)

> ⚠️ All materials must use URP/Lit or URP/Simple Lit — Standard shader turns pink

### Color Palette

```
Floor:   #E8DCC8  |  Walls:    #F5F0E8
Accent:  #4A90D9  |  Gold:     #F5A623
Success: #7ED321  |  Dark BG:  #1A1A2E
```

### Directional Light

```
Rotation: (50, -30, 0) | Intensity: 1.0
Color: #FFF5E0 | Shadow: Soft, Strength 0.5
```

---

## 💾 Save System

- Phase 1: PlayerPrefs | Phase 2: JSON | Future: Remote DB

### PlayerPrefs Keys

```
"save_coins"         → float
"save_level"         → int
"save_stars_X"       → int
"save_shop_X_level"  → int
"save_char_gender"   → string ("male"/"female")
"save_char_age"      → string ("young"/"adult"/"senior")
"save_onboarded"     → int (0/1)
"save_tutorial_done" → int (0/1)
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

| Script                     | Status  | Responsibility                        |
| -------------------------- | ------- | ------------------------------------- |
| `GameManager.cs`           | ⬜      | Game state, scene transitions         |
| `EconomyManager.cs`        | ✅ Done | AddMoney, SpendMoney, OnMoneyChanged  |
| `CameraManager.cs`         | ✅ Done | Switch Gameplay ↔ Cinematic VCs       |
| `CameraInputHandler.cs`    | ✅ Done | Drag pan + pinch zoom                 |
| `AudioManager.cs`          | ⬜      | BGM + SFX                             |
| `FXManager.cs`             | ⬜      | Particle pool                         |
| `SafeAreaHandler.cs`       | ⬜      | Notch fix                             |
| `CharacterBase.cs`         | ✅ Done | Shared character logic                |
| `CustomerController.cs`    | ⬜      | Customer state machine                |
| `CustomerSpawner.cs`       | ⬜      | Spawn timing                          |
| `ShopController.cs`        | ✅ Done | Build, income generation              |
| `ShopData.cs`              | ✅ Done | ScriptableObject                      |
| `BuildUI.cs`               | ✅ Done | Build panel + coin display            |
| `LevelManager.cs`          | ⬜      | Level complete, star calc, multiplier |
| `LevelObjectiveManager.cs` | ⬜      | Track objectives                      |
| `UIManager.cs`             | ⬜      | HUD, panels                           |
| `SaveManager.cs`           | ⬜      | Save/load                             |
| `OnboardingManager.cs`     | ✅ Done | First-launch flow                     |
| `LoadingManager.cs`        | ✅ Done | Loading screen + scene routing        |
| `TutorialManager.cs`       | ✅ Done | In-game tutorial arrows + messages    |
| `ResetManager.cs`          | ⬜      | Full game reset                       |

---

## 📦 Folder Structure

```
Assets/
└── _Game/
    ├── Scripts/
    │   ├── Core/       GameManager, AudioManager, FXManager, SafeAreaHandler, ResetManager, LoadingManager
    │   ├── Camera/     CameraManager, CameraInputHandler
    │   ├── Character/  CharacterBase, CustomerController, CustomerSpawner
    │   ├── Shop/       ShopController, ShopData
    │   ├── Economy/    EconomyManager
    │   ├── Level/      LevelManager, LevelObjectiveManager, TutorialManager
    │   ├── UI/         UIManager, BuildUI, UpgradeUI, OnboardingManager
    │   └── Save/       SaveManager
    ├── Animations/
    ├── Prefabs/        Characters/, Shops/GroceryShop, FX/, Environment/
    ├── Scenes/         LoadingScreen, Onboarding, Levels/Level_01~03
    ├── ScriptableObjects/ Shops/GroceryShop_Data, Levels/, Customers/
    ├── Audio/          BGM/, SFX/
    └── Art/            Characters/, Environment/, Shops/, Props/
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
- `EnhancedTouchSupport.Enable()` must be called in OnEnable for touch input
- Drag-dropping prefab resets Y to 0 → always spawn via CustomerSpawner in code
- `Image Type: Filled` requires Source Image (UISprite) assigned before option appears
- Canvas Match must be 0.5 for correct scaling on both portrait and landscape
- Wire buttons via `AddListener` in script, not Inspector OnClick events
- TMP import dialog appears first time — click Import TMP Essentials
- `save_tutorial_done` must be reset separately — `DeleteAll()` also clears `save_onboarded`
- NavMeshAgent on Customer_Normal — disabled until Step 27 (NavMesh bake)
- Material slot: expand ▶ Materials in Mesh Renderer to see Element 0 for drag-drop
