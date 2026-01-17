using System;
using TMPro;
using UnityEngine;
using ZeroDaySiege.Core;

namespace ZeroDaySiege.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [Header("Visual Colors")]
        [SerializeField] private Color virusColor = new Color(0.2f, 0.8f, 0.2f, 1f);
        [SerializeField] private Color wormColor = new Color(1f, 0.8f, 0.2f, 1f);
        [SerializeField] private Color ransomwareColor = new Color(0.8f, 0.2f, 0.2f, 1f);

        [Header("Visual Sizes")]
        [SerializeField] private float normalSize = 0.8f;
        [SerializeField] private float ransomwareSize = 1.5f;

        public event Action<int, int> OnHPChanged;
        public event Action OnDied;

        private EnemyType enemyType;
        private EnemyState currentState;
        private int currentHP;
        private int maxHP;
        private float speed;
        private int wallDamage;
        private float attackCooldown;
        private int scoreValue;
        private float attackTimer;

        private SpriteRenderer visualRenderer;
        private TextMeshPro labelText;
        private EnemyHealthBar healthBar;

        public EnemyType Type => enemyType;
        public EnemyState State => currentState;
        public int CurrentHP => currentHP;
        public int MaxHP => maxHP;
        public float HPPercent => maxHP > 0 ? (float)currentHP / maxHP : 0f;
        public bool IsAlive => currentHP > 0 && currentState != EnemyState.Dead;
        public int ScoreValue => scoreValue;

        public void Initialize(EnemyType type, int wave, float difficultyMultiplier = 1f)
        {
            enemyType = type;
            var stats = EnemyData.GetStats(type);

            maxHP = EnemyData.CalculateScaledHP(type, wave, difficultyMultiplier);
            currentHP = maxHP;
            speed = stats.Speed;
            wallDamage = stats.WallDamage;
            attackCooldown = stats.AttackCooldown;
            scoreValue = stats.ScoreValue;
            attackTimer = attackCooldown;

            currentState = EnemyState.Moving;

            CreateVisual(stats);
            CreateHealthBar();
        }

        private void Update()
        {
            if (GameManager.Instance == null || !GameManager.Instance.IsPlaying)
                return;

            switch (currentState)
            {
                case EnemyState.Moving:
                    UpdateMovement();
                    break;
                case EnemyState.Attacking:
                    UpdateAttacking();
                    break;
            }
        }

        private void UpdateMovement()
        {
            var layout = GameLayout.Instance;
            if (layout == null) return;

            float targetY = layout.FirewallTop;
            Vector3 pos = transform.position;

            pos.y -= speed * Time.deltaTime;

            if (pos.y <= targetY)
            {
                pos.y = targetY;
                currentState = EnemyState.Attacking;
                attackTimer = attackCooldown;
            }

            transform.position = pos;
        }

        private void UpdateAttacking()
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                AttackFirewall();
                attackTimer = attackCooldown;
            }
        }

        private void AttackFirewall()
        {
            var firewall = Firewall.Firewall.Instance;
            if (firewall != null && !firewall.IsDestroyed)
            {
                firewall.TakeDamage(wallDamage);
            }
        }

        public bool TakeDamage(int amount)
        {
            if (amount <= 0 || !IsAlive) return false;

            currentHP = Mathf.Max(0, currentHP - amount);
            OnHPChanged?.Invoke(currentHP, maxHP);

            if (currentHP <= 0)
            {
                Die();
                return true;
            }

            return false;
        }

        private void Die()
        {
            if (currentState == EnemyState.Dead) return;

            currentState = EnemyState.Dead;
            OnDied?.Invoke();

            EnemyManager.Instance?.RemoveEnemy(this);
            Destroy(gameObject);
        }

        private void CreateVisual(EnemyStats stats)
        {
            float size = enemyType == EnemyType.Ransomware ? ransomwareSize : normalSize;
            Color color = GetColorForType(enemyType);

            var visualGO = new GameObject("Visual");
            visualGO.transform.SetParent(transform);
            visualGO.transform.localPosition = Vector3.zero;

            visualRenderer = visualGO.AddComponent<SpriteRenderer>();
            visualRenderer.sprite = CreateSquareSprite();
            visualRenderer.color = color;
            visualRenderer.sortingOrder = 10;
            visualGO.transform.localScale = new Vector3(size, size, 1f);

            var labelGO = new GameObject("Label");
            labelGO.transform.SetParent(transform);
            labelGO.transform.localPosition = Vector3.zero;

            labelText = labelGO.AddComponent<TextMeshPro>();
            labelText.text = stats.Placeholder;
            labelText.fontSize = enemyType == EnemyType.Ransomware ? 4f : 3f;
            labelText.alignment = TextAlignmentOptions.Center;
            labelText.color = Color.white;
            labelText.sortingOrder = 11;

            var rectTransform = labelText.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(2f, 2f);
        }

        private Color GetColorForType(EnemyType type)
        {
            return type switch
            {
                EnemyType.Virus => virusColor,
                EnemyType.Worm => wormColor,
                EnemyType.Ransomware => ransomwareColor,
                _ => virusColor
            };
        }

        private Sprite CreateSquareSprite()
        {
            var texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, Color.white);
            texture.Apply();
            return Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
        }

        private void CreateHealthBar()
        {
            var healthBarGO = new GameObject("HealthBar");
            healthBarGO.transform.SetParent(transform);

            healthBar = healthBarGO.AddComponent<EnemyHealthBar>();
            healthBar.Initialize(this);
        }
    }
}
