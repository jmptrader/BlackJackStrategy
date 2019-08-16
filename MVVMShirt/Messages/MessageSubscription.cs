using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVVMShirt.Messages
{
    public class MessageSubscription
    {
        public MessageSubscription()
        {
            SubscriptionId = Guid.NewGuid();
        }

        public string EventKey { get; set; }
        public Guid SubscriptionId { get; set; }
        public Func<Message, Task> EventFunction { get; set; }
    }
}
