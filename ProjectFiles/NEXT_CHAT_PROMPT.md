# 🏬 Shopping Mall Tycoon — Next Chat Prompt

> এই পুরো block নতুন chat-এ paste করো।

আমি Shopping Mall Tycoon বানাচ্ছি — Unity 6 (6000.0.47f1) URP Android mobile tycoon game।
Repo: https://github.com/tusher719/ShoppingMallTycoon
Speak to me in Bangla, write all code and docs in English.

[PROJECT_CONTEXT.md এর সম্পূর্ণ content paste করো]

## ✅ Completed Phases

### Phase 1 — Scene Setup ✅

Camera, walls, floor, Cinemachine 3.x, drag/pinch controls (PC ✅ Android ✅)

### Phase 2 — Character System ✅

CharacterBase.cs, Customer_Normal prefab, AnimatorOverrideController
Step 15 skipped — wire when real 3D model arrives

### Phase 3 — Onboarding ✅

LoadingScreen → Onboarding → Level_01 routing
Gender + Age selection, TutorialManager (4-step arrow overlay)

### Phase 4 — Economy & Shop ✅

- EconomyManager.cs — AddMoney, SpendMoney, OnMoneyChanged, starting coins 500
- ShopData SO — GroceryShop_Data (Cost:100, Income:20/5s, MaxCustomers:2)
- ShopController.cs — Build() + OnShopBuilt event + GenerateIncome() every 5s
- BuildUI.cs — BuildPanel + TxtCoins, grey+disabled when insufficient
- ⚠️ ShopController: `public ShopData Data => shopData;` — always use property

### Phase 5 — Customer System ✅

- NavMesh baked on Level_01 (Floor, Humanoid)
- CustomerController: Spawned→Walking→Shopping→Paying→Leaving
  - shoppingDuration: 5s, payingDuration: 2s
  - Payment: baseIncome \* 1f (multiplier wires Phase 6 Step 35)
  - OnCustomerServed event fires on payment
  - Exit: MoveTo(0,0,-18) then Destroy
- CustomerSpawner: event-driven via ShopController.OnShopBuilt
  - spawnInterval: 8s, maxCustomers: 5, SpawnPoint: (0,0,-15)
  - ⚠️ Target Shop Inspector-এ assign করা যাবে না — event দিয়ে auto-set হয়
- Windows ✅ Android ✅

## 📌 Current Step

**→ Step 32: LevelObjectiveManager.cs (Phase 6 start)**

## Phase 6 Plan

- Step 32: LevelObjectiveManager.cs
- Step 33: Track shops built, customers served, money earned
- Step 34: Satisfaction system
- Step 35: LevelManager.cs + star calc + income multiplier wire
- Step 36: Mission UI panel

## ⚠️ Key Rules (don't forget)

- CustomerSpawner Target Shop — event-driven, not Inspector
- ShopController: Data property, never .shopData direct
- Cinemachine: Output Channel, not Priority
- Buttons: AddListener in script only
- Materials: URP/Lit or URP/Simple Lit only
- Dynamic shop cost (Phase 7): baseCost \* Mathf.Pow(1.5f, shopsBuilt)

## 🗒️ Pending (Phase 7)

- Grid placement (tile 5×5)
- Dynamic shop cost
- Full HUD: coins+rate, level%, gems, shop count, customer count
- Gem system, 3-tier upgrade, mission panel, level complete panel
