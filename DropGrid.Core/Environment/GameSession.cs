using System;
namespace DropGrid.Core.Environment
{
    public class CoreGameSession
    {
        private CoreGameEnvironment Environment { get; }
        private Player[] Players { get; }

        public CoreGameSession(CoreGameEnvironment environment, Player[] players)
        {
            Environment = environment;
            Players = players;
        }
    }
}
