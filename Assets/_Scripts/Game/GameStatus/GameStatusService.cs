using System;
using UnityEngine;

namespace _Scripts.Game.GameStatus
{
    public class GameStatusService : IGameStatusService, IDisposable
    {
        private const GameStatus DEFAULT_GAME_STATUS = GameStatus.Idle;

        public GameStatus CurrentGameStatus { get; private set; } = DEFAULT_GAME_STATUS;
        public GameStatus PreviousGameStatus { get; private set; }
        
        public event Action<GameStatus> OnChangeGameStatus;
        
        public void ChangeGameState(GameStatus gameStatus)
        {
            if (gameStatus == CurrentGameStatus)
                return;
            
            Debug.Log($"Changing game status to {gameStatus}");

            PreviousGameStatus = CurrentGameStatus;
            CurrentGameStatus = gameStatus;
            
            OnChangeGameStatus?.Invoke(CurrentGameStatus);
        }

        public void Dispose()
        {
            CurrentGameStatus = DEFAULT_GAME_STATUS;
            PreviousGameStatus = DEFAULT_GAME_STATUS;
            
            OnChangeGameStatus = null;
        }
    }
}