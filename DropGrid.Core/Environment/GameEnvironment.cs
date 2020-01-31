using System;
using System.Collections.Generic;
using DropGrid.Core.Logic;

namespace DropGrid.Core.Environment
{
    public class GameEnvironment
    {
        public static readonly ActionType[] ACTION_ORDER = {
            ActionType.UNIT_DEPLOYMENT,
            ActionType.UNIT_ATTACK,
            ActionType.UNIT_MOVE,
            ActionType.ABILITY_DEPLOYMENT,
        };

        private GameMap Map { get; }
        private GameEntityManager Entities { get; }

        public GameEnvironment(GameMap map)
        {
            Map = map;
            Entities = new GameEntityManager();
        }
    }
}
