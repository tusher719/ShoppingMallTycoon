# 🏬 Shopping Mall Tycoon — Changelog

## [0.1.0] — Phase 1: Scene Setup ✅ COMPLETE

- Unity 6 URP project initialized
- Folder structure: `Assets/_Game/`
- `Level_01` scene + Hierarchy separators
- Mall floor (Plane, 40×40), Walls, Entrance gap 8 units South (X -4 to +4)
- Cinemachine 3.1.7 (`Unity.Cinemachine` namespace)
- VC_Gameplay (Ortho 10, pos 0,20,-15, rot 45,0,0)
- Cinematic VCs: ShopUnlock, NewFloor, MallOverview, LevelComplete (Channel02)
- CinemachineBrain Channel Mask = Default only
- CameraManager.cs (Output Channel switching)
- CameraInputHandler.cs — drag pan + pinch zoom (PC ✅ Android ✅)

**Lessons:** Cinemachine 3.x Priority broken → Output Channel. `Input.GetTouch` broken in New Input System → EnhancedTouchSupport.

---

## [0.1.1] — Material & Lighting _(apply as you go)_

- URP Asset mobile settings (Additional Lights off, Post Processing off)
- Directional Light: (50,-30,0), #FFF5E0, soft shadow 0.5
- Floor: URP/Lit #E8DCC8 | Walls: URP/Lit #F5F0E8
- Ambient: Flat, #8AA0B0

> ⚠️ Standard shader = pink in URP. Always use URP/Lit or URP/Simple Lit.

---

## [0.2.0] — Phase 2: Characters ✅ COMPLETE

- `CharacterBase.cs` — abstract: MoveTo, PlayAnim, StopMoving, HasReachedDestination
- `BaseAnimator.controller` — Idle, Walk, Browse, Pay states
- `Customer_Normal` prefab — Capsule, URP/Simple Lit #4A90D9
- NavMeshAgent: Speed 3.5, StopDist 1.5, Radius 0.3
- `Customer_Normal_Override.overrideController` → BaseAnimator

**Lessons:** AnimatorOverrideController slots only appear when states have Motion clips. Step 15 skipped — wire when real model arrives.

---

## [0.3.0] — Phase 3: Onboarding ✅ COMPLETE

- `LoadingManager.cs` — AsyncOperation, routes on `save_onboarded`
- Onboarding scene — Gender + Age selection (#4A90D9 selected, #444466 default)
- `OnboardingManager.cs` — AddListener pattern
- `TutorialManager.cs` — 4-step arrow overlay, `save_tutorial_done`
- Tested: Windows ✅ Android ✅

**Lessons:** `Image Type: Filled` needs Source Image first. `save_tutorial_done` reset separately. Buttons via AddListener only.

---

## [0.4.0] — Phase 4: Economy & Shop ✅ COMPLETE

- `EconomyManager.cs` — AddMoney, SpendMoney, OnMoneyChanged, OnMoneyAdded, starting coins 500
- `ShopData.cs` ScriptableObject — GroceryShop_Data (Cost:100, Income:20, MaxCustomers:2)
- `GroceryShop` prefab — ShopBody (#4A90D9) + ShopSign (#F5A623) + ShopTrigger
- `ShopController.cs` — Build() + OnShopBuilt event + GenerateIncome() every 5s
- `BuildUI.cs` — BuildPanel (bottom), TxtCoins (top, #F5A623), grey+disabled when insufficient

---

## [0.5.0] — Phase 5: Customer System ✅ COMPLETE

- NavMesh baked on Level_01 (Floor, Humanoid, All Game Objects, Render Meshes)
- `CustomerController.cs` — state machine: Spawned→Walking→Shopping→Paying→Leaving
  - shoppingDuration: 5s, payingDuration: 2s
  - Payment: baseIncome _ tierMultiplier _ levelMultiplier
  - OnCustomerServed, OnCustomerWaiting, OnCustomerLeft events
  - Exit toward (0, 0, -18) then Destroy
- `CustomerSpawner.cs` — event-driven, random shop selection from built shops list
  - spawnInterval: 8s, maxCustomers: 5, SpawnPoint: (0, 0, -15)
- Windows ✅ Android ✅

**Lessons:** CustomerSpawner Target Shop — Inspector assign না, event দিয়ে করতে হয়. BuildUI Instantiate করে clone, original GroceryShop না. `ShopController.Data` property ব্যবহার করো. Orthographic camera-তে OnMouseDown কাজ করে না → Physics.RaycastAll.

---

## [0.6.0] — Phase 6: Objectives & Level ✅ COMPLETE

### Step 32 — LevelObjectiveManager.cs

- Tracks: shopsBuilt, customersServed, moneyEarned
- Targets: 3 shops, 20 customers, $500 earned
- Events: OnShopsProgress, OnCustomersProgress, OnMoneyProgress, OnAllObjectivesComplete

### Step 33 — Money Earned Tracking

- EconomyManager: OnMoneyAdded event (earned amount, not balance)
- LevelObjectiveManager subscribes to OnMoneyAdded

### Step 34 — SatisfactionManager.cs

- Base: 70, High: 80, Low: 30
- +1.5 per customer served, -2 per customer waiting
- CustomerController: OnCustomerWaiting (on shop arrival), OnCustomerLeft (before Destroy)

### Step 35 — LevelManager.cs

- Listens: OnAllObjectivesComplete → CalculateStars() → OnLevelComplete
- Star calc: satisfaction >= 80 → 3★, >= 50 → 2★, else 1★
- Saves: save_stars_X, save_level
- GetIncomeMultiplier(): Mathf.Pow(1.2f, currentLevelIndex)
- CustomerController multiplier wired

### Step 36 — MissionUI + LevelCompletePanel

- MissionPanel (Top Right): Shops X/3, Customers X/20, Earned $X/500
- LevelCompletePanel (Center): "Level Complete!" + "Stars: X/3"
- TMP emoji not supported → plain text only

---

## [0.7.0] — Phase 7: Upgrade & UI (partial)

### Step 37 — Grid-based Shop Placement ✅

- `GridManager.cs` — 3×3 grid, tileSize 5, origin (-5, 0, 0)
- TryGetNextTile() — row-major, occupancy tracked
- BuildUI: spawns in --- Mall --- parent, max 9 shops
- CustomerSpawner: random shop selection from \_builtShops list

**commit:** `feat: add grid-based shop placement and multi-shop customer routing`

### Step 38 — Shop Upgrade Logic (3-tier) ✅

- ShopData: UpgradeTier[] (tierName, upgradeCost, incomeMultiplier, maxCustomers)
- GroceryShop_Data tiers: Tier1(×1.0/$0), Tier2(×1.5/$150), Tier3(×2.25/$300)
- ShopController: Upgrade(), CanUpgrade(), GetUpgradeCost(), GetCurrentMultiplier(), GetTierName(), GetMaxCustomers()
- ShopController: unique ShopID + DisplayName ("Grocery Shop #1")
- OnShopUpgraded event

**commit:** `feat: add 3-tier shop upgrade logic with ScriptableObject tiers`

### Step 38B — Upgrade UI ✅

- `UpgradeUI.cs` — OpenPanel(shop), ClosePanel(), RefreshUI()
- Shows: DisplayName, CurrentTier, Cost: -$X, Income: $X → $Y/5s (or Max Level)
- `ShopClickHandler.cs` — Physics.RaycastAll on ShopBody collider
- Main Camera: PhysicsRaycaster component added
- FindFirstObjectByType(FindObjectsInactive.Include) for inactive panel

**commit:** `feat: add shop upgrade UI with 3-tier system and shop click handler`

### Step 39 — Full HUD ✅

- `HUDManager.cs` — TopBar left box: Coins, +X/min, Shops: X, Customers: X
- Income rate: recalculates every 1s from all built shops' current tier
- OnShopUpgraded → rate updates instantly
- CustomerSpawner notifies HUDManager.OnCustomerSpawned()

---

## [0.7B.0] — Phase 7B: Audio & FX _(not started)_

- AudioManager, FXManager, FX prefabs

## [0.8.0] — Phase 8: Save & Polish _(not started)_

- SafeAreaHandler, SaveManager, ResetManager

## [0.9.0] — Phase 9: Build & Test _(in progress)_

- Android build settings ✅
- Phase 1 Android test ✅
- Full MVP test, performance check, PC test — pending

## [1.0.0] — MVP Release _(target)_

- Level 1–3 playable on Android, all phases complete
