using Locator.Mobile.BL.Request;

namespace Locator.Mobile.BL.Client
{
    public class ExecuteParams : IExecuteParams
    {
        public ExecuteParams()
        {
            Type = HTTPType.GET;
        }

        public string Address { get; set; }
        public BaseRequest Request { get; set; }
        public HTTPType Type { get; set; }
        public byte[] ByteArray { get; set; }
    }
}
