using UnityEngine;
using Zenject;

namespace _Scripts.Game.UI.Screens.Provider.Providers
{
    public class ScreenLayoutConfigsProvider : IScreenLayoutConfigsProvider
    {
        [Inject] private AllScreenLayoutConfigs _allScreenLayoutConfigs;

        public bool TryGetLayoutConfigByType(ScreenLayoutType layoutType, out ScreenLayoutConfig config)
        {
            ScreenLayoutConfig findConfig = 
                _allScreenLayoutConfigs.ScreenLayoutConfigs.Find(x => x.LayoutType == layoutType);

            config = findConfig;
            
            if (findConfig != null)
                return true;
            
            Debug.LogError($"{layoutType} : current layout config not found");
            return false;
        }

        public ScreenLayout GetScreenLayoutBasePrefab() => 
            _allScreenLayoutConfigs.Prefab;
    }
}