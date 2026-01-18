using UnityEngine;

namespace ZeroDaySiege.Core
{
    public class GameLayout : MonoBehaviour
    {
        public static GameLayout Instance { get; private set; }

        [Header("Camera Settings")]
        [SerializeField] private float orthographicSize = 10f;

        [Header("Vertical Boundaries (World Units)")]
        [SerializeField] private float spawnY = 8f;
        [SerializeField] private float firewallY = -4f;
        [SerializeField] private float firewallHeight = 1f;
        [SerializeField] private float towerSlotsY = -6f;

        [Header("Horizontal Settings")]
        [SerializeField] private float playAreaWidth = 10f;

        public float SpawnY => spawnY;
        public float FirewallY => firewallY;
        public float FirewallTop => firewallY + firewallHeight;
        public float TowerSlotsY => towerSlotsY;
        public float PlayAreaWidth => playAreaWidth;
        public float PlayAreaLeft => -playAreaWidth / 2f;
        public float PlayAreaRight => playAreaWidth / 2f;
        public float PlayAreaHeight => spawnY - FirewallTop;

        public float NormalizedSpeedToWorld(float normalizedSpeed)
        {
            return normalizedSpeed * PlayAreaHeight;
        }

        public float ScreenTop => orthographicSize;
        public float ScreenBottom => -orthographicSize;

        private Camera mainCamera;
        private LineRenderer spawnLineRenderer;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            mainCamera = Camera.main;
            ConfigureCamera();
            CreateSpawnLine();
        }

        private void CreateSpawnLine()
        {
            var lineGO = new GameObject("SpawnLine");
            lineGO.transform.SetParent(transform);

            spawnLineRenderer = lineGO.AddComponent<LineRenderer>();
            spawnLineRenderer.positionCount = 2;
            spawnLineRenderer.SetPosition(0, new Vector3(PlayAreaLeft, spawnY, 0));
            spawnLineRenderer.SetPosition(1, new Vector3(PlayAreaRight, spawnY, 0));

            spawnLineRenderer.startWidth = 0.05f;
            spawnLineRenderer.endWidth = 0.05f;

            Color spawnColor = new Color(1f, 0.2f, 0.2f, 0.6f);
            spawnLineRenderer.startColor = spawnColor;
            spawnLineRenderer.endColor = spawnColor;

            spawnLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            spawnLineRenderer.sortingOrder = 1;
        }

        private void ConfigureCamera()
        {
            if (mainCamera == null) return;

            mainCamera.orthographic = true;
            mainCamera.orthographicSize = orthographicSize;
            mainCamera.transform.position = new Vector3(0, 0, -10);
        }

        public Vector3 GetTowerSlotPosition(int slotIndex)
        {
            float slotSpacing = playAreaWidth / 6f;
            float x = PlayAreaLeft + slotSpacing * (slotIndex + 1);
            return new Vector3(x, towerSlotsY, 0);
        }

        public float NormalizedToWorldY(float normalized)
        {
            return Mathf.Lerp(spawnY, firewallY, normalized);
        }

        public float WorldToNormalizedY(float worldY)
        {
            return Mathf.InverseLerp(spawnY, firewallY, worldY);
        }

        public float NormalizedToWorldX(float normalized)
        {
            return Mathf.Lerp(PlayAreaLeft, PlayAreaRight, normalized);
        }

        private void OnDrawGizmos()
        {
            float left = -playAreaWidth / 2f;
            float right = playAreaWidth / 2f;

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(new Vector3(left, spawnY, 0), new Vector3(right, spawnY, 0));

            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(left, firewallY, 0), new Vector3(right, firewallY, 0));
            Gizmos.DrawLine(new Vector3(left, firewallY + firewallHeight, 0), new Vector3(right, firewallY + firewallHeight, 0));

            Gizmos.color = Color.green;
            for (int i = 0; i < 5; i++)
            {
                Vector3 pos = GetTowerSlotPosition(i);
                Gizmos.DrawWireSphere(pos, 0.5f);
            }

            Gizmos.color = Color.white;
            Gizmos.DrawLine(new Vector3(left, spawnY, 0), new Vector3(left, firewallY, 0));
            Gizmos.DrawLine(new Vector3(right, spawnY, 0), new Vector3(right, firewallY, 0));
        }
    }
}
