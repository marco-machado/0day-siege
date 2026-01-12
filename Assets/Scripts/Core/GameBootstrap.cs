using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using TMPro;
using ZeroDaySiege.UI;

namespace ZeroDaySiege.Core
{
    public class GameBootstrap : MonoBehaviour
    {
        private GameObject runCanvas;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var bootstrapGO = new GameObject("[GameBootstrap]");
            bootstrapGO.AddComponent<GameBootstrap>();
            DontDestroyOnLoad(bootstrapGO);
        }

        private void Start()
        {
            SetupScreenController();
            SetupGameManager();
            SetupWaveManager();
            SetupGameLayout();
            SetupEventSystem();
            SetupRunUI();
            SetupPauseUI();
            SetupDebugControls();
        }

        private void SetupScreenController()
        {
            var screenGO = new GameObject("[ScreenController]");
            screenGO.AddComponent<ScreenController>();
            DontDestroyOnLoad(screenGO);
        }

        private void SetupGameManager()
        {
            if (GameManager.Instance != null) return;

            var managerGO = new GameObject("[GameManager]");
            managerGO.AddComponent<GameManager>();
        }

        private void SetupWaveManager()
        {
            if (WaveManager.Instance != null) return;

            var waveGO = new GameObject("[WaveManager]");
            waveGO.AddComponent<WaveManager>();
        }

        private void SetupGameLayout()
        {
            if (GameLayout.Instance != null) return;

            var layoutGO = new GameObject("[GameLayout]");
            layoutGO.AddComponent<GameLayout>();
            layoutGO.AddComponent<PlaceholderVisuals>();
        }

        private void SetupEventSystem()
        {
            if (Object.FindAnyObjectByType<EventSystem>() != null) return;

            var eventSystemGO = new GameObject("[EventSystem]");
            eventSystemGO.AddComponent<EventSystem>();
            eventSystemGO.AddComponent<InputSystemUIInputModule>();
            DontDestroyOnLoad(eventSystemGO);
        }

        private void SetupRunUI()
        {
            runCanvas = new GameObject("[RunCanvas]");
            DontDestroyOnLoad(runCanvas);
            var canvasGO = runCanvas;

            var canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 100;

            var scaler = canvasGO.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;

            canvasGO.AddComponent<GraphicRaycaster>();

            var waveContainerGO = new GameObject("WaveContainer");
            waveContainerGO.transform.SetParent(canvasGO.transform, false);

            var containerRect = waveContainerGO.AddComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(0.5f, 1f);
            containerRect.anchorMax = new Vector2(0.5f, 1f);
            containerRect.pivot = new Vector2(0.5f, 1f);
            containerRect.anchoredPosition = new Vector2(0, -50);
            containerRect.sizeDelta = new Vector2(400, 80);

            var waveTextGO = new GameObject("WaveText");
            waveTextGO.transform.SetParent(waveContainerGO.transform, false);

            var textRect = waveTextGO.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            var waveText = waveTextGO.AddComponent<TextMeshProUGUI>();
            waveText.text = "Wave 0 / 20";
            waveText.fontSize = 48;
            waveText.alignment = TextAlignmentOptions.Center;
            waveText.color = Color.white;

            var runUI = canvasGO.AddComponent<RunUI>();
            runUI.SetReferences(waveText, waveContainerGO);

            waveContainerGO.SetActive(false);
        }

        private void SetupPauseUI()
        {
            var pauseButton = CreatePauseButton();
            var (pauseOverlay, resumeBtn, restartBtn, quitBtn) = CreatePauseOverlay();
            var confirmDialog = CreateConfirmationDialog();

            var pauseUI = runCanvas.AddComponent<PauseUI>();
            pauseUI.SetReferences(pauseButton, pauseOverlay, resumeBtn, restartBtn, quitBtn, confirmDialog);
        }

        private Button CreatePauseButton()
        {
            var buttonGO = new GameObject("PauseButton");
            buttonGO.transform.SetParent(runCanvas.transform, false);

            var rect = buttonGO.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.anchoredPosition = new Vector2(-30, -50);
            rect.sizeDelta = new Vector2(80, 80);

            var image = buttonGO.AddComponent<Image>();
            image.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);

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
            text.fontSize = 36;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            buttonGO.SetActive(false);
            return button;
        }

        private (GameObject overlay, Button resume, Button restart, Button quit) CreatePauseOverlay()
        {
            var overlayGO = new GameObject("PauseOverlay");
            overlayGO.transform.SetParent(runCanvas.transform, false);

            var overlayRect = overlayGO.AddComponent<RectTransform>();
            overlayRect.anchorMin = Vector2.zero;
            overlayRect.anchorMax = Vector2.one;
            overlayRect.offsetMin = Vector2.zero;
            overlayRect.offsetMax = Vector2.zero;

            var overlayImage = overlayGO.AddComponent<Image>();
            overlayImage.color = new Color(0, 0, 0, 0.7f);

            var panelGO = new GameObject("Panel");
            panelGO.transform.SetParent(overlayGO.transform, false);

            var panelRect = panelGO.AddComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.5f, 0.5f);
            panelRect.anchorMax = new Vector2(0.5f, 0.5f);
            panelRect.pivot = new Vector2(0.5f, 0.5f);
            panelRect.sizeDelta = new Vector2(500, 450);

            var panelImage = panelGO.AddComponent<Image>();
            panelImage.color = new Color(0.15f, 0.15f, 0.15f, 0.95f);

            var titleGO = new GameObject("Title");
            titleGO.transform.SetParent(panelGO.transform, false);

            var titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 1f);
            titleRect.anchorMax = new Vector2(0.5f, 1f);
            titleRect.pivot = new Vector2(0.5f, 1f);
            titleRect.anchoredPosition = new Vector2(0, -40);
            titleRect.sizeDelta = new Vector2(400, 60);

            var titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "PAUSED";
            titleText.fontSize = 48;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = Color.white;
            titleText.fontStyle = FontStyles.Bold;

            var resumeBtn = CreateMenuButton(panelGO.transform, "Resume", new Vector2(0, 20));
            var restartBtn = CreateMenuButton(panelGO.transform, "Restart", new Vector2(0, -70));
            var quitBtn = CreateMenuButton(panelGO.transform, "Quit", new Vector2(0, -160));

            overlayGO.SetActive(false);
            return (overlayGO, resumeBtn, restartBtn, quitBtn);
        }

        private Button CreateMenuButton(Transform parent, string label, Vector2 position)
        {
            var buttonGO = new GameObject(label + "Button");
            buttonGO.transform.SetParent(parent, false);

            var rect = buttonGO.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = new Vector2(300, 70);

            var image = buttonGO.AddComponent<Image>();
            image.color = new Color(0.3f, 0.3f, 0.3f, 1f);

            var button = buttonGO.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = new Color(0.4f, 0.4f, 0.4f, 1f);
            colors.pressedColor = new Color(0.25f, 0.25f, 0.25f, 1f);
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
            text.fontSize = 32;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            return button;
        }

        private ConfirmationDialog CreateConfirmationDialog()
        {
            var dialogGO = new GameObject("ConfirmationDialog");
            dialogGO.transform.SetParent(runCanvas.transform, false);

            var overlayRect = dialogGO.AddComponent<RectTransform>();
            overlayRect.anchorMin = Vector2.zero;
            overlayRect.anchorMax = Vector2.one;
            overlayRect.offsetMin = Vector2.zero;
            overlayRect.offsetMax = Vector2.zero;

            var overlayImage = dialogGO.AddComponent<Image>();
            overlayImage.color = new Color(0, 0, 0, 0.8f);

            var panelGO = new GameObject("Panel");
            panelGO.transform.SetParent(dialogGO.transform, false);

            var panelRect = panelGO.AddComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.5f, 0.5f);
            panelRect.anchorMax = new Vector2(0.5f, 0.5f);
            panelRect.pivot = new Vector2(0.5f, 0.5f);
            panelRect.sizeDelta = new Vector2(450, 300);

            var panelImage = panelGO.AddComponent<Image>();
            panelImage.color = new Color(0.2f, 0.2f, 0.2f, 0.98f);

            var titleGO = new GameObject("Title");
            titleGO.transform.SetParent(panelGO.transform, false);

            var titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 1f);
            titleRect.anchorMax = new Vector2(0.5f, 1f);
            titleRect.pivot = new Vector2(0.5f, 1f);
            titleRect.anchoredPosition = new Vector2(0, -30);
            titleRect.sizeDelta = new Vector2(400, 50);

            var titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "Confirm";
            titleText.fontSize = 36;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = Color.white;
            titleText.fontStyle = FontStyles.Bold;

            var messageGO = new GameObject("Message");
            messageGO.transform.SetParent(panelGO.transform, false);

            var messageRect = messageGO.AddComponent<RectTransform>();
            messageRect.anchorMin = new Vector2(0.5f, 0.5f);
            messageRect.anchorMax = new Vector2(0.5f, 0.5f);
            messageRect.pivot = new Vector2(0.5f, 0.5f);
            messageRect.anchoredPosition = new Vector2(0, 20);
            messageRect.sizeDelta = new Vector2(380, 60);

            var messageText = messageGO.AddComponent<TextMeshProUGUI>();
            messageText.text = "Are you sure?";
            messageText.fontSize = 28;
            messageText.alignment = TextAlignmentOptions.Center;
            messageText.color = new Color(0.8f, 0.8f, 0.8f, 1f);

            var yesBtn = CreateDialogButton(panelGO.transform, "Yes", new Vector2(-80, -100));
            var noBtn = CreateDialogButton(panelGO.transform, "No", new Vector2(80, -100));

            dialogGO.SetActive(false);

            var dialog = dialogGO.AddComponent<ConfirmationDialog>();
            dialog.SetReferences(dialogGO, titleText, messageText, yesBtn, noBtn);
            return dialog;
        }

        private Button CreateDialogButton(Transform parent, string label, Vector2 position)
        {
            var buttonGO = new GameObject(label + "Button");
            buttonGO.transform.SetParent(parent, false);

            var rect = buttonGO.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = new Vector2(120, 55);

            var image = buttonGO.AddComponent<Image>();
            image.color = new Color(0.35f, 0.35f, 0.35f, 1f);

            var button = buttonGO.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = new Color(0.45f, 0.45f, 0.45f, 1f);
            colors.pressedColor = new Color(0.3f, 0.3f, 0.3f, 1f);
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
            text.fontSize = 26;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            return button;
        }

        private void SetupDebugControls()
        {
            var debugGO = new GameObject("[DebugControls]");
            debugGO.AddComponent<DebugControls>();
            DontDestroyOnLoad(debugGO);
        }
    }
}
