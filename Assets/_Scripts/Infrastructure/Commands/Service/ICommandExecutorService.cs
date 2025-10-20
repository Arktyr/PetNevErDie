using Cysharp.Threading.Tasks;

namespace _Scripts.Infrastructure.Commands
{
    public interface ICommandExecutorService
    {
        UniTask<CommandStatus> ExecuteCommand(Command command);
        UniTask AbortAllCommands();
        void ClearAndCancelAllCommands();
    }
}