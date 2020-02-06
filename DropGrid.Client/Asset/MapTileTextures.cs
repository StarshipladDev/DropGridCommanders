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
        private static readonly Dictionary<int, SpriteAnimation> Textures = new Dictionary<int, SpriteAnimation>();
        private static bool _initialised;

        public static void Initialise()
        {
            if (_initialised)
                return;
            
            Register(TileType.EMPTY, null);
            Register(TileType.TEST1, CreateAnimation(new Point(1, 0)));
            Register(TileType.TEST2, CreateAnimation(new Point(2, 0)));

            _initialised = true;
        }
        
        private static SpriteAnimation CreateAnimation(params Point[] tilesetCoord)
        {
            SpriteAnimation animation = new SpriteAnimation();
            foreach (Point coord in tilesetCoord)
            {
                Sprite tileSprite = AssetRegistry.TILESET.GetSpriteAt(coord.X, coord.Y);
                animation.AddFrame(tileSprite, 1000);
            }
            return animation;
        }

        private static void Register(TileType tileTypeTemplate, SpriteAnimation tileAnimation)
        {
            Textures.Add(tileTypeTemplate.Id, tileAnimation);
        }

        public static SpriteAnimation GetById(int tileId)
        {
            return Textures[tileId] == null
                ? throw new ArgumentException("No texture exist for tile id: " + tileId)
                : Textures[tileId];
        }
    }
}