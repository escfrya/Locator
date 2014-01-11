using Locator.Mobile.BL.Client;

namespace Locator.Mobile.BL.ServiceClient
{
    public class BaseServiceCommand<TResult>
        where TResult : new()
    {
        protected readonly IRequestClient Client;
        public ExecuteParams ExecuteParams;

        public string Url
        {
            get { return ExecuteParams.Address; }
        }

        public BaseServiceCommand(IRequestClient client, ExecuteParams executeParams)
        {
            Client = client;
            ExecuteParams = executeParams;
        }

        public TResult Process()
        {
            return Client.Get<TResult>(ExecuteParams);
        }
    }
}
