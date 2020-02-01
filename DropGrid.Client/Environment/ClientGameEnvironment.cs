using System;
using DropGrid.Core.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.Environment
{
    public class ClientGameEnvironment : CoreGameEnvironment
    {
        public new ClientMap Map {
            get { return (ClientMap)base.Map; }
            set { base.Map = value; }
        }

        public ClientGameEnvironment()
        {
        }
    }
}
