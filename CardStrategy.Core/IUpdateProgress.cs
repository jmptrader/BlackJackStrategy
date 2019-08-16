using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardStrategy.Core
{
    public interface IUpdateProgress
    {
        Task Update(decimal current, decimal total);
    }
}
