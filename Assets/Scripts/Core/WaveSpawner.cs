using System.Collections;
using UnityEngine;
using ZeroDaySiege.Enemies;

namespace ZeroDaySiege.Core
{
    public class WaveSpawner : MonoBehaviour
    {
        public static WaveSpawner Instance { get; private set; }

        private Coroutine spawnCoroutine;
        private WaveDefinition[] currentStageWaves;

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
            SubscribeToStageManager();
        }

        private void SubscribeToWaveManager()
        {
            if (WaveManager.Instance != null)
            {
                WaveManager.Instance.OnWaveStateChanged += HandleWaveStateChanged;
                Debug.Log("[WaveSpawner] Subscribed to WaveManager");

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

        private void SubscribeToStageManager()
        {
            if (StageManager.Instance != null)
            {
                StageManager.Instance.OnStageSelected += HandleStageSelected;
                LoadCurrentStageWaves();
            }
            else
            {
                StartCoroutine(RetryStageSubscription());
            }
        }

        private System.Collections.IEnumerator RetrySubscription()
        {
            yield return null;
            SubscribeToWaveManager();
        }

        private System.Collections.IEnumerator RetryStageSubscription()
        {
            yield return null;
            SubscribeToStageManager();
        }

        private void OnDestroy()
        {
            if (WaveManager.Instance != null)
            {
                WaveManager.Instance.OnWaveStateChanged -= HandleWaveStateChanged;
            }
            if (StageManager.Instance != null)
            {
                StageManager.Instance.OnStageSelected -= HandleStageSelected;
            }
        }

        private void HandleStageSelected(StageData stageData)
        {
            LoadCurrentStageWaves();
        }

        private void LoadCurrentStageWaves()
        {
            if (StageManager.Instance != null)
            {
                currentStageWaves = StageManager.Instance.GetCurrentStageWaves();
                Debug.Log($"[WaveSpawner] Loaded {currentStageWaves?.Length ?? 0} waves for stage {StageManager.Instance.CurrentStageId}");
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

            if (currentStageWaves == null || currentStageWaves.Length == 0)
            {
                LoadCurrentStageWaves();
            }

            if (currentStageWaves == null || currentStageWaves.Length == 0)
            {
                Debug.LogError("[WaveSpawner] No wave data available");
                return;
            }

            int waveIndex = GameManager.Instance.CurrentWave - 1;
            if (waveIndex < 0 || waveIndex >= currentStageWaves.Length)
            {
                Debug.LogWarning($"[WaveSpawner] Invalid wave index: {waveIndex}");
                return;
            }

            spawnCoroutine = StartCoroutine(SpawnWaveEnemies(currentStageWaves[waveIndex]));
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
                while (!GameManager.Instance.IsPlaying)
                {
                    yield return null;
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
    }
}
