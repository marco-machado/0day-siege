using System;
using UnityEngine;

namespace ZeroDaySiege.Core
{
    public enum Difficulty { Normal, Hard }

    [Serializable]
    public class StageProgress
    {
        private const float FullHPThreshold = 0.999f;

        public bool normalCleared;
        public bool normalHalfHP;
        public bool normalFullHP;
        public bool hardCleared;
        public bool hardHalfHP;
        public bool hardFullHP;

        public bool IsCleared(Difficulty difficulty) =>
            difficulty == Difficulty.Normal ? normalCleared : hardCleared;

        public bool IsHalfHP(Difficulty difficulty) =>
            difficulty == Difficulty.Normal ? normalHalfHP : hardHalfHP;

        public bool IsFullHP(Difficulty difficulty) =>
            difficulty == Difficulty.Normal ? normalFullHP : hardFullHP;

        public void MarkCleared(Difficulty difficulty, float hpPercent)
        {
            if (difficulty == Difficulty.Normal)
            {
                normalCleared = true;
                if (hpPercent >= 0.5f) normalHalfHP = true;
                if (hpPercent >= FullHPThreshold) normalFullHP = true;
            }
            else
            {
                hardCleared = true;
                if (hpPercent >= 0.5f) hardHalfHP = true;
                if (hpPercent >= FullHPThreshold) hardFullHP = true;
            }
        }
    }

    public static class StageProgressStorage
    {
        private const string KeyPrefix = "StageProgress_";

        public static StageProgress Load(int chapter, int stageId)
        {
            var key = $"{KeyPrefix}{chapter}_{stageId}";
            var json = PlayerPrefs.GetString(key, "{}");
            return JsonUtility.FromJson<StageProgress>(json) ?? new StageProgress();
        }

        public static void Save(int chapter, int stageId, StageProgress progress)
        {
            var key = $"{KeyPrefix}{chapter}_{stageId}";
            var json = JsonUtility.ToJson(progress);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }

        public static void ClearAll()
        {
            foreach (var stage in StageManager.AllStages)
            {
                PlayerPrefs.DeleteKey($"{KeyPrefix}{stage.Chapter}_{stage.Id}");
            }
            PlayerPrefs.Save();
        }
    }
}
