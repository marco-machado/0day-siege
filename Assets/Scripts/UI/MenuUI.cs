using UnityEngine;
using UnityEngine.UI;
using ZeroDaySiege.Core;

namespace ZeroDaySiege.UI
{
    public class MenuUI : MonoBehaviour
    {
        private GameObject menuContainer;
        private Button startButton;
        private StageSelectUI stageSelectUI;

        public void SetReferences(GameObject container, Button start)
        {
            menuContainer = container;
            startButton = start;

            startButton.onClick.AddListener(OnStartClicked);
        }

        public void SetStageSelectUI(StageSelectUI stageSelect)
        {
            stageSelectUI = stageSelect;
        }

        private void OnEnable()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += HandleStateChanged;
                UpdateVisibility(GameManager.Instance.CurrentState);
            }
        }

        private void OnDisable()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= HandleStateChanged;
            }
        }

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                UpdateVisibility(GameManager.Instance.CurrentState);
            }
        }

        private void HandleStateChanged(GameState previousState, GameState newState)
        {
            UpdateVisibility(newState);
        }

        private void UpdateVisibility(GameState state)
        {
            if (menuContainer == null) return;

            bool shouldShow = state == GameState.Menu;
            menuContainer.SetActive(shouldShow);
        }

        public void ShowMenu()
        {
            if (menuContainer != null)
            {
                menuContainer.SetActive(true);
            }
        }

        public void HideMenu()
        {
            if (menuContainer != null)
            {
                menuContainer.SetActive(false);
            }
        }

        private void OnStartClicked()
        {
            HideMenu();
            stageSelectUI?.Show();
        }
    }
}
