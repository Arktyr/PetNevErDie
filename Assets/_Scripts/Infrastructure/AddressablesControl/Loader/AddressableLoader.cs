using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace KingdomCum.FTUE.AddressablesControl
{
    public class AddressableLoader : IAddressableLoader
    {
        private readonly IAddressableCacheService _addressableCacheService;

        public AddressableLoader(IAddressableCacheService addressableCacheService)
        {
            _addressableCacheService = addressableCacheService;
        }

        public async UniTask<T> LoadAsset<T>(AssetReference assetReference, LifeTimeMode lifeTimeMode) where T : Object
        {
            var loadedAsset = Addressables.LoadAssetAsync<T>(assetReference);
            await loadedAsset.Task;

            var result = loadedAsset.Result;

            if (result == null)
            {
                Debug.LogError($"{typeof(T)} : This type wrong for this reference {assetReference}");
                return result;
            }

            _addressableCacheService.AddHolder(result, assetReference.AssetGUID, lifeTimeMode);
            return result;
        }
        
        public async UniTask<T> LoadAsset<T>(string name, LifeTimeMode lifeTimeMode) where T : Object
        {
            var loadedAsset = Addressables.LoadAssetAsync<T>(name);
            await loadedAsset.Task;

            var result = loadedAsset.Result;

            if (result == null)
            {
                Debug.LogError($"{typeof(T)} : This type wrong for this reference {name}");
                return result;
            }

            _addressableCacheService.AddHolder(result, name, lifeTimeMode);
            return result;
        }
        
        public void UnloadAsset(AssetReference assetReference) => 
            _addressableCacheService.RemoveReference(assetReference.AssetGUID);

        public void UnloadAsset(string assetAddressablesKey) =>
            _addressableCacheService.RemoveReference(assetAddressablesKey);
    }
}