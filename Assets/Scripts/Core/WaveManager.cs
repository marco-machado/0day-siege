using System;
using System.Collections;
using UnityEngine;
using ZeroDaySiege.Enemies;

namespace ZeroDaySiege.Core
{
    public class WaveManager : MonoBehaviour
    {
        public const float TransitionDuration = 1f;

        public static WaveManager Instance { get; private set; }

        public event Action<WaveState> OnWaveStateChanged;

        private WaveState currentWaveState;
        public WaveState CurrentWaveState => currentWaveState;

        private Coroutine transitionCoroutine;
        private bool spawningComplete;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            currentWaveState = WaveState.Idle;
        }

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += HandleGameStateChanged;
                GameManager.Instance.OnWaveChanged += HandleWaveChanged;
            }

            if (EnemyManager.Instance != null)
            {
                EnemyManager.Instance.OnEnemyDied += HandleEnemyDied;
            }
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= HandleGameStateChanged;
                GameManager.Instance.OnWaveChanged -= HandleWaveChanged;
            }

            if (EnemyManager.Instance != null)
            {
                EnemyManager.Instance.OnEnemyDied -= HandleEnemyDied;
            }
        }

        private void HandleEnemyDied(Enemy enemy, int score)
        {
            if (EnemyManager.Instance.ActiveEnemyCount > 0) return;
            if (!GameManager.Instance.IsPlaying) return;
            if (!spawningComplete) return;

            bool isFinalWave = GameManager.Instance.CurrentWave >= GameManager.TotalWaves;

            if (isFinalWave)
            {
                Debug.Log("[WaveManager] All enemies defeated on final wave, triggering victory");
                GameManager.Instance.EndRun(true);
            }
            else
            {
                Debug.Log("[WaveManager] All enemies defeated, starting transition to next wave");
                SetWaveState(WaveState.Transitioning);
                transitionCoroutine = StartCoroutine(TransitionToNextWave());
            }
        }

        private void HandleWaveChanged(int wave)
        {
            if (wave == 1 && GameManager.Instance.IsPlaying)
            {
                StartWave();
            }
        }

        private void HandleGameStateChanged(GameState previousState, GameState newState)
        {
            if (newState == GameState.Playing && previousState == GameState.Menu)
            {
                StartWave();
            }
            else if (newState == GameState.Playing && previousState == GameState.GameOver)
            {
                StartWave();
            }
            else if (newState == GameState.GameOver || newState == GameState.Menu)
            {
                StopWave();
            }
        }

        private void StartWave()
        {
            if (transitionCoroutine != null)
            {
                StopCoroutine(transitionCoroutine);
                transitionCoroutine = null;
            }
            spawningComplete = false;
            SetWaveState(WaveState.InProgress);
        }

        private void StopWave()
        {
            if (transitionCoroutine != null)
            {
                StopCoroutine(transitionCoroutine);
                transitionCoroutine = null;
            }
            spawningComplete = false;
            SetWaveState(WaveState.Idle);
        }

        public void CompleteCurrentWave()
        {
            if (currentWaveState != WaveState.InProgress) return;
            if (!GameManager.Instance.IsPlaying) return;

            spawningComplete = true;
            Debug.Log("[WaveManager] Spawning complete, waiting for all enemies to be defeated");

            if (EnemyManager.Instance == null || EnemyManager.Instance.ActiveEnemyCount == 0)
            {
                bool isFinalWave = GameManager.Instance.CurrentWave >= GameManager.TotalWaves;
                if (isFinalWave)
                {
                    Debug.Log("[WaveManager] Final wave complete with no enemies, triggering victory");
                    GameManager.Instance.EndRun(true);
                }
                else
                {
                    Debug.Log("[WaveManager] Wave complete with no enemies remaining, starting transition");
                    SetWaveState(WaveState.Transitioning);
                    transitionCoroutine = StartCoroutine(TransitionToNextWave());
                }
            }
        }

        private IEnumerator TransitionToNextWave()
        {
            yield return new WaitForSeconds(TransitionDuration);

            transitionCoroutine = null;

            GameManager.Instance.AdvanceWave();
            SetWaveState(WaveState.InProgress);
        }

        private void SetWaveState(WaveState newState)
        {
            if (currentWaveState == newState) return;

            currentWaveState = newState;
            OnWaveStateChanged?.Invoke(newState);
            Debug.Log($"Wave state: {newState}");
        }
    }
}
