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
        private bool awaitingVictory;

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

            bool isFinalWave = GameManager.Instance.CurrentWave >= GameManager.TotalWaves;
            bool spawningComplete = currentWaveState == WaveState.Transitioning || awaitingVictory;

            if (isFinalWave && spawningComplete)
            {
                awaitingVictory = false;
                Debug.Log("[WaveManager] All enemies defeated on final wave, triggering victory");
                GameManager.Instance.EndRun(true);
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
            SetWaveState(WaveState.InProgress);
        }

        private void StopWave()
        {
            if (transitionCoroutine != null)
            {
                StopCoroutine(transitionCoroutine);
                transitionCoroutine = null;
            }
            awaitingVictory = false;
            SetWaveState(WaveState.Idle);
        }

        public void CompleteCurrentWave()
        {
            if (currentWaveState != WaveState.InProgress) return;
            if (!GameManager.Instance.IsPlaying) return;

            SetWaveState(WaveState.Transitioning);
            transitionCoroutine = StartCoroutine(TransitionToNextWave());
        }

        private IEnumerator TransitionToNextWave()
        {
            yield return new WaitForSeconds(TransitionDuration);

            transitionCoroutine = null;

            if (GameManager.Instance.CurrentWave >= GameManager.TotalWaves)
            {
                if (EnemyManager.Instance == null || EnemyManager.Instance.ActiveEnemyCount == 0)
                {
                    GameManager.Instance.EndRun(true);
                }
                else
                {
                    awaitingVictory = true;
                    Debug.Log("[WaveManager] Final wave spawning complete, waiting for all enemies to be defeated");
                }
            }
            else
            {
                GameManager.Instance.AdvanceWave();
                SetWaveState(WaveState.InProgress);
            }
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
