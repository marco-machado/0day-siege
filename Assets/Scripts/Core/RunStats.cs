using System;
using UnityEngine;
using ZeroDaySiege.Enemies;
using ZeroDaySiege.Firewall;

namespace ZeroDaySiege.Core
{
    public class RunStats : MonoBehaviour
    {
        public static RunStats Instance { get; private set; }

        public int EnemiesDefeated { get; private set; }
        public int VirusesKilled { get; private set; }
        public int WormsKilled { get; private set; }
        public int RansomwareKilled { get; private set; }
        public int WaveReached { get; private set; }
        public bool PerfectWall { get; private set; }
        public int FinalScore { get; private set; }

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
                GameManager.Instance.OnWaveChanged += HandleWaveChanged;
            }

            if (EnemyManager.Instance != null)
            {
                EnemyManager.Instance.OnEnemyDied += HandleEnemyDied;
            }

            if (Firewall.Firewall.Instance != null)
            {
                Firewall.Firewall.Instance.OnHPChanged += HandleHPChanged;
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

            if (Firewall.Firewall.Instance != null)
            {
                Firewall.Firewall.Instance.OnHPChanged -= HandleHPChanged;
            }
        }

        private void HandleGameStateChanged(GameState previousState, GameState newState)
        {
            if (newState == GameState.Playing && previousState == GameState.Menu)
            {
                ResetStats();
            }
            else if (newState == GameState.Playing && previousState == GameState.GameOver)
            {
                ResetStats();
            }
            else if (newState == GameState.GameOver)
            {
                CaptureEndOfRunStats();
            }
        }

        private void HandleWaveChanged(int wave)
        {
            if (wave > WaveReached)
            {
                WaveReached = wave;
            }
        }

        private void HandleEnemyDied(Enemy enemy, int score)
        {
            EnemiesDefeated++;

            switch (enemy.Type)
            {
                case EnemyType.Virus:
                    VirusesKilled++;
                    break;
                case EnemyType.Worm:
                    WormsKilled++;
                    break;
                case EnemyType.Ransomware:
                    RansomwareKilled++;
                    break;
            }
        }

        private void HandleHPChanged(int current, int max)
        {
            if (current < max)
            {
                PerfectWall = false;
            }
        }

        private void ResetStats()
        {
            EnemiesDefeated = 0;
            VirusesKilled = 0;
            WormsKilled = 0;
            RansomwareKilled = 0;
            WaveReached = 0;
            PerfectWall = true;
            FinalScore = 0;
            Debug.Log("[RunStats] Stats reset");
        }

        private void CaptureEndOfRunStats()
        {
            if (GameManager.Instance != null)
            {
                WaveReached = GameManager.Instance.CurrentWave;
            }

            if (ScoreManager.Instance != null)
            {
                FinalScore = ScoreManager.Instance.CurrentScore;
            }

            Debug.Log($"[RunStats] Run ended - Wave: {WaveReached}, Enemies: {EnemiesDefeated}, Score: {FinalScore}, Perfect: {PerfectWall}");
        }
    }
}
