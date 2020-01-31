using System;
using DropGrid.Core.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DropGrid.Client.Graphics
{
    public class MapRenderer
    {
        private IMapViewPerspective viewPerspective;
        private MapTileRenderer tileRenderer;

        public MapRenderer(IMapViewPerspective perspective)
        {
            viewPerspective = perspective;
            tileRenderer = new MapTileRenderer();
        }

        public void Render(Map map, GameEngine engine, GraphicsRenderer render, GameTime gameTime)
        {
            render.Start();
            for (int i = 0; i < map.Width * map.Height; i++)
            {
                int tileX = i % map.Width;
                int tileY = i / map.Width;
                float drawX = tileX * MapTileRenderer.TILE_SIZE;
                float drawY = tileY * MapTileRenderer.TILE_SIZE;
                tileRenderer.Render(map.getTile(i), render, gameTime, drawX, drawY);
            }
            render.Finish();
        }

        public void Update(Map map, GameEngine engine, GameTime gameTime)
        {
        }
    }
}
