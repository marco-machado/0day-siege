using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
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
            DontDestroyOnLoad(managerGO);
        }

        private void SetupWaveManager()
        {
            if (WaveManager.Instance != null) return;

            var waveGO = new GameObject("[WaveManager]");
            waveGO.AddComponent<WaveManager>();
            DontDestroyOnLoad(waveGO);
        }

        private void SetupGameLayout()
        {
            if (GameLayout.Instance != null) return;

            var layoutGO = new GameObject("[GameLayout]");
            layoutGO.AddComponent<GameLayout>();
            layoutGO.AddComponent<PlaceholderVisuals>();
            DontDestroyOnLoad(layoutGO);
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
            var canvas = UIFactory.CreateRunCanvas();
            runCanvas = canvas.gameObject;
            DontDestroyOnLoad(runCanvas);

            var (waveContainer, waveText) = UIFactory.CreateWaveDisplay(runCanvas.transform);

            var runUI = runCanvas.AddComponent<RunUI>();
            runUI.SetReferences(waveText, waveContainer);

            waveContainer.SetActive(false);
        }

        private void SetupPauseUI()
        {
            var pauseButton = UIFactory.CreatePauseButton(runCanvas.transform);
            var (pauseOverlay, resumeBtn, restartBtn, quitBtn) = UIFactory.CreatePauseOverlay(runCanvas.transform);
            var confirmDialog = UIFactory.CreateConfirmationDialog(runCanvas.transform);

            var pauseUI = runCanvas.AddComponent<PauseUI>();
            pauseUI.SetReferences(pauseButton, pauseOverlay, resumeBtn, restartBtn, quitBtn, confirmDialog);
        }

        private void SetupDebugControls()
        {
            var debugGO = new GameObject("[DebugControls]");
            debugGO.AddComponent<DebugControls>();
            DontDestroyOnLoad(debugGO);
        }
    }
}
