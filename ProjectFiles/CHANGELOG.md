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

- `EconomyManager.cs` — AddMoney, SpendMoney, OnMoneyChanged, starting coins 500
- `ShopData.cs` ScriptableObject — GroceryShop_Data (Cost:100, Income:20, MaxCustomers:2)
- `GroceryShop` prefab — ShopBody (#4A90D9) + ShopSign (#F5A623) + ShopTrigger
- `ShopController.cs` — Build() + OnShopBuilt event + GenerateIncome() every 5s
- `BuildUI.cs` — BuildPanel (bottom), TxtCoins (top, #F5A623), grey+disabled when insufficient
- Shop spawns fixed (0,0,5) — grid → Phase 7

---

## [0.5.0] — Phase 5: Customer System ✅ COMPLETE

#### Step 27 — NavMesh Bake

- NavMesh Surface on Floor (Humanoid, All Game Objects, Render Meshes)
- Baked — blue walkable area on floor only ✅
- NavMeshAgent on Customer_Normal enabled

#### Step 28 — CustomerController.cs

- State machine: Spawned → Walking → Shopping → Paying → Leaving
- Extends CharacterBase
- shoppingDuration: 5s, payingDuration: 2s
- Payment: `baseIncome * multiplier` (multiplier=1f placeholder until Phase 6)
- `OnCustomerServed` fires on payment
- Exit toward (0, 0, -18) then Destroy

#### Step 29 — CustomerSpawner.cs

- Event-driven: `ShopController.OnShopBuilt` → `_targetShop` auto-set
- ⚠️ No Inspector assign — BuildUI instantiates clones, not original
- spawnInterval: 8s, maxCustomers: 5, SpawnPoint: (0, 0, -15)

#### Step 30 — Full Flow Test

- Windows ✅ Android ✅
- Build → Spawn → Walk → Shop → Pay → Exit — all working
- Known: SpawnPoint wall-এর কাছে — Phase 7-এ adjust

#### Step 31 — Payment wired to EconomyManager

- `[Customer] Paid: +20 coins` ✅
- Multiplier placeholder (1f) — Phase 6 Step 35-এ wire হবে

**Lessons:**

- CustomerSpawner Target Shop — Inspector assign না, event দিয়ে করতে হয়
- BuildUI Instantiate করে clone, original GroceryShop না
- `ShopController.Data` property ব্যবহার করো, `.shopData` direct না

---

## [0.6.0] — Phase 6: Objectives & Level _(next)_

- LevelObjectiveManager, Satisfaction system, LevelManager, Mission UI

## [0.7.0] — Phase 7: Upgrade & UI _(not started)_

- Grid placement, dynamic shop cost, 3-tier upgrade, full HUD, gem system

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
