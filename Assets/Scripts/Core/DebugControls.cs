using UnityEngine;
using UnityEngine.InputSystem;
using ZeroDaySiege.Enemies;
using ZeroDaySiege.Firewall;
using ZeroDaySiege.Towers;

namespace ZeroDaySiege.Core
{
    public class DebugControls : MonoBehaviour
    {
        private void Update()
        {
            var keyboard = Keyboard.current;
            if (keyboard == null) return;

            HandlePauseInput(keyboard);

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            HandleDebugInput(keyboard);
#endif
        }

        private void HandlePauseInput(Keyboard keyboard)
        {
            if (!keyboard.escapeKey.wasPressedThisFrame) return;

            var state = GameManager.Instance?.CurrentState;
            if (state == null) return;

            switch (state)
            {
                case GameState.Playing:
                    GameManager.Instance.PauseGame();
                    break;
                case GameState.Paused:
                    GameManager.Instance.ResumeGame();
                    break;
            }
        }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        private void HandleDebugInput(Keyboard keyboard)
        {
            if (keyboard.f1Key.wasPressedThisFrame)
            {
                Debug.Log("[Debug] Starting run...");
                GameManager.Instance.StartRun();
            }

            if (keyboard.f2Key.wasPressedThisFrame)
            {
                Debug.Log($"[Debug] Advancing wave... (current: {GameManager.Instance.CurrentWave})");
                GameManager.Instance.AdvanceWave();
            }

            if (keyboard.f3Key.wasPressedThisFrame)
            {
                Debug.Log("[Debug] Triggering defeat...");
                GameManager.Instance.EndRun(false);
            }

            if (keyboard.f4Key.wasPressedThisFrame)
            {
                Debug.Log("[Debug] Returning to menu...");
                GameManager.Instance.ReturnToMenu();
            }

            if (keyboard.f5Key.wasPressedThisFrame)
            {
                Debug.Log($"[Debug] Wave {GameManager.Instance.CurrentWave} spawning complete...");
                WaveManager.Instance?.CompleteCurrentWave();
            }

            if (keyboard.f6Key.wasPressedThisFrame)
            {
                if (Firewall.Firewall.Instance != null)
                {
                    Debug.Log($"[Debug] Damaging firewall by 200 (current: {Firewall.Firewall.Instance.CurrentHP})");
                    Firewall.Firewall.Instance.TakeDamage(200);
                }
            }

            if (keyboard.f7Key.wasPressedThisFrame)
            {
                if (Firewall.Firewall.Instance != null)
                {
                    Debug.Log($"[Debug] Healing firewall by 30% (current: {Firewall.Firewall.Instance.CurrentHP})");
                    Firewall.Firewall.Instance.HealPercent(0.30f);
                }
            }

            if (keyboard.f8Key.wasPressedThisFrame)
            {
                float randomX = Random.value;
                int wave = GameManager.Instance?.CurrentWave ?? 1;
                Debug.Log($"[Debug] Spawning Virus at X={randomX:F2}, wave {wave}");
                EnemyManager.Instance?.SpawnEnemy(EnemyType.Virus, randomX, wave);
            }

            if (keyboard.f9Key.wasPressedThisFrame)
            {
                float randomX = Random.value;
                int wave = GameManager.Instance?.CurrentWave ?? 1;
                Debug.Log($"[Debug] Spawning Worm at X={randomX:F2}, wave {wave}");
                EnemyManager.Instance?.SpawnEnemy(EnemyType.Worm, randomX, wave);
            }

            if (keyboard.f10Key.wasPressedThisFrame)
            {
                int wave = GameManager.Instance?.CurrentWave ?? 1;
                Debug.Log($"[Debug] Spawning Ransomware at center, wave {wave}");
                EnemyManager.Instance?.SpawnEnemy(EnemyType.Ransomware, 0.5f, wave);
            }

            if (keyboard.tKey.wasPressedThisFrame && !keyboard.shiftKey.isPressed)
            {
                var towerManager = TowerManager.Instance;
                if (towerManager != null)
                {
                    int nextSlot = towerManager.GetNextEmptySlot();
                    if (nextSlot >= 0)
                    {
                        Debug.Log($"[Debug] Placing BaseTower in slot {nextSlot}");
                        towerManager.PlaceTower(nextSlot, TowerType.BaseTower);
                    }
                    else
                    {
                        Debug.Log("[Debug] All tower slots are occupied");
                    }
                }
            }

            if (keyboard.tKey.wasPressedThisFrame && keyboard.shiftKey.isPressed)
            {
                var towerManager = TowerManager.Instance;
                if (towerManager != null)
                {
                    Debug.Log("[Debug] Clearing all towers");
                    towerManager.ClearAllTowers();
                }
            }

            if (keyboard.digit1Key.wasPressedThisFrame)
            {
                PlaceTowerOfType(TowerType.BaseTower);
            }

            if (keyboard.digit2Key.wasPressedThisFrame)
            {
                PlaceTowerOfType(TowerType.AOETower);
            }

            if (keyboard.digit3Key.wasPressedThisFrame)
            {
                PlaceTowerOfType(TowerType.BurstTower);
            }

            if (keyboard.digit4Key.wasPressedThisFrame)
            {
                PlaceTowerOfType(TowerType.PiercingTower);
            }

            if (keyboard.digit5Key.wasPressedThisFrame)
            {
                PlaceTowerOfType(TowerType.BruteForceNode);
            }
        }

        private void PlaceTowerOfType(TowerType type)
        {
            var towerManager = TowerManager.Instance;
            if (towerManager == null) return;

            int nextSlot = towerManager.GetNextEmptySlot();
            if (nextSlot >= 0)
            {
                Debug.Log($"[Debug] Placing {type} in slot {nextSlot}");
                towerManager.PlaceTower(nextSlot, type);
            }
            else
            {
                Debug.Log($"[Debug] All tower slots are occupied, cannot place {type}");
            }
        }
#endif
    }
}
