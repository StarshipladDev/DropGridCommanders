using System;
using DropGrid.Client.Asset;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DropGrid.Client.Map
{
    public class GameMap
    {
        private Camera camera;
        private IPerspective viewPerspective;
        Tile[] tiles;
        public int Width { get;  }
        public int Height { get; }

        public GameMap(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            tiles = new Tile[width * height];
            for (int i = 0; i < tiles.Length; i++)
                tiles[i] = Tile.TEST_1;

            camera = new Camera(0, 0);
            viewPerspective = new IsometricPerspective();
        }

        public void Draw(GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            for (int i = 0; i < tiles.Length; i++)
            {
                int tileX = i % Width;
                int tileY = i / Width;
                double internX = tileX * Tile.SIZE;
                double internY = tileY * Tile.SIZE;
                Vector2 screenCoords = viewPerspective.toViewCoordinates(new Vector2((int) internX, (int) internY));
                tiles[i].Draw(engine, spriteBatch, gameTime, screenCoords.X + camera.OffsetX, screenCoords.Y + camera.OffsetY);
            }
            spriteBatch.End();
        }

        public void Update(GameEngine engine, GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Up))
            {
                camera.Pan(0, 6);
            } else if (state.IsKeyDown(Keys.Down))
            {
                camera.Pan(0, -6);
            }

            if (state.IsKeyDown(Keys.Left))
            {
                camera.Pan(6, 0);
            } else if (state.IsKeyDown(Keys.Right))
            {
                camera.Pan(-6, 0);
            }
        }
    }
}
