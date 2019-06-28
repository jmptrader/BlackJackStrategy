using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardStrategy.Core.Models
{
    public class AnalysisConfiguration
    {
        public decimal PlayerFunds { get; set; } = 100;

        public BettingStrategy BettingStrategy { get; set; }

        public decimal TargetFunds { get; set; } = 150;

        public int DeckCountPerShoe { get; set; }

        public int StartingAnte { get; set; }

    }
}
