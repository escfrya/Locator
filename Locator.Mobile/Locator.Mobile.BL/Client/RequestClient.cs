using System;
using System.Net;
using Locator.Mobile.BL.Exceptions;
using Locator.Mobile.BL.Response;
using Locator.Mobile.DAL;
using Locator.Mobile.DAL.Tables;

namespace Locator.Mobile.BL.Client
{
    public abstract class RequestClient : IRequestClient
    {
        protected readonly ISerializer Serializer;

        protected RequestClient(ISerializer serializer)
        {
            Serializer = serializer;
        }

        protected abstract RequestResponse Execute(ExecuteParams param);

        private RequestResponse ProcessRequest(ExecuteParams param)
        {
            RequestResponse result = null;
            try
            {
                result = Execute(param);
            }
			catch (Exception ex)
            {
            }

            ProcessErrors(result);
            return result;
        }

        /// <summary>
        /// Обработка ошибок
        /// </summary>
        private void ProcessErrors(RequestResponse response)
        {
            if (response == null)
                throw new NetworkException("Network problem");

            // если код ошибки положительный
            if ((int)response.StatusCode >= (int)HttpStatusCode.OK && (int)response.StatusCode < (int)HttpStatusCode.Ambiguous)
                return;

            var message = response.StatusCode.ToString() + " " + response.Content;

            if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                throw new NotLoggedException(message);

            throw new HttpCodeException(response.StatusCode, message);
        }

        public void Get(ExecuteParams param)
        {
            ProcessRequest(param);
        }

        public TRes Get<TRes>(ExecuteParams param)
        {
            var response = ProcessRequest(param);
            try
            {
                return Serializer.Deserialize<TRes>(response.Content);
            }
            catch (Exception exception)
            {
                throw new NetworkException(exception.Message);
            }
        }
    }

    public interface ICacheHelper
    {
        T Get<T>(string url);
        void Save<T>(T entity, string url);
    }

    public class CacheHelper : ICacheHelper
    {
        private readonly IRequestsRepository requestsRepository;
        protected readonly ISerializer serializer;

        public CacheHelper(ISerializer serializer, IRequestsRepository requestsRepository)
        {
            this.serializer = serializer;
            this.requestsRepository = requestsRepository;
        }

        public T Get<T>(string url)
        {
            var json = requestsRepository.Get(url);
            if (json == null) return default(T);
            return serializer.Deserialize<T>(json.Response);
        }

        public void Save<T>(T entity, string url)
        {
            var request = new Requests {RequestUrl = url, Response = serializer.Serialize(entity)};
            requestsRepository.Save(request, url);
        }
    }
}
