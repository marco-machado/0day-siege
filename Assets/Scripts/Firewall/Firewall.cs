using System;
using System.Collections;
using UnityEngine;
using ZeroDaySiege.Core;

namespace ZeroDaySiege.Firewall
{
    public class Firewall : MonoBehaviour
    {
        public static Firewall Instance { get; private set; }

        [Header("Stats")]
        [SerializeField] private int baseMaxHP = 2000;

        [Header("Visual Colors")]
        [SerializeField] private Color healthyColor = new Color(0f, 0.8f, 1f, 0.8f);
        [SerializeField] private Color damagedColor = new Color(1f, 0.5f, 0f, 0.8f);
        [SerializeField] private Color criticalColor = new Color(1f, 0.2f, 0.2f, 0.8f);

        [Header("Flicker Settings")]
        [SerializeField] private float criticalFlickerSpeed = 8f;
        [SerializeField] private float criticalFlickerMin = 0.5f;

        public event Action<int, int> OnHPChanged;
        public event Action<FirewallHealthState> OnHealthStateChanged;
        public event Action OnFirewallDestroyed;

        private int currentHP;
        private int maxHP;
        private FirewallHealthState healthState;
        private SpriteRenderer visualRenderer;
        private Coroutine flickerCoroutine;

        public int CurrentHP => currentHP;
        public int MaxHP => maxHP;
        public float HPPercent => maxHP > 0 ? (float)currentHP / maxHP : 0f;
        public bool IsDestroyed => currentHP <= 0;
        public FirewallHealthState HealthState => healthState;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            maxHP = baseMaxHP;
            currentHP = maxHP;
            healthState = FirewallHealthState.Healthy;

            CreateVisual();
        }

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += HandleGameStateChanged;
            }
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= HandleGameStateChanged;
            }
        }

        private void CreateVisual()
        {
            var layout = GameLayout.Instance;
            if (layout == null) return;

            var visualGO = new GameObject("FirewallVisual");
            visualGO.transform.SetParent(transform);
            visualGO.transform.position = new Vector3(0, layout.FirewallY + 0.5f, 0);

            visualRenderer = visualGO.AddComponent<SpriteRenderer>();
            visualRenderer.sprite = CreateSquareSprite();
            visualRenderer.color = healthyColor;
            visualGO.transform.localScale = new Vector3(layout.PlayAreaWidth, 1f, 1f);
        }

        private Sprite CreateSquareSprite()
        {
            var texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, Color.white);
            texture.Apply();
            return Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
        }

        private void HandleGameStateChanged(GameState previousState, GameState newState)
        {
            if (newState == GameState.Playing &&
                (previousState == GameState.Menu || previousState == GameState.GameOver))
            {
                ResetHP();
            }
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0 || IsDestroyed) return;

            currentHP = Mathf.Max(0, currentHP - amount);
            OnHPChanged?.Invoke(currentHP, maxHP);

            UpdateHealthState();

            if (currentHP <= 0)
            {
                OnFirewallDestroyed?.Invoke();
                GameManager.Instance?.EndRun(false);
            }
        }

        public void Heal(int amount)
        {
            if (amount <= 0 || IsDestroyed) return;

            currentHP = Mathf.Min(maxHP, currentHP + amount);
            OnHPChanged?.Invoke(currentHP, maxHP);

            UpdateHealthState();
        }

        public void HealPercent(float percent)
        {
            int healAmount = Mathf.RoundToInt(maxHP * percent);
            Heal(healAmount);
        }

        public void ResetHP()
        {
            maxHP = baseMaxHP;
            currentHP = maxHP;
            healthState = FirewallHealthState.Healthy;

            StopFlicker();
            UpdateVisualColor();

            OnHPChanged?.Invoke(currentHP, maxHP);
            OnHealthStateChanged?.Invoke(healthState);
        }

        private void UpdateHealthState()
        {
            var newState = GetHealthStateFromPercent(HPPercent);

            if (newState != healthState)
            {
                healthState = newState;
                UpdateVisualColor();
                OnHealthStateChanged?.Invoke(healthState);
            }
        }

        private FirewallHealthState GetHealthStateFromPercent(float percent)
        {
            if (percent <= 0f) return FirewallHealthState.Destroyed;
            if (percent <= 0.25f) return FirewallHealthState.Critical;
            if (percent <= 0.50f) return FirewallHealthState.Damaged;
            return FirewallHealthState.Healthy;
        }

        private void UpdateVisualColor()
        {
            if (visualRenderer == null) return;

            StopFlicker();

            switch (healthState)
            {
                case FirewallHealthState.Healthy:
                    visualRenderer.color = healthyColor;
                    break;
                case FirewallHealthState.Damaged:
                    visualRenderer.color = damagedColor;
                    break;
                case FirewallHealthState.Critical:
                    visualRenderer.color = criticalColor;
                    flickerCoroutine = StartCoroutine(FlickerRoutine());
                    break;
                case FirewallHealthState.Destroyed:
                    visualRenderer.color = new Color(criticalColor.r, criticalColor.g, criticalColor.b, 0.3f);
                    break;
            }
        }

        private void StopFlicker()
        {
            if (flickerCoroutine != null)
            {
                StopCoroutine(flickerCoroutine);
                flickerCoroutine = null;
            }
        }

        private IEnumerator FlickerRoutine()
        {
            while (healthState == FirewallHealthState.Critical)
            {
                float alpha = Mathf.Lerp(criticalFlickerMin, criticalColor.a,
                    (Mathf.Sin(Time.unscaledTime * criticalFlickerSpeed) + 1f) * 0.5f);
                visualRenderer.color = new Color(criticalColor.r, criticalColor.g, criticalColor.b, alpha);
                yield return null;
            }
        }
    }
}
