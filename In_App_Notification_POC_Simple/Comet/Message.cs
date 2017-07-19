using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace In_App_Notification_POC_Simple.Comet
{
    public class Message
    {
        /// <summary>
        /// The name who will receive this message.
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// The message content.
        /// </summary>
        public string MessageContent { get; set; }
    }
}