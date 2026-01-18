using UnityEngine;

namespace ZeroDaySiege.Towers
{
    public class PiercingRail : MonoBehaviour
    {
        private const float FadeDuration = 0.15f;
        private const float RailWidth = 0.08f;

        private LineRenderer lineRenderer;
        private float fadeTimer;
        private Color baseColor;

        public void Initialize(Vector3 start, Vector3 end, Color color, bool isCritical)
        {
            baseColor = isCritical ? new Color(1f, 0.3f, 0.3f, 1f) : color;
            fadeTimer = FadeDuration;

            CreateLineRenderer(start, end);
        }

        private void CreateLineRenderer(Vector3 start, Vector3 end)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);

            lineRenderer.startWidth = RailWidth;
            lineRenderer.endWidth = RailWidth;
            lineRenderer.startColor = baseColor;
            lineRenderer.endColor = baseColor;

            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.sortingOrder = 15;
        }

        private void Update()
        {
            fadeTimer -= Time.deltaTime;

            if (fadeTimer <= 0f)
            {
                Destroy(gameObject);
                return;
            }

            float alpha = fadeTimer / FadeDuration;
            Color fadedColor = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
            lineRenderer.startColor = fadedColor;
            lineRenderer.endColor = fadedColor;
        }
    }
}
