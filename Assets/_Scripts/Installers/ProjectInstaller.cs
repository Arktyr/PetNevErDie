using _Scripts.Game.GameStatus;
using _Scripts.Game.GameTime;
using _Scripts.Game.GameTime.Provider;
using _Scripts.Infrastructure.SceneLoader;
using _Scripts.Infrastructure.StateMachine;
using _Scripts.Infrastructure.StateMachine.Factory;
using _Scripts.Infrastructure.StateMachine.States;
using Zenject;

namespace _Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStateMachine();
            BindStates();
            BindGameServices();
        }

        private void BindStateMachine()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
        }

        private void BindStates()
        {
        }

        private void BindGameServices()
        {
            Container.BindInterfacesAndSelfTo<GameStatusService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameTimeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameTimeProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoaderService>().AsSingle();
        }
    }
}