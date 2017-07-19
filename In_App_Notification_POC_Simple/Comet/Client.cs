using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services.Description;

namespace In_App_Notification_POC_Simple.Comet
{
    public class Client
    {
        private ManualResetEvent messageEvent = new ManualResetEvent(false);
        private Queue<Message> _managerMessageQueue = new Queue<Message>();
        private Queue<Message> _employeeMessageQueue = new Queue<Message>();

        public bool EmployeeEnqueueMessage(Message message)
        {
            lock (_employeeMessageQueue)
            {
                _employeeMessageQueue.Enqueue(message);
                var state = messageEvent.Set();
                return state;
            }

        }
        public bool EmployeeDequeueMessage()
        {
            messageEvent.WaitOne();
            lock (_employeeMessageQueue)
            {
                if (_employeeMessageQueue.Count > 0)
                {
                    messageEvent.Reset();
                }
                return _employeeMessageQueue.Count > 0;
            }
        }
        public bool ManagerEnqueueMessage(Message message)
        {
            lock (_managerMessageQueue)
            {
                _managerMessageQueue.Enqueue(message);
                var state = messageEvent.Set();
                return state;
            }

        }
        public bool ManagerDequeueMessage()
        {
            messageEvent.WaitOne();
            lock (_managerMessageQueue)
            {
                if (_managerMessageQueue.Count == 1)
                {
                    messageEvent.Reset();
                }
                return _managerMessageQueue.Count > 0;
            }
        }
    }

}