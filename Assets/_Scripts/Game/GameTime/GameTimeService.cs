using System;
using _Scripts.Common;
using _Scripts.Game.GameStatus;
using Zenject;

namespace _Scripts.Game.GameTime
{
    public class GameTimeService : IDisposable, IGameTimeService
    {
        [Inject] private readonly IGameStatusService _gameStatusService;
        [Inject] private readonly ITimerService _timerService;

        public TimeSpan TotalActiveTimePlayed()
        {
            if (_timerService.TryGetTimer(TimerConstants.TimerActivePlayTime, out var timer) == false)
                return TimeSpan.Zero;
            
            return TimeSpan.FromSeconds(timer.CurrentTime);
        }

        public TimeSpan TotalTimePlayed()
        {
            if (_timerService.TryGetTimer(TimerConstants.TimerTotalPlayTime, out var timer) == false)
                return TimeSpan.Zero;
            
            return TimeSpan.FromSeconds(timer.CurrentTime);
        }

        public void Initialize()
        {
            _gameStatusService.OnChangeGameStatus += SetGameTimer;

            _timerService.StartTimer(TimerConstants.TimerTotalPlayTime, 1, Int32.MaxValue, TimerMode.Rising);
            _timerService.StartTimer(TimerConstants.TimerActivePlayTime, 1, Int32.MaxValue, TimerMode.Rising);
        }

        private void SetGameTimer(GameStatus.GameStatus status)
        {
            switch (status)
            {
                case GameStatus.GameStatus.Idle:
                case GameStatus.GameStatus.Pause:
                    _timerService.PauseTimer(TimerConstants.TimerActivePlayTime);
                    break;
                case GameStatus.GameStatus.Run:
                    _timerService.ResumeTimer(TimerConstants.TimerActivePlayTime);
                    break;
                default:
                    _timerService.PauseTimer(TimerConstants.TimerTotalPlayTime);
                    return;
            }
        }

        public void Dispose()
        {
            _timerService.StopTimer(TimerConstants.TimerActivePlayTime);
            _timerService.StopTimer(TimerConstants.TimerTotalPlayTime);
            
            _gameStatusService.OnChangeGameStatus -= SetGameTimer;
        }
    }
}