using System;
using UnityEngine;
using ZeroDaySiege.Enemies;

namespace ZeroDaySiege.Core
{
    [Serializable]
    public class StageData
    {
        public string stageId;
        public string stageName;
        public StageRewards rewards;
        public StageWaveData[] waves;

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
    public class StageRewards
    {
        public int normal;
        public int hard;
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
