using _Scripts.Infrastructure.StateMachine.BaseStates;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Scripts.Infrastructure.StateMachine.Factory
{
  public interface IStateFactory
  {
    UniTask<T> GetState<T>() where T : class, IExitableState;
    void SetContainer(DiContainer container);
  }
}