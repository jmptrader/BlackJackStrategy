using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardStrategy.Core
{
    public static class BettingStrategyHelper
    {
        public static decimal DetermineBet(decimal currentBet, 
            BettingStrategy bettingStrategy, bool playerWins, 
            decimal currentFunds, decimal targetFunds)
        {
            switch (bettingStrategy)
            {
                case BettingStrategy.Steady:
                    return currentBet;

                case BettingStrategy.DoubleOnLose:
                    return playerWins ? currentBet : currentBet * 2;

                case BettingStrategy.DoubleOnWin:
                    return playerWins ? currentBet * 2 : currentBet;

                case BettingStrategy.IncreaseOnLose:
                    return playerWins ? currentBet : currentBet + 1;

                case BettingStrategy.IncreaseOnWin:
                    return playerWins ? currentBet + 1 : currentBet;

                case BettingStrategy.Martingale:                    
                    return targetFunds - currentFunds;
            }

            return 0;
        }
    }
}
