namespace _Scripts.Common
{
    public interface ITimerService
    {
        Timer StartTimer(string timerConstant, int timerTickDelay, int timeTarget, TimerMode mode);
        void PauseTimer(string timerConstant);
        void ResumeTimer(string timerConstant);
        void RestartTimer(string timerConstant);
        void StopTimer(string timerConstant);
        bool TryGetTimer(string timerConstant, out Timer timer);
    }
}