using System;
using UnityEngine;
using ZeroDaySiege.Enemies;

namespace ZeroDaySiege.Core
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        public event Action<int> OnScoreChanged;
        public event Action<int> OnCardThresholdReached;

        private static readonly int[] CardThresholds = { 50, 120, 220, 350, 520, 730, 1000, 1350 };

        private int currentScore;
        private int cardsEarned;
        private float lastKillTime;
        private int killStreakBonus;
        private int frameKillCount;
        private int lastKillFrame;

        private const float KillStreakWindow = 2f;
        private const int MaxKillStreakBonus = 50;
        private const int MultiKillBonusPerEnemy = 5;
        private const int MaxMultiKillBonus = 100;

        public int CurrentScore => currentScore;
        public int CardsEarned => cardsEarned;
        public int NextCardThreshold => cardsEarned < CardThresholds.Length ? CardThresholds[cardsEarned] : int.MaxValue;

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
            if (EnemyManager.Instance != null)
            {
                EnemyManager.Instance.OnEnemyDied += HandleEnemyDied;
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += HandleGameStateChanged;
            }
        }

        private void OnDestroy()
        {
            if (EnemyManager.Instance != null)
            {
                EnemyManager.Instance.OnEnemyDied -= HandleEnemyDied;
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= HandleGameStateChanged;
            }
        }

        private void HandleGameStateChanged(GameState previousState, GameState newState)
        {
            if (newState == GameState.Menu || (newState == GameState.Playing && previousState == GameState.Menu))
            {
                ResetScore();
            }
        }

        private void HandleEnemyDied(Enemy enemy, int baseScore)
        {
            int bonus = CalculateBonus(baseScore);
            int totalScore = baseScore + bonus;

            currentScore += totalScore;
            OnScoreChanged?.Invoke(currentScore);

            Debug.Log($"[ScoreManager] +{totalScore} (base: {baseScore}, bonus: {bonus}), Total: {currentScore}");

            CheckCardThresholds();
        }

        private int CalculateBonus(int baseScore)
        {
            int bonus = 0;
            float currentTime = Time.time;

            if (currentTime - lastKillTime <= KillStreakWindow)
            {
                killStreakBonus = Mathf.Min(killStreakBonus + 1, MaxKillStreakBonus);
            }
            else
            {
                killStreakBonus = 0;
            }
            lastKillTime = currentTime;
            bonus += killStreakBonus;

            int currentFrame = Time.frameCount;
            if (currentFrame == lastKillFrame)
            {
                frameKillCount++;
                int multiKillBonus = Mathf.Min(frameKillCount * MultiKillBonusPerEnemy, MaxMultiKillBonus);
                bonus += multiKillBonus;
            }
            else
            {
                frameKillCount = 0;
                lastKillFrame = currentFrame;
            }

            return bonus;
        }

        private void CheckCardThresholds()
        {
            while (cardsEarned < CardThresholds.Length && currentScore >= CardThresholds[cardsEarned])
            {
                cardsEarned++;
                Debug.Log($"[ScoreManager] Card threshold {cardsEarned} reached at score {currentScore}");
                OnCardThresholdReached?.Invoke(cardsEarned);
            }
        }

        public void ResetScore()
        {
            currentScore = 0;
            cardsEarned = 0;
            killStreakBonus = 0;
            frameKillCount = 0;
            lastKillTime = 0f;
            lastKillFrame = 0;
            OnScoreChanged?.Invoke(currentScore);
            Debug.Log("[ScoreManager] Score reset");
        }

        public void AddScore(int amount)
        {
            if (amount <= 0) return;

            currentScore += amount;
            OnScoreChanged?.Invoke(currentScore);
            CheckCardThresholds();
        }
    }
}
