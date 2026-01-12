using UnityEngine;
using UnityEngine.UI;
using ZeroDaySiege.Core;

namespace ZeroDaySiege.UI
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] private Button pauseButton;
        [SerializeField] private GameObject pauseOverlay;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private ConfirmationDialog confirmationDialog;

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += HandleStateChanged;
                UpdateVisibility(GameManager.Instance.CurrentState);
            }
        }

        private void OnDestroy()
        {
            if (pauseButton != null)
                pauseButton.onClick.RemoveListener(OnPauseClicked);
            if (resumeButton != null)
                resumeButton.onClick.RemoveListener(OnResumeClicked);
            if (restartButton != null)
                restartButton.onClick.RemoveListener(OnRestartClicked);
            if (quitButton != null)
                quitButton.onClick.RemoveListener(OnQuitClicked);

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
            bool showPauseButton = state == GameState.Playing;
            bool showPauseOverlay = state == GameState.Paused;

            if (pauseButton != null)
                pauseButton.gameObject.SetActive(showPauseButton);

            if (pauseOverlay != null)
                pauseOverlay.SetActive(showPauseOverlay);
        }

        private void OnPauseClicked()
        {
            GameManager.Instance?.PauseGame();
        }

        private void OnResumeClicked()
        {
            GameManager.Instance?.ResumeGame();
        }

        private void OnRestartClicked()
        {
            confirmationDialog?.Show(
                "Restart Run?",
                "Progress will be lost.",
                () => GameManager.Instance?.RestartRun()
            );
        }

        private void OnQuitClicked()
        {
            confirmationDialog?.Show(
                "Quit Run?",
                "Progress will be lost.",
                () => GameManager.Instance?.ReturnToMenu()
            );
        }

        public void SetReferences(Button pause, GameObject overlay, Button resume,
                                   Button restart, Button quit, ConfirmationDialog dialog)
        {
            pauseButton = pause;
            pauseOverlay = overlay;
            resumeButton = resume;
            restartButton = restart;
            quitButton = quit;
            confirmationDialog = dialog;

            if (pauseButton != null)
                pauseButton.onClick.AddListener(OnPauseClicked);
            if (resumeButton != null)
                resumeButton.onClick.AddListener(OnResumeClicked);
            if (restartButton != null)
                restartButton.onClick.AddListener(OnRestartClicked);
            if (quitButton != null)
                quitButton.onClick.AddListener(OnQuitClicked);
        }
    }
}
