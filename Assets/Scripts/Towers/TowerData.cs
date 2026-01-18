using System.Collections.Generic;
using UnityEngine;

namespace ZeroDaySiege.Towers
{
    public readonly struct TowerStats
    {
        public readonly int Damage;
        public readonly float FireRate;
        public readonly float Range;
        public readonly float ProjectileSpeed;
        public readonly float SplashRadius;
        public readonly float SplashFalloff;
        public readonly string Placeholder;
        public readonly Color Color;

        public TowerStats(
            int damage,
            float fireRate,
            float range,
            float projectileSpeed,
            string placeholder,
            Color color,
            float splashRadius = 0f,
            float splashFalloff = 0f)
        {
            Damage = damage;
            FireRate = fireRate;
            Range = range;
            ProjectileSpeed = projectileSpeed;
            SplashRadius = splashRadius;
            SplashFalloff = splashFalloff;
            Placeholder = placeholder;
            Color = color;
        }

        public bool IsAOE => SplashRadius > 0f;
    }

    public static class TowerData
    {
        private static readonly Dictionary<TowerType, TowerStats> Stats = new()
        {
            {
                TowerType.BaseTower,
                new TowerStats(
                    damage: 50,
                    fireRate: 1.0f,
                    range: 0.9f,
                    projectileSpeed: 10.0f,
                    placeholder: "[T1]",
                    color: new Color(0f, 0.8f, 1f, 1f)
                )
            },
            {
                TowerType.AOETower,
                new TowerStats(
                    damage: 40,
                    fireRate: 1.2f,
                    range: 0.9f,
                    projectileSpeed: 4.0f,
                    placeholder: "[T2]",
                    color: new Color(1f, 0.6f, 0.2f, 1f),
                    splashRadius: 0.12f,
                    splashFalloff: 0.5f
                )
            },
            {
                TowerType.BurstTower,
                new TowerStats(
                    damage: 150,
                    fireRate: 0.33f,
                    range: 0.9f,
                    projectileSpeed: 8.0f,
                    placeholder: "[T3]",
                    color: new Color(0.2f, 0.6f, 1f, 1f)
                )
            },
            {
                TowerType.PiercingTower,
                new TowerStats(
                    damage: 50,
                    fireRate: 1.0f,
                    range: 0.9f,
                    projectileSpeed: float.PositiveInfinity,
                    placeholder: "[T4]",
                    color: new Color(0.7f, 0.3f, 1f, 1f)
                )
            },
            {
                TowerType.BruteForceNode,
                new TowerStats(
                    damage: 18,
                    fireRate: 0.83f,
                    range: 0.85f,
                    projectileSpeed: 8.0f,
                    placeholder: "[T5]",
                    color: new Color(1f, 0.5f, 0.2f, 1f)
                )
            }
        };

        public static TowerStats GetStats(TowerType type)
        {
            return Stats.TryGetValue(type, out var stats) ? stats : Stats[TowerType.BaseTower];
        }

        public static float ConvertRangeToWorld(float normalizedRange, float spawnY, float firewallY)
        {
            return normalizedRange * Mathf.Abs(spawnY - firewallY);
        }
    }
}
