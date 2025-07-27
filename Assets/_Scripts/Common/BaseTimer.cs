using System;
using System.Threading;
using System.Timers;
using Cysharp.Threading.Tasks;

namespace _Scripts.Common
{
    public class BaseTimer : IDisposable
    {
        private readonly long _timerDelay;
        private long _timeTarget;
        
        private CancellationTokenSource _cts;
        
        public bool IsRunning { get; private set; }
        public long CurrentTime {get; private set;}
        
        public event Action OnCompleteTimer;
        public event Action OnResumeTimer;
        public event Action OnPauseTimer;
        public event Action OnStartTimer;

        public BaseTimer(long timeTarget = 0, 
            long timerDelay = 1)
        {
            _timeTarget = timeTarget;
            _timerDelay = timerDelay;
        }

        public void StartTimer()
        {
            if (IsRunning)
                return;
            
            IsRunning = true;
            
            _cts = new CancellationTokenSource();
            
            RunTimer()
                .AttachExternalCancellation(_cts.Token)
                .Forget();
            
            OnStartTimer?.Invoke();
        }

        public void StartInfinityTimer()
        {
            _timeTarget = long.MaxValue;
            StartTimer();
        }

        public void ResumeTimer()
        {
            if (IsRunning)
                return;
            
            IsRunning = true;
            
            _cts = new CancellationTokenSource();
            
            RunTimer()
                .AttachExternalCancellation(_cts.Token)
                .Forget();
            
            OnResumeTimer?.Invoke();
        }
        
        public void PauseTimer()
        {
            if (IsRunning == false)
                return;
            
            IsRunning = false;
            _cts?.Cancel();
            _cts?.Dispose();
            
            OnPauseTimer?.Invoke();
        }

        public void KillTimer() => 
            CleanUp();

        public void RestartTimer()
        {
            CleanUp();
            
            _cts = new CancellationTokenSource();
            
            StartTimer();
        }

        private async UniTask RunTimer()
        {
            while (!TryTimerEnd())
            {
                await UniTask.WaitForSeconds(_timerDelay)
                    .AttachExternalCancellation(_cts.Token);
                
                CurrentTime += _timerDelay;
            }
        }

        private void CompleteTimer()
        {
            KillTimer();
            OnCompleteTimer?.Invoke();
        }

        private bool TryTimerEnd()
        {
            if (CurrentTime < _timeTarget) 
                return false;
            
            CompleteTimer();
            return true;
        }

        private void CleanUp()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            CurrentTime = 0;
            IsRunning = false;
        }

        public void Dispose()
        {
            CleanUp();
        }
    }
}