using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ZeroDaySiege.Core;
using ZeroDaySiege.Firewall;

namespace ZeroDaySiege.UI
{
    public class FirewallUI : MonoBehaviour
    {
        [SerializeField] private Image healthBarFill;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private GameObject container;

        private static readonly Color HealthyColor = new Color(0f, 0.8f, 1f, 1f);
        private static readonly Color DamagedColor = new Color(1f, 0.6f, 0f, 1f);
        private static readonly Color CriticalColor = new Color(1f, 0.2f, 0.2f, 1f);

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += HandleStateChanged;
                UpdateVisibility(GameManager.Instance.CurrentState);
            }

            if (Firewall.Firewall.Instance != null)
            {
                Firewall.Firewall.Instance.OnHPChanged += HandleHPChanged;
                Firewall.Firewall.Instance.OnHealthStateChanged += HandleHealthStateChanged;
                UpdateHealthBar(Firewall.Firewall.Instance.CurrentHP, Firewall.Firewall.Instance.MaxHP);
                UpdateHealthColor(Firewall.Firewall.Instance.HealthState);
            }
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= HandleStateChanged;
            }

            if (Firewall.Firewall.Instance != null)
            {
                Firewall.Firewall.Instance.OnHPChanged -= HandleHPChanged;
                Firewall.Firewall.Instance.OnHealthStateChanged -= HandleHealthStateChanged;
            }
        }

        private void HandleStateChanged(GameState previousState, GameState newState)
        {
            UpdateVisibility(newState);
        }

        private void HandleHPChanged(int current, int max)
        {
            UpdateHealthBar(current, max);
        }

        private void HandleHealthStateChanged(FirewallHealthState state)
        {
            UpdateHealthColor(state);
        }

        private void UpdateVisibility(GameState state)
        {
            bool shouldShow = state == GameState.Playing ||
                              state == GameState.Paused ||
                              state == GameState.CardSelection;

            if (container != null)
            {
                container.SetActive(shouldShow);
            }
        }

        private void UpdateHealthBar(int current, int max)
        {
            if (healthBarFill != null)
            {
                healthBarFill.fillAmount = max > 0 ? (float)current / max : 0f;
            }

            if (hpText != null)
            {
                hpText.text = $"{current} / {max}";
            }
        }

        private void UpdateHealthColor(FirewallHealthState state)
        {
            if (healthBarFill == null) return;

            healthBarFill.color = state switch
            {
                FirewallHealthState.Healthy => HealthyColor,
                FirewallHealthState.Damaged => DamagedColor,
                FirewallHealthState.Critical => CriticalColor,
                FirewallHealthState.Destroyed => CriticalColor,
                _ => HealthyColor
            };
        }

        public void SetReferences(Image fill, TextMeshProUGUI text, GameObject containerObj)
        {
            healthBarFill = fill;
            hpText = text;
            container = containerObj;
        }
    }
}
