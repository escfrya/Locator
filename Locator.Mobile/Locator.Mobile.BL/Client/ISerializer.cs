namespace Locator.Mobile.BL.Client
{
    /// <summary>
    /// Сериализатор
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Deserialize the specified content.
        /// </summary>
        /// <param name="content">Content.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        T Deserialize<T>(string content);
        /// <summary>
        /// Serialize the specified obj.
        /// </summary>
        /// <param name="obj">Object.</param>
        string Serialize(object obj);
    }
}
