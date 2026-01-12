using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ZeroDaySiege.UI;

namespace ZeroDaySiege.Core
{
    public class GameBootstrap : MonoBehaviour
    {
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
            SetupGameLayout();
            SetupRunUI();
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

        private void SetupGameLayout()
        {
            if (GameLayout.Instance != null) return;

            var layoutGO = new GameObject("[GameLayout]");
            layoutGO.AddComponent<GameLayout>();
            layoutGO.AddComponent<PlaceholderVisuals>();
        }

        private void SetupRunUI()
        {
            var canvasGO = new GameObject("[RunCanvas]");
            DontDestroyOnLoad(canvasGO);

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

        private void SetupDebugControls()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            var debugGO = new GameObject("[DebugControls]");
            debugGO.AddComponent<DebugControls>();
            DontDestroyOnLoad(debugGO);
#endif
        }
    }
}
