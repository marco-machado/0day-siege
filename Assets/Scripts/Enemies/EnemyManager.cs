using System;
using System.Collections.Generic;
using UnityEngine;
using ZeroDaySiege.Core;

namespace ZeroDaySiege.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance { get; private set; }

        public event Action<Enemy> OnEnemySpawned;
        public event Action<Enemy, int> OnEnemyDied;
        public event Action OnAllEnemiesDefeated;

        private readonly List<Enemy> activeEnemies = new();

        public int ActiveEnemyCount => activeEnemies.Count;
        public IReadOnlyList<Enemy> ActiveEnemies => activeEnemies;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

        private void HandleGameStateChanged(GameState previousState, GameState newState)
        {
            if (newState == GameState.Menu || newState == GameState.GameOver)
            {
                ClearAllEnemies();
            }
        }

        public Enemy SpawnEnemy(EnemyType type, float normalizedX, int wave = 1, float difficultyMultiplier = 1f)
        {
            var layout = GameLayout.Instance;
            if (layout == null)
            {
                Debug.LogWarning("[EnemyManager] Cannot spawn enemy: GameLayout not found");
                return null;
            }

            float worldX = layout.NormalizedToWorldX(normalizedX);
            Vector3 spawnPosition = new(worldX, layout.SpawnY, 0f);

            var enemyGO = new GameObject($"Enemy_{type}");
            enemyGO.transform.position = spawnPosition;

            var enemy = enemyGO.AddComponent<Enemy>();
            enemy.Initialize(type, wave, difficultyMultiplier);

            activeEnemies.Add(enemy);
            OnEnemySpawned?.Invoke(enemy);

            Debug.Log($"[EnemyManager] Spawned {type} at X={normalizedX:F2} (world: {worldX:F2}), HP={enemy.MaxHP}");

            return enemy;
        }

        public void RemoveEnemy(Enemy enemy)
        {
            if (enemy == null) return;

            if (activeEnemies.Remove(enemy))
            {
                OnEnemyDied?.Invoke(enemy, enemy.ScoreValue);
                Debug.Log($"[EnemyManager] Enemy died: {enemy.Type}, Score: {enemy.ScoreValue}, Remaining: {activeEnemies.Count}");

                if (activeEnemies.Count == 0 && GameManager.Instance?.IsPlaying == true)
                {
                    if (WaveManager.Instance?.CurrentWaveState == WaveState.InProgress)
                    {
                        OnAllEnemiesDefeated?.Invoke();
                    }
                }
            }
        }

        public void ClearAllEnemies()
        {
            for (int i = activeEnemies.Count - 1; i >= 0; i--)
            {
                var enemy = activeEnemies[i];
                if (enemy != null && enemy.gameObject != null)
                {
                    Destroy(enemy.gameObject);
                }
            }
            activeEnemies.Clear();
            Debug.Log("[EnemyManager] Cleared all enemies");
        }

        public Enemy GetClosestEnemyToFirewall()
        {
            if (activeEnemies.Count == 0) return null;

            Enemy closest = null;
            float closestY = float.MaxValue;

            foreach (var enemy in activeEnemies)
            {
                if (enemy == null || !enemy.IsAlive) continue;

                float y = enemy.transform.position.y;
                if (y < closestY)
                {
                    closestY = y;
                    closest = enemy;
                }
            }

            return closest;
        }
    }
}
