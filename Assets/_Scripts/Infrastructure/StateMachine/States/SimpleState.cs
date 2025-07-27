

using _Scripts.Infrastructure.StateMachine.BaseStates;
using Cysharp.Threading.Tasks;

namespace _Scripts.Infrastructure.StateMachine.States
{
  public class SimpleState : IState
  {
    public virtual void Enter()
    {
    }

    public virtual UniTask Exit()
    {
      return UniTask.CompletedTask;
    }

    void IExitableState.EndExit(){}
  }
}