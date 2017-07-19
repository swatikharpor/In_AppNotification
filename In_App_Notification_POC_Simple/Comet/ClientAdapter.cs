using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace In_App_Notification_POC_Simple.Comet
{
    public class ClientAdapter
    {
        private Dictionary<string, Client> recipients = new Dictionary<string, Client>();
        private Dictionary<string, Client> newMessageRecipients = new Dictionary<string, Client>();

        /// <summary>
        /// Send a message to a particular recipient.
        /// </summary>
        public bool EmployeeSendMessage(Message message)
        {
            var state = new bool();
            if (recipients.Count > 0)
            {
                if (recipients.ContainsKey(message.RecipientName))
                {
                    Client client = recipients[message.RecipientName];

                    state = client.EmployeeEnqueueMessage(message);
                    JoinNewMessage(message.RecipientName.Trim());
                }
            }
            return state;
        }

        /// <summary>
        /// Called by a individual recipient to wait and receive a message.
        /// </summary>
        /// <returns>The message content</returns>
        public bool EmployeeGetMessage(string userName)
        {
            var state = new bool();
            if (newMessageRecipients.Count > 0)
            {

                if (newMessageRecipients.ContainsKey(userName))
                {
                    Client client = recipients[userName];

                    state = client.EmployeeDequeueMessage();
                }
            }
            return state;
        }
        public bool ManagerSendMessage(Message message)
        {
            var state = new bool();
            if (recipients.ContainsKey(message.RecipientName))
            {
                Client client = recipients[message.RecipientName];

                state = client.ManagerEnqueueMessage(message);
                JoinNewMessage(message.RecipientName.Trim());
            }
            return state;
        }

        /// <summary>
        /// Called by a individual recipient to wait and receive a message.
        /// </summary>
        /// <returns>The message content</returns>
        public bool ManagerGetMessage(string userName)
        {
            var state = new bool();

            if (newMessageRecipients.ContainsKey(userName))
            {
                Client client = recipients[userName];

                state = client.ManagerDequeueMessage();
            }

            return state;
        }
        /// <summary>
        /// Join a user to the recipient list.
        /// </summary>
        public void Join(string userName)
        {
            recipients[userName] = new Client();
        }
        public void JoinNewMessage(string userName)
        {
            newMessageRecipients[userName] = new Client();
        }

        /// <summary>
        /// Singleton pattern.
        /// This pattern will ensure there is only one instance of this class in the system.
        /// </summary>
        public static ClientAdapter Instance = new ClientAdapter();
        private ClientAdapter() { }
    }
}
