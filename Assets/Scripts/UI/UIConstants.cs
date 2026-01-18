using UnityEngine;

namespace ZeroDaySiege.UI
{
    public static class UIConstants
    {
        public static readonly Vector2 ReferenceResolution = new(1080, 1920);
        public const float CanvasMatchWidthOrHeight = 0.5f;
        public const int RunCanvasSortOrder = 100;

        public static readonly Vector2 WaveContainerPosition = new(0, -50);
        public static readonly Vector2 WaveContainerSize = new(400, 80);
        public const int WaveTextFontSize = 48;

        public static readonly Vector2 PauseButtonPosition = new(-30, -50);
        public static readonly Vector2 PauseButtonSize = new(80, 80);
        public const int PauseButtonFontSize = 36;
        public static readonly Color PauseButtonColor = new(0.2f, 0.2f, 0.2f, 0.8f);

        public static readonly Vector2 PausePanelSize = new(500, 450);
        public static readonly Vector2 PauseTitlePosition = new(0, -40);
        public static readonly Vector2 PauseTitleSize = new(400, 60);
        public const int PauseTitleFontSize = 48;

        public static readonly Vector2 MenuButtonSize = new(300, 70);
        public const int MenuButtonFontSize = 32;
        public static readonly Vector2 ResumeButtonPosition = new(0, 20);
        public static readonly Vector2 RestartButtonPosition = new(0, -70);
        public static readonly Vector2 QuitButtonPosition = new(0, -160);

        public static readonly Vector2 DialogPanelSize = new(450, 300);
        public static readonly Vector2 DialogTitlePosition = new(0, -30);
        public static readonly Vector2 DialogTitleSize = new(400, 50);
        public static readonly Vector2 DialogMessagePosition = new(0, 20);
        public static readonly Vector2 DialogMessageSize = new(380, 60);
        public static readonly Vector2 DialogButtonSize = new(120, 55);
        public static readonly Vector2 DialogYesButtonPosition = new(-80, -100);
        public static readonly Vector2 DialogNoButtonPosition = new(80, -100);
        public const int DialogTitleFontSize = 36;
        public const int DialogMessageFontSize = 28;
        public const int DialogButtonFontSize = 26;

        public static readonly Color OverlayColor = new(0, 0, 0, 0.7f);
        public static readonly Color DialogOverlayColor = new(0, 0, 0, 0.8f);
        public static readonly Color PanelColor = new(0.15f, 0.15f, 0.15f, 0.95f);
        public static readonly Color DialogPanelColor = new(0.2f, 0.2f, 0.2f, 0.98f);
        public static readonly Color ButtonColor = new(0.3f, 0.3f, 0.3f, 1f);
        public static readonly Color ButtonHighlightColor = new(0.4f, 0.4f, 0.4f, 1f);
        public static readonly Color ButtonPressedColor = new(0.25f, 0.25f, 0.25f, 1f);
        public static readonly Color DialogButtonColor = new(0.35f, 0.35f, 0.35f, 1f);
        public static readonly Color DialogButtonHighlightColor = new(0.45f, 0.45f, 0.45f, 1f);
        public static readonly Color DialogButtonPressedColor = new(0.3f, 0.3f, 0.3f, 1f);
        public static readonly Color MessageTextColor = new(0.8f, 0.8f, 0.8f, 1f);

        public static readonly Color VignetteColor = new(0.8f, 0f, 0f, 0.5f);

        public static readonly Vector2 ScoreContainerPosition = new(0, -130);
        public static readonly Vector2 ScoreContainerSize = new(300, 50);
        public const int ScoreTextFontSize = 36;

        public static readonly Vector2 MenuTitlePosition = new(0, 200);
        public static readonly Vector2 MenuTitleSize = new(600, 120);
        public const int MenuTitleFontSize = 72;
        public static readonly Vector2 MenuStartButtonPosition = new(0, -50);
        public static readonly Vector2 MenuStartButtonSize = new(350, 80);
        public const int MenuStartButtonFontSize = 40;
        public static readonly Color MenuBackgroundColor = new(0.05f, 0.05f, 0.1f, 1f);
        public static readonly Color MenuTitleColor = new(0f, 0.8f, 1f, 1f);

        public static readonly Color CardSelectionOverlayColor = new(0, 0, 0, 0.9f);
        public static readonly Vector2 CardSelectionTitlePosition = new(0, -80);
        public static readonly Vector2 CardSelectionTitleSize = new(600, 60);
        public const int CardSelectionTitleFontSize = 42;

        public static readonly Vector2 CardSize = new(240, 340);
        public const float CardSpacing = 30f;
        public static readonly Vector2 CardContainerPosition = new(0, 20);
        public const int CardTitleFontSize = 26;
        public const int CardDescriptionFontSize = 20;
        public const int CardDetailsFontSize = 18;
        public static readonly Color CardBackgroundColor = new(0.15f, 0.15f, 0.2f, 1f);
        public static readonly Color CardBorderColor = new(0.4f, 0.4f, 0.5f, 1f);
        public static readonly Color CardHoverColor = new(0.25f, 0.25f, 0.3f, 1f);
        public static readonly Color PlaceTowerCardColor = new(0.15f, 0.35f, 0.55f, 1f);
        public static readonly Color UpgradeCardColor = new(0.45f, 0.25f, 0.5f, 1f);
        public static readonly Color WallRepairCardColor = new(0.2f, 0.45f, 0.3f, 1f);

        public static readonly Vector2 RerollButtonSize = new(280, 60);
        public static readonly Vector2 RerollButtonPosition = new(0, 280);
        public const int RerollButtonFontSize = 24;
        public static readonly Color RerollButtonColor = new(0.25f, 0.25f, 0.3f, 1f);
        public static readonly Color RerollButtonDisabledColor = new(0.15f, 0.15f, 0.15f, 0.6f);

        public static readonly Vector2 KeyDisplayPosition = new(380, -80);
        public static readonly Vector2 KeyDisplaySize = new(100, 50);
        public const int KeyDisplayFontSize = 32;
        public static readonly Color KeyDisplayColor = new(1f, 0.85f, 0.2f, 1f);

        public static readonly Vector2 SlotModalSize = new(700, 280);
        public static readonly Vector2 SlotModalPosition = new(0, 0);
        public static readonly Vector2 SlotButtonSize = new(100, 100);
        public const float SlotButtonSpacing = 20f;
        public const int SlotButtonFontSize = 24;
        public static readonly Vector2 SlotModalTitlePosition = new(0, -30);
        public static readonly Vector2 SlotModalTitleSize = new(600, 50);
        public const int SlotModalTitleFontSize = 32;
        public static readonly Vector2 SlotCancelButtonPosition = new(0, 90);
        public static readonly Vector2 SlotCancelButtonSize = new(160, 50);
        public static readonly Color SlotButtonOccupiedColor = new(0.3f, 0.15f, 0.15f, 0.7f);
        public static readonly Color SlotButtonAvailableColor = new(0.2f, 0.4f, 0.2f, 1f);

        public static readonly Vector2 GameOverPanelSize = new(550, 500);
        public static readonly Vector2 GameOverTitlePosition = new(0, -50);
        public static readonly Vector2 GameOverTitleSize = new(500, 80);
        public const int GameOverTitleFontSize = 56;
        public static readonly Vector2 GameOverStatsPosition = new(0, 20);
        public static readonly Vector2 GameOverStatsSize = new(450, 200);
        public const int GameOverStatsFontSize = 32;
        public static readonly Vector2 GameOverButtonPosition1 = new(-100, -180);
        public static readonly Vector2 GameOverButtonPosition2 = new(100, -180);
        public static readonly Color VictoryColor = new(0f, 0.8f, 1f, 1f);
        public static readonly Color DefeatColor = new(1f, 0.2f, 0.2f, 1f);

        public static readonly Vector2 StageSelectTitlePosition = new(0, 350);
        public static readonly Vector2 StageSelectTitleSize = new(500, 80);
        public const int StageSelectTitleFontSize = 52;
        public static readonly Vector2 StageButtonSize = new(180, 120);
        public const float StageButtonSpacing = 20f;
        public const int StageButtonFontSize = 22;
        public static readonly Color StageButtonColor = new(0.2f, 0.3f, 0.4f, 1f);
        public static readonly Color StageButtonSelectedColor = new(0f, 0.5f, 0.7f, 1f);
        public static readonly Color StageButtonLockedColor = new(0.15f, 0.15f, 0.15f, 0.8f);
        public static readonly Vector2 SelectedStageTextPosition = new(0, -150);
        public static readonly Vector2 SelectedStageTextSize = new(600, 50);
        public const int SelectedStageTextFontSize = 28;
        public static readonly Vector2 StageStartButtonPosition = new(0, -250);
        public static readonly Vector2 StageBackButtonPosition = new(0, -340);
    }
}
