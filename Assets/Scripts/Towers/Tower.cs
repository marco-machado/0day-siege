using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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
        private float splashRadius;
        private float splashFalloff;
        private float worldSplashRadius;
        private Color towerColor;

        private float fireTimer;
        private Enemy currentTarget;

        private const float PiercingHitWidth = 0.5f;

        private SpriteRenderer visualRenderer;
        private TextMeshPro labelText;
        private LineRenderer rangeIndicator;
        private bool isHovered;

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
            splashRadius = stats.SplashRadius;
            splashFalloff = stats.SplashFalloff;
            towerColor = stats.Color;

            var layout = GameLayout.Instance;
            if (layout != null)
            {
                worldRange = TowerData.ConvertRangeToWorld(normalizedRange, layout.SpawnY, layout.FirewallY);
                worldSplashRadius = splashRadius * layout.PlayAreaWidth;
            }

            currentState = TowerState.Idle;
            fireTimer = 0f;

            CreateVisual(stats);

            Debug.Log($"[Tower] Initialized {type} in slot {slot}, Damage={baseDamage}, Rate={fireRate}/s, Range={worldRange:F1} units");
        }

        private void Update()
        {
            UpdateHover();

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

        private void UpdateHover()
        {
            if (rangeIndicator == null) return;

            var mouse = Mouse.current;
            if (mouse == null) return;

            Vector3 mouseScreen = mouse.position.ReadValue();
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
            mouseWorld.z = 0f;

            float distance = Vector2.Distance(mouseWorld, transform.position);
            bool nowHovered = distance <= towerSize * 0.5f;

            if (nowHovered != isHovered)
            {
                isHovered = nowHovered;
                rangeIndicator.gameObject.SetActive(isHovered);
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

            if (towerType == TowerType.PiercingTower)
            {
                FirePiercing(finalDamage, isCrit);
            }
            else
            {
                FireProjectile(finalDamage, isCrit);
            }
        }

        private void FireProjectile(int damage, bool isCrit)
        {
            var projectileGO = new GameObject($"Projectile_{towerType}");
            projectileGO.transform.position = transform.position;

            var projectile = projectileGO.AddComponent<Projectile>();

            if (splashRadius > 0f)
            {
                projectile.InitializeAOE(currentTarget, projectileSpeed, damage, isCrit, worldSplashRadius, splashFalloff);
                Debug.Log($"[Tower] {towerType} fired AOE at {currentTarget.Type}, Damage={damage}, Splash={worldSplashRadius:F2}{(isCrit ? " (CRIT!)" : "")}");
            }
            else
            {
                projectile.Initialize(currentTarget, projectileSpeed, damage, isCrit);
                Debug.Log($"[Tower] {towerType} fired at {currentTarget.Type}, Damage={damage}{(isCrit ? " (CRIT!)" : "")}");
            }

            projectile.OnHit += HandleProjectileHit;
        }

        private void FirePiercing(int damage, bool isCrit)
        {
            var enemyManager = EnemyManager.Instance;
            if (enemyManager == null) return;

            Vector3 start = transform.position;
            Vector3 targetPos = currentTarget.transform.position;
            Vector3 direction = (targetPos - start).normalized;

            var layout = GameLayout.Instance;
            float maxDistance = layout != null ? Mathf.Abs(layout.SpawnY - layout.FirewallY) + 2f : 15f;
            Vector3 end = start + direction * maxDistance;

            int enemiesHit = 0;
            int totalDamage = 0;

            var enemies = new System.Collections.Generic.List<Enemy>(enemyManager.ActiveEnemies);
            foreach (var enemy in enemies)
            {
                if (enemy == null || !enemy.IsAlive) continue;

                float distToLine = DistancePointToLine(enemy.transform.position, start, end);
                if (distToLine <= PiercingHitWidth)
                {
                    enemy.TakeDamage(damage);
                    totalDamage += damage;
                    enemiesHit++;
                }
            }

            var railGO = new GameObject($"PiercingRail_{towerType}");
            var rail = railGO.AddComponent<PiercingRail>();
            rail.Initialize(start, end, towerColor, isCrit);

            if (enemiesHit > 0)
            {
                OnDamageDealt?.Invoke(totalDamage, isCrit ? 1 : 0);
                Debug.Log($"[Tower] {towerType} piercing hit {enemiesHit} enemies for {totalDamage} total{(isCrit ? " (CRIT!)" : "")}");
            }
        }

        private float DistancePointToLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
        {
            Vector3 line = lineEnd - lineStart;
            float lineLengthSq = line.sqrMagnitude;
            if (lineLengthSq == 0f) return Vector3.Distance(point, lineStart);

            float t = Mathf.Clamp01(Vector3.Dot(point - lineStart, line) / lineLengthSq);
            Vector3 projection = lineStart + t * line;
            return Vector3.Distance(point, projection);
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

            CreateRangeIndicator(stats.Color);
        }

        private void CreateRangeIndicator(Color color)
        {
            var rangeGO = new GameObject("RangeIndicator");
            rangeGO.transform.SetParent(transform);
            rangeGO.transform.localPosition = Vector3.zero;

            rangeIndicator = rangeGO.AddComponent<LineRenderer>();
            rangeIndicator.useWorldSpace = false;
            rangeIndicator.loop = true;

            int segments = 64;
            rangeIndicator.positionCount = segments;

            for (int i = 0; i < segments; i++)
            {
                float angle = (float)i / segments * Mathf.PI * 2f;
                float x = Mathf.Cos(angle) * worldRange;
                float y = Mathf.Sin(angle) * worldRange;
                rangeIndicator.SetPosition(i, new Vector3(x, y, 0f));
            }

            rangeIndicator.startWidth = 0.05f;
            rangeIndicator.endWidth = 0.05f;

            Color rangeColor = new Color(color.r, color.g, color.b, 0.4f);
            rangeIndicator.startColor = rangeColor;
            rangeIndicator.endColor = rangeColor;

            rangeIndicator.material = new Material(Shader.Find("Sprites/Default"));
            rangeIndicator.sortingOrder = 2;

            rangeGO.SetActive(false);
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
