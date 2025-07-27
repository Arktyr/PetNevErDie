using Cysharp.Threading.Tasks;

namespace _Scripts.Infrastructure.StateMachine.BaseStates
{
  public interface IState: IExitableState
  {
    UniTask Enter();
  }
}