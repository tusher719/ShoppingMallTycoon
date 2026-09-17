# 🏬 Shopping Mall Tycoon — Next Chat Prompt

> এই পুরো block টা নতুন chat এ paste করো।

---

আমি Shopping Mall Tycoon বানাচ্ছি — Unity 6 (6000.0.47f1) URP Android mobile tycoon game।
Repo: https://github.com/tusher719/ShoppingMallTycoon
Speak to me in Bangla, write all code and docs in English.

[PROJECT_CONTEXT.md এর সম্পূর্ণ content paste করো]

## ✅ Completed Phases

### Phase 1 — Scene Setup ✅
Camera, walls, floor, Cinemachine 3.x, drag/pinch controls

### Phase 2 — Character System ✅
CharacterBase.cs, Customer_Normal prefab, AnimatorOverrideController
Step 15 skipped — wire when real 3D model arrives
NavMeshAgent disabled until Step 27

### Phase 3 — Onboarding ✅
LoadingScreen → Onboarding → Level_01 routing
Gender + Age selection, TutorialManager (4-step arrow overlay)

### Phase 4 — Economy & Shop ✅
- EconomyManager.cs — AddMoney, SpendMoney, OnMoneyChanged event, starting coins 500
- ShopData ScriptableObject — GroceryShop_Data (Cost:100, Income:20, MaxCustomers:2)
- GroceryShop prefab — ShopBody (#4A90D9) + ShopSign (#F5A623) + ShopTrigger
- ShopController.cs — Build(), GenerateIncome() every 5s
- BuildUI.cs — BuildPanel (bottom), TxtCoins (top), button grey+disabled when coins low
- Shop spawns at fixed (0,0,5) — grid placement → Phase 7
- LevelManager multiplier placeholder — wires in Phase 6

## 📌 Current Step

**→ Step 27: NavMesh bake on Level_01**

Phase 5 শুরু হবে। Customer system:
- Step 27: NavMesh bake
- Step 28: CustomerController.cs (state machine)
- Step 29: CustomerSpawner.cs
- Step 30: Full shopping flow (Spawn→Walk→Shop→Queue→Pay→Exit)
- Step 31: Payment wired to EconomyManager

## 🗒️ Pending Decisions (Phase 7)
- Grid-based shop placement (tile 5×5)
- HUD: per-min income rate, level progress %, gem count, shop count, customer count
- Gem system: earn via milestones, spend for instant upgrade/speed boost, tap FX to collect
- Mission panel: task + reward (coins/gems) + per-task progress bar
