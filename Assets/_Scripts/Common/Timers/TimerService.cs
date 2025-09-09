using System.Collections.Generic;

namespace _Scripts.Common
{
    public class TimerService : ITimerService
    {
        private readonly Dictionary<string, Timer> _timers = new();

        public Timer StartTimer(string timerConstant,
            int timerTickDelay, 
            int timeTarget, 
            TimerMode mode)
        {
            if (_timers.ContainsKey(timerConstant)) 
                StopTimer(timerConstant);
            
            Timer newTimer = new Timer(timeTarget, timerTickDelay, mode);
            newTimer.StartTimer();
            _timers.Add(timerConstant, newTimer);

            return newTimer;
        }
        
        public void PauseTimer(string timerConstant)
        {
            if (_timers.TryGetValue(timerConstant, out var timer) == false)
                return;
            
            timer.PauseTimer();
        }
        
        public void ResumeTimer(string timerConstant)
        {
            if (_timers.TryGetValue(timerConstant, out var timer) == false)
                return;
            
            timer.ResumeTimer();
        }

        public void RestartTimer(string timerConstant)
        {
            if (_timers.TryGetValue(timerConstant, out var timer) == false)
                return;
            
            timer.RestartTimer();
        }
        
        public void StopTimer(string timerConstant)
        {
            if (_timers.TryGetValue(timerConstant, out var timer) == false)
                return;
            
            timer.KillTimer();
            _timers.Remove(timerConstant);
        }

        public bool TryGetTimer(string timerConstant, out Timer timer) => 
            _timers.TryGetValue(timerConstant, out timer);
    }
}