using TMPro;
using UnityEngine;

namespace ZeroDaySiege.UI
{
    public class DamageNumber : MonoBehaviour
    {
        private TextMeshPro text;
        private float lifetime;
        private float elapsed;
        private Vector3 velocity;
        private Color baseColor;

        private const float DefaultLifetime = 0.6f;
        private const float FloatSpeed = 2f;
        private const float SpreadRange = 0.3f;

        public bool IsActive { get; private set; }

        public void Initialize()
        {
            var textGO = new GameObject("Text");
            textGO.transform.SetParent(transform);
            textGO.transform.localPosition = Vector3.zero;

            text = textGO.AddComponent<TextMeshPro>();
            text.fontSize = 4f;
            text.alignment = TextAlignmentOptions.Center;
            text.sortingOrder = 100;

            var rect = text.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(3f, 1f);

            gameObject.SetActive(false);
        }

        public void Show(Vector3 position, int damage, bool isCrit)
        {
            transform.position = position + new Vector3(
                Random.Range(-SpreadRange, SpreadRange),
                Random.Range(0f, SpreadRange),
                0f
            );

            baseColor = isCrit ? new Color(1f, 0.3f, 0.1f, 1f) : new Color(1f, 1f, 1f, 1f);
            text.color = baseColor;
            text.text = isCrit ? $"{damage}!" : damage.ToString();
            text.fontSize = isCrit ? 5f : 4f;

            velocity = new Vector3(Random.Range(-0.5f, 0.5f), FloatSpeed, 0f);
            lifetime = DefaultLifetime;
            elapsed = 0f;

            IsActive = true;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!IsActive) return;

            elapsed += Time.deltaTime;

            if (elapsed >= lifetime)
            {
                Hide();
                return;
            }

            transform.position += velocity * Time.deltaTime;
            velocity.y *= 0.95f;

            float alpha = 1f - (elapsed / lifetime);
            text.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);

            float scale = 1f + (elapsed / lifetime) * 0.2f;
            text.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}
