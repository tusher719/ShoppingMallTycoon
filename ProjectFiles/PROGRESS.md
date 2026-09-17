# 🏬 Shopping Mall Tycoon — Progress Tracker

> Mark each step ✅ when done and commit to Git.

---

## 🔧 Dev Info

- **Unity:** 6000.0.47f1 (URP)
- **Platform:** Android (primary), PC (secondary)
- **Repo:** https://github.com/tusher719/ShoppingMallTycoon
- **Current Phase:** Phase 3 — Onboarding (Step 21 next)

---

## 📦 Phase 1 — Project Setup & Scene ✅ COMPLETE

| #   | Task                                    | Status  | Git Commit Message                      |
| --- | --------------------------------------- | ------- | --------------------------------------- |
| 01  | Unity project create (3D URP)           | ✅ Done | `init: setup Unity 6 URP project`       |
| 02  | Folder structure create                 | ✅ Done | `init: add project folder structure`    |
| 03  | Level_01 scene create + Hierarchy setup | ✅ Done | `init: add Level_01 scene`              |
| 04  | Mall floor blockout (Plane, 40×40)      | ✅ Done | `level01: add mall floor blockout`      |
| 05  | Walls + entrance gap blockout           | ✅ Done | `level01: add walls and entrance gap`   |
| 06  | Cinemachine 3.1.7 install               | ✅ Done | `camera: install Cinemachine package`   |
| 07  | Gameplay camera (isometric) setup       | ✅ Done | `camera: add isometric gameplay camera` |
| 08  | Cinematic virtual cameras setup         | ✅ Done | `camera: add cinematic virtual cameras` |
| 09  | CameraManager.cs                        | ✅ Done | `camera: add CameraManager script`      |
| 10  | Mobile camera controls (drag/pinch)     | ✅ Done | `camera: add mobile touch controls`     |

**Notes:**

- Cinemachine 3.x namespace: `Unity.Cinemachine` (different from 2.x)
- Cinematic VCs: Output Channel = Channel02, Brain Channel Mask = Default only
- Camera controls: New Input System + EnhancedTouchSupport — tested on PC ✅ and Android ✅

---

## 🎨 Material & Lighting — TODO _(apply as you go)_

| #   | Task                                           | Status | Notes                                      |
| --- | ---------------------------------------------- | ------ | ------------------------------------------ |
| L1  | URP Asset mobile settings configure            | ⬜     | Additional Lights off, Post Processing off |
| L2  | Directional Light setup (50,-30,0), warm white | ⬜     | Soft shadow, strength 0.5                  |
| L3  | Mall floor material (URP/Lit, #E8DCC8)         | ⬜     | Replace default grey plane                 |
| L4  | Wall materials (URP/Lit, #F5F0E8)              | ⬜     | Apply to all walls                         |
| L5  | Ambient light setup (Flat, #8AA0B0)            | ⬜     | Window → Rendering → Lighting              |

> ⚠️ All new materials must use URP/Lit or URP/Simple Lit. Standard shader = pink in URP.

---

## 🎭 Phase 2 — Character & Animation System ✅ COMPLETE

| #   | Task                                   | Status  | Git Commit Message                      |
| --- | -------------------------------------- | ------- | --------------------------------------- |
| 11  | CharacterBase.cs (shared logic)        | ✅ Done | `character: add CharacterBase script`   |
| 12  | BaseAnimator controller (shared)       | ✅ Done | `anim: add BaseAnimator controller`     |
| 13  | Customer_Normal prefab + animator      | ✅ Done | `character: add Customer_Normal prefab` |
| 14  | Animation override controller setup    | ✅ Done | `anim: add animation override system`   |
| 15  | Walk / Idle / Browse / Pay clips wired | ⏭ Skip | Wire when real 3D model arrives         |

**Notes:**

- Placeholder: Capsule mesh, URP/Simple Lit, #4A90D9
- BaseAnimator: 4 states (Idle, Walk, Browse, Pay), Any State transitions, Trigger parameters
- AnimatorOverrideController: `Customer_Normal_Override` → BaseAnimator override
- NavMeshAgent: Speed 3.5, Stopping Distance 1.5, Radius 0.3, Base Offset 0
- Prefab position Y:1 (Capsule height = 2, pivot at center)

---

## 🎬 Phase 3 — Onboarding _(in progress)_

| #   | Task                                           | Status  | Git Commit Message                              |
| --- | ---------------------------------------------- | ------- | ----------------------------------------------- |
| 16  | LoadingScreen scene + LoadingManager.cs        | ✅ Done | `loading: add LoadingScreen and LoadingManager` |
| 17  | Onboarding scene create                        | ✅ Done | `onboard: add Onboarding scene`                 |
| 18  | Gender select UI (Male/Female)                 | ✅ Done | `onboard: add gender select UI`                 |
| 19  | Age group select UI (Young/Adult/Senior)       | ✅ Done | `onboard: add age select UI`                    |
| 20  | OnboardingManager.cs + skip logic              | ✅ Done | `onboard: add OnboardingManager`                |
| 21  | TutorialManager.cs (arrow + message, Level_01) | ⬜      | `tutorial: add in-game tutorial system`         |

**Notes:**

- LoadingManager routes based on `save_onboarded` PlayerPrefs key
- Buttons wired via `AddListener` — not Inspector OnClick
- Selected button color: #4A90D9 | Default: #444466
- START GAME disabled until both gender and age are selected
- `Image Type: Filled` requires UISprite assigned to Source Image first
- Canvas Match 0.5 needed for correct scaling on both Android and Windows

**Tutorial Plan (Step 21):**

| Step | Arrow Target          | Message                                 |
| ---- | --------------------- | --------------------------------------- |
| 1    | Build button (bottom) | "Tap here to build your first shop!"    |
| 2    | South wall entrance   | "Customers will enter through here"     |
| 3    | Mall floor center     | "Place your shop anywhere on the floor" |
| 4    | HUD top area          | "Earn money and upgrade your shops!"    |

---

## 💰 Phase 4 — Economy & Shop _(not started)_

| #   | Task                                       | Status | Git Commit Message                            |
| --- | ------------------------------------------ | ------ | --------------------------------------------- |
| 22  | EconomyManager.cs (with income multiplier) | ⬜     | `economy: add EconomyManager with multiplier` |
| 23  | ShopData ScriptableObject                  | ⬜     | `shop: add ShopData ScriptableObject`         |
| 24  | GroceryShop prefab (blockout)              | ⬜     | `shop: add GroceryShop prefab`                |
| 25  | ShopController.cs                          | ⬜     | `shop: add ShopController`                    |
| 26  | Build Shop UI + deduct money               | ⬜     | `shop: add build UI and deduct cost`          |

---

## 👤 Phase 5 — Customer System _(not started)_

| #   | Task                                                          | Status | Git Commit Message                         |
| --- | ------------------------------------------------------------- | ------ | ------------------------------------------ |
| 27  | NavMesh bake on Level_01                                      | ⬜     | `nav: bake NavMesh on Level_01`            |
| 28  | CustomerController.cs (state machine)                         | ⬜     | `customer: add CustomerController`         |
| 29  | CustomerSpawner.cs                                            | ⬜     | `customer: add CustomerSpawner`            |
| 30  | Customer flow: Spawn → Shop → Shelf → Queue → Checkout → Exit | ⬜     | `customer: add full shopping flow`         |
| 31  | Customer pays → EconomyManager (with multiplier)              | ⬜     | `customer: wire payment to EconomyManager` |

---

## 🎯 Phase 6 — Objectives, Level & Satisfaction _(not started)_

| #   | Task                                               | Status | Git Commit Message                        |
| --- | -------------------------------------------------- | ------ | ----------------------------------------- |
| 32  | LevelObjectiveManager.cs                           | ⬜     | `level: add LevelObjectiveManager`        |
| 33  | Track: shops built, customers served, money earned | ⬜     | `level: track core objectives`            |
| 34  | Satisfaction system                                | ⬜     | `level: add satisfaction system`          |
| 35  | LevelManager.cs — level complete + star calc       | ⬜     | `level: add level complete and star calc` |
| 36  | Mission UI panel                                   | ⬜     | `ui: add mission panel`                   |

---

## ⬆️ Phase 7 — Upgrade & UI _(not started)_

| #   | Task                                    | Status | Git Commit Message                         |
| --- | --------------------------------------- | ------ | ------------------------------------------ |
| 37  | Shop upgrade logic (3 tiers)            | ⬜     | `shop: add 3-tier upgrade logic`           |
| 38  | Shop upgrade UI panel                   | ⬜     | `ui: add shop upgrade panel`               |
| 39  | HUD — money, stars, level, satisfaction | ⬜     | `ui: add HUD elements`                     |
| 40  | Level complete panel (stars + reward)   | ⬜     | `ui: add level complete panel`             |
| 41  | Cinematic trigger on shop unlock        | ⬜     | `camera: trigger cinematic on shop unlock` |

---

## 🔊 Phase 7B — Audio & FX _(not started)_

| #   | Task                                           | Status | Git Commit Message                       |
| --- | ---------------------------------------------- | ------ | ---------------------------------------- |
| 42  | AudioManager.cs (BGM + SFX, singleton)         | ⬜     | `audio: add AudioManager`                |
| 43  | BGM clips add + level theme play               | ⬜     | `audio: add BGM clips and level theme`   |
| 44  | SFX clips wire (click, coin, unlock, complete) | ⬜     | `audio: wire SFX clips`                  |
| 45  | FXManager.cs (object pool)                     | ⬜     | `fx: add FXManager with object pool`     |
| 46  | FX_CoinEarn prefab + trigger                   | ⬜     | `fx: add coin earn particle effect`      |
| 47  | FX_ShopUnlock prefab + trigger                 | ⬜     | `fx: add shop unlock particle effect`    |
| 48  | FX_LevelComplete prefab + trigger              | ⬜     | `fx: add level complete particle effect` |

---

## 💾 Phase 8 — Save, Reset & Polish _(not started)_

| #   | Task                                       | Status | Git Commit Message                         |
| --- | ------------------------------------------ | ------ | ------------------------------------------ |
| 49  | SafeAreaHandler.cs (notch fix)             | ⬜     | `ui: add safe area handler`                |
| 50  | Canvas Scaler setup (1080×1920, match 0.5) | ⬜     | `ui: configure canvas scaler for mobile`   |
| 51  | SaveManager.cs (PlayerPrefs)               | ⬜     | `save: add PlayerPrefs SaveManager`        |
| 52  | Save character selection + BGM/SFX volume  | ⬜     | `save: save onboarding and audio settings` |
| 53  | Save: coins, level, shop state, stars      | ⬜     | `save: save game state`                    |
| 54  | Load on game start                         | ⬜     | `save: load state on start`                |
| 55  | ResetManager.cs (PC + Android)             | ⬜     | `reset: add game reset system`             |
| 56  | Reset confirmation UI                      | ⬜     | `ui: add reset confirmation dialog`        |

---

## 📱 Phase 9 — Build & Test _(in progress)_

| #   | Task                             | Status  | Git Commit Message                      |
| --- | -------------------------------- | ------- | --------------------------------------- |
| 57  | Android build settings configure | ✅ Done | `build: configure Android settings`     |
| 58  | Install on phone — test Phase 1  | ✅ Done | `build: MVP Phase 1 Android test build` |
| 59  | Performance check + fix          | ⬜      | `build: mobile performance pass`        |
| 60  | PC standalone build test         | ⬜      | `build: PC standalone test build`       |

---

## 📌 Current Step

**→ Step 21: TutorialManager.cs — in-game tutorial arrows + messages in Level_01**

---

## 🐛 Known Issues / Blockers

- Cinemachine 3.x Priority checkbox does not work → solved with Output Channel isolation
- `EnhancedTouchSupport.Enable()` must be called in OnEnable for touch input
- Drag-dropping prefab resets Y to 0 → spawn via CustomerSpawner in code
- `Image Type: Filled` requires a Source Image (UISprite) assigned before option appears
- Canvas Match must be 0.5 for correct scaling on both portrait and landscape

---

## 💡 Decisions Made

- Grid tile size: 5×5 units
- Mall Level 1: 40×40 units floor, 8-unit entrance gap (South wall, X -4 to +4)
- Camera: Isometric Orthographic + Cinemachine 3.x cinematics
- Cinemachine 3.x: `Unity.Cinemachine` namespace, Channel-based VC isolation
- Input: New Input System Package (Active Input Handling)
- No player character control — management game only
- MVP scope: Level 1–3, single shop type, no staff
- Character system: CharacterBase + AnimatorOverrideController (modular, extensible)
- Customer placeholder: Capsule, URP/Simple Lit #4A90D9, Y:1
- Animation clips: wire when real 3D model arrives (Step 15 skipped)
- First launch: gender + age onboarding before Level_01
- Income multiplier: `Mathf.Pow(1.2f, levelIndex)` per level
- Audio: AudioManager singleton, BGM loop + SFX PlayOneShot
- FX: FXManager object pool, 3 effect types (CoinEarn, ShopUnlock, LevelComplete)
- UI: Canvas Scaler 1080×1920, Match 0.5, SafeAreaHandler for notch support
- Save Phase 1: PlayerPrefs | Phase 2: JSON | Future: Remote DB
- Reset: clears all PlayerPrefs data, reloads from Onboarding
- Materials: URP/Lit or URP/Simple Lit only — Standard shader not allowed
- Lighting: Directional (50,-30,0), warm white #FFF5E0, soft shadow strength 0.5
- Android build: package `com.tusher.shoppingmalltycoon`, IL2CPP, ARM64
- Buttons wired via AddListener in script — not Inspector OnClick
- LoadingScreen ProgressBar Pos Y: -400, LoadingText Pos Y: -480 (works on both Windows and Android)
