using System;
using System.Collections.Generic;
using DropGrid.Core.Logic;

namespace DropGrid.Core.Environment
{
    public abstract class CoreGameEnvironment
    {
        public static readonly ActionType[] ACTION_ORDER = {
            ActionType.UNIT_DEPLOYMENT,
            ActionType.UNIT_ATTACK,
            ActionType.UNIT_MOVE,
            ActionType.ABILITY_DEPLOYMENT,
        };

        private Map Map { get; set; }
        private GameEntityManager Entities { get; }

        public CoreGameEnvironment()
        {
            Entities = new GameEntityManager();
        }
    }
}
