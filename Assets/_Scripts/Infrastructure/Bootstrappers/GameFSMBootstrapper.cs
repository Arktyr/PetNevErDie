using _Scripts.Infrastructure.StateMachine;
using _Scripts.Infrastructure.StateMachine.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using IInitializable = Zenject.IInitializable;
using IState = _Scripts.Infrastructure.StateMachine.BaseStates.IState;

namespace _Scripts.Infrastructure.Bootstrappers
{
    public class GameFSMBootstrapper<TInitialState> : IInitializable
        where TInitialState : class, IState
    {
        private readonly DiContainer _diContainer;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IStateFactory _stateFactory;

        public GameFSMBootstrapper(DiContainer diContainer,
            IGameStateMachine gameStateMachine, 
            IStateFactory stateFactory)
        {
            _diContainer = diContainer;
            _gameStateMachine = gameStateMachine;
            _stateFactory = stateFactory;
        }

        public void Initialize()
        {
            _stateFactory.SetContainer(_diContainer);
            
            _gameStateMachine.Enter<TInitialState>()
                .Forget();
        }
    }
}