using System;
using DropGrid.Core.Environment;

namespace DropGrid.Core
{
    public class CoreGameSession
    {
        protected CoreGameEnvironment Environment { get; }
        private CorePlayer[] Players { get; }

        public CoreGameSession(CoreGameEnvironment environment, CorePlayer[] players)
        {
            Environment = environment;
            Players = players;
        }
    }
}
