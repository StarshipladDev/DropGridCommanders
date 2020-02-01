using System;
using DropGrid.Client.Environment;
using DropGrid.Core;
using DropGrid.Core.Environment;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.Environment
{
    public class ClientGameSession : CoreGameSession
    {
        public new ClientGameEnvironment Environment
        {
            get { return (ClientGameEnvironment)base.Environment; }
            set { Environment = value; }
        }

        public ClientGameSession(ClientGameEnvironment clientEnvironment, CorePlayer[] players)
            : base(clientEnvironment, players)
        {
        }
    }
}
