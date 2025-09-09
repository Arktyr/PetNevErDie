using System.Collections.Generic;
using _Scripts.Game.UI.Screens.Provider.Factory;
using UnityEngine;
using Zenject;

namespace _Scripts.Game.UI.Screens.Provider
{
    public class ScreenLayoutService : IScreenLayoutService
    {
        [Inject] private IScreenLayoutFactory _screenLayoutFactory;
        
        private readonly Dictionary<ScreenLayoutType, ScreenLayout> _screenLayouts = new();

        public bool TryGetLayout(ScreenLayoutType layoutType, out ScreenLayout layout)
        {
            layout = null;
            
            if (_screenLayouts.TryGetValue(layoutType, out var existLayout))
            {
                layout = existLayout;
                return true;
            }

            if (_screenLayoutFactory.TryCreate(layoutType, out var newLayout) == false)
                return false;
            
            layout = newLayout;
            _screenLayouts.Add(layoutType, newLayout);
            return true;
        }

        public void AddScreenToLayoutByType(ScreenLayoutType layoutType, BaseScreen baseScreen)
        {
            if (_screenLayouts.TryGetValue(layoutType, out var layout) == false)
                return;

            layout.AddScreen(baseScreen);
            baseScreen.OnDestroy += OnDestroyScreen;
        }

        private void OnDestroyScreen(BaseScreen screen)
        {
            RemoveFromLayoutByType(screen);
        }

        public void RemoveFromLayoutByType(BaseScreen baseScreen)
        {
            if (_screenLayouts.TryGetValue(baseScreen.ScreenLayoutType, out var layout) == false)
                return;

            layout.RemoveScreen(baseScreen);
            baseScreen.OnDestroy -= OnDestroyScreen;

            if (layout.Screens.Count > 0)
                return;
            
            layout.DestroyLayout();
            _screenLayouts.Remove(baseScreen.ScreenLayoutType);
        }
    }
}