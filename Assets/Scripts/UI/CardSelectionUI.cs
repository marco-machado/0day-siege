using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZeroDaySiege.Cards;
using ZeroDaySiege.Core;

namespace ZeroDaySiege.UI
{
    public class CardSelectionUI : MonoBehaviour
    {
        private GameObject overlay;
        private Transform cardContainer;
        private Button rerollButton;
        private TextMeshProUGUI keyDisplayText;
        private TextMeshProUGUI titleText;
        private SlotSelectionModal slotModal;

        private readonly List<CardUI> cardInstances = new();
        private int pendingPlaceTowerCardIndex = -1;
        private CardPool cardPool;

        public void SetReferences(GameObject overlayGO, Transform container, Button rerollBtn,
                                   TextMeshProUGUI keyDisplay, TextMeshProUGUI title, SlotSelectionModal modal)
        {
            overlay = overlayGO;
            cardContainer = container;
            rerollButton = rerollBtn;
            keyDisplayText = keyDisplay;
            titleText = title;
            slotModal = modal;
            cardPool = new CardPool();

            if (rerollButton != null)
            {
                rerollButton.onClick.AddListener(OnRerollClicked);
            }

            if (slotModal != null)
            {
                slotModal.OnSlotSelected += OnSlotSelected;
                slotModal.OnCancelled += OnSlotSelectionCancelled;
            }
        }

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += HandleGameStateChanged;
            }

            if (CardManager.Instance != null)
            {
                CardManager.Instance.OnCardsOffered += HandleCardsOffered;
            }

            if (DecryptKeyManager.Instance != null)
            {
                DecryptKeyManager.Instance.OnKeysChanged += UpdateKeyDisplay;
                UpdateKeyDisplay(DecryptKeyManager.Instance.CurrentKeys);
            }
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= HandleGameStateChanged;
            }

            if (CardManager.Instance != null)
            {
                CardManager.Instance.OnCardsOffered -= HandleCardsOffered;
            }

            if (DecryptKeyManager.Instance != null)
            {
                DecryptKeyManager.Instance.OnKeysChanged -= UpdateKeyDisplay;
            }

            if (rerollButton != null)
            {
                rerollButton.onClick.RemoveListener(OnRerollClicked);
            }

            if (slotModal != null)
            {
                slotModal.OnSlotSelected -= OnSlotSelected;
                slotModal.OnCancelled -= OnSlotSelectionCancelled;
            }
        }

        private void HandleGameStateChanged(GameState previousState, GameState newState)
        {
            bool shouldShow = newState == GameState.CardSelection;

            if (overlay != null)
            {
                overlay.SetActive(shouldShow);
            }

            if (!shouldShow)
            {
                ClearCards();
                pendingPlaceTowerCardIndex = -1;
            }
        }

        private void HandleCardsOffered(CardData[] cards)
        {
            ClearCards();

            var availableSlots = cardPool.GetAvailableSlots();

            foreach (var cardData in cards)
            {
                var (cardGO, button, title, description, details, header) = UIFactory.CreateCard(cardContainer);
                var cardUI = cardGO.AddComponent<CardUI>();
                cardUI.SetReferences(button, title, description, details, header);
                cardUI.Configure(cardData, cardInstances.Count, availableSlots);
                cardUI.OnClicked += OnCardClicked;
                cardInstances.Add(cardUI);
            }

            UpdateRerollButton();
        }

        private void ClearCards()
        {
            foreach (var cardUI in cardInstances)
            {
                if (cardUI != null)
                {
                    cardUI.OnClicked -= OnCardClicked;
                    Destroy(cardUI.gameObject);
                }
            }
            cardInstances.Clear();
        }

        private void OnCardClicked(int index)
        {
            if (CardManager.Instance == null) return;

            var cards = CardManager.Instance.CurrentOfferedCards;
            if (cards == null || index < 0 || index >= cards.Length) return;

            var card = cards[index];

            if (card.Category == CardCategory.PlaceTower)
            {
                var availableSlots = cardPool.GetAvailableSlots();
                if (availableSlots.Count > 0)
                {
                    pendingPlaceTowerCardIndex = index;
                    slotModal?.Show(availableSlots);
                }
                else
                {
                    Debug.LogWarning("[CardSelectionUI] No available slots for tower placement");
                }
            }
            else
            {
                CardManager.Instance.SelectCard(index);
            }
        }

        private void OnSlotSelected(int slotIndex)
        {
            if (CardManager.Instance != null && pendingPlaceTowerCardIndex >= 0)
            {
                CardManager.Instance.SelectCardWithSlot(pendingPlaceTowerCardIndex, slotIndex);
            }
            pendingPlaceTowerCardIndex = -1;
        }

        private void OnSlotSelectionCancelled()
        {
            pendingPlaceTowerCardIndex = -1;
        }

        private void OnRerollClicked()
        {
            if (CardManager.Instance != null)
            {
                CardManager.Instance.TryReroll();
                UpdateRerollButton();
            }
        }

        private void UpdateKeyDisplay(int keys)
        {
            if (keyDisplayText != null)
            {
                keyDisplayText.text = keys.ToString();
            }
            UpdateRerollButton();
        }

        private void UpdateRerollButton()
        {
            if (rerollButton == null) return;

            bool canReroll = DecryptKeyManager.Instance != null &&
                            DecryptKeyManager.Instance.CanSpend(1);

            rerollButton.interactable = canReroll;
        }
    }
}
