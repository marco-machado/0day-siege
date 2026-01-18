using System.Collections.Generic;
using UnityEngine;
using ZeroDaySiege.Towers;

namespace ZeroDaySiege.Cards
{
    public class CardPool
    {
        private int nextCardId;
        private const float WallRepairChance = 0.15f;
        private const float WallRepairHealPercent = 0.30f;

        private static readonly TowerType[] AllTowerTypes =
        {
            TowerType.BaseTower,
            TowerType.AOETower,
            TowerType.BurstTower,
            TowerType.PiercingTower,
            TowerType.BruteForceNode
        };

        public CardData[] GenerateCards(int count)
        {
            var pool = new List<CardData>();

            pool.AddRange(GetPlaceTowerCards());
            pool.AddRange(GetUpgradeCards());

            bool includeWallRepair = ShouldIncludeWallRepair();

            var selected = new List<CardData>();
            int targetCount = includeWallRepair ? count - 1 : count;

            while (selected.Count < targetCount && pool.Count > 0)
            {
                int idx = Random.Range(0, pool.Count);
                selected.Add(pool[idx]);
                pool.RemoveAt(idx);
            }

            if (includeWallRepair)
            {
                var wallRepairCard = CreateWallRepairCard();
                int insertPos = Random.Range(0, selected.Count + 1);
                selected.Insert(insertPos, wallRepairCard);
            }

            if (selected.Count == 0)
            {
                Debug.Log("[CardPool] No valid cards available");
            }

            return selected.ToArray();
        }

        public List<int> GetAvailableSlots()
        {
            var available = new List<int>();
            if (TowerManager.Instance == null) return available;

            for (int i = 0; i < 5; i++)
            {
                if (!TowerManager.Instance.IsSlotOccupied(i))
                {
                    available.Add(i);
                }
            }
            return available;
        }

        private List<CardData> GetPlaceTowerCards()
        {
            var cards = new List<CardData>();
            if (TowerManager.Instance == null) return cards;

            var availableSlots = GetAvailableSlots();
            if (availableSlots.Count == 0) return cards;

            foreach (var towerType in AllTowerTypes)
            {
                if (!TowerManager.Instance.HasTowerOfType(towerType))
                {
                    cards.Add(CreatePlaceTowerCard(towerType));
                }
            }

            return cards;
        }

        private List<CardData> GetUpgradeCards()
        {
            var cards = new List<CardData>();
            if (TowerManager.Instance == null) return cards;

            foreach (var tower in TowerManager.Instance.ActiveTowers)
            {
                if (tower == null) continue;

                if (tower.DamageTier < UpgradeTier.Tier2)
                {
                    var nextTier = tower.DamageTier == UpgradeTier.None ? UpgradeTier.Tier1 : UpgradeTier.Tier2;
                    cards.Add(CreateUpgradeCard(tower, UpgradeType.Damage, nextTier));
                }

                if (tower.FireRateTier < UpgradeTier.Tier2)
                {
                    var nextTier = tower.FireRateTier == UpgradeTier.None ? UpgradeTier.Tier1 : UpgradeTier.Tier2;
                    cards.Add(CreateUpgradeCard(tower, UpgradeType.FireRate, nextTier));
                }
            }

            return cards;
        }

        private bool ShouldIncludeWallRepair()
        {
            if (Firewall.Firewall.Instance == null) return false;

            bool isDamaged = Firewall.Firewall.Instance.HPPercent < 1f;
            return isDamaged && Random.value < WallRepairChance;
        }

        private CardData CreatePlaceTowerCard(TowerType towerType)
        {
            string displayName = GetTowerDisplayName(towerType);
            return new CardData(
                id: nextCardId++,
                category: CardCategory.PlaceTower,
                displayName: $"Deploy {displayName}",
                description: $"Place a {displayName} in an empty slot",
                towerType: towerType
            );
        }

        private CardData CreateUpgradeCard(Tower tower, UpgradeType upgradeType, UpgradeTier tier)
        {
            string upgradeName = upgradeType == UpgradeType.Damage ? "Damage+" : "Fire Rate+";
            string tierText = tier == UpgradeTier.Tier1 ? "I" : "II";
            string effectText = tier == UpgradeTier.Tier1 ? "+25%" : "+50%";
            string towerName = GetTowerDisplayName(tower.Type);

            return new CardData(
                id: nextCardId++,
                category: CardCategory.TowerUpgrade,
                displayName: $"{upgradeName} {tierText}",
                description: $"{effectText} {(upgradeType == UpgradeType.Damage ? "damage" : "attack speed")}",
                towerType: tower.Type,
                upgradeTier: tier,
                upgradeType: upgradeType,
                targetTowerSlot: tower.SlotIndex
            );
        }

        private CardData CreateWallRepairCard()
        {
            int healPercent = Mathf.RoundToInt(WallRepairHealPercent * 100);
            return new CardData(
                id: nextCardId++,
                category: CardCategory.WallRepair,
                displayName: "Repair Firewall",
                description: $"Restore {healPercent}% Firewall HP",
                healPercent: WallRepairHealPercent
            );
        }

        private string GetTowerDisplayName(TowerType type)
        {
            return type switch
            {
                TowerType.BaseTower => "Base Tower",
                TowerType.AOETower => "AOE Tower",
                TowerType.BurstTower => "Burst Tower",
                TowerType.PiercingTower => "Piercing Tower",
                TowerType.BruteForceNode => "Brute Force Node",
                _ => type.ToString()
            };
        }
    }
}
