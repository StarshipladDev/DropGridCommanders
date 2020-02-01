using System;
using DropGrid.Client.Environment;
using DropGrid.Core.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DropGrid.Client.Graphics
{
    public class MapRenderer
    {
        private MapRenderer() { }

        public static void Render(GameEngine engine, GraphicsRenderer render, ClientMap map, GameTime gameTime)
        {
            render.Start();
            for (int i = 0; i < map.Width * map.Height; i++)
            {
                int tileX = i % map.Width;
                int tileY = i / map.Width;
                float drawX = tileX * MapTileRenderer.TILE_SIZE;
                float drawY = tileY * MapTileRenderer.TILE_SIZE;
                MapTileRenderer.Render(render, gameTime, map.GetTile(i), drawX, drawY);
            }
            render.Finish();
        }

        public static void Update(GameEngine engine, GameTime gameTime, CoreMap map)
        {
        }
    }
}
