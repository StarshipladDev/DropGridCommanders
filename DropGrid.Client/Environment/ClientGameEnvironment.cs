using DropGrid.Core.Environment;

namespace DropGrid.Client.Environment
{
    public sealed class ClientGameEnvironment : CoreGameEnvironment
    {
        public new ClientMap Map {
            get => (ClientMap)base.Map;
            set => base.Map = value;
        }
    }
}
