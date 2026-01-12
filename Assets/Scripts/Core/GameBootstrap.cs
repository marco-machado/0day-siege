using UnityEngine;

namespace ZeroDaySiege.Core
{
    public class GameBootstrap : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var bootstrapGO = new GameObject("[GameBootstrap]");
            bootstrapGO.AddComponent<GameBootstrap>();
            DontDestroyOnLoad(bootstrapGO);
        }

        private void Start()
        {
            SetupScreenController();
            SetupGameManager();
            SetupGameLayout();
        }

        private void SetupScreenController()
        {
            var screenGO = new GameObject("[ScreenController]");
            screenGO.AddComponent<ScreenController>();
            DontDestroyOnLoad(screenGO);
        }

        private void SetupGameManager()
        {
            if (GameManager.Instance != null) return;

            var managerGO = new GameObject("[GameManager]");
            managerGO.AddComponent<GameManager>();
        }

        private void SetupGameLayout()
        {
            if (GameLayout.Instance != null) return;

            var layoutGO = new GameObject("[GameLayout]");
            layoutGO.AddComponent<GameLayout>();
            layoutGO.AddComponent<PlaceholderVisuals>();
        }
    }
}
