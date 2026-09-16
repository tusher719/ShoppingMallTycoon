# 🏬 Shopping Mall Tycoon — Changelog

---

## [0.1.0] — Phase 1 Scene Setup _(in progress)_

### Done

- Unity 6 URP project initialized
- Folder structure created under `Assets/_Game/`
- `Level_01` scene created with Hierarchy separators
- Mall floor blockout (Plane, 40×40 units)
- Walls blockout (North, West, East, South_Left, South_Right)
- Entrance gap: 8 units at South wall (X -4 to +4)

### Remaining

- Cinemachine package install
- Isometric gameplay camera
- Cinematic virtual cameras (VC_Gameplay, VC_ShopUnlock, etc.)
- CameraManager.cs
- Mobile touch controls (drag pan, pinch zoom)

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

## [0.9.0] — Phase 9 Build & Test _(not started)_

### Planned

- Android build configure
- Install on phone — full playtest
- Performance pass
- PC standalone build

---

## [1.0.0] — MVP Release _(target)_

### Goal

- Level 1–3 fully playable on Android
- All Phase 1–9 complete
- Audio + FX working
- Safe area handled on all Android devices
- Offline, no crashes
