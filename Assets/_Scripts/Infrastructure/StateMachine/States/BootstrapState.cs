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
        
        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }

        public void EndExit()
        {
            
        }

        public async UniTask Enter()
        {
            await _sceneLoader.LoadScene(SceneName.Game);
            
        }
    }
}