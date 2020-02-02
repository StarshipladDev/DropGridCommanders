using DropGrid.Client.Environment;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.Graphics
{
    public static class EnvironmentRenderer
    {
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
