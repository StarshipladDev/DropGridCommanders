using DropGrid.Client.Environment;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.Graphics.Renderer
{
    public static class GameSessionRenderer
    {
        public static void Render(GameEngine engine, GraphicsRenderer renderer, ClientGameSession session, GameTime gameTime)
        {
            ClientGameEnvironment environment = session.Environment;
            if (environment != null)
            {
                EnvironmentRenderer.Render(engine, renderer, environment, gameTime);
            }
        }

        public static void Update(GameEngine engine, ClientGameSession session, GameTime gameTime)
        {
            ClientGameEnvironment environment = session.Environment;
            if (environment != null)
            {
                EnvironmentRenderer.Update(engine, environment, gameTime);
            }
        }
    }
}
