﻿using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardStrategy.Models
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
        public int DealerCard { get; set; }
    }
}
