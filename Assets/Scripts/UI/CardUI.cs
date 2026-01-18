using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZeroDaySiege.Cards;
using ZeroDaySiege.Towers;

namespace ZeroDaySiege.UI
{
    public class CardUI : MonoBehaviour
    {
        private Button button;
        private TextMeshProUGUI titleText;
        private TextMeshProUGUI descriptionText;
        private TextMeshProUGUI detailsText;
        private Image headerImage;

        private int cardIndex;
        private CardData cardData;

        public event Action<int> OnClicked;

        public void SetReferences(Button btn, TextMeshProUGUI title, TextMeshProUGUI description,
                                   TextMeshProUGUI details, Image header)
        {
            button = btn;
            titleText = title;
            descriptionText = description;
            detailsText = details;
            headerImage = header;

            if (button != null)
            {
                button.onClick.AddListener(HandleClick);
            }
        }

        public void Configure(CardData card, int index, List<int> availableSlots = null)
        {
            cardData = card;
            cardIndex = index;

            UpdateVisuals(card, availableSlots);
        }

        private void UpdateVisuals(CardData card, List<int> availableSlots)
        {
            if (titleText != null)
            {
                titleText.text = card.DisplayName.ToUpper();
            }

            if (descriptionText != null)
            {
                descriptionText.text = card.Description;
            }

            if (headerImage != null)
            {
                headerImage.color = GetCardColor(card.Category);
            }

            if (detailsText != null)
            {
                detailsText.text = GetDetailsText(card, availableSlots);
            }
        }

        private Color GetCardColor(CardCategory category)
        {
            return category switch
            {
                CardCategory.PlaceTower => UIConstants.PlaceTowerCardColor,
                CardCategory.TowerUpgrade => UIConstants.UpgradeCardColor,
                CardCategory.WallRepair => UIConstants.WallRepairCardColor,
                _ => UIConstants.CardBackgroundColor
            };
        }

        private string GetDetailsText(CardData card, List<int> availableSlots)
        {
            switch (card.Category)
            {
                case CardCategory.PlaceTower:
                    if (availableSlots != null && availableSlots.Count > 0)
                    {
                        var slotNumbers = new List<string>();
                        foreach (var slot in availableSlots)
                        {
                            slotNumbers.Add((slot + 1).ToString());
                        }
                        return $"Slots: {string.Join(", ", slotNumbers)}";
                    }
                    return "No slots available";

                case CardCategory.TowerUpgrade:
                    string towerName = GetTowerDisplayName(card.TowerType);
                    return $"Target: {towerName} (Slot {card.TargetTowerSlot + 1})";

                case CardCategory.WallRepair:
                    if (Firewall.Firewall.Instance != null)
                    {
                        int currentHP = Firewall.Firewall.Instance.CurrentHP;
                        int maxHP = Firewall.Firewall.Instance.MaxHP;
                        int healAmount = Mathf.RoundToInt(maxHP * card.HealPercent);
                        int newHP = Mathf.Min(currentHP + healAmount, maxHP);
                        return $"{currentHP} → {newHP} HP";
                    }
                    return "Restore HP";

                default:
                    return "";
            }
        }

        private string GetTowerDisplayName(TowerType type)
        {
            return type switch
            {
                TowerType.BaseTower => "Base Tower",
                TowerType.AOETower => "AOE Tower",
                TowerType.BurstTower => "Burst Tower",
                TowerType.PiercingTower => "Piercing Tower",
                TowerType.BruteForceNode => "Brute Force",
                _ => type.ToString()
            };
        }

        private void HandleClick()
        {
            OnClicked?.Invoke(cardIndex);
        }

        private void OnDestroy()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(HandleClick);
            }
        }
    }
}
