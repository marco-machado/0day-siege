using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZeroDaySiege.Core;

namespace ZeroDaySiege.UI
{
    public class GameOverUI : MonoBehaviour
    {
        private GameObject screenContainer;
        private TextMeshProUGUI titleText;
        private TextMeshProUGUI statsText;
        private Button restartButton;
        private Button menuButton;

        private string currentStageDisplayId;

        public void SetReferences(GameObject container, TextMeshProUGUI title, TextMeshProUGUI stats,
                                   Button restart, Button menu)
        {
            screenContainer = container;
            titleText = title;
            statsText = stats;
            restartButton = restart;
            menuButton = menu;
        }

        private void Start()
        {
            if (restartButton != null)
            {
                restartButton.onClick.AddListener(OnRestartClicked);
            }

            if (menuButton != null)
            {
                menuButton.onClick.AddListener(OnMenuClicked);
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += HandleStateChanged;
                UpdateVisibility(GameManager.Instance.CurrentState);
            }
        }

        private void OnDestroy()
        {
            if (restartButton != null)
            {
                restartButton.onClick.RemoveListener(OnRestartClicked);
            }

            if (menuButton != null)
            {
                menuButton.onClick.RemoveListener(OnMenuClicked);
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= HandleStateChanged;
            }
        }

        private void HandleStateChanged(GameState previousState, GameState newState)
        {
            UpdateVisibility(newState);
        }

        private void UpdateVisibility(GameState state)
        {
            bool shouldShow = state == GameState.GameOver;

            if (screenContainer != null)
            {
                screenContainer.SetActive(shouldShow);
            }

            if (shouldShow)
            {
                UpdateContent();
            }
        }

        private void UpdateContent()
        {
            if (GameManager.Instance == null) return;

            var outcome = GameManager.Instance.LastRunOutcome;
            bool isVictory = outcome == RunOutcome.Victory;

            currentStageDisplayId = StageManager.Instance?.CurrentStage?.GetDisplayId() ?? "1-1";

            if (titleText != null)
            {
                titleText.text = isVictory ? "VICTORY" : "FIREWALL BREACHED";
                titleText.color = isVictory ? UIConstants.VictoryColor : UIConstants.DefeatColor;
            }

            if (isVictory)
            {
                StageManager.Instance?.UnlockNextStage();
            }

            UpdateStats(isVictory);
            UpdateButtonLabels(isVictory);
        }

        private void UpdateStats(bool isVictory)
        {
            if (statsText == null) return;

            int waveReached = GameManager.Instance?.CurrentWave ?? 0;
            int totalWaves = GameManager.TotalWaves;
            int enemiesDefeated = RunStats.Instance?.EnemiesDefeated ?? 0;
            int finalScore = ScoreManager.Instance?.CurrentScore ?? 0;

            string stageName = StageManager.Instance?.CurrentStage?.stageName ?? "Unknown";
            string waveLabel = isVictory ? "Wave" : "Wave Reached";
            string scoreLabel = "Final Score";

            string stats = $"Stage: {currentStageDisplayId} - {stageName}\n" +
                          $"{waveLabel}: {waveReached} / {totalWaves}\n" +
                          $"Enemies Defeated: {enemiesDefeated}\n" +
                          $"{scoreLabel}: {finalScore:N0}";

            if (isVictory)
            {
                string bestKey = StageManager.Instance?.CurrentStage?.GetStorageKey() ?? "1_1";
                int personalBest = ScoreManager.Instance?.GetPersonalBest(bestKey) ?? 0;
                bool isNewBest = finalScore > personalBest;

                if (isNewBest && personalBest > 0)
                {
                    stats += $"\nPersonal Best: {personalBest:N0} (NEW!)";
                }
                else if (personalBest > 0)
                {
                    stats += $"\nPersonal Best: {personalBest:N0}";
                }

                if (isNewBest)
                {
                    ScoreManager.Instance?.SetPersonalBest(bestKey, finalScore);
                }
            }

            statsText.text = stats;
        }

        private void UpdateButtonLabels(bool isVictory)
        {
            if (restartButton != null)
            {
                var text = restartButton.GetComponentInChildren<TextMeshProUGUI>();
                if (text != null)
                {
                    text.text = isVictory ? "Restart" : "Retry";
                }
            }
        }

        private void OnRestartClicked()
        {
            GameManager.Instance?.RestartRun();
        }

        private void OnMenuClicked()
        {
            GameManager.Instance?.ReturnToMenu();
        }
    }
}
