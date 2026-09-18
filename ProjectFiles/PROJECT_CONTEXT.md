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

- `EconomyManager`: AddMoney, SpendMoney, OnMoneyChanged, starting coins 500
- `ShopData` SO: GroceryShop_Data (Cost:100, Income:20/5s, MaxCustomers:2)
- `ShopController`: Build() + OnShopBuilt event + GenerateIncome() every 5s
  - ⚠️ `public ShopData Data => shopData;` — always use property, never .shopData
- `BuildUI`: BuildPanel + TxtCoins, grey+disabled when insufficient
- Shop fixed at (0,0,5) — grid → Phase 7

## ✅ Phase 5 — Customer System ✅ DONE

- NavMesh baked on Level_01 (Floor, Humanoid)
- `CustomerController`: Spawned→Walking→Shopping→Paying→Leaving
  - shoppingDuration: 5s, payingDuration: 2s
  - Payment: `baseIncome * multiplier` (multiplier=1f until Phase 6 Step 35)
  - `OnCustomerServed` event on payment
  - Exit: MoveTo(0,0,-18) then Destroy
- `CustomerSpawner`: event-driven via `ShopController.OnShopBuilt`
  - ⚠️ Never assign Target Shop in Inspector — event auto-sets `_targetShop`
  - spawnInterval: 8s, maxCustomers: 5, SpawnPoint: (0,0,-15)
- Windows ✅ Android ✅

---

## 📌 Current Step

**→ Step 32: LevelObjectiveManager.cs (Phase 6 start)**

## Phase 6 Plan

- Step 32: LevelObjectiveManager.cs
- Step 33: Track shops built, customers served, money earned
- Step 34: Satisfaction system
- Step 35: LevelManager.cs + star calc + income multiplier wire
- Step 36: Mission UI panel

## Pending (Phase 7)

- Grid placement (tile 5×5)
- Dynamic shop cost: `baseCost * Mathf.Pow(1.5f, shopsBuilt)`
- Full HUD: coins + per-min rate, level %, gems, shop count, customer count
- Gem system, 3-tier shop upgrade, mission panel, level complete panel

---

## 🎨 Color Palette

```
Floor: #E8DCC8  | Walls: #F5F0E8
Accent: #4A90D9 | Gold: #F5A623
Success: #7ED321 | Dark BG: #1A1A2E
```

## 😊 Satisfaction System (Phase 6)

```
Satisfaction = BaseSatisfaction - WaitingPenalty + ShopUpgradeBonus + DecorationBonus
> 80% → +10% customer arrival rate
< 30% → customers start leaving
```

## ⭐ Upgrade System (Phase 7)

| Tier | Capacity    | Income | Speed |
| ---- | ----------- | ------ | ----- |
| 1    | 2 customers | $20    | 100%  |
| 2    | 4 customers | $30    | 110%  |
| 3    | 6 customers | $45    | 125%  |

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
| BuildUI.cs               | ✅     |
| OnboardingManager.cs     | ✅     |
| LoadingManager.cs        | ✅     |
| TutorialManager.cs       | ✅     |
| LevelObjectiveManager.cs | ⬜     |
| LevelManager.cs          | ⬜     |
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
│   ├── Shop/      ShopController, ShopData
│   ├── Economy/   EconomyManager
│   ├── Level/     LevelManager, LevelObjectiveManager, TutorialManager
│   ├── UI/        UIManager, BuildUI, UpgradeUI, OnboardingManager
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
- TMP: Import TMP Essentials on first use
