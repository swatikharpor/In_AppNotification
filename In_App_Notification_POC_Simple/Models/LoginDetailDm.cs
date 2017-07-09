using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace In_App_Notification_POC_Simple.Models
{
    public class LoginDetailDm
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public ICollection<NotificationDm> TaskNotifications { get; set; }
        public ICollection<NotificationDm> TaskNotifications1 { get; set; }
    }
}