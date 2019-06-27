using CardStrategy.Blazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardStrategy.Blazor.ViewModels
{
    public class StrategyViewModel
    {
        public List<AvailableAction> AvailableActions { get; set; } = new List<AvailableAction>();

        public void Init()
        {
            AvailableActions = new List<AvailableAction>();
            for (int i = 4; i <= 21; i++)
            {
                AvailableActions.Add(new AvailableAction()
                {                    
                    PlayerValue = i,
                    PlayerAction = CardStrategy.Models.PlayerAction.Stand
                });
            }
        }

        public void ToggleAction(Guid key)
        {

        }
    }
}
