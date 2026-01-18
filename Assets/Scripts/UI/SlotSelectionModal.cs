using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZeroDaySiege.UI
{
    public class SlotSelectionModal : MonoBehaviour
    {
        private GameObject modalPanel;
        private Button[] slotButtons;
        private Button cancelButton;

        public event Action<int> OnSlotSelected;
        public event Action OnCancelled;

        public void SetReferences(GameObject panel, Button[] slots, Button cancel)
        {
            modalPanel = panel;
            slotButtons = slots;
            cancelButton = cancel;

            for (int i = 0; i < slotButtons.Length; i++)
            {
                int slotIndex = i;
                slotButtons[i].onClick.AddListener(() => HandleSlotClick(slotIndex));
            }

            if (cancelButton != null)
            {
                cancelButton.onClick.AddListener(HandleCancel);
            }
        }

        public void Show(List<int> availableSlots)
        {
            if (modalPanel == null) return;

            for (int i = 0; i < slotButtons.Length; i++)
            {
                bool isAvailable = availableSlots.Contains(i);
                slotButtons[i].interactable = isAvailable;
            }

            modalPanel.SetActive(true);
        }

        public void Hide()
        {
            if (modalPanel != null)
            {
                modalPanel.SetActive(false);
            }
        }

        private void HandleSlotClick(int slotIndex)
        {
            Hide();
            OnSlotSelected?.Invoke(slotIndex);
        }

        private void HandleCancel()
        {
            Hide();
            OnCancelled?.Invoke();
        }

        private void OnDestroy()
        {
            if (slotButtons != null)
            {
                foreach (var btn in slotButtons)
                {
                    if (btn != null)
                    {
                        btn.onClick.RemoveAllListeners();
                    }
                }
            }

            if (cancelButton != null)
            {
                cancelButton.onClick.RemoveAllListeners();
            }
        }
    }
}
