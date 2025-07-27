using System;

namespace _Scripts.Game.GameTime.Provider
{
    public class GameTimeProvider : IGameTimeProvider
    {
        private readonly IGameTimeService _gameTimeService;

        public GameTimeProvider(IGameTimeService gameTimeService)
        {
            _gameTimeService = gameTimeService;
        }

        public TimeSpan GetActiveTime() => 
            _gameTimeService.TotalActiveTimePlayed();
        
        public TimeSpan GetTotalTime() =>
            _gameTimeService.TotalTimePlayed();
    }
}