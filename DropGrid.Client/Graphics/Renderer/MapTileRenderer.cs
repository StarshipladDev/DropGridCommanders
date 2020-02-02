using DropGrid.Client.Asset;
using DropGrid.Client.Environment;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.Graphics
{
    public static class MapTileRenderer
    {
        internal static void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime, ClientMapTile tile, float x, float y)
        {
            SpriteAnimation tileAnimation = MapTileTextures.GetById(tile.Id);

            // TODO: Draw animation instead of just first sprite
            SpriteFrame frameToDraw = tileAnimation.GetFrame(0);
            renderer.Render(frameToDraw.Sprite, x, y, offsetY: tile.HeightOffset);
        }
        
        internal static void Update(GameEngine engine, GameTime gameTime, ClientMapTile tile)
        {
        }
    }
}
