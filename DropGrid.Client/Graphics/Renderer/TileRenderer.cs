using System;
using System.Collections.Generic;
using DropGrid.Client.Asset;
using DropGrid.Core.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.Graphics
{
    public class MapTileRenderer
    {
        /// <summary>
        /// The registrar of all tile textures associated with their ids.
        /// </summary>
        private static readonly Dictionary<int, SpriteAnimation> TEXTURES = new Dictionary<int, SpriteAnimation>();
        public static readonly int TILE_SIZE = 16 * GameEngine.GRAPHICS_SCALE;

        static MapTileRenderer()
        {
            TEXTURES.Add(0, null);
            TEXTURES.Add(1, CreateTileTextureAnimation(new Point(1, 0)));

        }

        internal void Render(MapTile tile, GraphicsRenderer renderer, GameTime gameTime, float x, float y)
        {
            SpriteAnimation tileAnimation = TEXTURES.ContainsKey(tile.Id) ? TEXTURES[tile.Id] : null;
            if (tileAnimation == null)
                return;

            // TODO: Draw animation instead of just first sprite
            SpriteFrame frameToDraw = tileAnimation.GetFrame(0);
            renderer.Render(frameToDraw, x, y);
        }

        private static SpriteAnimation CreateTileTextureAnimation(params Point[] tilesetCoord)
        {
            SpriteAnimation animation = SpriteAnimation.create(AssetRegistry.TILESET.Identifier);
            foreach (Point coord in tilesetCoord)
            {
                Sprite tileSprite = AssetRegistry.TILESET.getSpriteAt(coord.X, coord.Y);
                SpriteFrame frame = new SpriteFrame(tileSprite, 1000);
                animation.AddFrame(frame);
            }
            return animation;
        }
    }
}
