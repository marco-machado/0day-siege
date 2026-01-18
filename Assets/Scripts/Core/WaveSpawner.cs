using System.Collections;
using UnityEngine;
using ZeroDaySiege.Enemies;

namespace ZeroDaySiege.Core
{
    public class WaveSpawner : MonoBehaviour
    {
        public static WaveSpawner Instance { get; private set; }

        private Coroutine spawnCoroutine;

        private static readonly WaveDefinition[] Stage1Waves = CreateStage1Waves();

        private void Awake()
        {
            Debug.Log("[WaveSpawner] Awake called");
            if (Instance != null && Instance != this)
            {
                Debug.Log("[WaveSpawner] Duplicate instance, destroying");
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("[WaveSpawner] Instance set");
        }

        private void Start()
        {
            SubscribeToWaveManager();
        }

        private void SubscribeToWaveManager()
        {
            if (WaveManager.Instance != null)
            {
                WaveManager.Instance.OnWaveStateChanged += HandleWaveStateChanged;
                Debug.Log("[WaveSpawner] Subscribed to WaveManager");

                // If wave is already in progress (late subscription), start spawning
                if (WaveManager.Instance.CurrentWaveState == WaveState.InProgress)
                {
                    Debug.Log("[WaveSpawner] Wave already in progress, starting spawn");
                    StartSpawning();
                }
            }
            else
            {
                Debug.LogWarning("[WaveSpawner] WaveManager.Instance is null, retrying...");
                StartCoroutine(RetrySubscription());
            }
        }

        private System.Collections.IEnumerator RetrySubscription()
        {
            yield return null;
            SubscribeToWaveManager();
        }

        private void OnDestroy()
        {
            if (WaveManager.Instance != null)
            {
                WaveManager.Instance.OnWaveStateChanged -= HandleWaveStateChanged;
            }
        }

        private void HandleWaveStateChanged(WaveState state)
        {
            if (state == WaveState.InProgress)
            {
                StartSpawning();
            }
            else if (state == WaveState.Idle)
            {
                StopSpawning();
            }
        }

        private void StartSpawning()
        {
            StopSpawning();

            int waveIndex = GameManager.Instance.CurrentWave - 1;
            if (waveIndex < 0 || waveIndex >= Stage1Waves.Length)
            {
                Debug.LogWarning($"[WaveSpawner] Invalid wave index: {waveIndex}");
                return;
            }

            spawnCoroutine = StartCoroutine(SpawnWaveEnemies(Stage1Waves[waveIndex]));
        }

        private void StopSpawning()
        {
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
                spawnCoroutine = null;
            }
        }

        private IEnumerator SpawnWaveEnemies(WaveDefinition wave)
        {
            int currentWave = GameManager.Instance.CurrentWave;
            float difficulty = 1f;

            Debug.Log($"[WaveSpawner] Starting wave {wave.WaveNumber} with {wave.Enemies.Length} enemies");

            float elapsedTime = 0f;
            int spawnIndex = 0;

            while (spawnIndex < wave.Enemies.Length)
            {
                if (!GameManager.Instance.IsPlaying)
                {
                    yield break;
                }

                var spawn = wave.Enemies[spawnIndex];

                if (elapsedTime >= spawn.SpawnTime)
                {
                    EnemyManager.Instance.SpawnEnemy(spawn.Type, spawn.SpawnX, currentWave, difficulty);
                    spawnIndex++;
                }
                else
                {
                    yield return null;
                    elapsedTime += Time.deltaTime;
                }
            }

            Debug.Log($"[WaveSpawner] Wave {wave.WaveNumber} spawning complete");
            spawnCoroutine = null;

            WaveManager.Instance.CompleteCurrentWave();
        }

        private static WaveDefinition[] CreateStage1Waves()
        {
            return new WaveDefinition[]
            {
                // Wave 1: Introduction - 3 Viruses
                new(1, false, new EnemySpawn[]
                {
                    new(EnemyType.Virus, 0.5f, 0f),
                    new(EnemyType.Virus, 0.3f, 1.5f),
                    new(EnemyType.Virus, 0.7f, 1.5f),
                }),

                // Wave 2: 4 Viruses spread out
                new(2, false, new EnemySpawn[]
                {
                    new(EnemyType.Virus, 0.2f, 0f),
                    new(EnemyType.Virus, 0.8f, 0f),
                    new(EnemyType.Virus, 0.4f, 2f),
                    new(EnemyType.Virus, 0.6f, 2f),
                }),

                // Wave 3: 5 Viruses in sequence
                new(3, false, new EnemySpawn[]
                {
                    new(EnemyType.Virus, 0.5f, 0f),
                    new(EnemyType.Virus, 0.3f, 1f),
                    new(EnemyType.Virus, 0.7f, 2f),
                    new(EnemyType.Virus, 0.2f, 3f),
                    new(EnemyType.Virus, 0.8f, 4f),
                }),

                // Wave 4: Introduce Worms - 2 Worms + 3 Viruses
                new(4, false, new EnemySpawn[]
                {
                    new(EnemyType.Worm, 0.5f, 0f),
                    new(EnemyType.Virus, 0.3f, 1f),
                    new(EnemyType.Virus, 0.7f, 1f),
                    new(EnemyType.Worm, 0.5f, 2.5f),
                    new(EnemyType.Virus, 0.5f, 3.5f),
                }),

                // Wave 5: Mixed assault
                new(5, false, new EnemySpawn[]
                {
                    new(EnemyType.Virus, 0.2f, 0f),
                    new(EnemyType.Virus, 0.8f, 0f),
                    new(EnemyType.Worm, 0.4f, 1f),
                    new(EnemyType.Worm, 0.6f, 1f),
                    new(EnemyType.Virus, 0.5f, 2.5f),
                    new(EnemyType.Virus, 0.5f, 3f),
                }),

                // Wave 6: Worm rush
                new(6, false, new EnemySpawn[]
                {
                    new(EnemyType.Worm, 0.2f, 0f),
                    new(EnemyType.Worm, 0.5f, 0f),
                    new(EnemyType.Worm, 0.8f, 0f),
                    new(EnemyType.Virus, 0.3f, 2f),
                    new(EnemyType.Virus, 0.7f, 2f),
                }),

                // Wave 7: Sustained pressure
                new(7, false, new EnemySpawn[]
                {
                    new(EnemyType.Virus, 0.5f, 0f),
                    new(EnemyType.Worm, 0.3f, 0.5f),
                    new(EnemyType.Worm, 0.7f, 1f),
                    new(EnemyType.Virus, 0.2f, 2f),
                    new(EnemyType.Virus, 0.8f, 2.5f),
                    new(EnemyType.Worm, 0.5f, 3f),
                    new(EnemyType.Virus, 0.5f, 4f),
                }),

                // Wave 8: Flanking pattern
                new(8, false, new EnemySpawn[]
                {
                    new(EnemyType.Worm, 0.1f, 0f),
                    new(EnemyType.Worm, 0.9f, 0f),
                    new(EnemyType.Virus, 0.5f, 1f),
                    new(EnemyType.Virus, 0.3f, 2f),
                    new(EnemyType.Virus, 0.7f, 2f),
                    new(EnemyType.Worm, 0.5f, 3f),
                }),

                // Wave 9: Pre-boss buildup
                new(9, false, new EnemySpawn[]
                {
                    new(EnemyType.Virus, 0.2f, 0f),
                    new(EnemyType.Virus, 0.5f, 0f),
                    new(EnemyType.Virus, 0.8f, 0f),
                    new(EnemyType.Worm, 0.3f, 1.5f),
                    new(EnemyType.Worm, 0.7f, 1.5f),
                    new(EnemyType.Virus, 0.5f, 3f),
                    new(EnemyType.Worm, 0.5f, 4f),
                    new(EnemyType.Virus, 0.2f, 4.5f),
                    new(EnemyType.Virus, 0.8f, 4.5f),
                }),

                // Wave 10: Mini-boss - Ransomware with escorts
                new(10, true, new EnemySpawn[]
                {
                    new(EnemyType.Virus, 0.3f, 0f),
                    new(EnemyType.Virus, 0.7f, 0f),
                    new(EnemyType.Ransomware, 0.5f, 1f),
                    new(EnemyType.Worm, 0.2f, 2f),
                    new(EnemyType.Worm, 0.8f, 2f),
                }),

                // Wave 11: Post-boss intensity increase
                new(11, false, new EnemySpawn[]
                {
                    new(EnemyType.Worm, 0.3f, 0f),
                    new(EnemyType.Worm, 0.7f, 0f),
                    new(EnemyType.Virus, 0.5f, 0.5f),
                    new(EnemyType.Virus, 0.2f, 1.5f),
                    new(EnemyType.Virus, 0.8f, 1.5f),
                    new(EnemyType.Worm, 0.5f, 2.5f),
                    new(EnemyType.Virus, 0.4f, 3.5f),
                    new(EnemyType.Virus, 0.6f, 3.5f),
                }),

                // Wave 12: Cross pattern
                new(12, false, new EnemySpawn[]
                {
                    new(EnemyType.Virus, 0.5f, 0f),
                    new(EnemyType.Worm, 0.2f, 0.5f),
                    new(EnemyType.Worm, 0.8f, 0.5f),
                    new(EnemyType.Virus, 0.3f, 1.5f),
                    new(EnemyType.Virus, 0.7f, 1.5f),
                    new(EnemyType.Worm, 0.5f, 2.5f),
                    new(EnemyType.Virus, 0.1f, 3f),
                    new(EnemyType.Virus, 0.9f, 3f),
                }),

                // Wave 13: Swarm
                new(13, false, new EnemySpawn[]
                {
                    new(EnemyType.Worm, 0.2f, 0f),
                    new(EnemyType.Worm, 0.4f, 0f),
                    new(EnemyType.Worm, 0.6f, 0f),
                    new(EnemyType.Worm, 0.8f, 0f),
                    new(EnemyType.Virus, 0.5f, 1.5f),
                    new(EnemyType.Virus, 0.3f, 2f),
                    new(EnemyType.Virus, 0.7f, 2f),
                }),

                // Wave 14: Heavy viruses
                new(14, false, new EnemySpawn[]
                {
                    new(EnemyType.Virus, 0.2f, 0f),
                    new(EnemyType.Virus, 0.4f, 0.5f),
                    new(EnemyType.Virus, 0.6f, 1f),
                    new(EnemyType.Virus, 0.8f, 1.5f),
                    new(EnemyType.Worm, 0.5f, 2f),
                    new(EnemyType.Virus, 0.3f, 3f),
                    new(EnemyType.Virus, 0.7f, 3f),
                    new(EnemyType.Worm, 0.5f, 4f),
                }),

                // Wave 15: Second mini-boss
                new(15, true, new EnemySpawn[]
                {
                    new(EnemyType.Worm, 0.2f, 0f),
                    new(EnemyType.Worm, 0.8f, 0f),
                    new(EnemyType.Ransomware, 0.5f, 0.5f),
                    new(EnemyType.Virus, 0.3f, 2f),
                    new(EnemyType.Virus, 0.7f, 2f),
                    new(EnemyType.Worm, 0.5f, 3f),
                }),

                // Wave 16: Recovery wave
                new(16, false, new EnemySpawn[]
                {
                    new(EnemyType.Virus, 0.3f, 0f),
                    new(EnemyType.Virus, 0.7f, 0f),
                    new(EnemyType.Worm, 0.5f, 1.5f),
                    new(EnemyType.Virus, 0.2f, 2.5f),
                    new(EnemyType.Virus, 0.8f, 2.5f),
                    new(EnemyType.Virus, 0.5f, 3.5f),
                }),

                // Wave 17: Escalation
                new(17, false, new EnemySpawn[]
                {
                    new(EnemyType.Worm, 0.2f, 0f),
                    new(EnemyType.Worm, 0.5f, 0f),
                    new(EnemyType.Worm, 0.8f, 0f),
                    new(EnemyType.Virus, 0.3f, 1f),
                    new(EnemyType.Virus, 0.7f, 1f),
                    new(EnemyType.Worm, 0.5f, 2f),
                    new(EnemyType.Virus, 0.2f, 3f),
                    new(EnemyType.Virus, 0.8f, 3f),
                    new(EnemyType.Worm, 0.4f, 4f),
                    new(EnemyType.Worm, 0.6f, 4f),
                }),

                // Wave 18: Heavy assault
                new(18, false, new EnemySpawn[]
                {
                    new(EnemyType.Virus, 0.1f, 0f),
                    new(EnemyType.Virus, 0.3f, 0f),
                    new(EnemyType.Virus, 0.5f, 0f),
                    new(EnemyType.Virus, 0.7f, 0f),
                    new(EnemyType.Virus, 0.9f, 0f),
                    new(EnemyType.Worm, 0.3f, 1.5f),
                    new(EnemyType.Worm, 0.7f, 1.5f),
                    new(EnemyType.Virus, 0.5f, 3f),
                    new(EnemyType.Worm, 0.5f, 4f),
                }),

                // Wave 19: Pre-final buildup
                new(19, false, new EnemySpawn[]
                {
                    new(EnemyType.Worm, 0.2f, 0f),
                    new(EnemyType.Worm, 0.8f, 0f),
                    new(EnemyType.Virus, 0.5f, 0.5f),
                    new(EnemyType.Virus, 0.3f, 1f),
                    new(EnemyType.Virus, 0.7f, 1f),
                    new(EnemyType.Worm, 0.4f, 2f),
                    new(EnemyType.Worm, 0.6f, 2f),
                    new(EnemyType.Virus, 0.2f, 3f),
                    new(EnemyType.Virus, 0.8f, 3f),
                    new(EnemyType.Worm, 0.5f, 4f),
                    new(EnemyType.Virus, 0.5f, 5f),
                }),

                // Wave 20: Final boss wave
                new(20, true, new EnemySpawn[]
                {
                    new(EnemyType.Worm, 0.2f, 0f),
                    new(EnemyType.Worm, 0.8f, 0f),
                    new(EnemyType.Virus, 0.3f, 0.5f),
                    new(EnemyType.Virus, 0.7f, 0.5f),
                    new(EnemyType.Ransomware, 0.5f, 1f),
                    new(EnemyType.Worm, 0.3f, 3f),
                    new(EnemyType.Worm, 0.7f, 3f),
                    new(EnemyType.Virus, 0.5f, 4f),
                }),
            };
        }
    }
}
