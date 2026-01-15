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
    }
}
