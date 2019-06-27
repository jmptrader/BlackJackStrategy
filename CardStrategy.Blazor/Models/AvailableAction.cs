using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardStrategy.Blazor.Models
{
    public class AvailableAction
    {
        public AvailableAction()
        {
            Key = Guid.NewGuid();
        }
        public Guid Key { get; set; }
        public PlayerAction PlayerAction { get; set; }
        public int PlayerValue { get; set; }
    }
}
