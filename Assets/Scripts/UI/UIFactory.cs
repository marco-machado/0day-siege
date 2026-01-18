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

        public static (GameObject container, TextMeshProUGUI text) CreateScoreDisplay(Transform parent)
        {
            var containerGO = new GameObject("ScoreContainer");
            containerGO.transform.SetParent(parent, false);

            var containerRect = containerGO.AddComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(0.5f, 1f);
            containerRect.anchorMax = new Vector2(0.5f, 1f);
            containerRect.pivot = new Vector2(0.5f, 1f);
            containerRect.anchoredPosition = UIConstants.ScoreContainerPosition;
            containerRect.sizeDelta = UIConstants.ScoreContainerSize;

            var textGO = new GameObject("ScoreText");
            textGO.transform.SetParent(containerGO.transform, false);

            var textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            var scoreText = textGO.AddComponent<TextMeshProUGUI>();
            scoreText.text = "Score: 0";
            scoreText.fontSize = UIConstants.ScoreTextFontSize;
            scoreText.alignment = TextAlignmentOptions.Center;
            scoreText.color = Color.white;

            return (containerGO, scoreText);
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

        public static (GameObject container, Button startButton) CreateMenuScreen(Transform parent)
        {
            var containerGO = new GameObject("MenuScreen");
            containerGO.transform.SetParent(parent, false);

            var containerRect = containerGO.AddComponent<RectTransform>();
            containerRect.anchorMin = Vector2.zero;
            containerRect.anchorMax = Vector2.one;
            containerRect.offsetMin = Vector2.zero;
            containerRect.offsetMax = Vector2.zero;

            var bgImage = containerGO.AddComponent<Image>();
            bgImage.color = UIConstants.MenuBackgroundColor;

            var titleGO = new GameObject("Title");
            titleGO.transform.SetParent(containerGO.transform, false);

            var titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 0.5f);
            titleRect.anchorMax = new Vector2(0.5f, 0.5f);
            titleRect.pivot = new Vector2(0.5f, 0.5f);
            titleRect.anchoredPosition = UIConstants.MenuTitlePosition;
            titleRect.sizeDelta = UIConstants.MenuTitleSize;

            var titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "0 DAY SIEGE";
            titleText.fontSize = UIConstants.MenuTitleFontSize;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = UIConstants.MenuTitleColor;
            titleText.fontStyle = FontStyles.Bold;

            var startButton = CreateStartButton(containerGO.transform);

            return (containerGO, startButton);
        }

        private static Button CreateStartButton(Transform parent)
        {
            var buttonGO = new GameObject("StartButton");
            buttonGO.transform.SetParent(parent, false);

            var rect = buttonGO.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = UIConstants.MenuStartButtonPosition;
            rect.sizeDelta = UIConstants.MenuStartButtonSize;

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
            text.text = "START RUN";
            text.fontSize = UIConstants.MenuStartButtonFontSize;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            return button;
        }

        public static (GameObject overlay, Transform cardContainer, Button rerollBtn,
                       TextMeshProUGUI keyDisplay, TextMeshProUGUI titleText) CreateCardSelectionOverlay(Transform parent)
        {
            var overlayGO = new GameObject("CardSelectionOverlay");
            overlayGO.transform.SetParent(parent, false);

            var overlayRect = overlayGO.AddComponent<RectTransform>();
            overlayRect.anchorMin = Vector2.zero;
            overlayRect.anchorMax = Vector2.one;
            overlayRect.offsetMin = Vector2.zero;
            overlayRect.offsetMax = Vector2.zero;

            var overlayImage = overlayGO.AddComponent<Image>();
            overlayImage.color = UIConstants.CardSelectionOverlayColor;

            var titleGO = new GameObject("Title");
            titleGO.transform.SetParent(overlayGO.transform, false);

            var titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 1f);
            titleRect.anchorMax = new Vector2(0.5f, 1f);
            titleRect.pivot = new Vector2(0.5f, 1f);
            titleRect.anchoredPosition = UIConstants.CardSelectionTitlePosition;
            titleRect.sizeDelta = UIConstants.CardSelectionTitleSize;

            var titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "SELECT UPGRADE";
            titleText.fontSize = UIConstants.CardSelectionTitleFontSize;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = Color.white;
            titleText.fontStyle = FontStyles.Bold;

            var keyDisplayGO = new GameObject("KeyDisplay");
            keyDisplayGO.transform.SetParent(overlayGO.transform, false);

            var keyRect = keyDisplayGO.AddComponent<RectTransform>();
            keyRect.anchorMin = new Vector2(0.5f, 1f);
            keyRect.anchorMax = new Vector2(0.5f, 1f);
            keyRect.pivot = new Vector2(0.5f, 1f);
            keyRect.anchoredPosition = UIConstants.KeyDisplayPosition;
            keyRect.sizeDelta = UIConstants.KeyDisplaySize;

            var keyText = keyDisplayGO.AddComponent<TextMeshProUGUI>();
            keyText.text = "5";
            keyText.fontSize = UIConstants.KeyDisplayFontSize;
            keyText.alignment = TextAlignmentOptions.Right;
            keyText.color = UIConstants.KeyDisplayColor;

            var cardContainerGO = new GameObject("CardContainer");
            cardContainerGO.transform.SetParent(overlayGO.transform, false);

            var containerRect = cardContainerGO.AddComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(0.5f, 0.5f);
            containerRect.anchorMax = new Vector2(0.5f, 0.5f);
            containerRect.pivot = new Vector2(0.5f, 0.5f);
            containerRect.anchoredPosition = UIConstants.CardContainerPosition;
            float totalWidth = (UIConstants.CardSize.x * 3) + (UIConstants.CardSpacing * 2);
            containerRect.sizeDelta = new Vector2(totalWidth, UIConstants.CardSize.y);

            var layout = cardContainerGO.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = UIConstants.CardSpacing;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            var rerollBtn = CreateRerollButton(overlayGO.transform);

            overlayGO.SetActive(false);
            return (overlayGO, cardContainerGO.transform, rerollBtn, keyText, titleText);
        }

        private static Button CreateRerollButton(Transform parent)
        {
            var buttonGO = new GameObject("RerollButton");
            buttonGO.transform.SetParent(parent, false);

            var rect = buttonGO.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = UIConstants.RerollButtonPosition;
            rect.sizeDelta = UIConstants.RerollButtonSize;

            var image = buttonGO.AddComponent<Image>();
            image.color = UIConstants.RerollButtonColor;

            var button = buttonGO.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = UIConstants.ButtonHighlightColor;
            colors.pressedColor = UIConstants.ButtonPressedColor;
            colors.disabledColor = UIConstants.RerollButtonDisabledColor;
            button.colors = colors;

            var textGO = new GameObject("Text");
            textGO.transform.SetParent(buttonGO.transform, false);

            var textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            var text = textGO.AddComponent<TextMeshProUGUI>();
            text.text = "DECRYPT - Reroll All";
            text.fontSize = UIConstants.RerollButtonFontSize;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            return button;
        }

        public static (GameObject cardGO, Button button, TextMeshProUGUI title,
                       TextMeshProUGUI description, TextMeshProUGUI details, Image background) CreateCard(Transform parent)
        {
            var cardGO = new GameObject("Card");
            cardGO.transform.SetParent(parent, false);

            var cardRect = cardGO.AddComponent<RectTransform>();
            cardRect.sizeDelta = UIConstants.CardSize;

            var layoutElement = cardGO.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = UIConstants.CardSize.x;
            layoutElement.preferredHeight = UIConstants.CardSize.y;

            var bgImage = cardGO.AddComponent<Image>();
            bgImage.color = UIConstants.CardBackgroundColor;

            var button = cardGO.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = UIConstants.CardHoverColor;
            colors.pressedColor = UIConstants.CardBackgroundColor;
            button.colors = colors;

            var headerGO = new GameObject("Header");
            headerGO.transform.SetParent(cardGO.transform, false);

            var headerRect = headerGO.AddComponent<RectTransform>();
            headerRect.anchorMin = new Vector2(0, 1);
            headerRect.anchorMax = new Vector2(1, 1);
            headerRect.pivot = new Vector2(0.5f, 1);
            headerRect.anchoredPosition = new Vector2(0, -10);
            headerRect.sizeDelta = new Vector2(-20, 60);

            var headerImage = headerGO.AddComponent<Image>();
            headerImage.color = UIConstants.PlaceTowerCardColor;

            var titleGO = new GameObject("Title");
            titleGO.transform.SetParent(cardGO.transform, false);

            var titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 1f);
            titleRect.anchorMax = new Vector2(0.5f, 1f);
            titleRect.pivot = new Vector2(0.5f, 1f);
            titleRect.anchoredPosition = new Vector2(0, -20);
            titleRect.sizeDelta = new Vector2(220, 50);

            var titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "TOWER NAME";
            titleText.fontSize = UIConstants.CardTitleFontSize;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = Color.white;
            titleText.fontStyle = FontStyles.Bold;

            var descGO = new GameObject("Description");
            descGO.transform.SetParent(cardGO.transform, false);

            var descRect = descGO.AddComponent<RectTransform>();
            descRect.anchorMin = new Vector2(0.5f, 0.5f);
            descRect.anchorMax = new Vector2(0.5f, 0.5f);
            descRect.pivot = new Vector2(0.5f, 0.5f);
            descRect.anchoredPosition = new Vector2(0, 20);
            descRect.sizeDelta = new Vector2(210, 120);

            var descText = descGO.AddComponent<TextMeshProUGUI>();
            descText.text = "Card description goes here";
            descText.fontSize = UIConstants.CardDescriptionFontSize;
            descText.alignment = TextAlignmentOptions.Center;
            descText.color = new Color(0.85f, 0.85f, 0.85f, 1f);

            var detailsGO = new GameObject("Details");
            detailsGO.transform.SetParent(cardGO.transform, false);

            var detailsRect = detailsGO.AddComponent<RectTransform>();
            detailsRect.anchorMin = new Vector2(0.5f, 0f);
            detailsRect.anchorMax = new Vector2(0.5f, 0f);
            detailsRect.pivot = new Vector2(0.5f, 0f);
            detailsRect.anchoredPosition = new Vector2(0, 15);
            detailsRect.sizeDelta = new Vector2(210, 50);

            var detailsText = detailsGO.AddComponent<TextMeshProUGUI>();
            detailsText.text = "Available: 1, 2, 4";
            detailsText.fontSize = UIConstants.CardDetailsFontSize;
            detailsText.alignment = TextAlignmentOptions.Center;
            detailsText.color = new Color(0.6f, 0.6f, 0.6f, 1f);

            return (cardGO, button, titleText, descText, detailsText, headerImage);
        }

        public static (GameObject modal, Button[] slotButtons, Button cancelButton) CreateSlotSelectionModal(Transform parent)
        {
            var modalGO = new GameObject("SlotSelectionModal");
            modalGO.transform.SetParent(parent, false);

            var overlayRect = modalGO.AddComponent<RectTransform>();
            overlayRect.anchorMin = Vector2.zero;
            overlayRect.anchorMax = Vector2.one;
            overlayRect.offsetMin = Vector2.zero;
            overlayRect.offsetMax = Vector2.zero;

            var overlayImage = modalGO.AddComponent<Image>();
            overlayImage.color = UIConstants.DialogOverlayColor;

            var panelGO = new GameObject("Panel");
            panelGO.transform.SetParent(modalGO.transform, false);

            var panelRect = panelGO.AddComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.5f, 0.5f);
            panelRect.anchorMax = new Vector2(0.5f, 0.5f);
            panelRect.pivot = new Vector2(0.5f, 0.5f);
            panelRect.anchoredPosition = UIConstants.SlotModalPosition;
            panelRect.sizeDelta = UIConstants.SlotModalSize;

            var panelImage = panelGO.AddComponent<Image>();
            panelImage.color = UIConstants.PanelColor;

            var titleGO = new GameObject("Title");
            titleGO.transform.SetParent(panelGO.transform, false);

            var titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 1f);
            titleRect.anchorMax = new Vector2(0.5f, 1f);
            titleRect.pivot = new Vector2(0.5f, 1f);
            titleRect.anchoredPosition = UIConstants.SlotModalTitlePosition;
            titleRect.sizeDelta = UIConstants.SlotModalTitleSize;

            var titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "SELECT TOWER SLOT";
            titleText.fontSize = UIConstants.SlotModalTitleFontSize;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = Color.white;
            titleText.fontStyle = FontStyles.Bold;

            var slotsContainerGO = new GameObject("SlotsContainer");
            slotsContainerGO.transform.SetParent(panelGO.transform, false);

            var slotsRect = slotsContainerGO.AddComponent<RectTransform>();
            slotsRect.anchorMin = new Vector2(0.5f, 0.5f);
            slotsRect.anchorMax = new Vector2(0.5f, 0.5f);
            slotsRect.pivot = new Vector2(0.5f, 0.5f);
            slotsRect.anchoredPosition = new Vector2(0, 10);
            float totalSlotsWidth = (UIConstants.SlotButtonSize.x * 5) + (UIConstants.SlotButtonSpacing * 4);
            slotsRect.sizeDelta = new Vector2(totalSlotsWidth, UIConstants.SlotButtonSize.y);

            var layout = slotsContainerGO.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = UIConstants.SlotButtonSpacing;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            var slotButtons = new Button[5];
            for (int i = 0; i < 5; i++)
            {
                slotButtons[i] = CreateSlotButton(slotsContainerGO.transform, i);
            }

            var cancelBtn = CreateSlotCancelButton(panelGO.transform);

            modalGO.SetActive(false);
            return (modalGO, slotButtons, cancelBtn);
        }

        private static Button CreateSlotButton(Transform parent, int slotIndex)
        {
            var buttonGO = new GameObject($"Slot{slotIndex}Button");
            buttonGO.transform.SetParent(parent, false);

            var rect = buttonGO.AddComponent<RectTransform>();
            rect.sizeDelta = UIConstants.SlotButtonSize;

            var layoutElement = buttonGO.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = UIConstants.SlotButtonSize.x;
            layoutElement.preferredHeight = UIConstants.SlotButtonSize.y;

            var image = buttonGO.AddComponent<Image>();
            image.color = UIConstants.SlotButtonAvailableColor;

            var button = buttonGO.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = UIConstants.ButtonHighlightColor;
            colors.pressedColor = UIConstants.ButtonPressedColor;
            colors.disabledColor = UIConstants.SlotButtonOccupiedColor;
            button.colors = colors;

            var textGO = new GameObject("Text");
            textGO.transform.SetParent(buttonGO.transform, false);

            var textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            var text = textGO.AddComponent<TextMeshProUGUI>();
            text.text = (slotIndex + 1).ToString();
            text.fontSize = UIConstants.SlotButtonFontSize;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            return button;
        }

        private static Button CreateSlotCancelButton(Transform parent)
        {
            var buttonGO = new GameObject("CancelButton");
            buttonGO.transform.SetParent(parent, false);

            var rect = buttonGO.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = UIConstants.SlotCancelButtonPosition;
            rect.sizeDelta = UIConstants.SlotCancelButtonSize;

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
            text.text = "Cancel";
            text.fontSize = UIConstants.DialogButtonFontSize;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            return button;
        }

        public static (GameObject overlay, TextMeshProUGUI titleText, TextMeshProUGUI statsText,
                       Button restartBtn, Button menuBtn) CreateGameOverUI(Transform parent)
        {
            var overlayGO = new GameObject("GameOverOverlay");
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
            panelRect.sizeDelta = UIConstants.GameOverPanelSize;

            var panelImage = panelGO.AddComponent<Image>();
            panelImage.color = UIConstants.PanelColor;

            var titleGO = new GameObject("Title");
            titleGO.transform.SetParent(panelGO.transform, false);

            var titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 1f);
            titleRect.anchorMax = new Vector2(0.5f, 1f);
            titleRect.pivot = new Vector2(0.5f, 1f);
            titleRect.anchoredPosition = UIConstants.GameOverTitlePosition;
            titleRect.sizeDelta = UIConstants.GameOverTitleSize;

            var titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "VICTORY";
            titleText.fontSize = UIConstants.GameOverTitleFontSize;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = UIConstants.VictoryColor;
            titleText.fontStyle = FontStyles.Bold;

            var statsGO = new GameObject("Stats");
            statsGO.transform.SetParent(panelGO.transform, false);

            var statsRect = statsGO.AddComponent<RectTransform>();
            statsRect.anchorMin = new Vector2(0.5f, 0.5f);
            statsRect.anchorMax = new Vector2(0.5f, 0.5f);
            statsRect.pivot = new Vector2(0.5f, 0.5f);
            statsRect.anchoredPosition = UIConstants.GameOverStatsPosition;
            statsRect.sizeDelta = UIConstants.GameOverStatsSize;

            var statsText = statsGO.AddComponent<TextMeshProUGUI>();
            statsText.text = "Wave: 20 / 20\nEnemies Defeated: 0\nFinal Score: 0";
            statsText.fontSize = UIConstants.GameOverStatsFontSize;
            statsText.alignment = TextAlignmentOptions.Center;
            statsText.color = Color.white;

            var restartBtn = CreateGameOverButton(panelGO.transform, "Restart", UIConstants.GameOverButtonPosition1);
            var menuBtn = CreateGameOverButton(panelGO.transform, "Menu", UIConstants.GameOverButtonPosition2);

            overlayGO.SetActive(false);
            return (overlayGO, titleText, statsText, restartBtn, menuBtn);
        }

        private static Button CreateGameOverButton(Transform parent, string label, Vector2 position)
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
            text.fontSize = UIConstants.DialogButtonFontSize;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            return button;
        }

        public static (GameObject container, Button[] stageButtons, TextMeshProUGUI[] stageLabels,
                       TextMeshProUGUI selectedText, Button startButton, Button backButton) CreateStageSelectScreen(Transform parent)
        {
            var containerGO = new GameObject("StageSelectScreen");
            containerGO.transform.SetParent(parent, false);

            var containerRect = containerGO.AddComponent<RectTransform>();
            containerRect.anchorMin = Vector2.zero;
            containerRect.anchorMax = Vector2.one;
            containerRect.offsetMin = Vector2.zero;
            containerRect.offsetMax = Vector2.zero;

            var bgImage = containerGO.AddComponent<Image>();
            bgImage.color = UIConstants.MenuBackgroundColor;

            var titleGO = new GameObject("Title");
            titleGO.transform.SetParent(containerGO.transform, false);

            var titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 0.5f);
            titleRect.anchorMax = new Vector2(0.5f, 0.5f);
            titleRect.pivot = new Vector2(0.5f, 0.5f);
            titleRect.anchoredPosition = UIConstants.StageSelectTitlePosition;
            titleRect.sizeDelta = UIConstants.StageSelectTitleSize;

            var titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "SELECT STAGE";
            titleText.fontSize = UIConstants.StageSelectTitleFontSize;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = UIConstants.MenuTitleColor;
            titleText.fontStyle = FontStyles.Bold;

            var stagesContainerGO = new GameObject("StagesContainer");
            stagesContainerGO.transform.SetParent(containerGO.transform, false);

            var stagesRect = stagesContainerGO.AddComponent<RectTransform>();
            stagesRect.anchorMin = new Vector2(0.5f, 0.5f);
            stagesRect.anchorMax = new Vector2(0.5f, 0.5f);
            stagesRect.pivot = new Vector2(0.5f, 0.5f);
            stagesRect.anchoredPosition = new Vector2(0, 100);
            float totalWidth = (UIConstants.StageButtonSize.x * 5) + (UIConstants.StageButtonSpacing * 4);
            stagesRect.sizeDelta = new Vector2(totalWidth, UIConstants.StageButtonSize.y);

            var layout = stagesContainerGO.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = UIConstants.StageButtonSpacing;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            var stageButtons = new Button[5];
            var stageLabels = new TextMeshProUGUI[5];

            for (int i = 0; i < 5; i++)
            {
                var (btn, label) = CreateStageButton(stagesContainerGO.transform, i);
                stageButtons[i] = btn;
                stageLabels[i] = label;
            }

            var selectedTextGO = new GameObject("SelectedStageText");
            selectedTextGO.transform.SetParent(containerGO.transform, false);

            var selectedRect = selectedTextGO.AddComponent<RectTransform>();
            selectedRect.anchorMin = new Vector2(0.5f, 0.5f);
            selectedRect.anchorMax = new Vector2(0.5f, 0.5f);
            selectedRect.pivot = new Vector2(0.5f, 0.5f);
            selectedRect.anchoredPosition = UIConstants.SelectedStageTextPosition;
            selectedRect.sizeDelta = UIConstants.SelectedStageTextSize;

            var selectedText = selectedTextGO.AddComponent<TextMeshProUGUI>();
            selectedText.text = "Selected: 1-1 - Entry Point";
            selectedText.fontSize = UIConstants.SelectedStageTextFontSize;
            selectedText.alignment = TextAlignmentOptions.Center;
            selectedText.color = Color.white;

            var startButton = CreateStageSelectButton(containerGO.transform, "START RUN", UIConstants.StageStartButtonPosition);
            var backButton = CreateStageSelectButton(containerGO.transform, "BACK", UIConstants.StageBackButtonPosition);

            containerGO.SetActive(false);
            return (containerGO, stageButtons, stageLabels, selectedText, startButton, backButton);
        }

        private static (Button button, TextMeshProUGUI label) CreateStageButton(Transform parent, int index)
        {
            var buttonGO = new GameObject($"Stage{index}Button");
            buttonGO.transform.SetParent(parent, false);

            var rect = buttonGO.AddComponent<RectTransform>();
            rect.sizeDelta = UIConstants.StageButtonSize;

            var layoutElement = buttonGO.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = UIConstants.StageButtonSize.x;
            layoutElement.preferredHeight = UIConstants.StageButtonSize.y;

            var image = buttonGO.AddComponent<Image>();
            image.color = UIConstants.StageButtonColor;

            var button = buttonGO.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = UIConstants.ButtonHighlightColor;
            colors.pressedColor = UIConstants.ButtonPressedColor;
            colors.disabledColor = UIConstants.StageButtonLockedColor;
            button.colors = colors;

            var textGO = new GameObject("Text");
            textGO.transform.SetParent(buttonGO.transform, false);

            var textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = new Vector2(5, 5);
            textRect.offsetMax = new Vector2(-5, -5);

            var text = textGO.AddComponent<TextMeshProUGUI>();
            text.text = $"1-{index + 1}\n[LOCKED]";
            text.fontSize = UIConstants.StageButtonFontSize;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            return (button, text);
        }

        private static Button CreateStageSelectButton(Transform parent, string label, Vector2 position)
        {
            var buttonGO = new GameObject(label.Replace(" ", "") + "Button");
            buttonGO.transform.SetParent(parent, false);

            var rect = buttonGO.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = UIConstants.MenuStartButtonSize;

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
            text.fontSize = UIConstants.MenuStartButtonFontSize;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            return button;
        }
    }
}
