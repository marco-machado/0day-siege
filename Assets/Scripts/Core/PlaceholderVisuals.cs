using UnityEngine;

namespace ZeroDaySiege.Core
{
    public class PlaceholderVisuals : MonoBehaviour
    {
        [Header("Colors")]
        [SerializeField] private Color towerSlotColor = new Color(0.3f, 0.3f, 0.3f, 0.8f);

        private void Start()
        {
            CreateVisuals();
        }

        private void CreateVisuals()
        {
            var layout = GameLayout.Instance;
            if (layout == null) return;

            for (int i = 0; i < 5; i++)
            {
                Vector3 pos = layout.GetTowerSlotPosition(i);
                pos.z = 0.5f;
                CreateRegion($"TowerSlot_{i + 1}",
                    pos,
                    new Vector2(1.2f, 1.2f),
                    towerSlotColor);
            }
        }

        private void CreateRegion(string name, Vector3 position, Vector2 size, Color color)
        {
            var go = new GameObject(name);
            go.transform.SetParent(transform);
            go.transform.position = position;

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = CreateSquareSprite();
            sr.color = color;
            go.transform.localScale = new Vector3(size.x, size.y, 1);
        }

        private Sprite CreateSquareSprite()
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, Color.white);
            texture.Apply();

            return Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
        }
    }
}
