using _Scripts.Common.Logger;
using UnityEngine;
using Zenject;

namespace KingdomCum.FTUE.AddressablesControl.Provider
{
    public class AddressableCacheProvider : IAddressableCacheProvider
    {
        [Inject] private AddressableCacheConfig _config;

        public int GetTimeToReleaseFromLifeTimeMode(LifeTimeMode mode)
        {
            if (_config.TimeToReleases == null)
            {
                DebugExtensions.LogDetailed(_config, "Addressable Cache Config Is Empty");
                return 0;
            }

            if (_config.TimeToReleases.TryGetValue(mode, out int timeToRelease))
                return timeToRelease;
            
            DebugExtensions.LogDetailed(_config.TimeToReleases, $"Addressable Cache Config Doesn't Exist {mode}");
            return 0;
        }
    }
}