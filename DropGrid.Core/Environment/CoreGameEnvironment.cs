using System;
using System.Collections.Generic;

namespace DropGrid.Core.Environment
{
    public class CoreGameEnvironment
    {
        public static readonly ActionType[] ACTION_ORDER = {
            ActionType.UNIT_DEPLOYMENT,
            ActionType.UNIT_ATTACK,
            ActionType.UNIT_MOVE,
            ActionType.ABILITY_DEPLOYMENT,
        };

        public CoreMap Map { get; set; }

        public CoreGameEnvironment()
        {
            
        }
    }
}
