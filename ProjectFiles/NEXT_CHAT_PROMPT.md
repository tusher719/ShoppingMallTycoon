# 🏬 Shopping Mall Tycoon — Next Chat Prompt

> এই পুরো block নতুন chat-এ paste করো।

আমি Shopping Mall Tycoon বানাচ্ছি — Unity 6 (6000.0.47f1) URP Android mobile tycoon game।
Repo: https://github.com/tusher719/ShoppingMallTycoon
Speak to me in Bangla, write all code and docs in English.

[PROJECT_CONTEXT.md এর সম্পূর্ণ content paste করো]

## ✅ Completed Phases

### Phase 1 — Scene Setup ✅

Camera, walls, floor, Cinemachine 3.x, drag/pinch controls (PC ✅ Android ✅)
Main Camera: PhysicsRaycaster component added

### Phase 2 — Character System ✅

CharacterBase.cs, Customer_Normal prefab, AnimatorOverrideController
Step 15 skipped — wire when real 3D model arrives

### Phase 3 — Onboarding ✅

LoadingScreen → Onboarding → Level_01 routing
Gender + Age selection, TutorialManager (4-step arrow overlay)

### Phase 4 — Economy & Shop ✅

- EconomyManager.cs — AddMoney, SpendMoney, OnMoneyChanged, OnMoneyAdded, starting coins 500
- ShopData SO — GroceryShop_Data (Cost:100, Income:20/5s)
  - Tier1: ×1.0/$0/2cust | Tier2: ×1.5/$150/4cust | Tier3: ×2.25/$300/6cust
- ShopController.cs — Build() + OnShopBuilt + GenerateIncome() every 5s
  - Upgrade(), CanUpgrade(), GetUpgradeCost(), GetCurrentMultiplier(), GetTierName()
  - Unique ShopID + DisplayName ("Grocery Shop #1")
  - OnShopUpgraded event
  - ⚠️ ShopController: `public ShopData Data => shopData;` — always use property
- BuildUI.cs — max 9 shops (GridManager 3×3), "Max" button when full
- GridManager.cs — 3×3 grid, tileSize 5, origin (-5,0,0), shops in --- Mall --- parent

### Phase 5 — Customer System ✅

- NavMesh baked on Level_01 (Floor, Humanoid)
- CustomerController: Spawned→Walking→Shopping→Paying→Leaving
  - shoppingDuration: 5s, payingDuration: 2s
  - Payment: baseIncome _ tierMultiplier _ levelMultiplier
  - OnCustomerServed, OnCustomerWaiting, OnCustomerLeft events
  - Exit: MoveTo(0,0,-18) then Destroy
- CustomerSpawner: event-driven via ShopController.OnShopBuilt
  - Random shop selection from \_builtShops list
  - spawnInterval: 8s, maxCustomers: 5, SpawnPoint: (0,0,-15)
  - Notifies HUDManager.OnCustomerSpawned()
  - ⚠️ Target Shop Inspector-এ assign করা যাবে না
- Windows ✅ Android ✅

### Phase 6 — Objectives & Level ✅

- LevelObjectiveManager: shops(3), customers(20), money($500)
- SatisfactionManager: base 70, +1.5/served, -2/waiting, high 80, low 30
- LevelManager: star calc (≥80%→3★, ≥50%→2★, else 1★)
  - GetIncomeMultiplier(): Mathf.Pow(1.2f, currentLevelIndex) — wired to CustomerController
- MissionUI: Shops/Customers/Earned progress (Top Right panel)
- LevelCompletePanel: "Level Complete!" + "Stars: X/3" (Center)

### Phase 7 (Steps 37–39) ✅

- GridManager: 3×3 grid shop placement
- ShopController: 3-tier upgrade via ScriptableObject tiers
- ShopClickHandler: Physics.RaycastAll on ShopBody (Orthographic camera fix)
- UpgradeUI: DisplayName, tier, Cost: -$X, Income: $X→$Y/5s
- HUDManager: TopBar left box — Coins, +X/min, Shops, Customers
  - Income rate: recalculates every 1s from all built shops

## 📌 Current Step

**→ Step 40: Gem System**

## Phase 7 Remaining Plan

- Step 40: Gem system (earn via milestones, spend for upgrades)
- Step 41: Mission panel (advanced — task list + reward + progress bar)
- Step 42: Level complete panel + Level Map (Current/Next/Upcoming/Coming Soon tiles)
- Step 43: Cinematic on shop unlock

## ⚠️ Key Rules (don't forget)

- CustomerSpawner Target Shop — event-driven, not Inspector
- ShopController: Data property, never .shopData direct
- Cinemachine: Output Channel, not Priority
- Buttons: AddListener in script only
- Materials: URP/Lit or URP/Simple Lit only
- Orthographic camera: Physics.RaycastAll, not OnMouseDown
- Main Camera: PhysicsRaycaster required for shop click
- FindFirstObjectByType(FindObjectsInactive.Include) for inactive panels
- TMP: no emoji support with LiberationSans SDF — use plain text
- Shops spawn in --- Mall --- parent via GridManager
- Dynamic shop cost (Phase 7 pending): baseCost \* Mathf.Pow(1.5f, shopsBuilt)

## 🗒️ Pending (Phase 7B+)

- AudioManager, FXManager, FX prefabs
- SafeAreaHandler, SaveManager, ResetManager
- Canvas Scaler: currently Constant Pixel Size — fix in Phase 8
