using System;
using System.Threading;
using _Scripts.Game.GameStatus;
using _Scripts.Infrastructure.StateMachine.BaseStates;
using _Scripts.Infrastructure.StateMachine.Factory;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Scripts.Infrastructure.StateMachine
{
  public class GameStateMachine : IGameStateMachine, ITickable, IDisposable
  {
    private readonly IStateFactory _stateFactory;
    private readonly IGameStatusService _gameStatusProvider; 
      
    private IExitableState _activeState;

    private readonly CancellationTokenSource _cts = new();

    public GameStateMachine(IStateFactory stateFactory, 
      IGameStatusService gameStatusProvider)
    {
      _stateFactory = stateFactory;
      _gameStatusProvider = gameStatusProvider;
    }

    public void Tick()
    {
      if (_gameStatusProvider.CurrentGameStatus == GameStatus.Pause)
        return;

      if(_activeState is IUpdateable updateableState)
        updateableState.Update();
    }

    public async UniTask Enter<TState>() where TState : class, IState
    {
       var state = await RequestEnter<TState>()
         .AttachExternalCancellation(_cts.Token);
       
       EnterState(state);
    }

    public async UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
    {
      var state = await RequestEnter<TState, TPayload>()
        .AttachExternalCancellation(_cts.Token);
      
      EnterPayloadState(state, payload);
    }

    private async UniTask<TState> RequestEnter<TState>() where TState : class, IState
    {
      var state = await RequestChangeState<TState>()
        .AttachExternalCancellation(_cts.Token);
      
      return state;
    }

    private async UniTask<TState> RequestEnter<TState, TPayload>()
      where TState : class, IPayloadState<TPayload>
    {
      var state = await RequestChangeState<TState>()
        .AttachExternalCancellation(_cts.Token);
      
      return state;
    }
    
    private TState EnterState<TState>(TState state) where TState : class, IState
    {
      _activeState = state;

      state.Enter();
      return state;
    }

    private TState EnterPayloadState<TState, TPayload>(TState state, TPayload payload) where TState : class, IPayloadState<TPayload>
    {
      _activeState = state;

      state.Enter(payload);
      return state;
    }
    
    private async UniTask<TState> RequestChangeState<TState>() where TState : class, IExitableState
    {
      if (_activeState == null) 
        return await GetState<TState>()
          .AttachExternalCancellation(_cts.Token);

      await _activeState.Exit()
        .AttachExternalCancellation(_cts.Token);
      
      _activeState.EndExit();

      return await GetState<TState>()
        .AttachExternalCancellation(_cts.Token);
    }

    private async UniTask<TState> GetState<TState>() where TState : class, IExitableState
    {
      TState state = await _stateFactory.GetState<TState>()
        .AttachExternalCancellation(_cts.Token);
      
      return state;
    }

    public void Dispose() => 
      _cts?.Dispose();
  }
}