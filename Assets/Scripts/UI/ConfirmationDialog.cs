using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ZeroDaySiege.UI
{
    public class ConfirmationDialog : MonoBehaviour
    {
        [SerializeField] private GameObject dialogPanel;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;

        private Action onConfirm;
        private Action onCancel;

        private void OnDestroy()
        {
            if (yesButton != null)
                yesButton.onClick.RemoveListener(OnYesClicked);
            if (noButton != null)
                noButton.onClick.RemoveListener(OnNoClicked);
        }

        public void Show(string title, string message, Action confirmAction, Action cancelAction = null)
        {
            onConfirm = confirmAction;
            onCancel = cancelAction;

            if (titleText != null)
                titleText.text = title;
            if (messageText != null)
                messageText.text = message;

            if (dialogPanel != null)
                dialogPanel.SetActive(true);
        }

        public void Hide()
        {
            if (dialogPanel != null)
                dialogPanel.SetActive(false);

            onConfirm = null;
            onCancel = null;
        }

        private void OnYesClicked()
        {
            var action = onConfirm;
            Hide();
            action?.Invoke();
        }

        private void OnNoClicked()
        {
            var action = onCancel;
            Hide();
            action?.Invoke();
        }

        public void SetReferences(GameObject panel, TextMeshProUGUI title, TextMeshProUGUI message,
                                   Button yes, Button no)
        {
            dialogPanel = panel;
            titleText = title;
            messageText = message;
            yesButton = yes;
            noButton = no;

            if (yesButton != null)
                yesButton.onClick.AddListener(OnYesClicked);
            if (noButton != null)
                noButton.onClick.AddListener(OnNoClicked);
        }
    }
}
