using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVVMShirt.Messages
{
    public interface IMessageBus
    {
        Guid Subscribe(string eventKey, Func<Message, Task> onEvent);

        Task Publish(string eventKey);

        bool Unsubscribe(Guid subscriptionKey);
    }
}
