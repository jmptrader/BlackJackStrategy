using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMShirt.Messages
{
    public class MessageBus : IMessageBus
    {
        private List<MessageSubscription> _subscriptions = new List<MessageSubscription>();

        public async Task Publish(string eventKey)
        {
            var targetSubscriptions = _subscriptions.Where(a => a.EventKey == eventKey);

            List<Task> events = new List<Task>();
            foreach (var subscription in targetSubscriptions)
            {
                var message = new Message();
                events.Add(subscription.EventFunction.Invoke(message));
            }

            await Task.WhenAll(events);
        }

        public Guid Subscribe(string eventKey, Func<Message, Task> onEvent)
        {
            var subscription = new MessageSubscription()
            {
                EventFunction = onEvent,
                EventKey = eventKey
            };

            _subscriptions.Add(subscription);

            return subscription.SubscriptionId;
        }

        public bool Unsubscribe(Guid subscriptionId)
        {
            var subscription = _subscriptions.SingleOrDefault(a => a.SubscriptionId == subscriptionId);
            if (subscription == null) return false;
            return _subscriptions.Remove(subscription);
        }
    }
}
