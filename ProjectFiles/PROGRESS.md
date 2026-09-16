# 🏬 Shopping Mall Tycoon — Progress Tracker

> প্রতিটা step শেষে ✅ দাও এবং git commit করো।

---

## 🔧 Dev Info

- **Unity:** 6000.0.47f1 (URP)
- **Platform:** Android (primary), PC (secondary)
- **Repo:** https://github.com/tusher719/ShoppingMallTycoon
- **Current Phase:** Phase 1 — Scene Setup & Camera

---

## 📦 Phase 1 — Project Setup & Scene

| #   | Task                                    | Status  | Git Commit Message                      |
| --- | --------------------------------------- | ------- | --------------------------------------- |
| 01  | Unity project create (3D URP)           | ✅ Done | `init: setup Unity 6 URP project`       |
| 02  | Folder structure create                 | ✅ Done | `init: add project folder structure`    |
| 03  | Level_01 scene create + Hierarchy setup | ✅ Done | `init: add Level_01 scene`              |
| 04  | Mall floor blockout (Plane, 40×40)      | ✅ Done | `level01: add mall floor blockout`      |
| 05  | Walls + entrance gap blockout           | ✅ Done | `level01: add walls and entrance gap`   |
| 06  | Cinemachine package install             | ⬜      | `camera: install Cinemachine package`   |
| 07  | Gameplay camera (isometric) setup       | ⬜      | `camera: add isometric gameplay camera` |
| 08  | Cinematic virtual cameras setup         | ⬜      | `camera: add cinematic virtual cameras` |
| 09  | CameraManager.cs                        | ⬜      | `camera: add CameraManager script`      |
| 10  | Mobile camera controls (drag/pinch)     | ⬜      | `camera: add mobile touch controls`     |

---

## 🎭 Phase 2 — Character & Animation System

| #   | Task                                   | Status | Git Commit Message                      |
| --- | -------------------------------------- | ------ | --------------------------------------- |
| 11  | CharacterBase.cs (shared logic)        | ⬜     | `character: add CharacterBase script`   |
| 12  | BaseAnimator controller (shared)       | ⬜     | `anim: add BaseAnimator controller`     |
| 13  | Customer_Normal prefab + animator      | ⬜     | `character: add Customer_Normal prefab` |
| 14  | Animation override controller setup    | ⬜     | `anim: add animation override system`   |
| 15  | Walk / Idle / Browse / Pay clips wired | ⬜     | `anim: wire customer animation clips`   |

---

## 🎮 Phase 3 — Onboarding

| #   | Task                                  | Status | Git Commit Message                  |
| --- | ------------------------------------- | ------ | ----------------------------------- |
| 16  | Onboarding scene create               | ⬜     | `onboard: add Onboarding scene`     |
| 17  | Gender select UI (Male/Female)        | ⬜     | `onboard: add gender select UI`     |
| 18  | Age group select UI                   | ⬜     | `onboard: add age select UI`        |
| 19  | OnboardingManager.cs (save selection) | ⬜     | `onboard: add OnboardingManager`    |
| 20  | Skip onboarding if already done       | ⬜     | `onboard: skip if returning player` |

---

## 💰 Phase 4 — Economy & Shop

| #   | Task                                       | Status | Git Commit Message                            |
| --- | ------------------------------------------ | ------ | --------------------------------------------- |
| 21  | EconomyManager.cs (with income multiplier) | ⬜     | `economy: add EconomyManager with multiplier` |
| 22  | ShopData ScriptableObject                  | ⬜     | `shop: add ShopData ScriptableObject`         |
| 23  | GroceryShop prefab (blockout)              | ⬜     | `shop: add GroceryShop prefab`                |
| 24  | ShopController.cs                          | ⬜     | `shop: add ShopController`                    |
| 25  | Build Shop UI + deduct money               | ⬜     | `shop: add build UI and deduct cost`          |

---

## 👤 Phase 5 — Customer System

| #   | Task                                                          | Status | Git Commit Message                         |
| --- | ------------------------------------------------------------- | ------ | ------------------------------------------ |
| 26  | NavMesh bake on Level_01                                      | ⬜     | `nav: bake NavMesh on Level_01`            |
| 27  | CustomerController.cs (state machine)                         | ⬜     | `customer: add CustomerController`         |
| 28  | CustomerSpawner.cs                                            | ⬜     | `customer: add CustomerSpawner`            |
| 29  | Customer flow: Spawn → Shop → Shelf → Queue → Checkout → Exit | ⬜     | `customer: add full shopping flow`         |
| 30  | Customer pays → EconomyManager (with multiplier)              | ⬜     | `customer: wire payment to EconomyManager` |

---

## 🎯 Phase 6 — Objectives, Level & Satisfaction

| #   | Task                                               | Status | Git Commit Message                        |
| --- | -------------------------------------------------- | ------ | ----------------------------------------- |
| 31  | LevelObjectiveManager.cs                           | ⬜     | `level: add LevelObjectiveManager`        |
| 32  | Track: shops built, customers served, money earned | ⬜     | `level: track core objectives`            |
| 33  | Satisfaction system                                | ⬜     | `level: add satisfaction system`          |
| 34  | LevelManager.cs — level complete + star calc       | ⬜     | `level: add level complete and star calc` |
| 35  | Mission UI panel                                   | ⬜     | `ui: add mission panel`                   |

---

## ⬆️ Phase 7 — Upgrade & UI

| #   | Task                                    | Status | Git Commit Message                         |
| --- | --------------------------------------- | ------ | ------------------------------------------ |
| 36  | Shop upgrade logic (3 tiers)            | ⬜     | `shop: add 3-tier upgrade logic`           |
| 37  | Shop upgrade UI panel                   | ⬜     | `ui: add shop upgrade panel`               |
| 38  | HUD — money, stars, level, satisfaction | ⬜     | `ui: add HUD elements`                     |
| 39  | Level complete panel (stars + reward)   | ⬜     | `ui: add level complete panel`             |
| 40  | Cinematic trigger on shop unlock        | ⬜     | `camera: trigger cinematic on shop unlock` |

---

## 🔊 Phase 7B — Audio & FX

| #   | Task                                           | Status | Git Commit Message                       |
| --- | ---------------------------------------------- | ------ | ---------------------------------------- |
| 41  | AudioManager.cs (BGM + SFX, singleton)         | ⬜     | `audio: add AudioManager`                |
| 42  | BGM clips add + level theme play               | ⬜     | `audio: add BGM clips and level theme`   |
| 43  | SFX clips wire (click, coin, unlock, complete) | ⬜     | `audio: wire SFX clips`                  |
| 44  | FXManager.cs (object pool)                     | ⬜     | `fx: add FXManager with object pool`     |
| 45  | FX_CoinEarn prefab + trigger                   | ⬜     | `fx: add coin earn particle effect`      |
| 46  | FX_ShopUnlock prefab + trigger                 | ⬜     | `fx: add shop unlock particle effect`    |
| 47  | FX_LevelComplete prefab + trigger              | ⬜     | `fx: add level complete particle effect` |

---

## 💾 Phase 8 — Save, Reset & Polish

| #   | Task                                       | Status | Git Commit Message                         |
| --- | ------------------------------------------ | ------ | ------------------------------------------ |
| 48  | SafeAreaHandler.cs (notch fix)             | ⬜     | `ui: add safe area handler`                |
| 49  | Canvas Scaler setup (1080×1920, match 0.5) | ⬜     | `ui: configure canvas scaler for mobile`   |
| 50  | SaveManager.cs (PlayerPrefs)               | ⬜     | `save: add PlayerPrefs SaveManager`        |
| 51  | Save character selection + BGM/SFX volume  | ⬜     | `save: save onboarding and audio settings` |
| 52  | Save: coins, level, shop state, stars      | ⬜     | `save: save game state`                    |
| 53  | Load on game start                         | ⬜     | `save: load state on start`                |
| 54  | ResetManager.cs (PC + Android)             | ⬜     | `reset: add game reset system`             |
| 55  | Reset confirmation UI                      | ⬜     | `ui: add reset confirmation dialog`        |

---

## 📱 Phase 9 — Build & Test

| #   | Task                                  | Status | Git Commit Message                      |
| --- | ------------------------------------- | ------ | --------------------------------------- |
| 56  | Android build settings configure      | ⬜     | `build: configure Android settings`     |
| 57  | **Install on phone — test Phase 1–8** | ⬜     | `build: MVP Phase 1 Android test build` |
| 58  | Performance check + fix               | ⬜     | `build: mobile performance pass`        |
| 59  | PC standalone build test              | ⬜     | `build: PC standalone test build`       |

---

## 🔮 Phase 10 — Level 2 & 3 (after MVP test ✅)

> Phase 9 test pass হলে শুরু করবো

- Level 2: Second shop unlock + cinematic
- Level 3: Customer types intro
- Level 4: Staff system
- Level 5: Mall expansion / new tile

---

## 📌 Current Step

**→ Step 06: Cinemachine package install**

---

## 🐛 Known Issues / Blockers

- (none yet)

---

## 💡 Decisions Made

- Grid tile size: 5×5 units
- Mall Level 1: 40×40 units floor, entrance gap 8 units (South wall, X -4 to +4)
- Camera: Isometric gameplay + Cinemachine cinematics
- No player character control — management only
- MVP: Level 1–3, single shop, no staff
- Character system: CharacterBase + AnimatorOverrideController (modular)
- First launch: gender + age onboarding
- Income: 1.2x multiplier per level `Mathf.Pow(1.2f, levelIndex)`
- Audio: AudioManager singleton, BGM loop + SFX PlayOneShot
- FX: FXManager object pool, 3 effects (CoinEarn, ShopUnlock, LevelComplete)
- UI: Canvas Scaler 1080×1920, match 0.5, SafeAreaHandler for notch
- Save Phase 1: PlayerPrefs | Phase 2: JSON | Future: Remote DB
- Reset: clears all data, restarts from onboarding
- Phase 9 = install on phone to test before continuing
