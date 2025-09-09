using UnityEngine;
using Zenject;

namespace _Scripts.Game.UI.Screens.Provider
{
    public class ScreenPrefabsProvider : IScreenPrefabsProvider
    {
        [Inject] private AllScreensConfig _screensConfig;

        public bool TryGetConfig<T>(out ScreenConfig config) where T : BaseScreen
        {
            config = _screensConfig.ScreenConfigs.Find(x => x.Prefab is T);

            if (config != null) 
                return true;
            
            Debug.LogError($"{typeof(T)} : Not found config for this screen");
            return false;
        }
    }
}