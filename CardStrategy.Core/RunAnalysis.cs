using CardStrategy.Common.Extensions;
using CardStrategy.Core.Models;
using CardStrategy.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardStrategy.Core
{
    public class RunAnalysis : IRunAnalysis
    {
        private readonly ILogger<RunAnalysis> _logger;
        private readonly IPlayHand _playHand;

        public RunAnalysis(ILogger<RunAnalysis> logger, IPlayHand playHand)
        {
            _logger = logger;
            _playHand = playHand;
        }

        public async Task<decimal> Run(AnalysisConfiguration analysisConfiguration, IUpdateProgress updateProgress)
        {            
            decimal currentBet = analysisConfiguration.StartingAnte;

            _logger.LogInformation($"Run: Start currentBet: {currentBet}");

            // Create the dealer
            var dealer = new Dealer()
            {
                Money = 9999999999 // House has infinite funds
            };

            // Create the table
            var table = new Table(dealer);

            // Create and shuffle the shoe
            var shoe = new Shoe(4);
            shoe.Cards.Shuffle();

            // Create the player and sit at the table
            var player = new Player()
            {
                Money = analysisConfiguration.PlayerFunds,
                Ante = analysisConfiguration.StartingAnte,
                Strategy = BuildStrategy(
                    analysisConfiguration.AvailableActions,
                    Deck.CreateDeck().Cards)
            };

            table.SitPlayer(player);            

            // Play each hand
            _playHand.Init(shoe.Cards, table);

            while (player.Money > 0 && player.Money < analysisConfiguration.TargetFunds && currentBet <= player.Money)
            {
                await updateProgress.Update(player.Money, analysisConfiguration.TargetFunds);

                _playHand.PlayerAnte(player, currentBet);
                
                _playHand.Deal();
                _logger.LogInformation($"Run: Dealt Players {string.Join(", ", _playHand.Table.Players.SelectMany(a => a.Hand.Cards).Select(a => a.Description))}");
                _logger.LogInformation($"Run: Dealt Dealer {string.Join(", ", _playHand.Table.Dealer.Hand.Cards.Select(a => a.Description))}");

                while (_playHand.GameInProgress)
                {
                    _playHand.Play();
                }

                _logger.LogInformation($"Run: Play Finished Players {string.Join(", ", _playHand.Table.Players.SelectMany(a => a.Hand.Cards).Select(a => a.Description))}");
                _logger.LogInformation($"Run: Play Finished Dealer {string.Join(", ", _playHand.Table.Dealer.Hand.Cards.Select(a => a.Description))}");

                bool playerWins = _playHand.Payout();
                currentBet = BettingStrategyHelper.DetermineBet(
                    currentBet, analysisConfiguration.BettingStrategy, playerWins, 
                    analysisConfiguration.TargetFunds, player.Money);                

                _logger.LogInformation($"Run: Player Funds {string.Join(", ", _playHand.Table.Players.Select(a => a.Money))}");
            }

            return player.Money;
        }

        private BlackJackStrategy BuildStrategy(
            IEnumerable<AvailableAction> actions, IEnumerable<Card> allCards)
        {
            var strategy = new BlackJackStrategy()
            {
                StrategyItems = new List<BlackJackStrategyItem>()
            };

            foreach (var action in actions)
            {
                List<Card> dealerCards = GetAllCardsForValue(
                    action.DealerCard, allCards);

                foreach (var card in dealerCards)
                {
                    var strategyItem = new BlackJackOverrideStrategyItem()
                    {
                        Action = action.PlayerAction,
                        DealerCard = card,
                        PlayerCardValues = new[] { action.PlayerValue }.ToList(),
                        PlayerTotalCardValue = action.PlayerValue
                    };
                    strategy.StrategyItems.Add(strategyItem);
                }
            }

            return strategy;
        }

        private List<Card> GetAllCardsForValue(int cardValue, IEnumerable<Card> allCards)
        {
            var matchingCardList = new List<Card>();
            foreach (var card in allCards)
            {
                if (card.Values.Contains(cardValue))
                {
                    matchingCardList.Add(card);
                }
            }

            return matchingCardList;
        }
    }
}
