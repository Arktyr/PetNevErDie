
using Cysharp.Threading.Tasks;

namespace _Scripts.Infrastructure.StateMachine.BaseStates
{
  public interface IExitableState
  { 
    UniTask Exit();
    void EndExit();
  }
}