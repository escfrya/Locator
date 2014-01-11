namespace Locator.Mobile.BL.Client
{
    public interface IRequestClient
    {
        TRes Get<TRes>(ExecuteParams param);
        void Get(ExecuteParams param);
    }
}
