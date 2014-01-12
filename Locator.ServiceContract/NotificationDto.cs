using System.Collections.Generic;
using Locator.Entity.Entities;

namespace Locator.ServiceContract
{
    /// <summary>
    /// Сообщение для отправки пуш уведомлений
    /// </summary>
    public class NotificationDto
    {
        /// <summary>
        /// Текст уведомления
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Тип уведомления
        /// </summary>
        public NotificationType NotificationType { get; set; }

        /// <summary>
        /// Идентификатор объекта, по которому отсылается уведомление
        /// </summary>
        public string ObjectId { get; set; }

        /// <summary>
        /// Количество объектов
        /// </summary>
        public int Count { get; set; }
    }


    public class NotificationPackage
    {
        public NotificationPackage()
        {
            Notifications = new List<NotificationDto>();
        }

        public List<NotificationDto> Notifications { get; set; }
    }

    /// <summary>
    /// Тип уведомления
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// Отправка местоположения
        /// </summary>
        Location = 0,

        /// <summary>
        /// Приложение обновлено
        /// </summary>
        ApplicationUpdated
    }

    /// <summary>
    ///     Информация об устройстве
    /// </summary>
    public class DeviceDto
    {
        /// <summary>
        /// Идентификатор устройства
        /// </summary>
        public string DeviceAppId { get; set; }

        /// <summary>
        /// Устаревший идентификатор устройства
        /// </summary>
        public string OldDeviceAppId { get; set; }

        /// <summary>
        /// Версия клиента
        /// </summary>
        public string ClientVersion { get; set; }

        /// <summary>
        /// Тип платформы
        /// </summary>
        public PlatformType PlatformType { get; set; }
    }
}