using UnityEngine;
using UnityEngine.UI;

namespace ZeroDaySiege.Enemies
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [Header("Layout")]
        [SerializeField] private Vector2 size = new(1f, 0.12f);
        [SerializeField] private Vector3 offset = new(0f, 0.6f, 0f);

        [Header("Colors")]
        [SerializeField] private Color backgroundColor = new(0.2f, 0.2f, 0.2f, 0.8f);
        [SerializeField] private Color healthyColor = new(0.2f, 0.8f, 0.2f, 1f);
        [SerializeField] private Color damagedColor = new(0.9f, 0.7f, 0.1f, 1f);
        [SerializeField] private Color criticalColor = new(0.9f, 0.2f, 0.2f, 1f);

        private Enemy enemy;
        private Canvas canvas;
        private Image backgroundImage;
        private Image fillImage;

        public void Initialize(Enemy targetEnemy)
        {
            enemy = targetEnemy;
            CreateHealthBarUI();

            enemy.OnHPChanged += UpdateHealthBar;
            UpdateHealthBar(enemy.CurrentHP, enemy.MaxHP);
        }

        private void OnDestroy()
        {
            if (enemy != null)
            {
                enemy.OnHPChanged -= UpdateHealthBar;
            }
        }

        private void CreateHealthBarUI()
        {
            float barSize = enemy.Type == EnemyType.Ransomware ? 1.5f : 1f;
            Vector2 scaledSize = size * barSize;
            Vector3 scaledOffset = offset * barSize;

            canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.sortingOrder = 20;

            var canvasScaler = gameObject.AddComponent<CanvasScaler>();
            canvasScaler.dynamicPixelsPerUnit = 100;

            var rectTransform = GetComponent<RectTransform>();
            rectTransform.localPosition = scaledOffset;
            rectTransform.sizeDelta = scaledSize;
            rectTransform.localScale = Vector3.one * 0.01f;

            var backgroundGO = new GameObject("Background");
            backgroundGO.transform.SetParent(transform, false);
            backgroundImage = backgroundGO.AddComponent<Image>();
            backgroundImage.color = backgroundColor;

            var bgRect = backgroundGO.GetComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;

            var fillGO = new GameObject("Fill");
            fillGO.transform.SetParent(transform, false);
            fillImage = fillGO.AddComponent<Image>();
            fillImage.color = healthyColor;

            var fillRect = fillGO.GetComponent<RectTransform>();
            fillRect.anchorMin = Vector2.zero;
            fillRect.anchorMax = Vector2.one;
            fillRect.pivot = new Vector2(0f, 0.5f);
            fillRect.offsetMin = new Vector2(1f, 1f);
            fillRect.offsetMax = new Vector2(-1f, -1f);
        }

        private void UpdateHealthBar(int current, int max)
        {
            if (fillImage == null) return;

            float percent = max > 0 ? (float)current / max : 0f;

            var rect = fillImage.GetComponent<RectTransform>();
            rect.anchorMax = new Vector2(percent, 1f);

            fillImage.color = GetColorForPercent(percent);
        }

        private Color GetColorForPercent(float percent)
        {
            if (percent <= 0.25f) return criticalColor;
            if (percent <= 0.50f) return damagedColor;
            return healthyColor;
        }
    }
}
