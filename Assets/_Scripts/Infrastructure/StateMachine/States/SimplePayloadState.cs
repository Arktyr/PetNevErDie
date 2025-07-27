using _Scripts.Infrastructure.StateMachine.BaseStates;
using Cysharp.Threading.Tasks;

namespace _Scripts.Infrastructure.StateMachine.States
{
  public class SimplePayloadState<TPayload> : IPayloadState<TPayload>
  {
    public virtual void Enter(TPayload payload)
    {
    }

    public virtual UniTask Exit()
    {
      return UniTask.CompletedTask;
    }

    void IExitableState.EndExit()
    {
      
    }
  }
}