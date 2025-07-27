using System;
using System.Threading;
using _Scripts.Infrastructure.StateMachine.BaseStates;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Scripts.Infrastructure.StateMachine.Factory
{
  public class StateFactory : IStateFactory, IDisposable
  {
    private readonly DiContainer _container;

    private readonly CancellationTokenSource _cts = new();

    public StateFactory(DiContainer container)
    {
      _container = container;
    }

    public async UniTask<T> GetState<T>() where T : class, IExitableState
    {
      await UniTask.WaitUntil(() => _container.HasBinding<T>(), PlayerLoopTiming.Update, _cts.Token);
      
      return _container.Resolve<T>();
    }

    public void Dispose() => 
      _cts?.Dispose();
  }
}