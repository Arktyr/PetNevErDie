using _Scripts.Common.Providers;
using _Scripts.Game.UI.Screens.Provider.Providers;
using UnityEngine;
using Zenject;

namespace _Scripts.Game.UI.Screens.Provider.Factory
{
    public class ScreenLayoutFactory : IScreenLayoutFactory
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private IScreenLayoutConfigsProvider _screenLayoutConfigsProvider;
        [Inject] private ISceneContainersProvider _sceneContainersProvider;
        
        public bool TryCreate(ScreenLayoutType layoutType, out ScreenLayout layout)
        {
            layout = null;
            
            if (_screenLayoutConfigsProvider.TryGetLayoutConfigByType(layoutType, out var config) == false)
                return false;

            ScreenLayout prefab = _screenLayoutConfigsProvider.GetScreenLayoutBasePrefab();
            
            if (_sceneContainersProvider.TryGetContainerByType<UISceneComponentsContainer>(out var container) == false)
                return false;

            Canvas mainCanvas = container.MainCanvas;

            if (prefab == null)
            {
                Debug.LogError("Screen Layout Prefab not found");
                return false;
            }

            layout = _diContainer.InstantiatePrefabForComponent<ScreenLayout>(prefab, mainCanvas.transform);
            layout.transform.SetSiblingIndex(config.CanvasSortingOrder);
            
            layout.Setup(config.LayoutType, config.CanvasSortingOrder);
            layout.name = config.LayoutType.ToString();

            return true;
        }
    }
}