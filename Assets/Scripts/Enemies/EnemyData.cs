using UnityEngine;

namespace ZeroDaySiege.Enemies
{
    public readonly struct EnemyStats
    {
        public readonly int BaseHP;
        public readonly float Speed;
        public readonly int WallDamage;
        public readonly float AttackCooldown;
        public readonly string DisplayName;
        public readonly string Placeholder;
        public readonly int ScoreValue;

        public EnemyStats(int baseHP, float speed, int wallDamage, float attackCooldown,
            string displayName, string placeholder, int scoreValue)
        {
            BaseHP = baseHP;
            Speed = speed;
            WallDamage = wallDamage;
            AttackCooldown = attackCooldown;
            DisplayName = displayName;
            Placeholder = placeholder;
            ScoreValue = scoreValue;
        }
    }

    public static class EnemyData
    {
        public static readonly EnemyStats Virus = new(
            baseHP: 100,
            speed: 0.2f,
            wallDamage: 15,
            attackCooldown: 1.0f,
            displayName: "Virus",
            placeholder: "[V]",
            scoreValue: 10
        );

        public static readonly EnemyStats Worm = new(
            baseHP: 60,
            speed: 0.3f,
            wallDamage: 10,
            attackCooldown: 0.8f,
            displayName: "Worm",
            placeholder: "[W]",
            scoreValue: 15
        );

        public static readonly EnemyStats Ransomware = new(
            baseHP: 500,
            speed: 0.2f,
            wallDamage: 100,
            attackCooldown: 2.0f,
            displayName: "Ransomware",
            placeholder: "[R]",
            scoreValue: 100
        );

        public static EnemyStats GetStats(EnemyType type)
        {
            return type switch
            {
                EnemyType.Virus => Virus,
                EnemyType.Worm => Worm,
                EnemyType.Ransomware => Ransomware,
                _ => Virus
            };
        }

        public static int CalculateScaledHP(EnemyType type, int wave, float difficultyMultiplier = 1f)
        {
            var stats = GetStats(type);
            float scaledHP = stats.BaseHP * difficultyMultiplier * (1f + (wave - 1) * 0.10f);
            return Mathf.RoundToInt(scaledHP);
        }
    }
}
