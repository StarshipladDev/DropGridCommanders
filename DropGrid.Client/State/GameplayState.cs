using System.Diagnostics;
using DropGrid.Client.Environment;
using DropGrid.Client.Graphics;
using DropGrid.Client.Graphics.Renderer;
using DropGrid.Core.Environment;
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

            _players = new[] {
                new ClientPlayer("Player 1", Color.Aquamarine),
                new ClientPlayer("Player 2", Color.Blue)
            };
            _gameEnvironment = new ClientGameEnvironment();
            _gameSession = new ClientGameSession(_gameEnvironment, _players);

            // TODO: Testing only
            _gameEnvironment.Map = new ClientMap(9, 9);
            CorePlayerUnit unit = PlayerUnitFactory.CreateNew(_players[0], PlayerUnitType.TestSoldier);
            _gameEnvironment.Map.Entities.Add(new ClientPlayerUnit(unit));
            
            CorePlayerUnit unit6 = PlayerUnitFactory.CreateNew(_players[0], PlayerUnitType.TestSoldier);
            unit6.SetPosition(3, 3);
            _gameEnvironment.Map.Entities.Add(new ClientPlayerUnit(unit6));
            
            CorePlayerUnit unit2 = PlayerUnitFactory.CreateNew(_players[1], PlayerUnitType.TestSniper);
            unit2.SetPosition(2,2);
            _gameEnvironment.Map.Entities.Add(new ClientPlayerUnit(unit2));
            
            CorePlayerUnit unit3 = PlayerUnitFactory.CreateNew(_players[0], PlayerUnitType.TestAssault);
            unit3.SetPosition(1,5);
            _gameEnvironment.Map.Entities.Add(new ClientPlayerUnit(unit3));
            
            CorePlayerUnit unit4 = PlayerUnitFactory.CreateNew(_players[1], PlayerUnitType.TestMech);
            unit4.SetPosition(0,6);
            _gameEnvironment.Map.Entities.Add(new ClientPlayerUnit(unit4));
        }

        public override void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime)
        {
            GameSessionRenderer.Render(engine, renderer, _gameSession, gameTime);

            if (engine.DebugMode)
            {
                RenderDebugInfo(engine, renderer, gameTime);
            }
        }

        private void RenderDebugInfo(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime)
        {
            // TODO: Temporary debug info
            (float ox, float oy) = renderer.CameraOffset;
            string cameraInfo = $"Camera: ({ox}, {oy})";
            string sessionInfo = "";
            if (_gameSession != null)
            {
                CorePlayer[] players = _gameSession.Players;
                sessionInfo += "Session:\n";
                foreach (CorePlayer player in players)
                {
                    sessionInfo += " -" + player + "\n";
                }
            }

            string entityInfo = "";
            if (_gameEnvironment != null)
            {
                entityInfo = "Entities: " + _gameEnvironment.Map.Entities.Count + "\n";
                foreach (CoreAbstractEntity entity in _gameEnvironment.Map.Entities)
                {
                    if (entity is ClientPlayerUnit unit)
                        entityInfo += " -" + unit.EntityType + "/" + unit.UnitType + " (" + unit.Player + ")\n";
                }
            }

            string debugInfo = "Debug Info:\n" + cameraInfo + "\n" + sessionInfo + "\n" + entityInfo;
            GameFont.Render(renderer, debugInfo, 20, 20, scale:0.75f);
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

            GameSessionRenderer.Update(engine, _gameSession, gameTime);
        }
    }
}
