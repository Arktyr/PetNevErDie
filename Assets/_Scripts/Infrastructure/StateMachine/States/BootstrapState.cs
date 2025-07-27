using _Scripts.Infrastructure.StateMachine.BaseStates;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
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