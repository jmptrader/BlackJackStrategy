using CardStrategy.Core;
using CardStrategy.Core.Models;
using CardStrategy.Models;
using Microsoft.Extensions.Logging;
using MVVMShirt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardStrategy.Blazor.ViewModels
{
    public class StrategyViewModel
    {
        private readonly IRunAnalysis _runAnalysis;
        private readonly ILogger _logger;

        public StrategyViewModel(IRunAnalysis runAnalysis, ILogger<StrategyViewModel> logger)
        {
            _runAnalysis = runAnalysis;
            _logger = logger;
        }

        public List<AvailableAction> AvailableActions { get; set; } = new List<AvailableAction>();

        public IWebCommand ToggleActionCommand { get; set; }

        public IWebCommand RunAnalysisCommand { get; set; }

        public void Init()
        {
            AvailableActions = new List<AvailableAction>();
            for (int dlrCard = 2; dlrCard <= 11; dlrCard++)
            {
                for (int i = 4; i <= 21; i++)
                {
                    AvailableActions.Add(new AvailableAction()
                    {
                        PlayerValue = i,
                        PlayerAction = CardStrategy.Models.PlayerAction.Stand,
                        DealerCard = dlrCard
                    });
                }
            }

            ToggleActionCommand = new WebCommand<Guid>(ToggleAction, ToggleActionEnabled);
            RunAnalysisCommand = new WebCommand<object>(a => RunAnalysis());
        }

        private void RunAnalysis()
        {
            _logger.LogInformation($"RunAnalysis: Starting Funds {PlayerFunds}");

            var analysisConfiguration = new AnalysisConfiguration()
            {
                BettingStrategy = BettingStrategy,
                DeckCountPerShoe = 4,
                PlayerFunds = PlayerFunds,
                StartingAnte = 10,
                TargetFunds = TargetFunds,
                AvailableActions = AvailableActions
            };

            PlayerFunds = _runAnalysis.Run(analysisConfiguration);

            _logger.LogInformation($"RunAnalysis: End Funds: {PlayerFunds}");
        }

        private void ToggleAction(Guid key)
        {
            var action = AvailableActions.First(a => a.Key == key);
            action.PlayerAction = RotatePlayerAction(action.PlayerAction);
        }

        private PlayerAction RotatePlayerAction(PlayerAction playerAction)
        {
            var values = (PlayerAction[])Enum.GetValues(typeof(PlayerAction));
            System.Diagnostics.Debug.WriteLine($"Values {values}, length: {values.Length}");
            int idx = Array.FindIndex(values, v => v == playerAction);
            System.Diagnostics.Debug.WriteLine($"idx {idx}");
            if (idx < values.Length - 1) idx++;
            else idx = 0;
            var returnValue = values.ElementAt(idx);
            System.Diagnostics.Debug.WriteLine($"ReturnValue {returnValue}");

            return returnValue;
        }

        public bool ToggleActionEnabled(Guid key)
        {
            return true;
        }

        public decimal PlayerFunds { get; set; } = 100;

        public BettingStrategy BettingStrategy { get; set; }

        public decimal TargetFunds { get; set; } = 150;
    }
}
