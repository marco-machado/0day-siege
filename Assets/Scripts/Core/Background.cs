using UnityEngine;

namespace ZeroDaySiege.Core
{
    public class Background : MonoBehaviour
    {
        public static Background Instance { get; private set; }

        [SerializeField] private string spritePath = "Backgrounds/bg_cyberspace_void";
        [SerializeField] private int sortingOrder = -100;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            CreateBackground();
        }

        private void CreateBackground()
        {
            var bgGO = new GameObject("BackgroundSprite");
            bgGO.transform.SetParent(transform);
            bgGO.transform.position = new Vector3(0, 0, 10);

            spriteRenderer = bgGO.AddComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = sortingOrder;

            var texture = Resources.Load<Texture2D>(spritePath);
            if (texture == null)
            {
                Debug.LogWarning($"Background texture not found at Resources/{spritePath}");
                return;
            }

            var sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                100f
            );
            spriteRenderer.sprite = sprite;

            ScaleToFitScreen();
        }

        private void ScaleToFitScreen()
        {
            if (spriteRenderer == null || spriteRenderer.sprite == null) return;

            var camera = Camera.main;
            if (camera == null) return;

            float screenHeight = camera.orthographicSize * 2f;
            float screenWidth = screenHeight * camera.aspect;

            var sprite = spriteRenderer.sprite;
            float spriteWidth = sprite.bounds.size.x;
            float spriteHeight = sprite.bounds.size.y;

            float scaleX = screenWidth / spriteWidth;
            float scaleY = screenHeight / spriteHeight;
            float scale = Mathf.Max(scaleX, scaleY);

            spriteRenderer.transform.localScale = new Vector3(scale, scale, 1);
        }

    }
}
