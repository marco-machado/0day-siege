using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ZeroDaySiege.UI
{
    public static class UIFactory
    {
        public static Canvas CreateRunCanvas()
        {
            var canvasGO = new GameObject("[RunCanvas]");

            var canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = UIConstants.RunCanvasSortOrder;

            var scaler = canvasGO.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = UIConstants.ReferenceResolution;
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = UIConstants.CanvasMatchWidthOrHeight;

            canvasGO.AddComponent<GraphicRaycaster>();

            return canvas;
        }

        public static (GameObject container, TextMeshProUGUI text) CreateWaveDisplay(Transform parent)
        {
            var containerGO = new GameObject("WaveContainer");
            containerGO.transform.SetParent(parent, false);

            var containerRect = containerGO.AddComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(0.5f, 1f);
            containerRect.anchorMax = new Vector2(0.5f, 1f);
            containerRect.pivot = new Vector2(0.5f, 1f);
            containerRect.anchoredPosition = UIConstants.WaveContainerPosition;
            containerRect.sizeDelta = UIConstants.WaveContainerSize;

            var textGO = new GameObject("WaveText");
            textGO.transform.SetParent(containerGO.transform, false);

            var textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            var waveText = textGO.AddComponent<TextMeshProUGUI>();
            waveText.text = "Wave 0 / 20";
            waveText.fontSize = UIConstants.WaveTextFontSize;
            waveText.alignment = TextAlignmentOptions.Center;
            waveText.color = Color.white;

            return (containerGO, waveText);
        }

        public static Button CreatePauseButton(Transform parent)
        {
            var buttonGO = new GameObject("PauseButton");
            buttonGO.transform.SetParent(parent, false);

            var rect = buttonGO.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.anchoredPosition = UIConstants.PauseButtonPosition;
            rect.sizeDelta = UIConstants.PauseButtonSize;

            var image = buttonGO.AddComponent<Image>();
            image.color = UIConstants.PauseButtonColor;

            var button = buttonGO.AddComponent<Button>();

            var textGO = new GameObject("Text");
            textGO.transform.SetParent(buttonGO.transform, false);

            var textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            var text = textGO.AddComponent<TextMeshProUGUI>();
            text.text = "II";
            text.fontSize = UIConstants.PauseButtonFontSize;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            buttonGO.SetActive(false);
            return button;
        }

        public static (GameObject overlay, Button resume, Button restart, Button quit) CreatePauseOverlay(Transform parent)
        {
            var overlayGO = new GameObject("PauseOverlay");
            overlayGO.transform.SetParent(parent, false);

            var overlayRect = overlayGO.AddComponent<RectTransform>();
            overlayRect.anchorMin = Vector2.zero;
            overlayRect.anchorMax = Vector2.one;
            overlayRect.offsetMin = Vector2.zero;
            overlayRect.offsetMax = Vector2.zero;

            var overlayImage = overlayGO.AddComponent<Image>();
            overlayImage.color = UIConstants.OverlayColor;

            var panelGO = new GameObject("Panel");
            panelGO.transform.SetParent(overlayGO.transform, false);

            var panelRect = panelGO.AddComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.5f, 0.5f);
            panelRect.anchorMax = new Vector2(0.5f, 0.5f);
            panelRect.pivot = new Vector2(0.5f, 0.5f);
            panelRect.sizeDelta = UIConstants.PausePanelSize;

            var panelImage = panelGO.AddComponent<Image>();
            panelImage.color = UIConstants.PanelColor;

            var titleGO = new GameObject("Title");
            titleGO.transform.SetParent(panelGO.transform, false);

            var titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 1f);
            titleRect.anchorMax = new Vector2(0.5f, 1f);
            titleRect.pivot = new Vector2(0.5f, 1f);
            titleRect.anchoredPosition = UIConstants.PauseTitlePosition;
            titleRect.sizeDelta = UIConstants.PauseTitleSize;

            var titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "PAUSED";
            titleText.fontSize = UIConstants.PauseTitleFontSize;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = Color.white;
            titleText.fontStyle = FontStyles.Bold;

            var resumeBtn = CreateMenuButton(panelGO.transform, "Resume", UIConstants.ResumeButtonPosition);
            var restartBtn = CreateMenuButton(panelGO.transform, "Restart", UIConstants.RestartButtonPosition);
            var quitBtn = CreateMenuButton(panelGO.transform, "Quit", UIConstants.QuitButtonPosition);

            overlayGO.SetActive(false);
            return (overlayGO, resumeBtn, restartBtn, quitBtn);
        }

        public static ConfirmationDialog CreateConfirmationDialog(Transform parent)
        {
            var dialogGO = new GameObject("ConfirmationDialog");
            dialogGO.transform.SetParent(parent, false);

            var overlayRect = dialogGO.AddComponent<RectTransform>();
            overlayRect.anchorMin = Vector2.zero;
            overlayRect.anchorMax = Vector2.one;
            overlayRect.offsetMin = Vector2.zero;
            overlayRect.offsetMax = Vector2.zero;

            var overlayImage = dialogGO.AddComponent<Image>();
            overlayImage.color = UIConstants.DialogOverlayColor;

            var panelGO = new GameObject("Panel");
            panelGO.transform.SetParent(dialogGO.transform, false);

            var panelRect = panelGO.AddComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.5f, 0.5f);
            panelRect.anchorMax = new Vector2(0.5f, 0.5f);
            panelRect.pivot = new Vector2(0.5f, 0.5f);
            panelRect.sizeDelta = UIConstants.DialogPanelSize;

            var panelImage = panelGO.AddComponent<Image>();
            panelImage.color = UIConstants.DialogPanelColor;

            var titleGO = new GameObject("Title");
            titleGO.transform.SetParent(panelGO.transform, false);

            var titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 1f);
            titleRect.anchorMax = new Vector2(0.5f, 1f);
            titleRect.pivot = new Vector2(0.5f, 1f);
            titleRect.anchoredPosition = UIConstants.DialogTitlePosition;
            titleRect.sizeDelta = UIConstants.DialogTitleSize;

            var titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "Confirm";
            titleText.fontSize = UIConstants.DialogTitleFontSize;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = Color.white;
            titleText.fontStyle = FontStyles.Bold;

            var messageGO = new GameObject("Message");
            messageGO.transform.SetParent(panelGO.transform, false);

            var messageRect = messageGO.AddComponent<RectTransform>();
            messageRect.anchorMin = new Vector2(0.5f, 0.5f);
            messageRect.anchorMax = new Vector2(0.5f, 0.5f);
            messageRect.pivot = new Vector2(0.5f, 0.5f);
            messageRect.anchoredPosition = UIConstants.DialogMessagePosition;
            messageRect.sizeDelta = UIConstants.DialogMessageSize;

            var messageText = messageGO.AddComponent<TextMeshProUGUI>();
            messageText.text = "Are you sure?";
            messageText.fontSize = UIConstants.DialogMessageFontSize;
            messageText.alignment = TextAlignmentOptions.Center;
            messageText.color = UIConstants.MessageTextColor;

            var yesBtn = CreateDialogButton(panelGO.transform, "Yes", UIConstants.DialogYesButtonPosition);
            var noBtn = CreateDialogButton(panelGO.transform, "No", UIConstants.DialogNoButtonPosition);

            dialogGO.SetActive(false);

            var dialog = dialogGO.AddComponent<ConfirmationDialog>();
            dialog.SetReferences(dialogGO, titleText, messageText, yesBtn, noBtn);
            return dialog;
        }

        private static Button CreateMenuButton(Transform parent, string label, Vector2 position)
        {
            var buttonGO = new GameObject(label + "Button");
            buttonGO.transform.SetParent(parent, false);

            var rect = buttonGO.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = UIConstants.MenuButtonSize;

            var image = buttonGO.AddComponent<Image>();
            image.color = UIConstants.ButtonColor;

            var button = buttonGO.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = UIConstants.ButtonHighlightColor;
            colors.pressedColor = UIConstants.ButtonPressedColor;
            button.colors = colors;

            var textGO = new GameObject("Text");
            textGO.transform.SetParent(buttonGO.transform, false);

            var textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            var text = textGO.AddComponent<TextMeshProUGUI>();
            text.text = label;
            text.fontSize = UIConstants.MenuButtonFontSize;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            return button;
        }

        private static Button CreateDialogButton(Transform parent, string label, Vector2 position)
        {
            var buttonGO = new GameObject(label + "Button");
            buttonGO.transform.SetParent(parent, false);

            var rect = buttonGO.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = UIConstants.DialogButtonSize;

            var image = buttonGO.AddComponent<Image>();
            image.color = UIConstants.DialogButtonColor;

            var button = buttonGO.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = UIConstants.DialogButtonHighlightColor;
            colors.pressedColor = UIConstants.DialogButtonPressedColor;
            button.colors = colors;

            var textGO = new GameObject("Text");
            textGO.transform.SetParent(buttonGO.transform, false);

            var textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            var text = textGO.AddComponent<TextMeshProUGUI>();
            text.text = label;
            text.fontSize = UIConstants.DialogButtonFontSize;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            return button;
        }

        public static (GameObject container, Image fill, TextMeshProUGUI text) CreateFirewallHealthBar(Transform parent)
        {
            var containerGO = new GameObject("FirewallHealthContainer");
            containerGO.transform.SetParent(parent, false);

            var containerRect = containerGO.AddComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(0.5f, 0f);
            containerRect.anchorMax = new Vector2(0.5f, 0f);
            containerRect.pivot = new Vector2(0.5f, 0f);
            containerRect.anchoredPosition = UIConstants.FirewallHealthPosition;
            containerRect.sizeDelta = UIConstants.FirewallHealthSize;

            var bgGO = new GameObject("Background");
            bgGO.transform.SetParent(containerGO.transform, false);

            var bgRect = bgGO.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;

            var bgImage = bgGO.AddComponent<Image>();
            bgImage.color = UIConstants.HealthBarBackgroundColor;

            var fillGO = new GameObject("Fill");
            fillGO.transform.SetParent(containerGO.transform, false);

            var fillRect = fillGO.AddComponent<RectTransform>();
            fillRect.anchorMin = Vector2.zero;
            fillRect.anchorMax = Vector2.one;
            fillRect.offsetMin = new Vector2(4, 4);
            fillRect.offsetMax = new Vector2(-4, -4);

            var fillImage = fillGO.AddComponent<Image>();
            fillImage.color = UIConstants.HealthBarHealthyColor;
            fillImage.type = Image.Type.Filled;
            fillImage.fillMethod = Image.FillMethod.Horizontal;
            fillImage.fillOrigin = 0;
            fillImage.fillAmount = 1f;

            var textGO = new GameObject("HPText");
            textGO.transform.SetParent(containerGO.transform, false);

            var textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            var hpText = textGO.AddComponent<TextMeshProUGUI>();
            hpText.text = "2000 / 2000";
            hpText.fontSize = UIConstants.HealthBarFontSize;
            hpText.alignment = TextAlignmentOptions.Center;
            hpText.color = Color.white;

            return (containerGO, fillImage, hpText);
        }

        public static Image CreateVignetteOverlay(Transform parent)
        {
            var vignetteGO = new GameObject("VignetteOverlay");
            vignetteGO.transform.SetParent(parent, false);
            vignetteGO.transform.SetAsFirstSibling();

            var rect = vignetteGO.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            var image = vignetteGO.AddComponent<Image>();
            image.sprite = CreateVignetteSprite();
            image.color = UIConstants.VignetteColor;
            image.raycastTarget = false;

            return image;
        }

        private static Sprite CreateVignetteSprite()
        {
            int size = 256;
            var texture = new Texture2D(size, size);
            var center = new Vector2(size / 2f, size / 2f);
            float maxDist = size / 2f;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    float dist = Vector2.Distance(new Vector2(x, y), center);
                    float normalizedDist = Mathf.Clamp01(dist / maxDist);
                    float alpha = Mathf.Pow(normalizedDist, 2f);
                    texture.SetPixel(x, y, new Color(1f, 1f, 1f, alpha));
                }
            }

            texture.Apply();
            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f));
        }
    }
}
