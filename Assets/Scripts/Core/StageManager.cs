using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroDaySiege.Core
{
    public class StageManager : MonoBehaviour
    {
        public static StageManager Instance { get; private set; }

        public event Action<StageData> OnStageSelected;
        public event Action<int, int> OnStageUnlocked;

        public const int StagesPerChapter = 25;
        public const int MvpStagesPerChapter = 5;

        private const string UnlockedStagesKey = "UnlockedStages";

        private readonly Dictionary<string, StageData> loadedStages = new();
        private readonly HashSet<string> unlockedStages = new();

        private StageData currentStage;
        public StageData CurrentStage => currentStage;
        public int CurrentChapter => currentStage?.chapter ?? 1;
        public int CurrentStageId => currentStage?.stageId ?? 1;

        public static readonly StageInfo[] AllStages = new StageInfo[]
        {
            new(1, 1, "Entry Point"),
            new(1, 2, "Packet Storm"),
            new(1, 3, "Payload Delivery"),
            new(1, 4, "Privilege Escalation"),
            new(1, 5, "Root Access")
        };

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadUnlockedStages();
            LoadAllStageData();
            SelectStage(1, 1);
        }

        private static string GetStageKey(int chapter, int stageId) => $"{chapter}_{stageId}";

        private void LoadUnlockedStages()
        {
            unlockedStages.Clear();
            unlockedStages.Add(GetStageKey(1, 1));

            string saved = PlayerPrefs.GetString(UnlockedStagesKey, "");
            if (!string.IsNullOrEmpty(saved))
            {
                foreach (var key in saved.Split(','))
                {
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        unlockedStages.Add(key.Trim());
                    }
                }
            }
        }

        private void SaveUnlockedStages()
        {
            var stages = new List<string>(unlockedStages);
            PlayerPrefs.SetString(UnlockedStagesKey, string.Join(",", stages));
            PlayerPrefs.Save();
        }

        private void LoadAllStageData()
        {
            foreach (var stageInfo in AllStages)
            {
                var stageData = LoadStageFromResources(stageInfo.Chapter, stageInfo.Id);
                if (stageData != null)
                {
                    loadedStages[GetStageKey(stageInfo.Chapter, stageInfo.Id)] = stageData;
                }
                else
                {
                    Debug.LogWarning($"[StageManager] Failed to load stage: {stageInfo.GetDisplayId()}");
                }
            }
        }

        private StageData LoadStageFromResources(int chapter, int stageId)
        {
            var resourcePath = $"Stages/stage_{chapter}_{stageId}";
            var textAsset = Resources.Load<TextAsset>(resourcePath);

            if (textAsset == null)
            {
                Debug.LogWarning($"[StageManager] Stage file not found: {resourcePath}");
                return null;
            }

            try
            {
                return StageData.FromJson(textAsset.text);
            }
            catch (Exception e)
            {
                Debug.LogError($"[StageManager] Failed to parse stage {chapter}-{stageId}: {e.Message}");
                return null;
            }
        }

        public bool SelectStage(int chapter, int stageId)
        {
            var key = GetStageKey(chapter, stageId);

            if (!IsStageUnlocked(chapter, stageId))
            {
                Debug.LogWarning($"[StageManager] Stage {chapter}-{stageId} is locked");
                return false;
            }

            if (!loadedStages.TryGetValue(key, out var stageData))
            {
                Debug.LogWarning($"[StageManager] Stage {chapter}-{stageId} not found");
                return false;
            }

            currentStage = stageData;
            OnStageSelected?.Invoke(currentStage);
            Debug.Log($"[StageManager] Selected stage: {chapter}-{stageId} - {stageData.stageName}");
            return true;
        }

        public bool IsStageUnlocked(int chapter, int stageId)
        {
            return unlockedStages.Contains(GetStageKey(chapter, stageId));
        }

        public void UnlockNextStage()
        {
            if (currentStage == null) return;

            var next = GetNextStage(currentStage.chapter, currentStage.stageId);
            if (next.HasValue)
            {
                var key = GetStageKey(next.Value.chapter, next.Value.stageId);
                if (!unlockedStages.Contains(key))
                {
                    unlockedStages.Add(key);
                    SaveUnlockedStages();
                    OnStageUnlocked?.Invoke(next.Value.chapter, next.Value.stageId);
                    Debug.Log($"[StageManager] Unlocked stage: {next.Value.chapter}-{next.Value.stageId}");
                }
            }
        }

        private (int chapter, int stageId)? GetNextStage(int chapter, int stageId)
        {
            for (int i = 0; i < AllStages.Length - 1; i++)
            {
                if (AllStages[i].Chapter == chapter && AllStages[i].Id == stageId)
                {
                    return (AllStages[i + 1].Chapter, AllStages[i + 1].Id);
                }
            }
            return null;
        }

        public StageData GetStageData(int chapter, int stageId)
        {
            return loadedStages.TryGetValue(GetStageKey(chapter, stageId), out var data) ? data : null;
        }

        public WaveDefinition[] GetCurrentStageWaves()
        {
            return currentStage?.ToWaveDefinitions() ?? Array.Empty<WaveDefinition>();
        }

        public int GetStageIndex(int chapter, int stageId)
        {
            for (int i = 0; i < AllStages.Length; i++)
            {
                if (AllStages[i].Chapter == chapter && AllStages[i].Id == stageId) return i;
            }
            return -1;
        }

        public StageProgress GetStageProgress(int chapter, int stageId)
        {
            return StageProgressStorage.Load(chapter, stageId);
        }

        public RewardBundle[] RecordCompletion(int chapter, int stageId, Difficulty difficulty, float hpPercent)
        {
            var stageData = GetStageData(chapter, stageId);
            if (stageData == null)
            {
                Debug.LogError($"[StageManager] Cannot record completion - stage {chapter}-{stageId} not found");
                return Array.Empty<RewardBundle>();
            }

            var progress = StageProgressStorage.Load(chapter, stageId);
            var earnedRewards = new List<RewardBundle>();

            earnedRewards.Add(stageData.rewards.completion);

            bool wasCleared = progress.IsCleared(difficulty);
            bool wasHalfHP = progress.IsHalfHP(difficulty);
            bool wasFullHP = progress.IsFullHP(difficulty);

            progress.MarkCleared(difficulty, hpPercent);

            if (!wasCleared && progress.IsCleared(difficulty))
                earnedRewards.Add(stageData.rewards.firstClear);
            if (!wasHalfHP && progress.IsHalfHP(difficulty))
                earnedRewards.Add(stageData.rewards.firstHalfHP);
            if (!wasFullHP && progress.IsFullHP(difficulty))
                earnedRewards.Add(stageData.rewards.firstFullHP);

            StageProgressStorage.Save(chapter, stageId, progress);
            return earnedRewards.ToArray();
        }

        public bool IsHardUnlocked(int chapter, int stageId)
        {
            var progress = StageProgressStorage.Load(chapter, stageId);
            return progress.normalCleared;
        }
    }

    public readonly struct StageInfo
    {
        public readonly int Chapter;
        public readonly int Id;
        public readonly string Name;

        public StageInfo(int chapter, int id, string name)
        {
            Chapter = chapter;
            Id = id;
            Name = name;
        }

        public string GetDisplayId() => $"{Chapter}-{Id}";
    }
}
