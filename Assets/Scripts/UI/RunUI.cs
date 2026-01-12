using UnityEngine;
using TMPro;
using ZeroDaySiege.Core;

namespace ZeroDaySiege.UI
{
    public class RunUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI waveText;
        [SerializeField] private GameObject waveContainer;

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnWaveChanged += HandleWaveChanged;
                GameManager.Instance.OnStateChanged += HandleStateChanged;
                UpdateVisibility(GameManager.Instance.CurrentState);
                UpdateWaveText(GameManager.Instance.CurrentWave);
            }
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnWaveChanged -= HandleWaveChanged;
                GameManager.Instance.OnStateChanged -= HandleStateChanged;
            }
        }

        private void HandleWaveChanged(int wave)
        {
            UpdateWaveText(wave);
        }

        private void HandleStateChanged(GameState previousState, GameState newState)
        {
            UpdateVisibility(newState);
        }

        private void UpdateWaveText(int wave)
        {
            if (waveText != null)
            {
                waveText.text = $"Wave {wave} / {GameManager.TotalWaves}";
            }
        }

        private void UpdateVisibility(GameState state)
        {
            bool shouldShow = state == GameState.Playing ||
                              state == GameState.Paused ||
                              state == GameState.CardSelection;

            if (waveContainer != null)
            {
                waveContainer.SetActive(shouldShow);
            }
        }

        public void SetReferences(TextMeshProUGUI text, GameObject container)
        {
            waveText = text;
            waveContainer = container;
        }
    }
}
