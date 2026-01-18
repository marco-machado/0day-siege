using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using ZeroDaySiege.Cards;
using ZeroDaySiege.Enemies;
using ZeroDaySiege.Firewall;
using ZeroDaySiege.Towers;
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
            SetupStageManager();
            SetupWaveSpawner();
            SetupGameLayout();
            SetupEnemyManager();
            SetupTowerManager();
            SetupScoreManager();
            SetupRunStats();
            SetupDecryptKeyManager();
            SetupCardManager();
            SetupFirewall();
            SetupEventSystem();
            SetupRunUI();
            SetupScoreUI();
            SetupVignetteOverlay();
            SetupPauseUI();
            SetupMenuUI();
            SetupStageSelectUI();
            SetupCardSelectionUI();
            SetupGameOverUI();
            SetupDamageNumbers();
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

        private void SetupStageManager()
        {
            if (StageManager.Instance != null) return;

            var stageGO = new GameObject("[StageManager]");
            stageGO.AddComponent<StageManager>();
            DontDestroyOnLoad(stageGO);
        }

        private void SetupWaveSpawner()
        {
            if (WaveSpawner.Instance != null)
            {
                Debug.Log("[GameBootstrap] WaveSpawner already exists");
                return;
            }

            Debug.Log("[GameBootstrap] Creating WaveSpawner...");
            var spawnerGO = new GameObject("[WaveSpawner]");
            spawnerGO.AddComponent<WaveSpawner>();
            DontDestroyOnLoad(spawnerGO);
            Debug.Log("[GameBootstrap] WaveSpawner created");
        }

        private void SetupGameLayout()
        {
            if (GameLayout.Instance != null) return;

            var layoutGO = new GameObject("[GameLayout]");
            layoutGO.AddComponent<GameLayout>();
            layoutGO.AddComponent<PlaceholderVisuals>();
            DontDestroyOnLoad(layoutGO);
        }

        private void SetupEnemyManager()
        {
            if (EnemyManager.Instance != null) return;

            var enemyGO = new GameObject("[EnemyManager]");
            enemyGO.AddComponent<EnemyManager>();
            DontDestroyOnLoad(enemyGO);
        }

        private void SetupTowerManager()
        {
            if (TowerManager.Instance != null) return;

            var towerGO = new GameObject("[TowerManager]");
            towerGO.AddComponent<TowerManager>();
            DontDestroyOnLoad(towerGO);
        }

        private void SetupScoreManager()
        {
            if (ScoreManager.Instance != null) return;

            var scoreGO = new GameObject("[ScoreManager]");
            scoreGO.AddComponent<ScoreManager>();
            DontDestroyOnLoad(scoreGO);
        }

        private void SetupRunStats()
        {
            if (RunStats.Instance != null) return;

            var statsGO = new GameObject("[RunStats]");
            statsGO.AddComponent<RunStats>();
            DontDestroyOnLoad(statsGO);
        }

        private void SetupDecryptKeyManager()
        {
            if (DecryptKeyManager.Instance != null) return;

            var keyGO = new GameObject("[DecryptKeyManager]");
            keyGO.AddComponent<DecryptKeyManager>();
            DontDestroyOnLoad(keyGO);
        }

        private void SetupCardManager()
        {
            if (CardManager.Instance != null) return;

            var cardGO = new GameObject("[CardManager]");
            cardGO.AddComponent<CardManager>();
            DontDestroyOnLoad(cardGO);
        }

        private void SetupFirewall()
        {
            if (Firewall.Firewall.Instance != null) return;

            var firewallGO = new GameObject("[Firewall]");
            firewallGO.AddComponent<Firewall.Firewall>();
            DontDestroyOnLoad(firewallGO);
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

        private void SetupScoreUI()
        {
            var (container, text) = UIFactory.CreateScoreDisplay(runCanvas.transform);

            var scoreUI = runCanvas.AddComponent<ScoreUI>();
            scoreUI.SetReferences(text, container);

            container.SetActive(false);
        }

        private void SetupVignetteOverlay()
        {
            var vignetteImage = UIFactory.CreateVignetteOverlay(runCanvas.transform);

            var vignette = runCanvas.AddComponent<VignetteOverlay>();
            vignette.SetReferences(vignetteImage);

            vignetteImage.gameObject.SetActive(false);
        }

        private void SetupPauseUI()
        {
            var pauseButton = UIFactory.CreatePauseButton(runCanvas.transform);
            var (pauseOverlay, resumeBtn, restartBtn, quitBtn) = UIFactory.CreatePauseOverlay(runCanvas.transform);
            var confirmDialog = UIFactory.CreateConfirmationDialog(runCanvas.transform);

            var pauseUI = runCanvas.AddComponent<PauseUI>();
            pauseUI.SetReferences(pauseButton, pauseOverlay, resumeBtn, restartBtn, quitBtn, confirmDialog);
        }

        private void SetupMenuUI()
        {
            var (container, startButton) = UIFactory.CreateMenuScreen(runCanvas.transform);

            var menuUI = runCanvas.AddComponent<MenuUI>();
            menuUI.SetReferences(container, startButton);
        }

        private void SetupStageSelectUI()
        {
            var (container, stageButtons, stageLabels, selectedText, startBtn, backBtn) =
                UIFactory.CreateStageSelectScreen(runCanvas.transform);

            var stageSelectUI = runCanvas.AddComponent<StageSelectUI>();
            stageSelectUI.SetReferences(container, stageButtons, stageLabels, selectedText, startBtn, backBtn);

            var menuUI = runCanvas.GetComponent<MenuUI>();
            menuUI?.SetStageSelectUI(stageSelectUI);
        }

        private void SetupCardSelectionUI()
        {
            var (overlay, cardContainer, rerollBtn, keyDisplay, titleText) = UIFactory.CreateCardSelectionOverlay(runCanvas.transform);
            var (modal, slotButtons, cancelBtn) = UIFactory.CreateSlotSelectionModal(runCanvas.transform);

            var slotModal = modal.AddComponent<SlotSelectionModal>();
            slotModal.SetReferences(modal, slotButtons, cancelBtn);

            var cardSelectionUI = runCanvas.AddComponent<CardSelectionUI>();
            cardSelectionUI.SetReferences(overlay, cardContainer, rerollBtn, keyDisplay, titleText, slotModal);
        }

        private void SetupGameOverUI()
        {
            var (overlay, title, stats, restart, menu) = UIFactory.CreateGameOverUI(runCanvas.transform);

            var gameOverUI = runCanvas.AddComponent<GameOverUI>();
            gameOverUI.SetReferences(overlay, title, stats, restart, menu);
        }

        private void SetupDamageNumbers()
        {
            if (DamageNumberManager.Instance != null) return;

            var damageNumberGO = new GameObject("[DamageNumberManager]");
            damageNumberGO.AddComponent<DamageNumberManager>();
            DontDestroyOnLoad(damageNumberGO);
        }

        private void SetupDebugControls()
        {
            var debugGO = new GameObject("[DebugControls]");
            debugGO.AddComponent<DebugControls>();
            DontDestroyOnLoad(debugGO);
        }
    }
}
