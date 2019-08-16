using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMShirt.Commands
{
    public interface IRelayCommand
    {
        void Execute(object param);
        bool CanExecute(object param);
    }
}
