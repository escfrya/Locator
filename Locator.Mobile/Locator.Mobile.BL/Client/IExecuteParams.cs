using Locator.Mobile.BL.Request;

namespace Locator.Mobile.BL.Client
{
    public interface IExecuteParams
    {
        string Address { get; set; }
        BaseRequest Request { get; set; }
        HTTPType Type { get; set; }
        byte[] ByteArray { get; set; }
    }
}
