using System;
using DropGrid.Client.Asset;
using DropGrid.Client.Environment;
using DropGrid.Core.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.Graphics
{
    public class EnvironmentRenderer
    {
        private EnvironmentRenderer() { }

        public static void Render(GameEngine engine, GraphicsRenderer renderer,
            ClientGameEnvironment clientEnvironment, GameTime gameTime)
        {
            ClientMap map = clientEnvironment.Map;
            if (map != null)
            {
                MapRenderer.Render(engine, renderer, map, gameTime);
            }
        }

        public static void Update(GameEngine engine, ClientGameEnvironment clientEnvironment, GameTime gameTime)
        {
            ClientMap map = clientEnvironment.Map;
            if (map != null)
            {
                MapRenderer.Update(engine, gameTime, map);
            }
        }
    }
}
