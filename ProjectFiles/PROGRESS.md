# 🏬 Shopping Mall Tycoon — Progress Tracker

## 🔧 Dev Info

- **Unity:** 6000.0.47f1 (URP)
- **Platform:** Android (primary), PC (secondary)
- **Repo:** https://github.com/tusher719/ShoppingMallTycoon
- **Current Phase:** Phase 6 — Objectives & Level (Step 32 next)

---

## Phase 1 — Scene Setup ✅ COMPLETE

| #   | Task                        | Status | Commit                                  |
| --- | --------------------------- | ------ | --------------------------------------- |
| 01  | Unity project (3D URP)      | ✅     | `init: setup Unity 6 URP project`       |
| 02  | Folder structure            | ✅     | `init: add project folder structure`    |
| 03  | Level_01 scene + Hierarchy  | ✅     | `init: add Level_01 scene`              |
| 04  | Mall floor (Plane, 40×40)   | ✅     | `level01: add mall floor blockout`      |
| 05  | Walls + entrance gap        | ✅     | `level01: add walls and entrance gap`   |
| 06  | Cinemachine 3.1.7           | ✅     | `camera: install Cinemachine package`   |
| 07  | Gameplay camera (isometric) | ✅     | `camera: add isometric gameplay camera` |
| 08  | Cinematic virtual cameras   | ✅     | `camera: add cinematic virtual cameras` |
| 09  | CameraManager.cs            | ✅     | `camera: add CameraManager script`      |
| 10  | Mobile camera controls      | ✅     | `camera: add mobile touch controls`     |

---

## Material & Lighting — apply as you go

| #   | Task                                    | Status |
| --- | --------------------------------------- | ------ |
| L1  | URP Asset mobile settings               | ⬜     |
| L2  | Directional Light (50,-30,0) warm white | ⬜     |
| L3  | Floor material (URP/Lit, #E8DCC8)       | ⬜     |
| L4  | Wall materials (URP/Lit, #F5F0E8)       | ⬜     |
| L5  | Ambient light (Flat, #8AA0B0)           | ⬜     |

---

## Phase 2 — Character System ✅ COMPLETE

| #   | Task                       | Status  | Commit                                  |
| --- | -------------------------- | ------- | --------------------------------------- |
| 11  | CharacterBase.cs           | ✅      | `character: add CharacterBase script`   |
| 12  | BaseAnimator controller    | ✅      | `anim: add BaseAnimator controller`     |
| 13  | Customer_Normal prefab     | ✅      | `character: add Customer_Normal prefab` |
| 14  | AnimatorOverrideController | ✅      | `anim: add animation override system`   |
| 15  | Animation clips            | ⏭ Skip | Wire when real 3D model arrives         |

---

## Phase 3 — Onboarding ✅ COMPLETE

| #   | Task                              | Status | Commit                                          |
| --- | --------------------------------- | ------ | ----------------------------------------------- |
| 16  | LoadingScreen + LoadingManager.cs | ✅     | `loading: add LoadingScreen and LoadingManager` |
| 17  | Onboarding scene                  | ✅     | `onboard: add Onboarding scene`                 |
| 18  | Gender select UI                  | ✅     | `onboard: add gender select UI`                 |
| 19  | Age group select UI               | ✅     | `onboard: add age select UI`                    |
| 20  | OnboardingManager.cs              | ✅     | `onboard: add OnboardingManager`                |
| 21  | TutorialManager.cs                | ✅     | `tutorial: add in-game tutorial system`         |

---

## Phase 4 — Economy & Shop ✅ COMPLETE

| #   | Task                      | Status | Commit                                        |
| --- | ------------------------- | ------ | --------------------------------------------- |
| 22  | EconomyManager.cs         | ✅     | `economy: add EconomyManager with multiplier` |
| 23  | ShopData ScriptableObject | ✅     | `shop: add ShopData ScriptableObject`         |
| 24  | GroceryShop prefab        | ✅     | `shop: add GroceryShop prefab`                |
| 25  | ShopController.cs         | ✅     | `shop: add ShopController`                    |
| 26  | BuildUI.cs + Build Panel  | ✅     | `shop: add build UI and deduct cost`          |

---

## Phase 5 — Customer System ✅ COMPLETE

| #   | Task                                  | Status | Commit                                           |
| --- | ------------------------------------- | ------ | ------------------------------------------------ |
| 27  | NavMesh bake on Level_01              | ✅     | `nav: bake NavMesh on Level_01`                  |
| 28  | CustomerController.cs (state machine) | ✅     | `customer: add CustomerController state machine` |
| 29  | CustomerSpawner.cs                    | ✅     | `customer: add CustomerSpawner`                  |
| 30  | Full shopping flow test               | ✅     | `customer: full shopping flow tested`            |
| 31  | Payment → EconomyManager              | ✅     | `customer: wire payment to EconomyManager`       |

---

## Phase 6 — Objectives & Level _(next)_

| #   | Task                                     | Status | Commit                             |
| --- | ---------------------------------------- | ------ | ---------------------------------- |
| 32  | LevelObjectiveManager.cs                 | ⬜     | `level: add LevelObjectiveManager` |
| 33  | Track: shops, customers, money           | ⬜     | `level: track core objectives`     |
| 34  | Satisfaction system                      | ⬜     | `level: add satisfaction system`   |
| 35  | LevelManager.cs + star calc + multiplier | ⬜     | `level: add LevelManager`          |
| 36  | Mission UI panel                         | ⬜     | `ui: add mission panel`            |

---

## Phase 7 — Upgrade & UI _(not started)_

| #   | Task                         | Status | Commit                                     | 
| --- | ---------------------------- | ------ | ------------------------------------------ |
| 37  | Grid-based shop placement    | ⬜     | `shop: add grid placement system`          |
| 38  | Shop upgrade logic (3 tiers) | ⬜     | `shop: add 3-tier upgrade logic`           |
| 38B | Shop upgrade UI              | ⬜     | `ui: add shop upgrade panel`               |
| 39  | Full HUD                     | ⬜     | `ui: add full HUD`                         |
| 40  | Gem system                   | ⬜     | `economy: add gem system`                  |
| 41  | Mission panel                | ⬜     | `ui: add mission panel`                    |
| 42  | Level complete panel         | ⬜     | `ui: add level complete panel`             |
| 43  | Cinematic on shop unlock     | ⬜     | `camera: trigger cinematic on shop unlock` |

---

## Phase 7B — Audio & FX _(not started)_

| #   | Task                       | Status | Commit                                   |
| --- | -------------------------- | ------ | ---------------------------------------- |
| 44  | AudioManager.cs            | ⬜     | `audio: add AudioManager`                |
| 45  | BGM clips + level theme    | ⬜     | `audio: add BGM clips and level theme`   |
| 46  | SFX clips                  | ⬜     | `audio: wire SFX clips`                  |
| 47  | FXManager.cs (object pool) | ⬜     | `fx: add FXManager with object pool`     |
| 48  | FX_CoinEarn                | ⬜     | `fx: add coin earn particle effect`      |
| 49  | FX_ShopUnlock              | ⬜     | `fx: add shop unlock particle effect`    |
| 50  | FX_LevelComplete           | ⬜     | `fx: add level complete particle effect` |
| 51  | FX_GemCollect              | ⬜     | `fx: add gem collect particle effect`    |

---

## Phase 8 — Save & Polish _(not started)_

| #   | Task                           | Status | Commit                                     |
| --- | ------------------------------ | ------ | ------------------------------------------ |
| 52  | SafeAreaHandler.cs             | ⬜     | `ui: add safe area handler`                |
| 53  | Canvas Scaler (1080×1920, 0.5) | ⬜     | `ui: configure canvas scaler`              |
| 54  | SaveManager.cs (PlayerPrefs)   | ⬜     | `save: add PlayerPrefs SaveManager`        |
| 55  | Save onboarding + audio        | ⬜     | `save: save onboarding and audio settings` |
| 56  | Save coins, level, shop, stars | ⬜     | `save: save game state`                    |
| 57  | Load on start                  | ⬜     | `save: load state on start`                |
| 58  | ResetManager.cs                | ⬜     | `reset: add game reset system`             |
| 59  | Reset confirmation UI          | ⬜     | `ui: add reset confirmation dialog`        |

---

## Phase 9 — Build & Test _(in progress)_

| #   | Task                    | Status | Commit                                  |
| --- | ----------------------- | ------ | --------------------------------------- |
| 60  | Android build settings  | ✅     | `build: configure Android settings`     |
| 61  | Phase 1 Android test    | ✅     | `build: MVP Phase 1 Android test build` |
| 62  | Performance check + fix | ⬜     | `build: mobile performance pass`        |
| 63  | PC standalone test      | ⬜     | `build: PC standalone test build`       |

---

## 📌 Current Step

**→ Step 32: LevelObjectiveManager.cs (Phase 6 start)**

---

## 🐛 Known Issues

- SpawnPoint wall-এর কাছে — Phase 7-এ adjust করবো
- Shop একই position-এ spawn হয় — Phase 7 grid system-এ fix
- Step 15 (animation clips) skipped — real model আসলে করবো
- NavMeshAgent on Customer_Normal disabled until Step 27 ✅ fixed

## 💡 Decisions Made

- Grid tile: 5×5 units
- Shop spawn: fixed (0,0,5) until Phase 7
- Mall Level 1: 40×40, 8-unit entrance gap South (X -4 to +4)
- Camera: Isometric Ortho + Cinemachine 3.x
- Input: New Input System Package
- No player control — management game only
- MVP: Level 1–3, single shop type, no staff
- Income multiplier: `Mathf.Pow(1.2f, levelIndex)` — Phase 6 Step 35
- Income interval: 5s per shop
- Dynamic shop cost: `baseCost * Mathf.Pow(1.5f, shopsBuilt)` — Phase 7
- CustomerSpawner: event-driven via OnShopBuilt — never Inspector assign
- ShopController.Data property — never .shopData direct
- Button disabled: grey (#666) + white text
- HUD: number-driven, per-min income, progress %, gems, shop+customer count
- Gem system: earn milestones/missions, spend upgrade/speed, tap FX to collect
- Audio: singleton BGM loop + SFX PlayOneShot
- FX: FXManager object pool
- Save Phase 1: PlayerPrefs | Phase 2: JSON | Future: Remote DB
- Android: `com.tusher.shoppingmalltycoon`, IL2CPP, ARM64
