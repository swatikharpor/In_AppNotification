namespace In_AppNotification_POC_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Notification")]
    public partial class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string NotificationType { get; set; }

        [Required]
        [StringLength(10)]
        public string NotificationMessage { get; set; }

        [Required]
        [StringLength(20)]
        public string From { get; set; }

        public bool IsRead { get; set; }
    }
}
