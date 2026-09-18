# 🏬 Shopping Mall Tycoon — Project Context

> নতুন chat-এ paste করো।

## 👤 Developer Info

- **Name:** Mehedi (Tusher)
- **OS:** Windows, Git Bash
- **Unity:** 6000.0.47f1 (URP)
- **Platform:** Android (primary), PC (secondary)
- **Repo:** https://github.com/tusher719/ShoppingMallTycoon

## 🎮 Game Overview

- **Genre:** Mobile Tycoon / Incremental Management
- **Core Loop:** Build Shop → Customers Arrive → Shop → Pay → Earn → Upgrade → Level Complete
- **Graphics:** 3D Low-Poly, Isometric
- **Internet:** Offline-first. Remote DB future (not MVP)
- **MVP:** Level 1–3, single shop type, no staff

---

## ✅ Phase 1 — Scene Setup ✅ DONE

- `using Unity.Cinemachine;` — namespace different from 2.x
- CinemachineBrain Channel Mask = Default only
- VC_Gameplay: Default channel, Ortho 10, pos (0,20,-15), rot (45,0,0)
- Cinematic VCs (Channel02): ShopUnlock, NewFloor, MallOverview, LevelComplete
- CameraInputHandler: drag pan + pinch zoom (PC ✅ Android ✅)
- Pan bounds: X(-15,15), Z(-15,15) | Zoom: OrthographicSize(5,20)
- Mall floor 40×40, walls, 8-unit entrance gap South (X -4 to +4)
- Main Camera: PhysicsRaycaster component added

## ✅ Phase 2 — Character System ✅ DONE

- `CharacterBase.cs` abstract: MoveTo, PlayAnim, StopMoving, HasReachedDestination
- `Customer_Normal` prefab: Capsule, #4A90D9, NavMeshAgent (Speed 3.5, StopDist 1.5)
- `BaseAnimator`: Idle/Walk/Browse/Pay states
- Step 15 skipped — real 3D model আসলে করবো

## ✅ Phase 3 — Onboarding ✅ DONE

- LoadingScreen → Onboarding → Level_01 (routes on `save_onboarded`)
- Gender + Age selection → PlayerPrefs
- TutorialManager: 4-step arrow overlay, `save_tutorial_done`

## ✅ Phase 4 — Economy & Shop ✅ DONE

- `EconomyManager`: AddMoney, SpendMoney, OnMoneyChanged, OnMoneyAdded, starting coins 500
- `ShopData` SO: GroceryShop_Data
  - Base: Cost:100, Income:20/5s, MaxCustomers:2
  - Tier 1: ×1.0, $0 upgrade, 2 customers
  - Tier 2: ×1.5, $150 upgrade, 4 customers
  - Tier 3: ×2.25, $300 upgrade, 6 customers
- `ShopController`: Build() + OnShopBuilt + GenerateIncome() every 5s
  - Upgrade(), CanUpgrade(), GetUpgradeCost(), GetCurrentMultiplier()
  - Unique ShopID + DisplayName ("Grocery Shop #1")
  - ⚠️ `public ShopData Data => shopData;` — always use property
- `BuildUI`: BuildPanel + max 9 shops (GridManager 3×3)
- `GridManager`: 3×3, tileSize 5, origin (-5, 0, 0)
- Shops spawn in `--- Mall ---` parent

## ✅ Phase 5 — Customer System ✅ DONE

- NavMesh baked on Level_01 (Floor, Humanoid)
- `CustomerController`: Spawned→Walking→Shopping→Paying→Leaving
  - shoppingDuration: 5s, payingDuration: 2s
  - Payment: baseIncome _ tierMultiplier _ levelMultiplier
  - OnCustomerServed, OnCustomerWaiting, OnCustomerLeft events
  - Exit: MoveTo(0,0,-18) then Destroy
- `CustomerSpawner`: event-driven via `ShopController.OnShopBuilt`
  - Random shop selection from \_builtShops list
  - spawnInterval: 8s, maxCustomers: 5, SpawnPoint: (0,0,-15)
  - Notifies HUDManager.OnCustomerSpawned()
  - ⚠️ Never assign Target Shop in Inspector
- Windows ✅ Android ✅

## ✅ Phase 6 — Objectives & Level ✅ DONE

- `LevelObjectiveManager`: tracks shops(3), customers(20), money($500)
  - Events: OnShopsProgress, OnCustomersProgress, OnMoneyProgress, OnAllObjectivesComplete
- `SatisfactionManager`: base 70, high 80, low 30
  - +1.5 per served, -2 per waiting
  - Events: OnSatisfactionChanged, OnHighSatisfaction, OnLowSatisfaction
- `LevelManager`: star calc (≥80%→3★, ≥50%→2★, else 1★)
  - GetIncomeMultiplier(): Mathf.Pow(1.2f, currentLevelIndex)
  - Saves: save_stars_X, save_level
  - Event: OnLevelComplete(int stars)
- `MissionUI`: Shops X/3, Customers X/20, Earned $X/500 (Top Right panel)
- `LevelCompletePanel`: "Level Complete!" + "Stars: X/3" (Center, starts inactive)

## ✅ Phase 7 (partial) — Steps 37–39 DONE

- `GridManager`: 3×3 grid shop placement
- Shop upgrade: 3-tier system via ScriptableObject
- `ShopClickHandler`: Physics.RaycastAll on ShopBody
- `UpgradeUI`: DisplayName, tier, Cost: -$X, Income: $X→$Y/5s
- `HUDManager`: TopBar left box — Coins, +X/min, Shops, Customers
  - Income rate: recalculates every 1s from all built shops

---

## 📌 Current Step

**→ Step 40: Gem System**

## Phase 7 Remaining

- Step 40: Gem system
- Step 41: Mission panel (advanced)
- Step 42: Level complete panel + Level Map (Current/Next/Upcoming/Coming Soon)
- Step 43: Cinematic on shop unlock

## Pending (Phase 7B+)

- AudioManager, FXManager, FX prefabs
- SafeAreaHandler, SaveManager, ResetManager
- Dynamic shop cost: `baseCost * Mathf.Pow(1.5f, shopsBuilt)`

---

## 🎨 Color Palette

```
Floor: #E8DCC8  | Walls: #F5F0E8
Accent: #4A90D9 | Gold: #F5A623
Success: #7ED321 | Dark BG: #1A1A2E
```

## ⭐ Upgrade System

| Tier | Capacity    | Income (base×) | Upgrade Cost |
| ---- | ----------- | -------------- | ------------ |
| 1    | 2 customers | $20 (×1.0)     | $0 (base)    |
| 2    | 4 customers | $30 (×1.5)     | $150         |
| 3    | 6 customers | $45 (×2.25)    | $300         |

## 🗺️ Level Structure

| Chapter | Levels | Theme       |
| ------- | ------ | ----------- |
| 1       | 1–5    | Tiny Mall   |
| 2       | 6–10   | City Mall   |
| 3       | 11–15  | Mega Mall   |
| 4       | 16–20  | Luxury Mall |

## 🛒 Shop Types

Mini Grocery (L1) → Clothing (L2) → Shoe (L3) → Coffee (L4) → Food Court (L5) → Electronics (L6+)

## 💾 PlayerPrefs Keys

```
save_coins, save_level, save_stars_X, save_shop_X_level
save_char_gender, save_char_age, save_onboarded, save_tutorial_done
vol_bgm, vol_sfx
```

## 🧩 Script Status

| Script                   | Status |
| ------------------------ | ------ |
| EconomyManager.cs        | ✅     |
| CameraManager.cs         | ✅     |
| CameraInputHandler.cs    | ✅     |
| CharacterBase.cs         | ✅     |
| CustomerController.cs    | ✅     |
| CustomerSpawner.cs       | ✅     |
| ShopController.cs        | ✅     |
| ShopData.cs              | ✅     |
| ShopClickHandler.cs      | ✅     |
| BuildUI.cs               | ✅     |
| UpgradeUI.cs             | ✅     |
| MissionUI.cs             | ✅     |
| HUDManager.cs            | ✅     |
| GridManager.cs           | ✅     |
| LevelObjectiveManager.cs | ✅     |
| SatisfactionManager.cs   | ✅     |
| LevelManager.cs          | ✅     |
| OnboardingManager.cs     | ✅     |
| LoadingManager.cs        | ✅     |
| TutorialManager.cs       | ✅     |
| UIManager.cs             | ⬜     |
| AudioManager.cs          | ⬜     |
| FXManager.cs             | ⬜     |
| SaveManager.cs           | ⬜     |
| SafeAreaHandler.cs       | ⬜     |
| GameManager.cs           | ⬜     |
| ResetManager.cs          | ⬜     |

## 📦 Folder Structure

```
Assets/_Game/
├── Scripts/
│   ├── Core/      GameManager, AudioManager, FXManager, SafeAreaHandler, LoadingManager
│   ├── Camera/    CameraManager, CameraInputHandler
│   ├── Character/ CharacterBase, CustomerController, CustomerSpawner
│   ├── Shop/      ShopController, ShopData, ShopClickHandler, GridManager
│   ├── Economy/   EconomyManager
│   ├── Level/     LevelManager, LevelObjectiveManager, SatisfactionManager, TutorialManager
│   ├── UI/        UIManager, BuildUI, UpgradeUI, MissionUI, HUDManager, OnboardingManager
│   └── Save/      SaveManager
├── Prefabs/       Characters/, Shops/GroceryShop, FX/
├── Scenes/        LoadingScreen, Onboarding, Levels/Level_01~03
└── ScriptableObjects/ Shops/GroceryShop_Data
```

## Build Settings Order

```
0 - LoadingScreen
1 - Onboarding
2 - Level_01
```

## 🚫 Not in MVP

Staff, Multiple floors, IAP/Ads, Cloud save, Daily rewards, VIP customers, Decoration

## ⚠️ Key Rules

- Materials: URP/Lit or URP/Simple Lit only — Standard = pink
- Cinemachine: Output Channel isolation, not Priority
- Buttons: AddListener in script, not Inspector OnClick
- `EnhancedTouchSupport.Enable()` in OnEnable
- CustomerSpawner Target Shop — event-driven, never Inspector assign
- `ShopController.Data` property, never `.shopData` direct
- Orthographic camera: use Physics.RaycastAll, not OnMouseDown
- Main Camera needs PhysicsRaycaster for shop click
- FindFirstObjectByType(FindObjectsInactive.Include) for inactive UI panels
- Shops spawn in `--- Mall ---` parent via GridManager
- TMP: Import TMP Essentials on first use; no emoji (LiberationSans SDF limitation)
- \_shopCounter is static — resets on domain reload only
