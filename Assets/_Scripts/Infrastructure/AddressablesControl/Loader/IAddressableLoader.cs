using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace KingdomCum.FTUE.AddressablesControl
{
    public interface IAddressableLoader
    {
        void UnloadAsset(AssetReference assetReference);
        void UnloadAsset(string assetAddressablesKey);
        UniTask<T> LoadAsset<T>(AssetReference assetReference, LifeTimeMode lifeTimeMode) where T : Object;
        UniTask<T> LoadAsset<T>(string name, LifeTimeMode lifeTimeMode) where T : Object;
    }
}