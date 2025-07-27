using _Scripts.Infrastructure.StateMachine.BaseStates;
using Cysharp.Threading.Tasks;

namespace _Scripts.Infrastructure.StateMachine
{
  public interface IGameStateMachine 
  {
    UniTask Enter<TState>() where TState : class, IState;
    UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
  }
}