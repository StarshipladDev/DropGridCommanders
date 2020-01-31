using System;
namespace DropGrid.Core.Environment
{
    public class GameSession
    {
        private GameEnvironment Environment { get; }
        private GamePlayer[] Players { get; }

        public GameSession(GameEnvironment environment, GamePlayer[] players)
        {
            Environment = environment;
            Players = players;
        }
    }
}
