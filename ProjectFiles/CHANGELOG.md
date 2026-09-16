# 🏬 Shopping Mall Tycoon — Changelog

---

## [0.1.0] — Phase 1 Scene Setup ✅ COMPLETE

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
- CameraManager.cs (Priority-based cinematic switching)
- CameraInputHandler.cs — New Input System + EnhancedTouchSupport
  - PC: mouse drag pan + scroll wheel zoom
  - Android: single finger drag pan + pinch zoom
  - Tested on both PC ✅ and Android ✅

### Lessons Learned

- Cinemachine 3.x: `Unity.Cinemachine` namespace (2.x থেকে আলাদা)
- Priority checkbox কাজ করে না → Output Channel দিয়ে VC isolate করতে হয়
- New Input System-এ `Input.GetTouch` কাজ করে না → `EnhancedTouchSupport` ব্যবহার করতে হয়
- Android build: Active Input Handling = Input System Package (New)

---

## [0.1.1] — Material & Lighting _(next up)_

### Planned

- URP Asset mobile settings (Additional Lights off, Post Processing off)
- Directional Light: rotation (50,-30,0), warm white (#FFF5E0), soft shadow 0.5
- Mall floor material (URP/Lit, #E8DCC8)
- Wall materials (URP/Lit, #F5F0E8)
- Ambient light setup (Flat, #8AA0B0)

> ⚠️ সব Material অবশ্যই URP/Lit বা URP/Simple Lit — Standard shader = pink

---

## [0.2.0] — Phase 2 Characters _(not started)_

### Planned

- CharacterBase.cs
- BaseAnimator controller
- Customer_Normal prefab
- AnimatorOverrideController system
- Animation clips wired

---

## [0.3.0] — Phase 3 Onboarding _(not started)_

### Planned

- Onboarding scene
- Gender + Age select UI
- OnboardingManager.cs
- Skip logic for returning players

---

## [0.4.0] — Phase 4 Economy & Shop _(not started)_

### Planned

- EconomyManager with 1.2x level multiplier
- ShopData ScriptableObject
- GroceryShop prefab
- ShopController.cs
- Build UI + cost deduction

---

## [0.5.0] — Phase 5 Customer System _(not started)_

### Planned

- NavMesh bake
- CustomerController state machine
- CustomerSpawner
- Full shopping flow
- Payment wired to EconomyManager

---

## [0.6.0] — Phase 6 Objectives & Level _(not started)_

### Planned

- LevelObjectiveManager
- Satisfaction system
- LevelManager + star calculation
- Mission UI

---

## [0.7.0] — Phase 7 Upgrade & UI _(not started)_

### Planned

- 3-tier shop upgrade
- Upgrade UI panel
- Full HUD
- Level complete panel
- Cinematic on shop unlock

---

## [0.7B.0] — Phase 7B Audio & FX _(not started)_

### Planned

- AudioManager.cs (BGM + SFX, DontDestroyOnLoad)
- BGM: bgm_menu, bgm_mall
- SFX: click, coin, customer arrive/pay, shop unlock, level complete
- FXManager.cs (object pool)
- FX_CoinEarn, FX_ShopUnlock, FX_LevelComplete prefabs

---

## [0.8.0] — Phase 8 Save, Reset & Polish _(not started)_

### Planned

- SafeAreaHandler.cs
- Canvas Scaler 1080×1920 setup
- SaveManager (PlayerPrefs)
- Save: coins, level, stars, shops, audio volumes, character
- ResetManager + confirmation UI

---

## [0.9.0] — Phase 9 Build & Test _(in progress)_

### Done

- Android build settings configured
  - Package: `com.tusher.shoppingmalltycoon`
  - IL2CPP, ARM64
- Phase 1 installed and tested on Android ✅

### Remaining

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
