I'm building Shopping Mall Tycoon — a Unity 6 (6000.0.47f1) URP Android mobile tycoon game.
Repo: https://github.com/tusher719/ShoppingMallTycoon
Speak to me in Bangla, write all code and docs in English.

## Stack

- Unity 6 URP, Android (primary) + PC (secondary)
- Cinemachine 3.1.7 (Unity.Cinemachine namespace)
- NavMesh (customer AI)
- New Input System + EnhancedTouchSupport
- CharacterBase + AnimatorOverrideController (modular character system)

## Core Loop

Build Shop → Customers Arrive → Shop → Checkout → Earn → Upgrade → Level Complete

## Already Done ✅

- Step 01–10: Scene setup, mall floor/walls, Cinemachine cameras, CameraManager, touch controls (PC & Android tested)
- Step 11–14: CharacterBase.cs, BaseAnimator, Customer_Normal prefab, AnimatorOverrideController (Step 15 skipped — wire when real model arrives)
- Step 16: LoadingScreen scene + LoadingManager.cs (progress bar, routes to Onboarding or Level_01 based on save_onboarded)
- Step 17–20: Onboarding scene — Gender (Male/Female) + Age (Young/Adult/Senior) select, OnboardingManager.cs (AddListener pattern, color feedback, START GAME disabled until both selected, saves to PlayerPrefs, skip logic for returning players)

## Build Settings

0 - \_Game/Scenes/LoadingScreen
1 - \_Game/Scenes/Onboarding
2 - \_Game/Scenes/Level_01

## Key Conventions

- All materials: URP/Lit or URP/Simple Lit (Standard shader = pink in URP)
- Singletons: GameManager, EconomyManager, CameraManager, AudioManager, FXManager, SaveManager, UIManager
- Events: C# static events, no direct references
- Buttons: wired via AddListener in script, not Inspector OnClick
- Income multiplier: Mathf.Pow(1.2f, levelIndex)
- PlayerPrefs keys: save_coins, save_level, save_stars_X, save_shop_X_level, save_char_gender, save_char_age, save_onboarded, vol_bgm, vol_sfx
- Canvas Scaler: 1080×1920, Match 0.5
- Cinemachine: Priority checkbox broken → use Output Channel isolation
- EnhancedTouchSupport.Enable() must be called in OnEnable

## Lessons Learned

- Image Type: Filled requires UISprite assigned to Source Image first
- Drag-dropping prefab resets Y to 0 → spawn via code
- Canvas Match must be 0.5 for correct scaling on Android and Windows

## Current Step

Step 21: TutorialManager.cs — 4-step arrow + message overlay in Level_01

Tutorial steps:
| Step | Target | Message |
|------|--------------------|-----------------------------------------|
| 1 | Build button | "Tap here to build your first shop!" |
| 2 | South wall entrance| "Customers will enter through here" |
| 3 | Mall floor center | "Place your shop anywhere on the floor" |
| 4 | HUD top area | "Earn money and upgrade your shops!" |

Start from Step 21. Ask questions first if anything is unclear.
