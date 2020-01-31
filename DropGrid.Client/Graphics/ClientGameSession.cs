using System;
using DropGrid.Core.Environment;

namespace DropGrid.Client.Graphics
{
    public class ClientGameSession
    {
        private CoreGameSession coreGameSession;

        private ClientGameEnvironment environment;
        private EnvironmentRenderer environmentRenderer;

        public ClientGameSession(ClientGameEnvironment clientEnvironment, Player[] players)
        {
            environment = clientEnvironment;
            environmentRenderer = new EnvironmentRenderer();

            coreGameSession = new CoreGameSession(clientEnvironment, players);
        }

        public void Render(GraphicsRenderer renderer, GameEngine engine)
        {

        }
    }
}
