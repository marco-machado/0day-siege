using System;
using UnityEngine;
using ZeroDaySiege.Core;
using ZeroDaySiege.Enemies;

namespace ZeroDaySiege.Towers
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Color normalColor = new Color(0f, 0.8f, 1f, 1f);
        [SerializeField] private Color criticalColor = new Color(1f, 0.3f, 0.3f, 1f);
        [SerializeField] private float size = 0.2f;

        public event Action<Projectile, Enemy, int, bool> OnHit;

        private Enemy target;
        private Vector3 targetPosition;
        private float speed;
        private int damage;
        private bool isCritical;
        private bool hasHit;

        private bool isAOE;
        private float splashRadius;
        private float splashFalloff;

        private SpriteRenderer visualRenderer;

        public void Initialize(Enemy target, float speed, int damage, bool isCritical)
        {
            this.target = target;
            this.targetPosition = target != null ? target.transform.position : transform.position;
            this.speed = speed;
            this.damage = damage;
            this.isCritical = isCritical;
            this.hasHit = false;
            this.isAOE = false;
            this.splashRadius = 0f;
            this.splashFalloff = 0f;

            CreateVisual();
        }

        public void InitializeAOE(Enemy target, float speed, int damage, bool isCritical, float worldSplashRadius, float falloff)
        {
            this.target = target;
            this.targetPosition = target != null ? target.transform.position : transform.position;
            this.speed = speed;
            this.damage = damage;
            this.isCritical = isCritical;
            this.hasHit = false;
            this.isAOE = true;
            this.splashRadius = worldSplashRadius;
            this.splashFalloff = falloff;

            CreateVisual();
        }

        private void Update()
        {
            if (hasHit) return;

            if (GameManager.Instance == null || !GameManager.Instance.IsPlaying)
                return;

            if (target != null && target.IsAlive)
            {
                targetPosition = target.transform.position;
            }

            MoveTowardTarget();
        }

        private void MoveTowardTarget()
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            float distanceThisFrame = speed * Time.deltaTime;
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            if (distanceThisFrame >= distanceToTarget)
            {
                transform.position = targetPosition;
                Hit();
            }
            else
            {
                transform.position += direction * distanceThisFrame;
            }
        }

        private void Hit()
        {
            if (hasHit) return;
            hasHit = true;

            if (isAOE)
            {
                HitAOE();
            }
            else
            {
                HitSingle();
            }

            Destroy(gameObject);
        }

        private void HitSingle()
        {
            if (target != null && target.IsAlive)
            {
                target.TakeDamage(damage);
                OnHit?.Invoke(this, target, damage, isCritical);
            }
        }

        private void HitAOE()
        {
            var enemyManager = EnemyManager.Instance;
            if (enemyManager == null) return;

            Vector3 impactPos = transform.position;
            int totalDamage = 0;
            int enemiesHit = 0;

            var enemies = new System.Collections.Generic.List<Enemy>(enemyManager.ActiveEnemies);
            foreach (var enemy in enemies)
            {
                if (enemy == null || !enemy.IsAlive) continue;

                float distance = Vector3.Distance(impactPos, enemy.transform.position);
                if (distance > splashRadius) continue;

                float damagePercent = 1f - (distance / splashRadius) * splashFalloff;
                int splashDamage = Mathf.FloorToInt(damage * damagePercent);

                if (splashDamage > 0)
                {
                    enemy.TakeDamage(splashDamage);
                    totalDamage += splashDamage;
                    enemiesHit++;
                }
            }

            if (enemiesHit > 0)
            {
                OnHit?.Invoke(this, target, totalDamage, isCritical);
                Debug.Log($"[Projectile] AOE hit {enemiesHit} enemies for {totalDamage} total damage");
            }
        }

        private void CreateVisual()
        {
            var visualGO = new GameObject("Visual");
            visualGO.transform.SetParent(transform);
            visualGO.transform.localPosition = Vector3.zero;

            visualRenderer = visualGO.AddComponent<SpriteRenderer>();
            visualRenderer.sprite = CreateCircleSprite();
            visualRenderer.color = isCritical ? criticalColor : normalColor;
            visualRenderer.sortingOrder = 15;
            visualGO.transform.localScale = new Vector3(size, size, 1f);
        }

        private Sprite CreateCircleSprite()
        {
            int resolution = 32;
            var texture = new Texture2D(resolution, resolution);
            texture.filterMode = FilterMode.Bilinear;

            float center = resolution / 2f;
            float radius = center - 1;

            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    float dist = Vector2.Distance(new Vector2(x, y), new Vector2(center, center));
                    if (dist <= radius)
                    {
                        texture.SetPixel(x, y, Color.white);
                    }
                    else
                    {
                        texture.SetPixel(x, y, Color.clear);
                    }
                }
            }

            texture.Apply();
            return Sprite.Create(texture, new Rect(0, 0, resolution, resolution), new Vector2(0.5f, 0.5f), resolution);
        }
    }
}
