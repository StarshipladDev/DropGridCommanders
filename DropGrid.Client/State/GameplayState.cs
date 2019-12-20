using System;
using DropGrid.Client.Asset;
using DropGrid.Client.Map;
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

        public GameplayState()
        {
            map = new GameMap(10, 7);
        }

        public override StateId GetId() => StateId.Gameplay;

        public override void Draw(GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime)
        {
            map.Draw(engine, spriteBatch, gameTime);
        }

        public override void Update(GameEngine engine, GameTime gameTime)
        {
            map.Update(engine, gameTime);
        }
    }
}
