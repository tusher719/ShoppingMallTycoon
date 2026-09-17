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

## [0.1.1] — Material & Lighting _(ongoing — apply as you go)_

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
  - Any State → all states (Trigger conditions, Has Exit Time: off)
- `Customer_Normal` prefab (placeholder Capsule)
  - Mesh: Capsule | Material: URP/Simple Lit, #4A90D9
  - Animator: Customer_Normal_Override controller
  - NavMeshAgent: Speed 3.5, Stopping Distance 1.5, Radius 0.3, Base Offset 0
  - Position Y: 1 (ground level)
- `Customer_Normal_Override.overrideController` → BaseAnimator override

### Lessons Learned

- AnimatorOverrideController slots only appear when BaseAnimator states have a Motion (clip) assigned
- Step 15 (animation clips) skipped — will wire when real 3D model arrives
- Drag-dropping prefab sets Y to 0 → spawn via code using CustomerSpawner

---

## [0.3.0] — Phase 3: Onboarding ✅ COMPLETE

### Done

#### Step 16 — LoadingScreen scene

- `LoadingManager.cs` — AsyncOperation with progress bar fill animation
- Routing logic: `PlayerPrefs.GetInt("save_onboarded") == 1` → loads `Level_01`, else → loads `Onboarding`
- UI: BG #1A1A2E, Logo text "MALL TYCOON" #F5A623, ProgressBar_Fill (Filled/Horizontal, UISprite, #F5A623), LoadingText
- Canvas Scaler: 1080×1920, Match 0.5
- ProgressBar Pos Y: -400, LoadingText Pos Y: -480 (tuned for both Windows and Android)

#### Steps 17–20 — Onboarding scene

- Gender selection: BtnMale / BtnFemale
- Age group selection: BtnYoung / BtnAdult / BtnSenior
- `OnboardingManager.cs`: buttons wired via `AddListener` (not Inspector OnClick)
- Visual feedback: default color #444466, selected color #4A90D9
- START GAME button disabled until both gender and age are selected
- On confirm: saves `save_char_gender`, `save_char_age`, `save_onboarded = 1` → loads Level_01
- Returning player skip: LoadingManager checks `save_onboarded` and bypasses Onboarding

#### Step 21 — TutorialManager _(next)_

- 4-step arrow + message overlay inside Level_01
- Step 1: "Tap here to build your first shop!" → Build button area
- Step 2: "Customers will enter through here" → South wall entrance gap
- Step 3: "Place your shop anywhere on the floor" → Mall floor center
- Step 4: "Earn money and upgrade your shops!" → HUD top area

### Lessons Learned

- `Image Type: Filled` requires a Source Image (e.g. UISprite) to be assigned first
- Canvas Match 0.5 is required for correct scaling across portrait and landscape
- Buttons should be wired via `AddListener` in script — cleaner than Inspector OnClick

---

## [0.4.0] — Phase 4: Economy & Shop _(not started)_

### Planned

- EconomyManager with 1.2x level multiplier
- ShopData ScriptableObject
- GroceryShop prefab
- ShopController.cs
- Build UI + cost deduction

---

## [0.5.0] — Phase 5: Customer System _(not started)_

### Planned

- NavMesh bake on Level_01
- CustomerController state machine
- CustomerSpawner
- Full shopping flow
- Payment wired to EconomyManager

---

## [0.6.0] — Phase 6: Objectives & Level _(not started)_

### Planned

- LevelObjectiveManager
- Satisfaction system
- LevelManager + star calculation
- Mission UI

---

## [0.7.0] — Phase 7: Upgrade & UI _(not started)_

### Planned

- 3-tier shop upgrade
- Upgrade UI panel
- Full HUD
- Level complete panel
- Cinematic trigger on shop unlock

---

## [0.7B.0] — Phase 7B: Audio & FX _(not started)_

### Planned

- AudioManager.cs (BGM + SFX, DontDestroyOnLoad)
- BGM: bgm_menu, bgm_mall
- SFX: click, coin, customer arrive/pay, shop unlock, level complete
- FXManager.cs (object pool)
- FX_CoinEarn, FX_ShopUnlock, FX_LevelComplete prefabs

---

## [0.8.0] — Phase 8: Save, Reset & Polish _(not started)_

### Planned

- SafeAreaHandler.cs
- Canvas Scaler 1080×1920 setup
- SaveManager (PlayerPrefs)
- Save: coins, level, stars, shops, audio volumes, character
- ResetManager + confirmation UI

---

## [0.9.0] — Phase 9: Build & Test _(in progress)_

### Done

- Android build settings configured
  - Package: `com.tusher.shoppingmalltycoon`
  - IL2CPP, ARM64
- Phase 1 installed and tested on Android ✅

### Remaining

- Full MVP test (after Phase 1–8 complete)
- Performance check + fix
- PC standalone build test

---

## [1.0.0] — MVP Release _(target)_

### Goal

- Level 1–3 fully playable on Android
- All Phase 1–9 complete
- Audio + FX working
- Safe area handled on all Android devices
- Offline, no crashes
