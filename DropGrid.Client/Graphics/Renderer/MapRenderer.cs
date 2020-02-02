using DropGrid.Client.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DropGrid.Client.Graphics
{
    public static class MapRenderer
    {
        public static void Render(GameEngine engine, GraphicsRenderer render, ClientMap map, GameTime gameTime)
        {
            render.Start();
            for (int i = 0; i < map.Width * map.Height; i++)
            {
                int tileX = i % map.Width;
                int tileY = i / map.Width;
                
                float drawX = tileX * ClientMapTile.TILE_WIDTH;
                
                ClientMapTile mapTile = map[i];
                float drawY = tileY * ClientMapTile.TILE_HEIGHT;
                
                MapTileRenderer.Render(engine, render, gameTime, mapTile, drawX, drawY);
            }
            
            render.Finish();
        }
        
        public static void Update(GameEngine engine, GameTime gameTime, ClientMap map)
        {
            ClientMapTile tile = GetHighlightedTile(engine, map);

            for (int i = 0; i < map.Width * map.Height; i++)
            {
                if (map[i] == tile)
                    map[i].HeightOffset = -20;
                else
                    map[i].HeightOffset = 0;
                
                MapTileRenderer.Update(engine, gameTime, map[i]);
            }
        }

        public static ClientMapTile GetHighlightedTile(GameEngine engine, ClientMap map)
        {
            MouseState mouse = Mouse.GetState();
            if (mouse.X < 0 || mouse.Y < 0)
                return null;

            Vector2 cameraOffset = engine.Renderer.CameraOffset;
            Vector2 projectedPosition = new Vector2(mouse.X - cameraOffset.X, mouse.Y - cameraOffset.Y);
            Vector2 result = ViewPerspectives.ISOMETRIC.ToInternal(projectedPosition);

            int selectX = (int) (result.X / ClientMapTile.TILE_WIDTH);
            if (selectX < 0 || selectX > map.Width - 1)
                return null;
            
            int selectY = (int) (result.Y / ClientMapTile.TILE_HEIGHT);
            if (selectY < 0 || selectY > map.Height - 1)
                return null;
            
            int selectIndex = selectX + selectY * map.Width;

            return map[selectIndex];
        }
    }
}
