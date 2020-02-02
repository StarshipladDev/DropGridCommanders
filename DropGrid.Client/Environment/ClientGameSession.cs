using DropGrid.Core;

namespace DropGrid.Client.Environment
{
    public sealed class ClientGameSession : CoreGameSession
    {
        public new ClientGameEnvironment Environment
        {
            get => (ClientGameEnvironment)base.Environment;
            set => base.Environment = value;
        }

        public ClientGameSession(ClientGameEnvironment clientEnvironment, ClientPlayer[] players)
            : base(clientEnvironment, players)
        {
        }
    }
}
