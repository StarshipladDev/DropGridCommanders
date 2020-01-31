using System;
using DropGrid.Client.Map;
using DropGrid.Core.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.Environment
{
    public class GameEnvironment
    {
        public Camera Camera { get; }

        public GameMap Map { get; internal set; }
        public GameMapRenderer MapRenderer;

        public GameEnvironment()
        {
            Camera = new Camera(0, 0);
            MapRenderer = new GameMapRenderer(MapViewPerspectives.ISOMETRIC);
        }

        public void Draw(GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime)
        {
            MapRenderer.Draw(Map, engine, spriteBatch, gameTime);
        }

        public void Update(GameEngine engine, GameTime gameTime)
        {

        }
    }
}
