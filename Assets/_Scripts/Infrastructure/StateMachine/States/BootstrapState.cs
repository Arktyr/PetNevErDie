using _Scripts.Common.Providers;
using _Scripts.Game.UI.Screens;
using _Scripts.Game.UI.Screens.Custom;
using _Scripts.Infrastructure.SceneLoader;
using _Scripts.Infrastructure.StateMachine.BaseStates;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        [Inject] private ISceneLoaderService _sceneLoader;
        [Inject] private ISceneContainersProvider _sceneContainersProvider;
        [Inject] private IScreenService _screenService;
        
        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }

        public void EndExit()
        {
            
        }

        public async UniTask Enter()
        {
            _screenService.TryOpenScreen<LoadScreen>(ScreenOpenHideMode.Animated, out var screen);

            await UniTask.WaitForSeconds(3);

            _screenService.TryHideAndDestroyScreen<LoadScreen>(ScreenOpenHideMode.Animated);
        }
    }
}