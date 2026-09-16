আমি Unity 6 (6000.0.47f1, URP) দিয়ে Android Mobile Tycoon game বানাচ্ছি — Shopping Mall Tycoon।
Repo: https://github.com/tusher719/ShoppingMallTycoon

## Stack:

- Unity 6 URP, Android + PC
- Cinemachine 3.1.7 (Unity.Cinemachine namespace)
- NavMesh (customer AI)
- New Input System (EnhancedTouchSupport)
- CharacterBase + AnimatorOverrideController (modular character system)

## Core Loop:

Build Shop → Customers Arrive → Shop → Checkout → Earn → Upgrade → Level Complete

## Key Systems:

- Camera: Isometric Orthographic (VC_Gameplay, Output: Default) + Cinemachine cinematic VCs (VC_ShopUnlock, VC_NewFloor, VC_MallOverview, VC_LevelComplete, Output: Channel02). CinemachineBrain Channel Mask = Default only. CameraInputHandler.cs: New Input System, drag pan + pinch zoom, PC & Android tested ✅
- Characters: CharacterBase (abstract) → CustomerController / StaffController. BaseAnimator (Idle/Walk/Browse/Pay, Any State transitions, Trigger params). AnimatorOverrideController per type. Placeholder: Capsule, URP/Simple Lit #4A90D9
- Onboarding: First launch → Gender (Male/Female) + Age (Young/Adult/Senior) → PlayerPrefs save
- Economy: EconomyManager singleton, $ currency, multiplier = Mathf.Pow(1.2f, levelIndex)
- Audio: AudioManager singleton (DontDestroyOnLoad), BGM (loop) + SFX (PlayOneShot). Keys: sfx_click, sfx_coin, sfx_customer_arrive, sfx_customer_pay, sfx_shop_unlock, sfx_level_complete
- FX: FXManager singleton, object pool. Types: FX_CoinEarn, FX_ShopUnlock, FX_LevelComplete
- UI: Canvas Scaler 1080×1920 match 0.5, SafeAreaHandler.cs for notch
- Save Phase 1: PlayerPrefs. Keys: save_coins, save_level, save_stars_X, save_shop_X_level, save_char_gender, save_char_age, save_onboarded, vol_bgm, vol_sfx
- Reset: PlayerPrefs.DeleteAll() → reload Onboarding scene
- Materials: URP/Lit বা URP/Simple Lit — Standard shader = pink (URP-তে)

## Mall Level 1:

- Floor: Plane 40×40 units
- Walls: North, West, East solid. South split (Left + Right) with 8-unit entrance gap (X -4 to +4)

## MVP Scope: Level 1–3, single Grocery shop, no staff, no floors

## Already Done:

✅ Step 01 - Unity 6 URP project created
✅ Step 02 - Folder structure (\_Game/Scripts/Core,Camera,Character,Shop,Economy,Level,UI,Save)
✅ Step 03 - Level_01 scene + Hierarchy separators
✅ Step 04 - Mall floor blockout (Plane, 40×40)
✅ Step 05 - Walls + entrance gap
✅ Step 06 - Cinemachine 3.1.7 installed
✅ Step 07 - Isometric gameplay camera (VC_Gameplay, Ortho size 10, pos 0,20,-15, rot 45,0,0)
✅ Step 08 - Cinematic VCs (ShopUnlock, NewFloor, MallOverview, LevelComplete — Channel02)
✅ Step 09 - CameraManager.cs
✅ Step 10 - Mobile touch controls (New Input System, drag pan + pinch zoom) — PC & Android ✅
✅ Step 11 - CharacterBase.cs (MoveTo, PlayAnim, StopMoving, HasReachedDestination)
✅ Step 12 - BaseAnimator controller (Idle/Walk/Browse/Pay, Any State transitions)
✅ Step 13 - Customer_Normal prefab (Capsule placeholder, URP/Simple Lit #4A90D9, NavMeshAgent)
✅ Step 14 - Customer_Normal_Override.overrideController → BaseAnimator
⏭ Step 15 - Animation clips — real model আসলে করবো

## Current Step:

Step 16: Onboarding scene create

এখন Step 16 থেকে শুরু করো। কাজ শুরুর আগে কোনো confusion বা নতুন কিছু থাকলে আগে question করো।
