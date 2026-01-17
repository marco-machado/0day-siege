using System;
using System.Collections.Generic;
using UnityEngine;
using ZeroDaySiege.Core;

namespace ZeroDaySiege.Towers
{
    public class TowerManager : MonoBehaviour
    {
        public static TowerManager Instance { get; private set; }

        public event Action<Tower> OnTowerPlaced;
        public event Action<Tower> OnTowerDestroyed;

        private readonly List<Tower> activeTowers = new();
        private TowerSlot[] slots;

        public int ActiveTowerCount => activeTowers.Count;
        public IReadOnlyList<Tower> ActiveTowers => activeTowers;

        public TowerType StartingTowerType { get; set; } = TowerType.BaseTower;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeSlots();
        }

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += HandleGameStateChanged;
            }
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= HandleGameStateChanged;
            }
        }

        private void InitializeSlots()
        {
            slots = new TowerSlot[TowerSlot.TotalSlots];
            for (int i = 0; i < TowerSlot.TotalSlots; i++)
            {
                slots[i] = new TowerSlot(i);
            }
        }

        private void HandleGameStateChanged(GameState previousState, GameState newState)
        {
            if (newState == GameState.Menu || newState == GameState.GameOver)
            {
                ClearAllTowers();
            }
            else if (newState == GameState.Playing && previousState == GameState.Menu)
            {
                PlaceStartingTower();
            }
        }

        private void PlaceStartingTower()
        {
            int middleSlot = TowerSlot.MiddleSlotIndex;
            if (!slots[middleSlot].IsOccupied)
            {
                PlaceTower(middleSlot, StartingTowerType);
                Debug.Log($"[TowerManager] Placed starting tower {StartingTowerType} in middle slot {middleSlot}");
            }
        }

        public Tower PlaceTower(int slotIndex, TowerType type)
        {
            if (slotIndex < 0 || slotIndex >= TowerSlot.TotalSlots)
            {
                Debug.LogWarning($"[TowerManager] Invalid slot index: {slotIndex}");
                return null;
            }

            if (slots[slotIndex].IsOccupied)
            {
                Debug.LogWarning($"[TowerManager] Slot {slotIndex} is already occupied");
                return null;
            }

            var layout = GameLayout.Instance;
            if (layout == null)
            {
                Debug.LogWarning("[TowerManager] Cannot place tower: GameLayout not found");
                return null;
            }

            Vector3 position = layout.GetTowerSlotPosition(slotIndex);

            var towerGO = new GameObject($"Tower_{type}_{slotIndex}");
            towerGO.transform.position = position;

            var tower = towerGO.AddComponent<Tower>();
            tower.Initialize(type, slotIndex);

            slots[slotIndex].SetTower(tower);
            activeTowers.Add(tower);
            OnTowerPlaced?.Invoke(tower);

            Debug.Log($"[TowerManager] Placed {type} in slot {slotIndex} at {position}");

            return tower;
        }

        public void RemoveTower(Tower tower)
        {
            if (tower == null) return;

            int slotIndex = tower.SlotIndex;
            if (slotIndex >= 0 && slotIndex < TowerSlot.TotalSlots)
            {
                slots[slotIndex].Clear();
            }

            if (activeTowers.Remove(tower))
            {
                OnTowerDestroyed?.Invoke(tower);
                Debug.Log($"[TowerManager] Removed tower from slot {slotIndex}");
            }

            if (tower.gameObject != null)
            {
                Destroy(tower.gameObject);
            }
        }

        public void ClearAllTowers()
        {
            for (int i = activeTowers.Count - 1; i >= 0; i--)
            {
                var tower = activeTowers[i];
                if (tower != null && tower.gameObject != null)
                {
                    Destroy(tower.gameObject);
                }
            }
            activeTowers.Clear();

            for (int i = 0; i < TowerSlot.TotalSlots; i++)
            {
                slots[i].Clear();
            }

            Debug.Log("[TowerManager] Cleared all towers");
        }

        public Tower GetTowerInSlot(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= TowerSlot.TotalSlots)
                return null;

            return slots[slotIndex].Tower;
        }

        public bool IsSlotOccupied(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= TowerSlot.TotalSlots)
                return false;

            return slots[slotIndex].IsOccupied;
        }

        public int GetNextEmptySlot()
        {
            for (int i = 0; i < TowerSlot.TotalSlots; i++)
            {
                if (!slots[i].IsOccupied)
                    return i;
            }
            return -1;
        }

        public bool HasTowerOfType(TowerType type)
        {
            foreach (var tower in activeTowers)
            {
                if (tower != null && tower.Type == type)
                    return true;
            }
            return false;
        }
    }
}
