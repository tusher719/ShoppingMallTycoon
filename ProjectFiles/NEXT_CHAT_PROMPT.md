# 🏬 Next Chat Prompt Template

> নতুন chat শুরু করার সময় নিচের prompt paste করো।
> [DONE STEPS] এবং [CURRENT STEP] নিজে update করো।

---

## 📋 Prompt (Copy & Paste):

```
আমি Unity 6 (6000.0.47f1, URP) দিয়ে Android Mobile Tycoon game বানাচ্ছি — Shopping Mall Tycoon।

## Stack:
- Unity 6 URP, Android + PC
- Cinemachine (gameplay + cinematic cameras)
- NavMesh (customer AI)
- CharacterBase + AnimatorOverrideController (modular character system)

## Core Loop:
Build Shop → Customers Arrive → Shop → Checkout → Earn → Upgrade → Level Complete

## Key Systems:
- Camera: Isometric gameplay cam + Cinemachine virtual cams (ShopUnlock, NewFloor, Achievement, LevelComplete)
- Characters: CharacterBase (abstract) → CustomerController / StaffController. Shared BaseAnimator + AnimatorOverrideController per type
- Onboarding: First launch → Gender (Male/Female) + Age (Young/Adult/Senior) select → saved to PlayerPrefs
- Economy: EconomyManager, single $ currency, income multiplier = Mathf.Pow(1.2f, levelIndex)
- Income: Level 1=1.0x, Level 2=1.2x, Level 3=1.44x ...
- Save: Phase 1 = PlayerPrefs, Phase 2 = JSON, Future = Remote DB
- Reset: Settings → Reset Game → confirm → PlayerPrefs.DeleteAll() → reload Onboarding scene
- Shops: 3-tier upgrade (Capacity / Income / Speed)
- Satisfaction: Base - WaitingPenalty + ShopUpgradeBonus + DecorationBonus

## Folder Structure:
Assets/_Game/Scripts/{Core, Camera, Character, Shop, Economy, Level, UI, Save}
Assets/_Game/{Animations, Prefabs, Scenes, ScriptableObjects, Art}

## Level 1 Objectives:
- Build Grocery Shop ($300, starting $500)
- Serve 10 Customers (~$25 each)
- Earn $300
- Reach 70% Satisfaction

## MVP Scope: Level 1–3, single shop, no staff, no floors

## Already Done:
✅ Step 01 - Unity 6 URP project created
✅ Step 02 - Folder structure created
[ADD MORE HERE as you complete steps]

## Current Step:
[WRITE CURRENT STEP HERE — e.g. "Step 03: Level_01 scene create + mall floor blockout"]

এখন [CURRENT STEP] এ help করো।
```

---

## ✏️ Update করার নিয়ম:

প্রতিটা step শেষে:
1. `PROGRESS.md` → সেই row Status = `✅ Done`
2. Git commit (commit msg PROGRESS.md থেকে নাও)
3. `CHANGELOG.md` → সেই phase-এর "Planned" item → "Added" এ move করো
4. এই prompt-এর `[DONE STEPS]` এ সেই step যোগ করো
5. `[CURRENT STEP]` update করো
6. নতুন chat-এ updated prompt paste করো
