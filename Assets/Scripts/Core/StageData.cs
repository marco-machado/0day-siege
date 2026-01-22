using System;
using UnityEngine;
using ZeroDaySiege.Enemies;

namespace ZeroDaySiege.Core
{
    [Serializable]
    public class RewardItem
    {
        public string itemId;
        public int amount;
    }

    [Serializable]
    public class RewardBundle
    {
        public int shards;
        public int keys;
        public int xp;
        public RewardItem[] items;
    }

    [Serializable]
    public class StageRewards
    {
        public RewardBundle completion;
        public RewardBundle firstClear;
        public RewardBundle firstHalfHP;
        public RewardBundle firstFullHP;
        public float hardMultiplier;
    }

    [Serializable]
    public class StageData
    {
        public int chapter;
        public int stageId;
        public string stageName;
        public StageRewards rewards;
        public StageWaveData[] waves;

        public string GetDisplayId() => $"{chapter}-{stageId}";
        public string GetStorageKey() => $"{chapter}_{stageId}";

        public WaveDefinition[] ToWaveDefinitions()
        {
            if (waves == null) return Array.Empty<WaveDefinition>();

            var definitions = new WaveDefinition[waves.Length];
            for (int i = 0; i < waves.Length; i++)
            {
                definitions[i] = waves[i].ToWaveDefinition();
            }
            return definitions;
        }

        public static StageData FromJson(string json)
        {
            return JsonUtility.FromJson<StageData>(json);
        }
    }

    [Serializable]
    public class StageWaveData
    {
        public int waveNumber;
        public bool isBoss;
        public StageEnemySpawn[] enemies;

        public WaveDefinition ToWaveDefinition()
        {
            var spawns = new EnemySpawn[enemies?.Length ?? 0];
            for (int i = 0; i < spawns.Length; i++)
            {
                spawns[i] = enemies[i].ToEnemySpawn();
            }
            return new WaveDefinition(waveNumber, isBoss, spawns);
        }
    }

    [Serializable]
    public class StageEnemySpawn
    {
        public string enemyType;
        public float spawnX;
        public float spawnTime;

        public EnemySpawn ToEnemySpawn()
        {
            var type = ParseEnemyType(enemyType);
            return new EnemySpawn(type, spawnX, spawnTime);
        }

        private static EnemyType ParseEnemyType(string typeName)
        {
            return typeName?.ToLowerInvariant() switch
            {
                "virus" or "basic" => EnemyType.Virus,
                "worm" or "fast" => EnemyType.Worm,
                "ransomware" or "boss" => EnemyType.Ransomware,
                _ => EnemyType.Virus
            };
        }
    }
}
