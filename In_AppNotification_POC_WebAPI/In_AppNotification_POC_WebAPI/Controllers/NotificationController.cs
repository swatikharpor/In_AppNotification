using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using In_AppNotification_POC_WebAPI.Models;
using In_App_Notification_POC_Simple.Models;


namespace In_AppNotification_POC_WebAPI.Controllers
{
    [RoutePrefix("notifications")]
    public class NotificationController : ApiController
    {
        private readonly InAppNotificationEntities _inAppNotificationEntities = new InAppNotificationEntities();



        [HttpGet, Route("{userId}/unread")]
        public List<TaskNotification> GetUnreadNotification(string userId)
        {
            var notifications = _inAppNotificationEntities.TaskNotifications.Where(notification => notification.IsRead == false && notification.NotificationTo.Equals(userId)).ToList();
            return notifications;
        }

        [HttpPost, Route("")]
        public bool AddNotification(TaskNotification notification)
        {
            _inAppNotificationEntities.TaskNotifications.Add(notification);
            var response = _inAppNotificationEntities.SaveChanges();
            return response > 0;
        }

        [HttpGet, Route("{notificationId}")]
        public TaskNotification GetNotificationById(string notificationId)
        {
            var notification = _inAppNotificationEntities.TaskNotifications.FirstOrDefault(x => x.Id.Equals(notificationId));
            if (notification != null) notification.IsRead = true;
            _inAppNotificationEntities.SaveChanges();
            return notification;
        }

        [HttpGet, Route("{userId}/unread/count")]
        public int GetUnreadNotificationCount(string userId)
        {
            return
                _inAppNotificationEntities.TaskNotifications.Count(notification => notification.IsRead == false && notification.NotificationTo.Equals(userId));
        }

        [HttpGet, Route("{userId}/topten")]
        public List<NotificationDm> GetTopTenNotification(string userId)
        {
            var notifications =
                _inAppNotificationEntities.TaskNotifications.Where(
                        notification => notification.IsRead == false && notification.NotificationTo.Equals(userId))
                    .Take(10)
                    .ToList()
                    .Select(x => new NotificationDm()
                    {
                        Id = x.Id,
                        NotificationTo = x.NotificationTo,
                        NotificationFrom = x.NotificationFrom,
                        NotificationMessage = x.NotificationMessage,
                        NotificationType = x.NotificationType
                    }).ToList();
            return notifications;
        }
    }
}