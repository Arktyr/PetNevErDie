using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Scripts.Common.Logger;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Commands
{
    public class CommandExecutorService : ICommandExecutorService, IDisposable
    {
        [Inject] private DiContainer _diContainer;
        
        private readonly List<Command> _activeCommands = new();

        private CancellationTokenSource _ctsExecute = new();
        private CancellationTokenSource _ctsAbort = new();
        
        public async UniTask<CommandStatus> ExecuteCommand(Command command)
        {
            _diContainer.Inject(command);

            if (_ctsExecute == null || _ctsExecute.IsCancellationRequested)
                _ctsExecute = new CancellationTokenSource();
            
            _activeCommands.Add(command);
            
            var status = await command.Execute()
                .AttachExternalCancellation(_ctsExecute.Token);

            switch (status)
            {
                case CommandStatus.Failed:
                    await command.Abort()
                        .AttachExternalCancellation(_ctsAbort.Token);
                    break;
                case CommandStatus.Pending:
                    DebugExtensions.LogDetailed(command,
                        $"Command Was Executed, But His Status Still {status}, Maybe You Forgot Change Status");
                    break;
            }

            _activeCommands.Remove(command);
            
            return status;
        }

        public async UniTask AbortAllCommands()
        {
            CancelToken(ref _ctsExecute);
            
            if (_ctsAbort == null ||_ctsAbort.IsCancellationRequested)
                _ctsAbort = new CancellationTokenSource();

            List<UniTask> tasks = new();
            
            foreach (var command in _activeCommands) 
                tasks.Add(command.Abort()
                    .AttachExternalCancellation(_ctsAbort.Token));
            
            _activeCommands.Clear();
            
            await UniTask.WhenAll(tasks)
                .AttachExternalCancellation(_ctsAbort.Token);
        }

        public void ClearAndCancelAllCommands()
        {
            CancelToken(ref _ctsExecute);
            CancelToken(ref _ctsAbort);
            _activeCommands.Clear();
        }

        private void CancelToken(ref CancellationTokenSource cts)
        {
            if (cts == null || cts.IsCancellationRequested)
                return;
            
            cts?.Cancel();
            cts?.Dispose();
            cts = null;
        }

        public void Dispose() => 
            ClearAndCancelAllCommands();
    }
}