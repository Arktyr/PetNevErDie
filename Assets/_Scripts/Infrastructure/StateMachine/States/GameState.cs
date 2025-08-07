using _Scripts.Infrastructure.StateMachine.BaseStates;
using Cysharp.Threading.Tasks;

namespace _Scripts.Infrastructure.StateMachine.States
{
    public class GameState : IState
    {
        public UniTask Exit()
        {
            return UniTask.CompletedTask;

        }

        public void EndExit()
        {
            
        }

        public UniTask Enter()
        {
            return UniTask.CompletedTask;
        }
    }
}