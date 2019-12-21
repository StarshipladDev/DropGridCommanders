using System;
using DropGrid.Client.Asset;
using DropGrid.Client.Map;
using DropGrid.Core.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.State
{
    /// <summary>
    /// The crux of the client-side game logic belongs here.
    /// </summary>
    class GameplayState : GameState
    {
        private GameMap map;
        private GameMapRenderer renderer;

        public override StateId GetId() => StateId.Gameplay;

        public override void Initialise(GameEngine engine)
        {
            base.Initialise(engine);
            map = new GameMap(10, 7);
            renderer = new GameMapRenderer(MapViewPerspectives.ISOMETRIC);
        }

        public override void Draw(GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime)
        {
            renderer.Draw(map, engine, spriteBatch, gameTime);
        }

        public override void Update(GameEngine engine, GameTime gameTime)
        {
            renderer.Update(map, engine, gameTime);
        }
    }
}
