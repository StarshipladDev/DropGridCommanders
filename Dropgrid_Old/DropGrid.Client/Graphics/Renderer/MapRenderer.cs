using System.Collections.Generic;
using AppKit;
using DropGrid.Client.Asset;
using DropGrid.Client.Environment;
using DropGrid.Core.Environment;
using DropGrid.MacOS.Graphics.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DropGrid.Client.Graphics
{
    public static class MapRenderer
    {
        public static void Render(GameEngine engine, GraphicsRenderer renderer, ClientMap map, GameTime gameTime)
        {
            Dictionary<int, List<CoreAbstractEntity>> sortedEntities = GetEntitiesByTilePosition(map);
            (ClientMapTile tile, Vector2 position) = GetHighlightedTile(engine, map);
            
            renderer.Start();
            for (int i = 0; i < map.Width * map.Height; ++i)
            {
                int tileX = i % map.Width;
                int tileY = i / map.Width;
                float drawX = tileX * ClientMapTile.TILE_WIDTH;
                float drawY = tileY * ClientMapTile.TILE_HEIGHT;
                
                MapTileRenderer.Render(engine, renderer, gameTime, map[i], drawX, drawY);
                if (tile != null && tileX == (int) position.X && tileY == (int) position.Y)
                    renderer.Render(AssetRegistry.TILE_SELECTION, drawX, drawY);
                
                if (sortedEntities.ContainsKey(i))
                    RenderEntities(engine, renderer, gameTime, sortedEntities[i], map[i], drawX, drawY);
            }
            
            renderer.Finish();
        }
        
        private static void RenderEntities(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime, 
            List<CoreAbstractEntity> entities, ClientMapTile onTile, float tileDrawX, float tileDrawY)
        {
            foreach (CoreAbstractEntity entity in entities)
            {
                EntityRenderer entityRenderer = EntityRenderers.Get(entity);
                entityRenderer.Render(engine, renderer, gameTime, entity, onTile, tileDrawX, tileDrawY);
            }
        }

        public static void Update(GameEngine engine, GameTime gameTime, ClientMap map)
        {
            for (int i = 0; i < map.Width * map.Height; i++)
            {
                MapTileRenderer.Update(engine, gameTime, map[i]);
            }
        }
        
        private static Dictionary<int, List<CoreAbstractEntity>> GetEntitiesByTilePosition(ClientMap map)
        {
            var result = new Dictionary<int, List<CoreAbstractEntity>>();
            map.Entities.ForEach(entity =>
            {
                if (!entity.GetType().IsSubclassOf(typeof(CoreAbstractEntity)))
                    return;

                int position = entity.GetGridX() + entity.GetGridY() * map.Width;
                if (!result.ContainsKey(position))
                    result[position] = new List<CoreAbstractEntity>();
                result[position].Add(entity);
            });
            return result;
        }

        public static (ClientMapTile selectedTile, Vector2 tilePosition) GetHighlightedTile(GameEngine engine, ClientMap map)
        {
            MouseState mouse = Mouse.GetState();
            if (mouse.X < 0 || mouse.Y < 0)
                return (null, new Vector2(-1, -1));

            Vector2 cameraOffset = engine.Renderer.CameraOffset;
            float mx = mouse.X - cameraOffset.X;
            float my = mouse.Y - cameraOffset.Y + ClientMapTile.TILE_HEIGHT / 2f;
            Vector2 projectedPosition = new Vector2(mx, my);
            Vector2 result = ViewPerspectives.ISOMETRIC.ToInternal(projectedPosition);
            float rx = result.X - ClientMapTile.TILE_WIDTH;
            float ry = result.Y;
            
            int selectX = (int) (rx / ClientMapTile.TILE_WIDTH);
            if (selectX < 0 || selectX > map.Width - 1)
                return (null, new Vector2(-1, -1));
            
            int selectY = (int) (ry / ClientMapTile.TILE_HEIGHT);
            if (selectY < 0 || selectY > map.Height - 1)
                return (null, new Vector2(-1, -1));
            
            int selectIndex = selectX + selectY * map.Width;

            return (map[selectIndex], new Vector2(selectX, selectY));
        }
    }
}
