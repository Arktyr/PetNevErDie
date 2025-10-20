using System;
using _Scripts.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Object = UnityEngine.Object;

namespace KingdomCum.FTUE.AddressablesControl
{
    public class AddressableHolder : IDisposable
    {
        [Inject] private ITimerService _timerService;
        
        private int _referencesCount;
        private readonly int _timeToRelease;

        public AddressableHolder(Object asset, int timeToRelease, string assetID)
        {
            Asset = asset;
            AssetID = assetID;
            _timeToRelease = timeToRelease;
        }
        
        public Object Asset { get; private set; }
        public string AssetID { get; private set; }

        public event Action<AddressableHolder> OnHolderRelease;

        private void OnTimerCompleted() => 
            OnHolderRelease?.Invoke(this);

        public void AddReference()
        {
            _referencesCount++;
            TryResetTimerToRelease();
        }

        public void RemoveReference()
        {
            _referencesCount = Mathf.Clamp(_referencesCount - 1, 0, Int32.MaxValue);
            TryStartTimerToRelease();
        }

        private void TryStartTimerToRelease()
        {
            if (_timerService.TryGetTimer(AssetID, out var existTimer))
            {
                if (existTimer.IsRunning)
                    return;
            }

            if (_referencesCount > 0)
                return;
            
            var timer = _timerService.StartTimer(AssetID, 1, _timeToRelease, TimerMode.Rising);
            timer.OnCompleteTimer += OnTimerCompleted;
        }
        
        private void TryResetTimerToRelease()
        {
            if (_timerService.TryGetTimer(AssetID, out var existTimer) == false)
                return;
            
            if (_referencesCount > 0)
                _timerService.RestartTimer(AssetID);
        }

        public void Dispose()
        {
            if (_timerService.TryGetTimer(AssetID, out var existTimer) == false)
                return;
            
            existTimer.OnCompleteTimer -= OnTimerCompleted;

            _timerService.StopTimer(AssetID);
        }
    }
}