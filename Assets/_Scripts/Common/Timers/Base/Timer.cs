using System;
using System.Threading;
using System.Timers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Scripts.Common
{
    public class Timer : IDisposable
    {
        private readonly int _timerDelay;
        private readonly int _timeTarget;
        
        private float _timeTicked;
        private CancellationTokenSource _cts;
        private TimerMode _mode;
        
        public bool IsRunning { get; private set; }

        public float CurrentTime
        {
            get
            {
                switch (_mode)
                {
                    case TimerMode.Rising:
                        return _timeTicked;
                    case TimerMode.Decrease:
                        return _timeTarget - _timeTicked;
                    default:
                        return _timeTicked;
                }
            }
        }

        public event Action OnCompleteTimer;
        public event Action OnResumeTimer;
        public event Action OnPauseTimer;
        public event Action OnStartTimer;
        public event Action<float> OnTickedTimer;

        public Timer(int timeTarget, 
            int timerDelay,
            TimerMode mode)
        {
            _timeTarget = timeTarget;
            _timerDelay = timerDelay;
            _mode = mode; 
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
                
                _timeTicked += _timerDelay;
                
                OnTickedTimer?.Invoke(CurrentTime);
            }
        }

        private void CompleteTimer()
        {
            IsRunning = false;
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
            _timeTicked = 0;
            IsRunning = false;
        }

        public void Dispose()
        {
            CleanUp();
        }
    }
}