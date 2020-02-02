using System;
using DropGrid.Core.Environment;

namespace DropGrid.Core
{
    /// <summary>
    /// Represents one game session between the <c cref="CorePlayer">players</c>,
    /// with information about the game rules, progression and the game world.
    /// </summary>
    public class CoreGameSession
    {
        // TODO: Find a proper place to put this.
        public static readonly ActionType[] ACTION_ORDER = {
            ActionType.UnitDeployment,
            ActionType.UnitAttack,
            ActionType.UnitMove,
            ActionType.AbilityDeployment,
        };
        
        /// <summary>
        /// The game environment in this session.
        /// </summary>
        protected CoreGameEnvironment Environment { get; set; }
        
        /// <summary>
        /// List of participating players in this session.
        /// </summary>
        private CorePlayer[] Players { get; }

        public CoreGameSession(CoreGameEnvironment environment, CorePlayer[] players)
        {
            Environment = environment;
            Players = players;
        }
    }
}
