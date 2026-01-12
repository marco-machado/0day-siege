using UnityEngine;
using UnityEngine.InputSystem;

namespace ZeroDaySiege.Core
{
    public class DebugControls : MonoBehaviour
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        private void Update()
        {
            var keyboard = Keyboard.current;
            if (keyboard == null) return;

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
        }
#endif
    }
}
