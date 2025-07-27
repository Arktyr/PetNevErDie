using System;

namespace _Scripts.Game.GameTime.Provider
{
    public interface IGameTimeProvider
    {
        TimeSpan GetActiveTime();
        TimeSpan GetTotalTime();
    }
}