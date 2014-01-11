using System;
using System.Threading;
using System.Threading.Tasks;

namespace Locator.Mobile.Presentation.Service
{
    public class TaskQueue : IDisposable
    {
        protected readonly IDispatcher Dispatcher;
        private Task task;
        private Task startTask;
        private readonly CancellationTokenSource tokenSource;
        private CancellationToken token;
        private Exception exception;

        public TaskQueue(IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
        }

        protected void Run(Action action)
        {
            if (startTask == null)
            {
                startTask = new Task(action, token);
                task = startTask;
            }
            else
            {
                task = task.ContinueWith(t =>
                {
                    if (token.IsCancellationRequested)
                        token.ThrowIfCancellationRequested();

                    try
                    {
                        action();
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }
                }, token);
            }

        }

        protected void RunOnUI(Action action)
        {
            Run(() => Dispatcher.Invoke(action));
        }

        protected void ProcessError(Action<Exception> action)
        {
            RunOnUI(() =>
            {
                if (exception != null)
                    action(exception);
            });
        }

        public void Start()
        {
            startTask.Start();
        }

        public void Cancel()
        {
            if (!tokenSource.IsCancellationRequested)
                tokenSource.Cancel();
        }

        public void Dispose()
        {
            Cancel();
        }
    }
}
