using UnityEngine;

namespace KingdomCum.FTUE.AddressablesControl
{
    public interface IAddressableCacheService
    {
        void AddHolder(Object asset, string assetID, LifeTimeMode lifeTimeMode);
        void RemoveReference(string assetID);
        void ClearCache();
    }
}