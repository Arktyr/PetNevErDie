using System;

namespace _Scripts.Game.GameTime
{
    public interface IGameTimeService
    {
        TimeSpan TotalActiveTimePlayed();
        TimeSpan TotalTimePlayed();
        void Initialize();
    }
}