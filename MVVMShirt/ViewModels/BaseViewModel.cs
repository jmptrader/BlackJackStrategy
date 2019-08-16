using MVVMShirt.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMShirt.ViewModels
{
    public abstract class BaseViewModel
    {
        protected IMessageBus MessageBus { get; set; }

        public BaseViewModel(IMessageBus messageBus)
        {
            MessageBus = messageBus;
        }
    }
}
