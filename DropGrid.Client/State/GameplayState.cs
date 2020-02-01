using System;
using DropGrid.Client.Asset;
using DropGrid.Client.Environment;
using DropGrid.Client.Graphics;
using DropGrid.Core.Environment;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.State
{
    /// <summary>
    /// The crux of the client-side game logic belongs here.
    /// </summary>
    class GameplayState : EngineState
    {
        private ClientGameSession gameSession;
        private ClientGameEnvironment gameEnvironment;
        private ClientPlayer[] players;

        public override StateId GetId() => StateId.Gameplay;

        public override void Initialise(GameEngine engine)
        {
            base.Initialise(engine);

            players = new ClientPlayer[] {
                new ClientPlayer("Player 1"),
                new ClientPlayer("Player 2")
            };
            gameEnvironment = new ClientGameEnvironment();
            gameSession = new ClientGameSession(gameEnvironment, players);

            gameEnvironment.Map = new ClientMap(9, 9);
        }

        public override void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime)
        {
            EnvironmentRenderer.Render(engine, renderer, gameEnvironment, gameTime);
        }

        public override void Update(GameEngine engine, GameTime gameTime)
        {
            EnvironmentRenderer.Update(engine, gameEnvironment, gameTime);
        }
    }
}
