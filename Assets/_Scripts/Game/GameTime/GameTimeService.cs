using System;
using _Scripts.Common;
using _Scripts.Game.GameStatus;

namespace _Scripts.Game.GameTime
{
    public class GameTimeService : IDisposable, IGameTimeService
    {
        private readonly BaseTimer _activeTimeTimer = new BaseTimer();
        private readonly BaseTimer _totalTimeTimer = new BaseTimer();
        
        private readonly IGameStatusService _gameStatusService;

        public GameTimeService(IGameStatusService gameStatusService)
        {
            _gameStatusService = gameStatusService;
        }

        public TimeSpan TotalActiveTimePlayed() => TimeSpan.FromSeconds(_activeTimeTimer.CurrentTime);
        public TimeSpan TotalTimePlayed() => TimeSpan.FromSeconds(_totalTimeTimer.CurrentTime);

        public void Initialize()
        {
            _gameStatusService.OnChangeGameStatus += SetGameTimer;
            _totalTimeTimer.StartInfinityTimer();
        }

        private void SetGameTimer(GameStatus.GameStatus status)
        {
            switch (status)
            {
                case GameStatus.GameStatus.Idle:
                    PauseActiveGameTimer();
                    break;
                case GameStatus.GameStatus.Pause:
                    PauseActiveGameTimer();
                    break;
                case GameStatus.GameStatus.Run:
                    ResumeActiveGameTimer();
                    break;
                default:
                    PauseActiveGameTimer();
                    return;
            }
        }

        private void ResumeActiveGameTimer() => 
            _activeTimeTimer.RestartTimer();

        private void PauseActiveGameTimer() => 
            _activeTimeTimer.PauseTimer();

        public void Dispose()
        {
            _activeTimeTimer?.Dispose();
            _totalTimeTimer?.Dispose();
            
            _gameStatusService.OnChangeGameStatus -= SetGameTimer;
        }
    }
}