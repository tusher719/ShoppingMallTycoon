# 🏬 Next Chat Prompt Template

---

## 📋 Prompt (Copy & Paste করো, [] অংশ নিজে update করো):

```
আমি Unity 6 (6000.0.47f1, URP) দিয়ে Android Mobile Tycoon game বানাচ্ছি — Shopping Mall Tycoon।
Repo: https://github.com/tusher719/ShoppingMallTycoon

## Stack:
- Unity 6 URP, Android + PC
- Cinemachine (gameplay + cinematic cameras)
- NavMesh (customer AI)
- CharacterBase + AnimatorOverrideController (modular character system)

## Core Loop:
Build Shop → Customers Arrive → Shop → Checkout → Earn → Upgrade → Level Complete

## Key Systems:
- Camera: Isometric gameplay (VC_Gameplay, priority 10) + Cinemachine virtual cams (VC_ShopUnlock, VC_NewFloor, VC_Achievement, VC_LevelComplete, priority 20 on trigger)
- Characters: CharacterBase (abstract) → CustomerController / StaffController. Shared BaseAnimator + AnimatorOverrideController per type
- Onboarding: First launch → Gender (Male/Female) + Age (Young/Adult/Senior) → saved to PlayerPrefs
- Economy: EconomyManager singleton, $ currency, multiplier = Mathf.Pow(1.2f, levelIndex)
- Audio: AudioManager singleton (DontDestroyOnLoad), BGM AudioSource (loop) + SFX AudioSource (PlayOneShot). Keys: sfx_click, sfx_coin, sfx_customer_arrive, sfx_customer_pay, sfx_shop_unlock, sfx_level_complete
- FX: FXManager singleton, object pool. Types: FX_CoinEarn, FX_ShopUnlock, FX_LevelComplete
- UI: Canvas Scaler 1080×1920 match 0.5, SafeAreaHandler.cs for notch
- Save Phase 1: PlayerPrefs. Keys: save_coins, save_level, save_stars_X, save_shop_X_level, save_char_gender, save_char_age, save_onboarded, vol_bgm, vol_sfx
- Reset: PlayerPrefs.DeleteAll() → reload Onboarding scene
- Satisfaction: Base - WaitingPenalty + ShopUpgradeBonus + DecorationBonus

## Mall Level 1:
- Floor: Plane 40×40 units
- Walls: North, West, East solid. South split (Left + Right) with 8-unit entrance gap (X -4 to +4)
- Entrance: South wall center

## MVP Scope: Level 1–3, single Grocery shop, no staff, no floors

## Already Done:
✅ Step 01 - Unity 6 URP project created
✅ Step 02 - Folder structure (_Game/Scripts/Core,Camera,Character,Shop,Economy,Level,UI,Save)
✅ Step 03 - Level_01 scene + Hierarchy separators (Managers, Environment, Mall, Customers, Canvas)
✅ Step 04 - Mall floor blockout (Plane, scale 4,1,4 = 40×40)
✅ Step 05 - Walls + entrance gap (Wall_North, Wall_West, Wall_East, Wall_South_Left, Wall_South_Right)
[ADD MORE ✅ as you complete steps]

## Current Step:
[WRITE HERE — e.g. "Step 06: Cinemachine package install + isometric gameplay camera setup"]

এখন [current step] এ help করো। কাজ শুরুর আগে কোনো confusion বা নতুন কিছু থাকলে আগে question করো।
```

---

## ✏️ Update নিয়ম (প্রতিটা step শেষে):

1. `PROGRESS.md` → সেই row Status = `✅ Done`
2. Git commit: `git add . && git commit -m "[commit msg from PROGRESS.md]" && git push`
3. `CHANGELOG.md` → সেই phase-এর item "Planned" থেকে "Done" section-এ move করো
4. এই prompt এর `[DONE STEPS]` এ নতুন ✅ যোগ করো
5. `[CURRENT STEP]` update করো
6. নতুন chat-এ updated prompt paste করো
