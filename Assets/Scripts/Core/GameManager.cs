using System;
using UnityEngine;

namespace ZeroDaySiege.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public event Action<GameState, GameState> OnStateChanged;

        [SerializeField] private GameState initialState = GameState.Menu;

        private GameState currentState;
        public GameState CurrentState => currentState;

        public bool IsPlaying => currentState == GameState.Playing;
        public bool IsPaused => currentState == GameState.Paused;
        public bool IsInRun => currentState == GameState.Playing ||
                               currentState == GameState.Paused ||
                               currentState == GameState.CardSelection;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            currentState = initialState;
        }

        public bool TryChangeState(GameState newState)
        {
            if (!IsValidTransition(currentState, newState))
            {
                Debug.LogWarning($"Invalid state transition: {currentState} -> {newState}");
                return false;
            }

            ChangeState(newState);
            return true;
        }

        private void ChangeState(GameState newState)
        {
            var previousState = currentState;
            currentState = newState;

            ApplyStateEffects(newState);
            OnStateChanged?.Invoke(previousState, newState);

            Debug.Log($"State changed: {previousState} -> {newState}");
        }

        private void ApplyStateEffects(GameState state)
        {
            switch (state)
            {
                case GameState.Playing:
                    Time.timeScale = 1f;
                    break;
                case GameState.Paused:
                case GameState.CardSelection:
                case GameState.GameOver:
                    Time.timeScale = 0f;
                    break;
                case GameState.Menu:
                    Time.timeScale = 1f;
                    break;
            }
        }

        private bool IsValidTransition(GameState from, GameState to)
        {
            if (from == to) return false;

            return (from, to) switch
            {
                (GameState.Menu, GameState.Playing) => true,

                (GameState.Playing, GameState.Paused) => true,
                (GameState.Playing, GameState.CardSelection) => true,
                (GameState.Playing, GameState.GameOver) => true,

                (GameState.Paused, GameState.Playing) => true,
                (GameState.Paused, GameState.Menu) => true,

                (GameState.CardSelection, GameState.Playing) => true,

                (GameState.GameOver, GameState.Menu) => true,
                (GameState.GameOver, GameState.Playing) => true,

                _ => false
            };
        }

        public void StartRun()
        {
            TryChangeState(GameState.Playing);
        }

        public void PauseGame()
        {
            TryChangeState(GameState.Paused);
        }

        public void ResumeGame()
        {
            if (currentState == GameState.Paused)
            {
                TryChangeState(GameState.Playing);
            }
        }

        public void ShowCardSelection()
        {
            TryChangeState(GameState.CardSelection);
        }

        public void CloseCardSelection()
        {
            if (currentState == GameState.CardSelection)
            {
                TryChangeState(GameState.Playing);
            }
        }

        public void EndRun(bool victory)
        {
            TryChangeState(GameState.GameOver);
        }

        public void ReturnToMenu()
        {
            TryChangeState(GameState.Menu);
        }

        public void RestartRun()
        {
            if (currentState == GameState.GameOver || currentState == GameState.Paused)
            {
                TryChangeState(GameState.Playing);
            }
        }
    }
}
