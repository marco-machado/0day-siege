using UnityEngine;

namespace ZeroDaySiege.Core
{
    public class ScreenController : MonoBehaviour
    {
        [Header("Target Aspect Ratio")]
        [SerializeField] private float targetAspectWidth = 9f;
        [SerializeField] private float targetAspectHeight = 16f;

        [Header("Pillarbox/Letterbox")]
        [SerializeField] private Color barColor = Color.black;

        [Header("Desktop Settings")]
        [SerializeField] private bool forceWindowedOnDesktop = false;
        [SerializeField] private int desktopWindowWidth = 608;
        [SerializeField] private int desktopWindowHeight = 1080;

        private Camera mainCamera;
        private Camera barCamera;

        private float TargetAspect => targetAspectWidth / targetAspectHeight;

        private void Awake()
        {
            mainCamera = Camera.main;
            SetupPlatform();
            SetupLetterboxing();
        }

        private void SetupPlatform()
        {
#if UNITY_IOS || UNITY_ANDROID
            Screen.orientation = ScreenOrientation.Portrait;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
#else
            if (forceWindowedOnDesktop && !Application.isEditor)
            {
                Screen.SetResolution(desktopWindowWidth, desktopWindowHeight, false);
            }
#endif
        }

        private void SetupLetterboxing()
        {
            CreateBarCamera();
            UpdateViewport();
        }

        private void CreateBarCamera()
        {
            var barCamGO = new GameObject("LetterboxCamera");
            barCamGO.transform.SetParent(transform);

            barCamera = barCamGO.AddComponent<Camera>();
            barCamera.depth = -100;
            barCamera.cullingMask = 0;
            barCamera.clearFlags = CameraClearFlags.SolidColor;
            barCamera.backgroundColor = barColor;
            barCamera.orthographic = true;
        }

        private void UpdateViewport()
        {
            if (mainCamera == null) return;

            float currentAspect = (float)Screen.width / Screen.height;
            float scaleHeight = currentAspect / TargetAspect;

            Rect viewportRect;

            if (scaleHeight < 1f)
            {
                viewportRect = new Rect(0, (1f - scaleHeight) / 2f, 1f, scaleHeight);
            }
            else
            {
                float scaleWidth = 1f / scaleHeight;
                viewportRect = new Rect((1f - scaleWidth) / 2f, 0, scaleWidth, 1f);
            }

            mainCamera.rect = viewportRect;
        }

        private void Update()
        {
#if UNITY_EDITOR
            UpdateViewport();
#endif
        }

        private void OnRectTransformDimensionsChange()
        {
            UpdateViewport();
        }

        private void OnValidate()
        {
            if (Application.isPlaying && mainCamera != null)
            {
                UpdateViewport();
            }
        }
    }
}
