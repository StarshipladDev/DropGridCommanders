using System;
using DropGrid.Client.Asset;
using DropGrid.Client.Environment;
using DropGrid.Core.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.State
{
    /// <summary>
    /// The crux of the client-side game logic belongs here.
    /// </summary>
    class GameplayState : GameState
    {
        private GameEnvironment gameEnvironment;

        public override StateId GetId() => StateId.Gameplay;

        public override void Initialise(GameEngine engine)
        {
            base.Initialise(engine);

            gameEnvironment = new GameEnvironment();
            gameEnvironment.Map = new GameMap(9, 9);
        }

        public override void Draw(GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime)
        {
            gameEnvironment.Draw(engine, spriteBatch, gameTime);
        }

        public override void Update(GameEngine engine, GameTime gameTime)
        {
            gameEnvironment.Update(engine, gameTime);
        }
    }
}
