using System;
using UnityEngine;
using ZeroDaySiege.Core;
using ZeroDaySiege.Towers;

namespace ZeroDaySiege.Cards
{
    public struct CardData
    {
        public readonly int Id;
        public readonly CardCategory Category;
        public readonly TowerType TowerType;
        public readonly UpgradeTier UpgradeTier;
        public readonly UpgradeType UpgradeType;
        public readonly int TargetTowerSlot;
        public readonly float HealPercent;
        public readonly string DisplayName;
        public readonly string Description;

        public CardData(int id, CardCategory category, string displayName, string description,
            TowerType towerType = TowerType.BaseTower, UpgradeTier upgradeTier = UpgradeTier.None,
            UpgradeType upgradeType = UpgradeType.None, int targetTowerSlot = -1, float healPercent = 0f)
        {
            Id = id;
            Category = category;
            TowerType = towerType;
            UpgradeTier = upgradeTier;
            UpgradeType = upgradeType;
            TargetTowerSlot = targetTowerSlot;
            HealPercent = healPercent;
            DisplayName = displayName;
            Description = description;
        }
    }

    public class CardManager : MonoBehaviour
    {
        public static CardManager Instance { get; private set; }

        public event Action<CardData[]> OnCardsOffered;
        public event Action<CardData> OnCardSelected;

        private CardPool cardPool;
        private CardData[] currentOfferedCards;
        private int pendingCardSelections;

        public CardData[] CurrentOfferedCards => currentOfferedCards;
        public bool HasPendingSelection => pendingCardSelections > 0;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            cardPool = new CardPool();
        }

        private void Start()
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnCardThresholdReached += HandleCardThresholdReached;
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += HandleGameStateChanged;
            }
        }

        private void OnDestroy()
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnCardThresholdReached -= HandleCardThresholdReached;
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= HandleGameStateChanged;
            }
        }

        private void HandleGameStateChanged(GameState previousState, GameState newState)
        {
            if (newState == GameState.Menu)
            {
                pendingCardSelections = 0;
                currentOfferedCards = null;
            }
        }

        private void HandleCardThresholdReached(int cardNumber)
        {
            Debug.Log($"[CardManager] Card threshold {cardNumber} reached, offering cards");
            pendingCardSelections++;
            OfferCards();
        }

        private void OfferCards()
        {
            currentOfferedCards = GenerateCardChoices(3);
            OnCardsOffered?.Invoke(currentOfferedCards);

            if (GameManager.Instance != null)
            {
                GameManager.Instance.ShowCardSelection();
            }
        }

        private CardData[] GenerateCardChoices(int count)
        {
            return cardPool.GenerateCards(count);
        }

        public void SelectCard(int index)
        {
            if (currentOfferedCards == null || index < 0 || index >= currentOfferedCards.Length)
            {
                Debug.LogWarning($"[CardManager] Invalid card selection: {index}");
                return;
            }

            var selectedCard = currentOfferedCards[index];
            Debug.Log($"[CardManager] Card selected: {selectedCard.DisplayName}");

            ApplyCardEffect(selectedCard);
            OnCardSelected?.Invoke(selectedCard);

            pendingCardSelections--;
            currentOfferedCards = null;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.CloseCardSelection();
            }

            if (pendingCardSelections > 0)
            {
                OfferCards();
            }
        }

        private void ApplyCardEffect(CardData card)
        {
            switch (card.Category)
            {
                case CardCategory.PlaceTower:
                    break;

                case CardCategory.TowerUpgrade:
                    ApplyTowerUpgrade(card);
                    break;

                case CardCategory.WallRepair:
                    RepairFirewall(card.HealPercent > 0 ? card.HealPercent : 0.30f);
                    break;
            }
        }

        private void ApplyTowerUpgrade(CardData card)
        {
            if (TowerManager.Instance == null) return;

            var tower = TowerManager.Instance.GetTowerInSlot(card.TargetTowerSlot);
            if (tower != null)
            {
                tower.ApplyUpgrade(card.UpgradeType, card.UpgradeTier);
                Debug.Log($"[CardManager] Applied {card.UpgradeType} {card.UpgradeTier} to {tower.Type} in slot {card.TargetTowerSlot}");
            }
            else
            {
                Debug.LogWarning($"[CardManager] No tower found in slot {card.TargetTowerSlot} for upgrade");
            }
        }

        private void RepairFirewall(float percent)
        {
            if (Firewall.Firewall.Instance != null)
            {
                Firewall.Firewall.Instance.HealPercent(percent);
                Debug.Log($"[CardManager] Repaired Firewall by {percent * 100}%");
            }
        }

        public void SelectCardWithSlot(int cardIndex, int slotIndex)
        {
            if (currentOfferedCards == null || cardIndex < 0 || cardIndex >= currentOfferedCards.Length)
            {
                Debug.LogWarning($"[CardManager] Invalid card selection: {cardIndex}");
                return;
            }

            var card = currentOfferedCards[cardIndex];

            if (card.Category != CardCategory.PlaceTower)
            {
                SelectCard(cardIndex);
                return;
            }

            if (TowerManager.Instance != null && !TowerManager.Instance.IsSlotOccupied(slotIndex))
            {
                TowerManager.Instance.PlaceTower(slotIndex, card.TowerType);
                Debug.Log($"[CardManager] Placed {card.TowerType} in slot {slotIndex}");
            }
            else
            {
                Debug.LogWarning($"[CardManager] Cannot place tower in slot {slotIndex}");
                return;
            }

            OnCardSelected?.Invoke(card);
            CompleteSelection();
        }

        public bool TryReroll()
        {
            if (DecryptKeyManager.Instance == null || !DecryptKeyManager.Instance.CanSpend(1))
            {
                Debug.Log("[CardManager] Cannot reroll: insufficient keys");
                return false;
            }

            DecryptKeyManager.Instance.Spend(1);
            currentOfferedCards = GenerateCardChoices(3);
            OnCardsOffered?.Invoke(currentOfferedCards);
            Debug.Log("[CardManager] Rerolled cards");
            return true;
        }

        private void CompleteSelection()
        {
            pendingCardSelections--;
            currentOfferedCards = null;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.CloseCardSelection();
            }

            if (pendingCardSelections > 0)
            {
                OfferCards();
            }
        }
    }
}
