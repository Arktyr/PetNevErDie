using System;
using System.Collections.Generic;
using System.Threading;
using _Scripts.Game.UI.Screens.Factory;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Scripts.Game.UI.Screens
{
    public class ScreenService : IScreenService, IDisposable
    {
        [Inject] private IScreenFactory _screenFactory;
        
        private readonly List<BaseScreen> _currentOpenScreens = new();
        
        private readonly CancellationTokenSource _cts = new();

        public bool TryOpenScreen<T>(ScreenOpenHideMode mode, out T screen) where T : BaseScreen
        {
            screen = null;
            BaseScreen findScreen = _currentOpenScreens.Find(screen => screen is T);

            if (findScreen != null) 
                TryHideAndDestroyScreen<T>(ScreenOpenHideMode.Immediately);
            
            if (_screenFactory.TryCreate<T>(out var newScreen) == false)
                return false;

            newScreen.Show(mode)
                .AttachExternalCancellation(_cts.Token);
            
            _currentOpenScreens.Add(newScreen);
            return newScreen;
        }
        
        public bool TryHideScreen<T>(ScreenOpenHideMode mode, out T screen) where T : BaseScreen
        {
            var findScreen = _currentOpenScreens.Find(screen => screen is T);
            screen = findScreen as T;
            
            if (findScreen == null)
                return false;
            
            findScreen.Hide(mode)
                .AttachExternalCancellation(_cts.Token);
            
            _currentOpenScreens.Remove(findScreen);
            return true;
        }
        
        public bool TryHideAndDestroyScreen<T>(ScreenOpenHideMode mode) where T : BaseScreen
        {
            if (TryHideScreen<T>(mode, out var screen ) == false)
                return false;

            screen.Destroy();
            
            return true;
        }

        public bool TryGetScreen<T>(out T screen) where T : BaseScreen
        {
            var findScreen = _currentOpenScreens.Find(screen => screen is T);

            screen = findScreen as T;
            
            return findScreen != null;
        }

        public void Dispose()
        {
            _cts?.Dispose();
        }
    }
}