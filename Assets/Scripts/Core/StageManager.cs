using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroDaySiege.Core
{
    public class StageManager : MonoBehaviour
    {
        public static StageManager Instance { get; private set; }

        public event Action<StageData> OnStageSelected;
        public event Action<string> OnStageUnlocked;

        private const string UnlockedStagesKey = "UnlockedStages";
        private const string FirstStageId = "1-1";

        private readonly Dictionary<string, StageData> loadedStages = new();
        private readonly HashSet<string> unlockedStages = new();

        private StageData currentStage;
        public StageData CurrentStage => currentStage;
        public string CurrentStageId => currentStage?.stageId ?? FirstStageId;

        public static readonly StageInfo[] AllStages = new StageInfo[]
        {
            new("1-1", "Entry Point"),
            new("1-2", "Packet Storm"),
            new("1-3", "Payload Delivery"),
            new("1-4", "Privilege Escalation"),
            new("1-5", "Root Access")
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
            SelectStage(FirstStageId);
        }

        private void LoadUnlockedStages()
        {
            unlockedStages.Clear();
            unlockedStages.Add(FirstStageId);

            string saved = PlayerPrefs.GetString(UnlockedStagesKey, "");
            if (!string.IsNullOrEmpty(saved))
            {
                foreach (var stageId in saved.Split(','))
                {
                    if (!string.IsNullOrWhiteSpace(stageId))
                    {
                        unlockedStages.Add(stageId.Trim());
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
                var stageData = LoadStageFromResources(stageInfo.Id);
                if (stageData != null)
                {
                    loadedStages[stageInfo.Id] = stageData;
                }
                else
                {
                    Debug.LogWarning($"[StageManager] Failed to load stage: {stageInfo.Id}");
                }
            }
        }

        private StageData LoadStageFromResources(string stageId)
        {
            var resourcePath = $"Stages/stage_{stageId.Replace("-", "_")}";
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
                Debug.LogError($"[StageManager] Failed to parse stage {stageId}: {e.Message}");
                return null;
            }
        }

        public bool SelectStage(string stageId)
        {
            if (!IsStageUnlocked(stageId))
            {
                Debug.LogWarning($"[StageManager] Stage {stageId} is locked");
                return false;
            }

            if (!loadedStages.TryGetValue(stageId, out var stageData))
            {
                Debug.LogWarning($"[StageManager] Stage {stageId} not found");
                return false;
            }

            currentStage = stageData;
            OnStageSelected?.Invoke(currentStage);
            Debug.Log($"[StageManager] Selected stage: {stageId} - {stageData.stageName}");
            return true;
        }

        public bool IsStageUnlocked(string stageId)
        {
            return unlockedStages.Contains(stageId);
        }

        public void UnlockNextStage()
        {
            if (currentStage == null) return;

            string nextStageId = GetNextStageId(currentStage.stageId);
            if (nextStageId != null && !unlockedStages.Contains(nextStageId))
            {
                unlockedStages.Add(nextStageId);
                SaveUnlockedStages();
                OnStageUnlocked?.Invoke(nextStageId);
                Debug.Log($"[StageManager] Unlocked stage: {nextStageId}");
            }
        }

        private string GetNextStageId(string currentId)
        {
            for (int i = 0; i < AllStages.Length - 1; i++)
            {
                if (AllStages[i].Id == currentId)
                {
                    return AllStages[i + 1].Id;
                }
            }
            return null;
        }

        public StageData GetStageData(string stageId)
        {
            return loadedStages.TryGetValue(stageId, out var data) ? data : null;
        }

        public WaveDefinition[] GetCurrentStageWaves()
        {
            return currentStage?.ToWaveDefinitions() ?? Array.Empty<WaveDefinition>();
        }

        public int GetStageIndex(string stageId)
        {
            for (int i = 0; i < AllStages.Length; i++)
            {
                if (AllStages[i].Id == stageId) return i;
            }
            return -1;
        }
    }

    public readonly struct StageInfo
    {
        public readonly string Id;
        public readonly string Name;

        public StageInfo(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
