using _Scripts.Game.UI.Screens.Provider;
using UnityEngine;
using Zenject;

namespace _Scripts.Game.UI.Screens.Factory
{
    public class ScreenFactory : IScreenFactory
    {
        [Inject] private IScreenPrefabsProvider _screenPrefabsProvider;
        [Inject] private DiContainer _container;
        [Inject] private IScreenLayoutService _screenLayoutService;

        public bool TryCreate<T>(out T screen) where T : BaseScreen
        {
            screen = null;
            
            if (_screenPrefabsProvider.TryGetConfig<T>(out var config) == false)
                return false;

            if (_screenLayoutService.TryGetLayout(config.LayoutType, out var layout) == false)
                return false;

            screen = _container.InstantiatePrefabForComponent<T>(config.Prefab, layout.transform);
            screen.Setup(layout.LayoutType);
            _screenLayoutService.AddScreenToLayoutByType(layout.LayoutType, screen);
            layout.AddScreen(screen);

            return true;
        }
    }
}