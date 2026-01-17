using System;
using TMPro;
using UnityEngine;
using ZeroDaySiege.Core;
using ZeroDaySiege.Enemies;

namespace ZeroDaySiege.Towers
{
    public class Tower : MonoBehaviour
    {
        [Header("Critical Hit")]
        [SerializeField] private float baseCritChance = 0.05f;
        [SerializeField] private float baseCritMultiplier = 1.5f;

        [Header("Visual")]
        [SerializeField] private float towerSize = 1.0f;

        public event Action<int, int> OnDamageDealt;

        private TowerType towerType;
        private TowerState currentState;
        private int slotIndex;

        private int baseDamage;
        private float fireRate;
        private float normalizedRange;
        private float worldRange;
        private float projectileSpeed;

        private float fireTimer;
        private Enemy currentTarget;

        private SpriteRenderer visualRenderer;
        private TextMeshPro labelText;

        public TowerType Type => towerType;
        public TowerState State => currentState;
        public int SlotIndex => slotIndex;
        public int Damage => baseDamage;
        public float FireRate => fireRate;
        public float Range => normalizedRange;
        public float WorldRange => worldRange;

        public void Initialize(TowerType type, int slot)
        {
            towerType = type;
            slotIndex = slot;

            var stats = TowerData.GetStats(type);
            baseDamage = stats.Damage;
            fireRate = stats.FireRate;
            normalizedRange = stats.Range;
            projectileSpeed = stats.ProjectileSpeed;

            var layout = GameLayout.Instance;
            if (layout != null)
            {
                worldRange = TowerData.ConvertRangeToWorld(normalizedRange, layout.SpawnY, layout.FirewallY);
            }

            currentState = TowerState.Idle;
            fireTimer = 0f;

            CreateVisual(stats);

            Debug.Log($"[Tower] Initialized {type} in slot {slot}, Damage={baseDamage}, Rate={fireRate}/s, Range={worldRange:F1} units");
        }

        private void Update()
        {
            if (GameManager.Instance == null || !GameManager.Instance.IsPlaying)
                return;

            fireTimer -= Time.deltaTime;

            UpdateTargeting();

            if (currentTarget != null && fireTimer <= 0f)
            {
                Fire();
                fireTimer = 1f / fireRate;
            }
        }

        private void UpdateTargeting()
        {
            var enemyManager = EnemyManager.Instance;
            if (enemyManager == null)
            {
                currentTarget = null;
                currentState = TowerState.Idle;
                return;
            }

            currentTarget = TargetingSystem.FindTarget(transform.position, worldRange, enemyManager.ActiveEnemies);

            currentState = currentTarget != null ? TowerState.Targeting : TowerState.Idle;
        }

        private void Fire()
        {
            if (currentTarget == null || !currentTarget.IsAlive)
                return;

            currentState = TowerState.Firing;

            bool isCrit = UnityEngine.Random.value < baseCritChance;
            int finalDamage = isCrit ? Mathf.FloorToInt(baseDamage * baseCritMultiplier) : baseDamage;

            var projectileGO = new GameObject($"Projectile_{towerType}");
            projectileGO.transform.position = transform.position;

            var projectile = projectileGO.AddComponent<Projectile>();
            projectile.Initialize(currentTarget, projectileSpeed, finalDamage, isCrit);
            projectile.OnHit += HandleProjectileHit;

            Debug.Log($"[Tower] {towerType} fired at {currentTarget.Type}, Damage={finalDamage}{(isCrit ? " (CRIT!)" : "")}");
        }

        private void HandleProjectileHit(Projectile projectile, Enemy enemy, int damage, bool wasCrit)
        {
            projectile.OnHit -= HandleProjectileHit;
            OnDamageDealt?.Invoke(damage, wasCrit ? 1 : 0);
        }

        private void CreateVisual(TowerStats stats)
        {
            var visualGO = new GameObject("Visual");
            visualGO.transform.SetParent(transform);
            visualGO.transform.localPosition = Vector3.zero;

            visualRenderer = visualGO.AddComponent<SpriteRenderer>();
            visualRenderer.sprite = CreateSquareSprite();
            visualRenderer.color = stats.Color;
            visualRenderer.sortingOrder = 5;
            visualGO.transform.localScale = new Vector3(towerSize, towerSize, 1f);

            var labelGO = new GameObject("Label");
            labelGO.transform.SetParent(transform);
            labelGO.transform.localPosition = Vector3.zero;

            labelText = labelGO.AddComponent<TextMeshPro>();
            labelText.text = stats.Placeholder;
            labelText.fontSize = 3f;
            labelText.alignment = TextAlignmentOptions.Center;
            labelText.color = Color.white;
            labelText.sortingOrder = 6;

            var rectTransform = labelText.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(2f, 2f);
        }

        private Sprite CreateSquareSprite()
        {
            var texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, Color.white);
            texture.Apply();
            return Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
        }

        public bool IsInRange(Vector3 position)
        {
            return Vector3.Distance(transform.position, position) <= worldRange;
        }
    }
}
