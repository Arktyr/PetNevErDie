using System;

namespace _Scripts.Game.GameStatus
{
    public interface IGameStatusService
    {
        GameStatus CurrentGameStatus { get; }
        GameStatus PreviousGameStatus { get; }
        event Action<GameStatus> OnChangeGameStatus;
        void ChangeGameState(GameStatus gameStatus);
    }
}