
using System.ComponentModel.DataAnnotations;


namespace In_App_Notification_POC_Simple.Models
{
    public class NotificationDm
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(10)]
        public string NotificationType { get; set; }

        [Required]
        [StringLength(10)]
        public string NotificationMessage { get; set; }

        [Required]
        [StringLength(20)]
        public string NotificationFrom { get; set; }
        public string NotificationTo { get; set; }
        public string TotalUnreadNotification { get; set; }
        public bool IsRead { get; set; }
    }
}