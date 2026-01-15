using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ZeroDaySiege.Firewall;

namespace ZeroDaySiege.UI
{
    public class VignetteOverlay : MonoBehaviour
    {
        [SerializeField] private Image vignetteImage;
        [SerializeField] private float pulseSpeed = 1.5f;
        [SerializeField] private float minAlpha = 0.3f;
        [SerializeField] private float maxAlpha = 0.7f;

        private Coroutine pulseCoroutine;
        private bool isActive;

        private void Start()
        {
            if (Firewall.Firewall.Instance != null)
            {
                Firewall.Firewall.Instance.OnHealthStateChanged += HandleHealthStateChanged;
                HandleHealthStateChanged(Firewall.Firewall.Instance.HealthState);
            }
        }

        private void OnDestroy()
        {
            if (Firewall.Firewall.Instance != null)
            {
                Firewall.Firewall.Instance.OnHealthStateChanged -= HandleHealthStateChanged;
            }
        }

        private void HandleHealthStateChanged(FirewallHealthState state)
        {
            SetActive(state == FirewallHealthState.Critical);
        }

        public void SetActive(bool active)
        {
            if (isActive == active) return;

            isActive = active;

            if (vignetteImage != null)
            {
                vignetteImage.gameObject.SetActive(active);
            }

            if (active)
            {
                pulseCoroutine = StartCoroutine(PulseAnimation());
            }
            else
            {
                if (pulseCoroutine != null)
                {
                    StopCoroutine(pulseCoroutine);
                    pulseCoroutine = null;
                }
            }
        }

        private IEnumerator PulseAnimation()
        {
            while (isActive && vignetteImage != null)
            {
                float alpha = Mathf.Lerp(minAlpha, maxAlpha,
                    (Mathf.Sin(Time.unscaledTime * pulseSpeed * Mathf.PI * 2f) + 1f) * 0.5f);

                var color = vignetteImage.color;
                color.a = alpha;
                vignetteImage.color = color;

                yield return null;
            }
        }

        public void SetReferences(Image image)
        {
            vignetteImage = image;
        }
    }
}
