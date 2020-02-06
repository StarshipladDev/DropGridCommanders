using System;
using System.Diagnostics;
using DropGrid.Client.Environment;
using DropGrid.Core.Environment;
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

            // TODO: Temporary debug info
            (float ox, float oy) = renderer.CameraOffset;
            string cameraInfo = $"Camera: ({ox}, {oy})";
            Debug.Assert(environment != null, nameof(environment) + " != null");
            string entityInfo = "Entities: " + environment.Map.Entities.Count + "\n";
            foreach (CoreAbstractEntity entity in environment.Map.Entities)
            {
                if (entity is ClientPlayerUnit unit)
                {
                    entityInfo += " - " + unit.EntityType + "/" + unit.UnitType + " (" + unit.Player + ")\n";
                }
            }
            string debugInfo = "Debug Info:\n" + cameraInfo + "\n" + entityInfo;
            
            GameFont.Render(renderer, debugInfo, 20, 20);
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
