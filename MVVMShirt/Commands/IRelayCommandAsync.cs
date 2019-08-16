using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVVMShirt.Commands
{
    public interface IRelayCommandAsync
    {
        Task Execute(object param);
        bool CanExecute(object param);
    }
}
