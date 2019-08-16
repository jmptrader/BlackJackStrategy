using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardStrategy.Core.Models
{
    public class UpdateProgress : IUpdateProgress
    {
        public Task Update(decimal current, decimal total)
        {
            throw new NotImplementedException();
        }
    }
}
