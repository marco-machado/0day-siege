using UnityEngine;
using UnityEngine.InputSystem;

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
        }
#endif
    }
}
