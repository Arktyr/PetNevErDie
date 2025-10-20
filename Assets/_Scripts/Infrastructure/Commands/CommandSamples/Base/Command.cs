using Cysharp.Threading.Tasks;

namespace _Scripts.Infrastructure.Commands
{
    public abstract class Command
    {
        public CommandStatus Status { get; private set; } = CommandStatus.Pending;
        public abstract UniTask<CommandStatus> Execute();
        public abstract UniTask Abort();
    }
}