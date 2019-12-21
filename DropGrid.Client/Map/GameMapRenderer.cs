using System;
using DropGrid.Core.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DropGrid.Client.Map
{
    public class GameMapRenderer
    {
        private Camera camera;
        private IMapViewPerspective viewPerspective;
        private MapTileRenderer tileRenderer;

        public GameMapRenderer(IMapViewPerspective perspective)
        {
            camera = new Camera(0, 0);
            viewPerspective = perspective;
            tileRenderer = new MapTileRenderer();
        }

        public void Draw(GameMap map, GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            for (int i = 0; i < map.Width * map.Height; i++)
            {
                int tileX = i % map.Width;
                int tileY = i / map.Width;
                float internX = tileX * MapTileRenderer.TILE_SIZE;
                float internY = tileY * MapTileRenderer.TILE_SIZE;
                Vector2 internalCoords = new Vector2(internX, internY);
                Vector2 screenCoords = viewPerspective.toViewCoordinates(internalCoords);
                tileRenderer.Draw(map.getTile(i), spriteBatch, gameTime, screenCoords.X + camera.OffsetX, screenCoords.Y + camera.OffsetY);
            }
            spriteBatch.End();
        }

        public void Update(GameMap map, GameEngine engine, GameTime gameTime)
        {

            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Up))
            {
                camera.Pan(0, 6);
            }
            else if (state.IsKeyDown(Keys.Down))
            {
                camera.Pan(0, -6);
            }

            if (state.IsKeyDown(Keys.Left))
            {
                camera.Pan(6, 0);
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                camera.Pan(-6, 0);
            }
        }
    }
}
