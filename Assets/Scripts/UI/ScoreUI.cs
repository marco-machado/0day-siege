using UnityEngine;
using TMPro;
using ZeroDaySiege.Core;

namespace ZeroDaySiege.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject scoreContainer;

        private void Start()
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnScoreChanged += HandleScoreChanged;
                UpdateScoreText(ScoreManager.Instance.CurrentScore);
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += HandleStateChanged;
                UpdateVisibility(GameManager.Instance.CurrentState);
            }
        }

        private void OnDestroy()
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnScoreChanged -= HandleScoreChanged;
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= HandleStateChanged;
            }
        }

        private void HandleScoreChanged(int score)
        {
            UpdateScoreText(score);
        }

        private void HandleStateChanged(GameState previousState, GameState newState)
        {
            UpdateVisibility(newState);
        }

        private void UpdateScoreText(int score)
        {
            if (scoreText != null)
            {
                scoreText.text = $"Score: {score}";
            }
        }

        private void UpdateVisibility(GameState state)
        {
            bool shouldShow = state == GameState.Playing ||
                              state == GameState.Paused ||
                              state == GameState.CardSelection;

            if (scoreContainer != null)
            {
                scoreContainer.SetActive(shouldShow);
            }
        }

        public void SetReferences(TextMeshProUGUI text, GameObject container)
        {
            scoreText = text;
            scoreContainer = container;
        }
    }
}
