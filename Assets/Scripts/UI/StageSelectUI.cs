using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZeroDaySiege.Core;

namespace ZeroDaySiege.UI
{
    public class StageSelectUI : MonoBehaviour
    {
        private GameObject screenContainer;
        private Button[] stageButtons;
        private TextMeshProUGUI[] stageLabels;
        private TextMeshProUGUI selectedStageText;
        private Button startButton;
        private Button backButton;

        private int selectedIndex = 0;

        public void SetReferences(GameObject container, Button[] buttons, TextMeshProUGUI[] labels,
                                  TextMeshProUGUI selectedText, Button start, Button back)
        {
            screenContainer = container;
            stageButtons = buttons;
            stageLabels = labels;
            selectedStageText = selectedText;
            startButton = start;
            backButton = back;

            for (int i = 0; i < stageButtons.Length; i++)
            {
                int index = i;
                stageButtons[i].onClick.AddListener(() => OnStageButtonClicked(index));
            }

            startButton.onClick.AddListener(OnStartClicked);
            backButton.onClick.AddListener(OnBackClicked);
        }

        private void OnEnable()
        {
            if (StageManager.Instance != null)
            {
                StageManager.Instance.OnStageUnlocked += HandleStageUnlocked;
            }
        }

        private void OnDisable()
        {
            if (StageManager.Instance != null)
            {
                StageManager.Instance.OnStageUnlocked -= HandleStageUnlocked;
            }
        }

        public void Show()
        {
            if (screenContainer == null) return;

            UpdateStageButtons();
            SelectFirstUnlockedStage();
            screenContainer.SetActive(true);
        }

        public void Hide()
        {
            if (screenContainer != null)
            {
                screenContainer.SetActive(false);
            }
        }

        private void HandleStageUnlocked(int chapter, int stageId)
        {
            UpdateStageButtons();
        }

        private void UpdateStageButtons()
        {
            if (StageManager.Instance == null) return;

            for (int i = 0; i < StageManager.AllStages.Length && i < stageButtons.Length; i++)
            {
                var stageInfo = StageManager.AllStages[i];
                bool isUnlocked = StageManager.Instance.IsStageUnlocked(stageInfo.Chapter, stageInfo.Id);

                stageButtons[i].interactable = isUnlocked;

                if (stageLabels[i] != null)
                {
                    string displayId = stageInfo.GetDisplayId();
                    string displayText = isUnlocked
                        ? $"{displayId}\n{stageInfo.Name}"
                        : $"{displayId}\n[LOCKED]";
                    stageLabels[i].text = displayText;
                    stageLabels[i].color = isUnlocked ? Color.white : new Color(0.5f, 0.5f, 0.5f, 1f);
                }
            }
        }

        private void SelectFirstUnlockedStage()
        {
            if (StageManager.Instance == null) return;

            for (int i = 0; i < StageManager.AllStages.Length; i++)
            {
                var stageInfo = StageManager.AllStages[i];
                if (StageManager.Instance.IsStageUnlocked(stageInfo.Chapter, stageInfo.Id))
                {
                    SelectStage(i);
                    return;
                }
            }
        }

        private void OnStageButtonClicked(int index)
        {
            if (index < 0 || index >= StageManager.AllStages.Length) return;

            var stageInfo = StageManager.AllStages[index];
            if (StageManager.Instance != null && StageManager.Instance.IsStageUnlocked(stageInfo.Chapter, stageInfo.Id))
            {
                SelectStage(index);
            }
        }

        private void SelectStage(int index)
        {
            if (index < 0 || index >= StageManager.AllStages.Length) return;

            selectedIndex = index;
            var stageInfo = StageManager.AllStages[index];

            if (StageManager.Instance != null)
            {
                StageManager.Instance.SelectStage(stageInfo.Chapter, stageInfo.Id);
            }

            UpdateSelectedStageDisplay();
            UpdateButtonHighlights();
        }

        private void UpdateSelectedStageDisplay()
        {
            if (selectedStageText == null || StageManager.Instance == null) return;

            var stage = StageManager.Instance.CurrentStage;
            if (stage != null)
            {
                selectedStageText.text = $"Selected: {stage.GetDisplayId()} - {stage.stageName}";
            }
        }

        private void UpdateButtonHighlights()
        {
            for (int i = 0; i < stageButtons.Length; i++)
            {
                var image = stageButtons[i].GetComponent<Image>();
                if (image != null)
                {
                    bool isSelected = i == selectedIndex;
                    image.color = isSelected
                        ? UIConstants.StageButtonSelectedColor
                        : UIConstants.StageButtonColor;
                }
            }
        }

        private void OnStartClicked()
        {
            Hide();
            GameManager.Instance?.StartRun();
        }

        private void OnBackClicked()
        {
            Hide();
            var menuUI = FindAnyObjectByType<MenuUI>();
            menuUI?.ShowMenu();
        }
    }
}
