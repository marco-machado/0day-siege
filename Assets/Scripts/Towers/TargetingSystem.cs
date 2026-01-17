using System.Collections.Generic;
using UnityEngine;
using ZeroDaySiege.Enemies;

namespace ZeroDaySiege.Towers
{
    public static class TargetingSystem
    {
        public static Enemy FindTarget(Vector3 towerPosition, float worldRange, IReadOnlyList<Enemy> enemies)
        {
            if (enemies == null || enemies.Count == 0)
                return null;

            Enemy bestTarget = null;
            int bestPriority = int.MinValue;

            foreach (var enemy in enemies)
            {
                if (enemy == null || !enemy.IsAlive)
                    continue;

                float distance = Vector3.Distance(towerPosition, enemy.transform.position);
                if (distance > worldRange)
                    continue;

                int priority = CalculatePriority(enemy);

                if (bestTarget == null || priority > bestPriority)
                {
                    bestTarget = enemy;
                    bestPriority = priority;
                }
                else if (priority == bestPriority)
                {
                    bestTarget = BreakTie(bestTarget, enemy);
                }
            }

            return bestTarget;
        }

        private static int CalculatePriority(Enemy enemy)
        {
            int priority = 0;

            if (enemy.State == EnemyState.Attacking)
                priority += 10000;

            if (enemy.Type == EnemyType.Ransomware)
                priority += 1000;

            return priority;
        }

        private static Enemy BreakTie(Enemy current, Enemy candidate)
        {
            float currentY = current.transform.position.y;
            float candidateY = candidate.transform.position.y;

            if (candidateY < currentY)
                return candidate;
            if (currentY < candidateY)
                return current;

            if (candidate.CurrentHP > current.CurrentHP)
                return candidate;
            if (current.CurrentHP > candidate.CurrentHP)
                return current;

            if (candidate.SpawnOrder < current.SpawnOrder)
                return candidate;

            return current;
        }
    }
}
