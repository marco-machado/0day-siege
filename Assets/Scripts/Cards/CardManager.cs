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
        public readonly string DisplayName;
        public readonly string Description;

        public CardData(int id, CardCategory category, string displayName, string description,
            TowerType towerType = TowerType.BaseTower, UpgradeTier upgradeTier = UpgradeTier.None)
        {
            Id = id;
            Category = category;
            TowerType = towerType;
            UpgradeTier = upgradeTier;
            DisplayName = displayName;
            Description = description;
        }
    }

    public class CardManager : MonoBehaviour
    {
        public static CardManager Instance { get; private set; }

        public event Action<CardData[]> OnCardsOffered;
        public event Action<CardData> OnCardSelected;

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
            var cards = new CardData[count];

            cards[0] = new CardData(
                id: 0,
                category: CardCategory.PlaceTower,
                displayName: "Deploy Tower",
                description: "Place a Basic Tower in an empty slot",
                towerType: TowerType.BaseTower
            );

            cards[1] = new CardData(
                id: 1,
                category: CardCategory.PlaceTower,
                displayName: "Deploy AOE Tower",
                description: "Place an AOE Tower in an empty slot",
                towerType: TowerType.AOETower
            );

            cards[2] = new CardData(
                id: 2,
                category: CardCategory.WallRepair,
                displayName: "Repair Firewall",
                description: "Restore 20% Firewall HP"
            );

            return cards;
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
                    PlaceTowerFromCard(card.TowerType);
                    break;

                case CardCategory.TowerUpgrade:
                    Debug.Log($"[CardManager] Tower upgrade not yet implemented");
                    break;

                case CardCategory.WallRepair:
                    RepairFirewall(0.20f);
                    break;
            }
        }

        private void PlaceTowerFromCard(TowerType towerType)
        {
            if (TowerManager.Instance == null) return;

            int emptySlot = TowerManager.Instance.GetNextEmptySlot();
            if (emptySlot >= 0)
            {
                TowerManager.Instance.PlaceTower(emptySlot, towerType);
                Debug.Log($"[CardManager] Placed {towerType} in slot {emptySlot}");
            }
            else
            {
                Debug.Log("[CardManager] No empty slots available for tower placement");
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
    }
}
