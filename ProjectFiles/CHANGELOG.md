# 🏬 Shopping Mall Tycoon — Changelog

---

## [0.1.0] — Phase 1: Scene Setup ✅ COMPLETE

### Done

- Unity 6 URP project initialized
- Folder structure created under `Assets/_Game/`
- `Level_01` scene created with Hierarchy separators
- Mall floor blockout (Plane, 40×40 units)
- Walls blockout (North, West, East, South_Left, South_Right)
- Entrance gap: 8 units at South wall (X -4 to +4)
- Cinemachine 3.1.7 installed (`Unity.Cinemachine` namespace)
- Isometric gameplay camera (VC_Gameplay, Orthographic size 10, pos 0,20,-15, rot 45,0,0)
- Cinematic virtual cameras (VC_ShopUnlock, VC_NewFloor, VC_MallOverview, VC_LevelComplete)
  - Output Channel = Channel02 (Default unchecked)
  - CinemachineBrain Channel Mask = Default only
- CameraManager.cs (cinematic switching via Output Channel)
- CameraInputHandler.cs — New Input System + EnhancedTouchSupport
  - PC: mouse drag pan + scroll wheel zoom
  - Android: single finger drag pan + pinch zoom
  - Tested on PC ✅ and Android ✅

### Lessons Learned

- Cinemachine 3.x uses `Unity.Cinemachine` namespace (different from 2.x)
- Priority checkbox does not work → use Output Channel to isolate VCs
- `Input.GetTouch` does not work in New Input System → use `EnhancedTouchSupport`
- Android build: Active Input Handling must be set to Input System Package (New)

---

## [0.1.1] — Material & Lighting _(apply as you go)_

### Planned

- URP Asset mobile settings (Additional Lights off, Post Processing off)
- Directional Light: rotation (50,-30,0), warm white (#FFF5E0), soft shadow 0.5
- Mall floor material (URP/Lit, #E8DCC8)
- Wall materials (URP/Lit, #F5F0E8)
- Ambient light setup (Flat, #8AA0B0)

> ⚠️ All materials must use URP/Lit or URP/Simple Lit — Standard shader = pink in URP

---

## [0.2.0] — Phase 2: Characters ✅ COMPLETE

### Done

- `CharacterBase.cs` — abstract base class (MoveTo, PlayAnim, StopMoving, HasReachedDestination)
- `BaseAnimator.controller` — 4 states: Idle, Walk, Browse, Pay
- `Customer_Normal` prefab (placeholder Capsule)
  - Mesh: Capsule | Material: URP/Simple Lit, #4A90D9
  - NavMeshAgent: Speed 3.5, Stopping Distance 1.5, Radius 0.3, Base Offset 0
- `Customer_Normal_Override.overrideController` → BaseAnimator override

### Lessons Learned

- AnimatorOverrideController slots only appear when BaseAnimator states have a Motion clip assigned
- Step 15 (animation clips) skipped — wire when real 3D model arrives

---

## [0.3.0] — Phase 3: Onboarding ✅ COMPLETE

### Done

#### Step 16 — LoadingScreen scene

- `LoadingManager.cs` — AsyncOperation with progress bar fill
- Routing: `save_onboarded == 1` → Level_01, else → Onboarding
- ProgressBar Pos Y: -400, LoadingText Pos Y: -480

#### Steps 17–20 — Onboarding scene

- Gender + Age selection with color feedback (#4A90D9 selected, #444466 default)
- `OnboardingManager.cs` — AddListener pattern
- START GAME disabled until both selected
- Saves save_char_gender, save_char_age, save_onboarded=1

#### Step 21 — TutorialManager ✅

- 4-step arrow + message overlay in Level_01
- Tap anywhere → next step, 0.5s input block
- `save_tutorial_done` key prevents replay
- Tested: Windows ✅ Android ✅

### Lessons Learned

- `Image Type: Filled` requires Source Image assigned first
- `save_tutorial_done` must be reset separately — `DeleteAll()` also clears `save_onboarded`
- Button wiring via `AddListener` — not Inspector OnClick

---

## [0.4.0] — Phase 4: Economy & Shop ✅ COMPLETE

### Done

#### Step 22 — EconomyManager.cs

- Singleton pattern
- `AddMoney(float)` — fires `OnMoneyChanged` event
- `SpendMoney(float)` — returns bool (false if insufficient)
- Starting coins: 500
- LevelManager multiplier placeholder — wired in Phase 6

#### Step 23 — ShopData ScriptableObject

- `[CreateAssetMenu]` — Right Click → Create → MallTycoon → Shop Data
- Fields: shopName, unlockLevel, baseCost, baseIncome, maxCustomers, shopPrefab, shopIcon
- Asset created: `GroceryShop_Data` (Cost:100, Income:20, MaxCustomers:2, UnlockLevel:1)

#### Step 24 — GroceryShop Prefab

- Hierarchy: GroceryShop → ShopBody (Cube, #4A90D9) + ShopSign (Cube, #F5A623) + ShopTrigger (Box Collider, IsTrigger)
- Materials: Mat_GroceryShop (URP/Lit, #4A90D9), Mat_ShopSign (URP/Lit, #F5A623)
- Saved to `Assets/_Game/Prefabs/Shops/GroceryShop`
- shopPrefab assigned in GroceryShop_Data ✅

#### Step 25 — ShopController.cs

- Attached to GroceryShop prefab
- `Build()` — sets isBuilt=true, fires `OnShopBuilt` event
- `GenerateIncome()` — called every 5s, sends baseIncome to EconomyManager
- Income interval: 5 seconds

#### Step 26 — BuildUI.cs + Build Panel

- BuildPanel: Bottom Center, 400×120, #1A1A2E
- BtnBuild: #4A90D9, disabled+grey (#666666) when coins insufficient
- TxtCoins: Top Center, #F5A623, updates via OnMoneyChanged event
- Shop spawns at fixed position (0, 0, 5) — grid placement in Phase 7
- Button disabled text stays white (Disabled Color set to #FFFFFF in Button component)

### Notes

- Shop spawn position is fixed for now — grid-based placement in Phase 7 (Step 37–38)
- Shop count display + per-min income rate + level progress — planned for Phase 7 HUD (Step 39)
- Gem system planned for Phase 7

---

## [0.5.0] — Phase 5: Customer System _(not started)_

### Planned

- NavMesh bake on Level_01
- CustomerController state machine (Spawned→Walking→Shopping→Queuing→Paying→Leaving)
- CustomerSpawner
- Full shopping flow
- Payment wired to EconomyManager

---

## [0.6.0] — Phase 6: Objectives & Level _(not started)_

### Planned

- LevelObjectiveManager
- Satisfaction system
- LevelManager + star calculation (wires income multiplier)
- Mission UI panel

---

## [0.7.0] — Phase 7: Upgrade & UI _(not started)_

### Planned

- Grid-based shop placement
- 3-tier shop upgrade system
- **Full HUD (number-driven, fun):**
  - 💰 Coins + per-min income rate e.g. `+$48/min`
  - 📊 Level progress bar + % + goal number e.g. `67% (2/3)`
  - 💎 Gems display + collect mechanic
  - 🏪 Shop count e.g. `Shops: 3`
  - 👥 Active customer count e.g. `Customers: 7`
- Mission panel: task list + reward (coins/gems) + per-task progress bar
- Level complete panel: ⭐⭐⭐ + coins earned + next level
- Cinematic trigger on shop unlock

---

## [0.7B.0] — Phase 7B: Audio & FX _(not started)_

### Planned

- AudioManager.cs (BGM + SFX, DontDestroyOnLoad)
- FXManager.cs (object pool)
- FX_CoinEarn, FX_ShopUnlock, FX_LevelComplete, FX_GemCollect prefabs

---

## [0.8.0] — Phase 8: Save, Reset & Polish _(not started)_

### Planned

- SafeAreaHandler.cs
- SaveManager (PlayerPrefs)
- ResetManager + confirmation UI

---

## [0.9.0] — Phase 9: Build & Test _(in progress)_

### Done

- Android build settings configured (com.tusher.shoppingmalltycoon, IL2CPP, ARM64)
- Phase 1 installed and tested on Android ✅

### Remaining

- Full MVP test (after Phase 1–8 complete)
- Performance check + fix
- PC standalone build test

---

## [1.0.0] — MVP Release _(target)_

- Level 1–3 fully playable on Android
- All Phase 1–9 complete
- Audio + FX working
- Safe area handled on all Android devices
- Offline, no crashes
