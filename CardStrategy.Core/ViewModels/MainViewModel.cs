using CardStrategy.Core.Models;
using CardStrategy.Models;
using Microsoft.Extensions.Logging;
using MVVMShirt.Commands;
using MVVMShirt.Messages;
using MVVMShirt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardStrategy.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IRunAnalysis _runAnalysis;
        private readonly ILogger _logger;

        public MainViewModel(IRunAnalysis runAnalysis, ILogger<MainViewModel> logger, IMessageBus messageBus) 
            : base(messageBus)
        {
            _runAnalysis = runAnalysis;
            _logger = logger; 
        }

        public List<AvailableAction> AvailableActions { get; set; } = new List<AvailableAction>();

        public IRelayCommand ToggleActionCommand { get; set; }

        public IRelayCommandAsync RunAnalysisCommand { get; set; }

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

            ToggleActionCommand = new RelayCommand<Guid>(ToggleAction, ToggleActionEnabled);
            RunAnalysisCommand = new RelayCommandAsync<object>(a => RunAnalysis());

            MessageBus.Subscribe("LOG_MESSAGE", UpdateLog);
        }

        private Task UpdateLog(Message arg)
        {
            throw new NotImplementedException();
        }

        private async Task RunAnalysis()
        {
            LogInformation($"RunAnalysis: Starting Funds {PlayerFunds}");

            var analysisConfiguration = new AnalysisConfiguration()
            {
                BettingStrategy = BettingStrategy,
                DeckCountPerShoe = 4,
                PlayerFunds = PlayerFunds,
                StartingAnte = 10,
                TargetFunds = TargetFunds,
                AvailableActions = AvailableActions
            };

            var updateProgress = new UpdateProgress();
            PlayerFunds = await _runAnalysis.Run(analysisConfiguration, updateProgress);

            LogInformation($"RunAnalysis: End Funds: {PlayerFunds}");
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

        public decimal PlayerFunds { get; set; } = 1000;

        public BettingStrategy BettingStrategy { get; set; }

        public decimal TargetFunds { get; set; } = 1500;

        public List<LogEntry> Log { get; set; } = new List<LogEntry>();

        private void LogInformation(string message)
        {
            _logger.LogInformation(message);
            Log.Add(new LogEntry(message));
        }
    }
}
