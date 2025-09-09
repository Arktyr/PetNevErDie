using _Scripts.Common;
using _Scripts.Common.Providers;
using _Scripts.Game.GameStatus;
using _Scripts.Game.GameTime;
using _Scripts.Game.GameTime.Provider;
using _Scripts.Game.UI.Screens;
using _Scripts.Game.UI.Screens.Factory;
using _Scripts.Game.UI.Screens.Provider;
using _Scripts.Game.UI.Screens.Provider.Factory;
using _Scripts.Game.UI.Screens.Provider.Providers;
using _Scripts.Infrastructure.SceneLoader;
using _Scripts.Infrastructure.StateMachine;
using _Scripts.Infrastructure.StateMachine.Factory;
using _Scripts.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private AllScreensConfig _allScreensConfig;
        [SerializeField] private AllScreenLayoutConfigs _allScreenLayoutConfigs;
        
        public override void InstallBindings()
        {
            BindStateMachine();
            BindStates();
            BindGameServices();
            BindUi();
            BindScriptable();
        }

        private void BindStateMachine()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
        }

        private void BindStates()
        {
        }

        private void BindScriptable()
        {
            Container.BindInterfacesAndSelfTo<AllScreensConfig>().FromInstance(_allScreensConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<AllScreenLayoutConfigs>().FromInstance(_allScreenLayoutConfigs).AsSingle();
        }

        private void BindUi()
        {
            Container.BindInterfacesAndSelfTo<ScreenService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScreenFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScreenLayoutFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScreenLayoutService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScreenPrefabsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScreenLayoutConfigsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneContainersProvider>().AsSingle();
        }

        private void BindGameServices()
        {
            Container.BindInterfacesAndSelfTo<GameStatusService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameTimeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameTimeProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoaderService>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimerService>().AsSingle();
        }
    }
}