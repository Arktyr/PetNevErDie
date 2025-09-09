using _Scripts.Common.Providers;
using _Scripts.Infrastructure.Bootstrappers;
using _Scripts.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class BootInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameFSMBootstrapper<BootstrapState>>().AsSingle();
        }
    }
}