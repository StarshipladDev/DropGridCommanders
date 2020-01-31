using System;
using DropGrid.Client.Asset;
using DropGrid.Client.Graphics;
using DropGrid.Core.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.State
{
    /// <summary>
    /// The crux of the client-side game logic belongs here.
    /// </summary>
    class GameplayState : EngineState
    {
        private ClientGameSession gameSession;
        private ClientGameEnvironment gameEnvironment;
        private Player[] players;

        public override StateId GetId() => StateId.Gameplay;

        public override void Initialise(GameEngine engine)
        {
            base.Initialise(engine);

            players = new Player[] {
                new Player("Player 1"),
                new Player("Player 2")
            };
            gameEnvironment = new ClientGameEnvironment();
            gameSession = new ClientGameSession(gameEnvironment, players);
        }

        public override void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime)
        {
            gameEnvironment.Draw(engine, renderer, gameTime);
        }

        public override void Update(GameEngine engine, GameTime gameTime)
        {
            gameEnvironment.Update(engine, gameTime);
        }
    }
}
