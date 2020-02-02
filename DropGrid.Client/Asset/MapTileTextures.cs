using System;
using System.Collections.Generic;
using DropGrid.Core.Environment;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.Asset
{
    public static class MapTileTextures
    {
        /// <summary>
        /// The registrar of all tile textures associated with their ids.
        /// </summary>
        private static readonly Dictionary<int, SpriteAnimation> TEXTURES = new Dictionary<int, SpriteAnimation>();
        private static bool initialised = false;

        public static void Initialise()
        {
            if (initialised)
                return;
            
            RegisterTexture(CoreMapTileType.EMPTY, null);
            RegisterTexture(CoreMapTileType.TEST1, CreateAnimation(new Point(1, 0)));
            RegisterTexture(CoreMapTileType.TEST2, CreateAnimation(new Point(2, 0)));

            initialised = true;
        }
        
        
        private static SpriteAnimation CreateAnimation(params Point[] tilesetCoord)
        {
            SpriteAnimation animation = SpriteAnimation.Create(AssetRegistry.TILESET.Identifier);
            foreach (Point coord in tilesetCoord)
            {
                Sprite tileSprite = AssetRegistry.TILESET.GetSpriteAt(coord.X, coord.Y);
                SpriteFrame frame = new SpriteFrame(tileSprite, 1000);
                animation.AddFrame(frame);
            }
            return animation;
        }

        private static void RegisterTexture(CoreMapTileType tileTypeTemplate, SpriteAnimation tileAnimation)
        {
            TEXTURES.Add(tileTypeTemplate.Id, tileAnimation);
        }

        public static SpriteAnimation GetTexturesById(int tileId)
        {
            return TEXTURES[tileId] == null
                ? throw new ArgumentException("No texture exist for tile id: " + tileId)
                : TEXTURES[tileId];
        }
    }
}