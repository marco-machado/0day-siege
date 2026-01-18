using System;
using UnityEngine;

namespace ZeroDaySiege.Core
{
    public class DecryptKeyManager : MonoBehaviour
    {
        public static DecryptKeyManager Instance { get; private set; }

        public event Action<int> OnKeysChanged;

        private const int MaxKeys = 99;
        private const int StartingKeys = 5;

        private int currentKeys;

        public int CurrentKeys => currentKeys;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            currentKeys = StartingKeys;
        }

        public bool CanSpend(int amount)
        {
            return currentKeys >= amount;
        }

        public bool Spend(int amount)
        {
            if (!CanSpend(amount))
            {
                Debug.Log($"[DecryptKeyManager] Cannot spend {amount} keys, only have {currentKeys}");
                return false;
            }

            currentKeys -= amount;
            OnKeysChanged?.Invoke(currentKeys);
            Debug.Log($"[DecryptKeyManager] Spent {amount} key(s), {currentKeys} remaining");
            return true;
        }

        public void Add(int amount)
        {
            int previousKeys = currentKeys;
            currentKeys = Mathf.Min(currentKeys + amount, MaxKeys);
            int actualAdded = currentKeys - previousKeys;

            if (actualAdded > 0)
            {
                OnKeysChanged?.Invoke(currentKeys);
                Debug.Log($"[DecryptKeyManager] Added {actualAdded} key(s), now have {currentKeys}");
            }
        }

        public void Reset()
        {
            currentKeys = StartingKeys;
            OnKeysChanged?.Invoke(currentKeys);
        }
    }
}
