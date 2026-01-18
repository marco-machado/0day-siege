using System.Collections.Generic;
using UnityEngine;
using ZeroDaySiege.Enemies;

namespace ZeroDaySiege.UI
{
    public class DamageNumberManager : MonoBehaviour
    {
        public static DamageNumberManager Instance { get; private set; }

        private const int PoolSize = 30;

        private readonly List<DamageNumber> pool = new();
        private Transform poolParent;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            CreatePool();
        }

        private void OnEnable()
        {
            if (EnemyManager.Instance != null)
            {
                EnemyManager.Instance.OnEnemySpawned += HandleEnemySpawned;
            }
        }

        private void OnDisable()
        {
            if (EnemyManager.Instance != null)
            {
                EnemyManager.Instance.OnEnemySpawned -= HandleEnemySpawned;
            }
        }

        private void CreatePool()
        {
            poolParent = new GameObject("DamageNumberPool").transform;
            poolParent.SetParent(transform);

            for (int i = 0; i < PoolSize; i++)
            {
                var go = new GameObject($"DamageNumber_{i}");
                go.transform.SetParent(poolParent);

                var damageNumber = go.AddComponent<DamageNumber>();
                damageNumber.Initialize();
                pool.Add(damageNumber);
            }
        }

        private void HandleEnemySpawned(Enemy enemy)
        {
            enemy.OnDamageTaken += (damage, isCrit) => ShowDamage(enemy.transform.position, damage, isCrit);
        }

        public void ShowDamage(Vector3 position, int damage, bool isCrit)
        {
            var damageNumber = GetFromPool();
            if (damageNumber != null)
            {
                damageNumber.Show(position, damage, isCrit);
            }
        }

        private DamageNumber GetFromPool()
        {
            foreach (var dn in pool)
            {
                if (!dn.IsActive)
                {
                    return dn;
                }
            }

            var go = new GameObject($"DamageNumber_{pool.Count}");
            go.transform.SetParent(poolParent);
            var newDamageNumber = go.AddComponent<DamageNumber>();
            newDamageNumber.Initialize();
            pool.Add(newDamageNumber);
            return newDamageNumber;
        }
    }
}
