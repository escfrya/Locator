namespace Locator.Mobile.DAL
{
    public interface IBaseRepository<T>
    {
        T Save(T entity, string url);
        T Get(string url);
        void Delete(string url);
    }
}
