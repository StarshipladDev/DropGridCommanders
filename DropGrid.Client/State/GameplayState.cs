using DropGrid.Client.Environment;
using DropGrid.Client.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DropGrid.Client.State
{
    /// <summary>
    /// The crux of the client-side game logic belongs here.
    /// </summary>
    class GameplayState : EngineState
    {
        private ClientGameSession _gameSession;
        private ClientGameEnvironment _gameEnvironment;
        private ClientPlayer[] _players;

        public override StateId GetId() => StateId.Gameplay;

        public override void Initialise(GameEngine engine)
        {
            base.Initialise(engine);

            _players = new ClientPlayer[] {
                new ClientPlayer("Player 1"),
                new ClientPlayer("Player 2")
            };
            _gameEnvironment = new ClientGameEnvironment();
            _gameSession = new ClientGameSession(_gameEnvironment, _players);

            _gameEnvironment.Map = new ClientMap(9, 9);
        }

        public override void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime)
        {
            EnvironmentRenderer.Render(engine, renderer, _gameEnvironment, gameTime);
        }

        public override void Update(GameEngine engine, GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.W))
                engine.Renderer.CameraPan(0, 5);
            else if (keyboard.IsKeyDown(Keys.S))
                engine.Renderer.CameraPan(0, -5);
            
            if (keyboard.IsKeyDown(Keys.A))
                engine.Renderer.CameraPan(5, 0);
            else if (keyboard.IsKeyDown(Keys.D))
                engine.Renderer.CameraPan(-5, 0);

            EnvironmentRenderer.Update(engine, _gameEnvironment, gameTime);
        }
    }
}
