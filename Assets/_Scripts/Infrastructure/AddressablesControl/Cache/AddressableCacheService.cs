using System;
using System.Collections.Generic;
using _Scripts.Common;
using KingdomCum.FTUE.AddressablesControl.Provider;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Object = UnityEngine.Object;

namespace KingdomCum.FTUE.AddressablesControl
{
    public class AddressableCacheService : IDisposable, IAddressableCacheService
    {
        [Inject] private ITimerService _timerService;
        [Inject] private IAddressableCacheProvider _addressableCacheProvider;
        
        private readonly List<AddressableHolder> _cachedAssets = new();

        public void AddHolder(Object asset, string assetID, LifeTimeMode lifeTimeMode)
        {
            if (TryAddExistAsset(assetID))
                return;
            
            var newHolder = AddNewAsset(asset, assetID, lifeTimeMode);
            newHolder.OnHolderRelease += OnReleaseAsset;
            newHolder.AddReference();
        }

        public void RemoveReference(string assetID)
        {
            if (_cachedAssets.Count == 0)
                return;
            
            var existAsset = _cachedAssets.Find(x => x.AssetID == assetID);
            existAsset?.RemoveReference();
        }

        public void ClearCache()
        {
            var cache = _cachedAssets.ToArray();
            
            foreach (var asset in cache)
            {
                ReleaseAsset(asset.Asset);
                asset.Dispose();
            }
            
            Resources.UnloadUnusedAssets();
            _cachedAssets.Clear();
        }

        private void RemoveHolder(AddressableHolder holder)
        {
            if (_cachedAssets.Contains(holder) == false)
                return;

            holder.Dispose();
            holder.OnHolderRelease -= OnReleaseAsset;
            _cachedAssets.Remove(holder);
        }

        private bool TryAddExistAsset(string assetID)
        {
            if (_cachedAssets.Count == 0)
                return false;
            
            var existAsset = _cachedAssets.Find(x => x.AssetID == assetID);

            if (existAsset == null)
                return false;
            
            existAsset.AddReference();
            return true;
        }

        private AddressableHolder AddNewAsset(Object asset, string assetID, LifeTimeMode lifeTimeMode)
        {
            int timeToReleaseHandle = _addressableCacheProvider.GetTimeToReleaseFromLifeTimeMode(lifeTimeMode);
            var newHolder = new AddressableHolder(asset, timeToReleaseHandle, assetID);
            _cachedAssets.Add(newHolder);
            return newHolder;
        }

        private void OnReleaseAsset(AddressableHolder holder)
        {
            ReleaseAsset(holder.Asset);
            RemoveHolder(holder);
        }

        private void ReleaseAsset(Object asset) => 
            Addressables.Release(asset);

        public void Dispose() => 
            ClearCache();
    }
}