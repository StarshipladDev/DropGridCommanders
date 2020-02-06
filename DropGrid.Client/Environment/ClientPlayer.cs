using DropGrid.Core.Environment;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.Environment
{
    public sealed class ClientPlayer : CorePlayer
    {
        internal Color FactionColor { get; set; }
        public ClientPlayer(string username, Color factionColor) : base(username)
        {
            FactionColor = factionColor;
        }

        public override string ToString()
        {
            return "ClientPlayer: " + Username;
        }
    }
}
