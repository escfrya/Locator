using System;
using Locator.Mobile.BL;
using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.ServiceClient;

namespace Locator.Mobile.Presentation.Service
{
    public class ServiceRequest<TResponse> : TaskQueue
         where TResponse : new()
    {
        protected object Response;
        protected readonly BaseServiceCommand<TResponse> Command;
        private readonly Action<TResponse> result;
        private readonly IBaseView view;
        private readonly ICacheHelper cacheHelper;

        public ServiceRequest(IDispatcher dispatcher, IBaseView view, BaseServiceCommand<TResponse> command,
                              Action<TResponse> result, ICacheHelper cacheHelper)
            : base(dispatcher)
        {
            Command = command;
            this.result = result;
            this.view = view;
            this.cacheHelper = cacheHelper;
        }

        protected void CallService()
        {
            Run(() => Response = Command.Process());
        }
        protected void ProcessResult()
        {
            RunOnUI(() => { if (Response != null) result((TResponse) Response); });
        }

        protected void LockView()
        {
            RunOnUI(() => { if (view != null) view.IsBusy = true; });
        }

        protected void UnLockView()
        {
            RunOnUI(() => { if (view != null) view.IsBusy = false; });
        }

        protected void ProcessError()
        {
            ProcessError(error =>
            {
                var message = error.Message + Environment.NewLine + "Повторить попытку?";
                if (view != null && view.OkCancelDialog(message))
                {
                    Copy().Request();
                }
            });
        }

        protected void LocalGet()
        {
            Run(() => Response = cacheHelper.Get<TResponse>(Command.Url));
        }

        protected void LocalSave()
        {
            Run(() => cacheHelper.Save(Response, Command.Url));
        }

        public void Request()
        {
            if (Command.ExecuteParams.Type == HTTPType.GET)
                CachedRequest();
            else
                SimpleRequest();
        }

        public void SimpleRequest()
        {
            LockView();
            CallService();
            ProcessResult();
            UnLockView();
            ProcessError();
            Start();
        }
        public void CachedRequest()
        {
            LockView();
            LocalGet();
            ProcessResult();
            CallService();
            ProcessResult();
            LocalSave();
            UnLockView();
            ProcessError();
            Start();
        }
        private ServiceRequest<TResponse> Copy()
        {
            return new ServiceRequest<TResponse>(Dispatcher, view, Command, result, cacheHelper);
        }
    }
}
