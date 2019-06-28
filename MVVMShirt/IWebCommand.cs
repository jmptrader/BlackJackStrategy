using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMShirt
{
    public interface IWebCommand
    {
        void Execute(object param);
        bool CanExecute(object param);
    }
}
