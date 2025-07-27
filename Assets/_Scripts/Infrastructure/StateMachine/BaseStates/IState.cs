namespace _Scripts.Infrastructure.StateMachine.BaseStates
{
  public interface IState: IExitableState
  {
    void Enter();
  }
}