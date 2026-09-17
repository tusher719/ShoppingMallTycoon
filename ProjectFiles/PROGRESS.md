# 🏬 Shopping Mall Tycoon — Progress Tracker

> Mark each step ✅ when done and commit to Git.

---

## 🔧 Dev Info

- **Unity:** 6000.0.47f1 (URP)
- **Platform:** Android (primary), PC (secondary)
- **Repo:** https://github.com/tusher719/ShoppingMallTycoon
- **Current Phase:** Phase 5 — Customer System (Step 27 next)

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

---

## 🎨 Material & Lighting — TODO _(apply as you go)_

| #   | Task                                           | Status | Notes                                      |
| --- | ---------------------------------------------- | ------ | ------------------------------------------ |
| L1  | URP Asset mobile settings configure            | ⬜     | Additional Lights off, Post Processing off |
| L2  | Directional Light setup (50,-30,0), warm white | ⬜     | Soft shadow, strength 0.5                  |
| L3  | Mall floor material (URP/Lit, #E8DCC8)         | ⬜     | Replace default grey plane                 |
| L4  | Wall materials (URP/Lit, #F5F0E8)              | ⬜     | Apply to all walls                         |
| L5  | Ambient light setup (Flat, #8AA0B0)            | ⬜     | Window → Rendering → Lighting              |

---

## 🎭 Phase 2 — Character & Animation System ✅ COMPLETE

| #   | Task                                   | Status  | Git Commit Message                      |
| --- | -------------------------------------- | ------- | --------------------------------------- |
| 11  | CharacterBase.cs (shared logic)        | ✅ Done | `character: add CharacterBase script`   |
| 12  | BaseAnimator controller (shared)       | ✅ Done | `anim: add BaseAnimator controller`     |
| 13  | Customer_Normal prefab + animator      | ✅ Done | `character: add Customer_Normal prefab` |
| 14  | Animation override controller setup    | ✅ Done | `anim: add animation override system`   |
| 15  | Walk / Idle / Browse / Pay clips wired | ⏭ Skip | Wire when real 3D model arrives         |

---

## 🎬 Phase 3 — Onboarding ✅ COMPLETE

| #   | Task                                           | Status  | Git Commit Message                              |
| --- | ---------------------------------------------- | ------- | ----------------------------------------------- |
| 16  | LoadingScreen scene + LoadingManager.cs        | ✅ Done | `loading: add LoadingScreen and LoadingManager` |
| 17  | Onboarding scene create                        | ✅ Done | `onboard: add Onboarding scene`                 |
| 18  | Gender select UI (Male/Female)                 | ✅ Done | `onboard: add gender select UI`                 |
| 19  | Age group select UI (Young/Adult/Senior)       | ✅ Done | `onboard: add age select UI`                    |
| 20  | OnboardingManager.cs + skip logic              | ✅ Done | `onboard: add OnboardingManager`                |
| 21  | TutorialManager.cs (arrow + message, Level_01) | ✅ Done | `tutorial: add in-game tutorial system`         |

---

## 💰 Phase 4 — Economy & Shop ✅ COMPLETE

| #   | Task                                       | Status  | Git Commit Message                            |
| --- | ------------------------------------------ | ------- | --------------------------------------------- |
| 22  | EconomyManager.cs (with income multiplier) | ✅ Done | `economy: add EconomyManager with multiplier` |
| 23  | ShopData ScriptableObject                  | ✅ Done | `shop: add ShopData ScriptableObject`         |
| 24  | GroceryShop prefab (blockout)              | ✅ Done | `shop: add GroceryShop prefab`                |
| 25  | ShopController.cs                          | ✅ Done | `shop: add ShopController`                    |
| 26  | Build Shop UI + deduct money               | ✅ Done | `shop: add build UI and deduct cost`          |

**Phase 4 Notes:**

- Shop spawns at fixed position (0,0,5) — grid placement → Phase 7
- Button grey+disabled when coins insufficient ✅
- Per-min income display → Phase 7 HUD
- Shop count display → Phase 7 HUD

---

## 👤 Phase 5 — Customer System _(next)_

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

| #   | Task                                              | Status | Git Commit Message                         |
| --- | ------------------------------------------------- | ------ | ------------------------------------------ |
| 37  | Grid-based shop placement                         | ⬜     | `shop: add grid placement system`          |
| 38  | Shop upgrade logic (3 tiers)                      | ⬜     | `shop: add 3-tier upgrade logic`           |
| 38B | Shop upgrade UI panel                             | ⬜     | `ui: add shop upgrade panel`               |
| 39  | Full HUD                                          | ⬜     | `ui: add full HUD`                         |
|     | — 💰 Coins + per-min income rate (`+$48/min`)     |        |                                            |
|     | — 📊 Level progress bar + % + goal number         |        |                                            |
|     | — 💎 Gems display                                 |        |                                            |
|     | — 🏪 Shop count (`Shops: 3`)                      |        |                                            |
|     | — 👥 Active customer count (`Customers: 7`)       |        |                                            |
| 40  | Gem system                                        | ⬜     | `economy: add gem system`                  |
|     | — Earn: mission complete / level complete / daily |        |                                            |
|     | — Spend: instant upgrade / speed boost            |        |                                            |
|     | — Collect: tap floating FX_GemCollect prefab      |        |                                            |
| 41  | Mission panel (task + reward + progress bar)      | ⬜     | `ui: add mission panel`                    |
| 42  | Level complete panel (⭐⭐⭐ + coins + next)      | ⬜     | `ui: add level complete panel`             |
| 43  | Cinematic trigger on shop unlock                  | ⬜     | `camera: trigger cinematic on shop unlock` |

---

## 🔊 Phase 7B — Audio & FX _(not started)_

| #   | Task                                           | Status | Git Commit Message                       |
| --- | ---------------------------------------------- | ------ | ---------------------------------------- |
| 44  | AudioManager.cs (BGM + SFX, singleton)         | ⬜     | `audio: add AudioManager`                |
| 45  | BGM clips add + level theme play               | ⬜     | `audio: add BGM clips and level theme`   |
| 46  | SFX clips wire (click, coin, unlock, complete) | ⬜     | `audio: wire SFX clips`                  |
| 47  | FXManager.cs (object pool)                     | ⬜     | `fx: add FXManager with object pool`     |
| 48  | FX_CoinEarn prefab + trigger                   | ⬜     | `fx: add coin earn particle effect`      |
| 49  | FX_ShopUnlock prefab + trigger                 | ⬜     | `fx: add shop unlock particle effect`    |
| 50  | FX_LevelComplete prefab + trigger              | ⬜     | `fx: add level complete particle effect` |
| 51  | FX_GemCollect prefab + trigger                 | ⬜     | `fx: add gem collect particle effect`    |

---

## 💾 Phase 8 — Save, Reset & Polish _(not started)_

| #   | Task                                       | Status | Git Commit Message                         |
| --- | ------------------------------------------ | ------ | ------------------------------------------ |
| 52  | SafeAreaHandler.cs (notch fix)             | ⬜     | `ui: add safe area handler`                |
| 53  | Canvas Scaler setup (1080×1920, match 0.5) | ⬜     | `ui: configure canvas scaler for mobile`   |
| 54  | SaveManager.cs (PlayerPrefs)               | ⬜     | `save: add PlayerPrefs SaveManager`        |
| 55  | Save character selection + BGM/SFX volume  | ⬜     | `save: save onboarding and audio settings` |
| 56  | Save: coins, level, shop state, stars      | ⬜     | `save: save game state`                    |
| 57  | Load on game start                         | ⬜     | `save: load state on start`                |
| 58  | ResetManager.cs (PC + Android)             | ⬜     | `reset: add game reset system`             |
| 59  | Reset confirmation UI                      | ⬜     | `ui: add reset confirmation dialog`        |

---

## 📱 Phase 9 — Build & Test _(in progress)_

| #   | Task                             | Status  | Git Commit Message                      |
| --- | -------------------------------- | ------- | --------------------------------------- |
| 60  | Android build settings configure | ✅ Done | `build: configure Android settings`     |
| 61  | Install on phone — test Phase 1  | ✅ Done | `build: MVP Phase 1 Android test build` |
| 62  | Performance check + fix          | ⬜      | `build: mobile performance pass`        |
| 63  | PC standalone build test         | ⬜      | `build: PC standalone test build`       |

---

## 📌 Current Step

**→ Step 27: NavMesh bake on Level_01**

---

## 🐛 Known Issues / Lessons Learned

- Cinemachine 3.x Priority checkbox does not work → solved with Output Channel isolation
- `EnhancedTouchSupport.Enable()` must be called in OnEnable for touch input
- Drag-dropping prefab resets Y to 0 → spawn via CustomerSpawner in code
- `Image Type: Filled` requires a Source Image (UISprite) assigned before option appears
- Canvas Match must be 0.5 for correct scaling on both portrait and landscape
- Wire buttons via `AddListener` in script, not via Inspector OnClick events
- TMP import dialog appears first time — click Import TMP Essentials
- `save_tutorial_done` must be reset separately — `DeleteAll()` also clears `save_onboarded`
- NavMeshAgent on Customer_Normal must be disabled until NavMesh is baked (Step 27)
- Material slot in Mesh Renderer — expand ▶ Materials to see Element 0 for drag-drop

---

## 💡 Decisions Made

- Grid tile size: 5×5 units (grid placement → Phase 7)
- Shop spawn: fixed at (0,0,5) until Phase 7 grid system
- Mall Level 1: 40×40 units floor, 8-unit entrance gap (South wall, X -4 to +4)
- Camera: Isometric Orthographic + Cinemachine 3.x cinematics
- Input: New Input System Package
- No player character control — management game only
- MVP scope: Level 1–3, single shop type, no staff
- Income multiplier: `Mathf.Pow(1.2f, levelIndex)` — wired in Phase 6
- Income interval: 5 seconds per shop
- Button disabled: grey (#666) + white text (Disabled Color #FFFFFF in Button component)
- HUD: number-driven, always visible, fun — per-min income, progress %, gem count, shop + customer count
- Gem system: earn via milestones/missions, spend for instant upgrade/speed boost, collect by tapping FX prefab
- Audio: AudioManager singleton, BGM loop + SFX PlayOneShot
- FX: FXManager object pool — CoinEarn, ShopUnlock, LevelComplete, GemCollect
- Save Phase 1: PlayerPrefs | Phase 2: JSON | Future: Remote DB
- Android build: package `com.tusher.shoppingmalltycoon`, IL2CPP, ARM64
