namespace _Scripts.Infrastructure.StateMachine.BaseStates
{
  public interface IPayloadState<TPayload> : IExitableState
  {
    void Enter(TPayload payload);
  }
}