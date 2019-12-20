using System;
using System.Collections.Generic;
using DropGrid.Client.Asset;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.Map
{
    public class Tile
    {
        public static readonly int SIZE = 16 * GameEngine.GRAPHICS_SCALE;
        private static readonly Dictionary<int, Tile> TILES = new Dictionary<int, Tile>();

        public static readonly Tile NONE = new Tile(0, new Point(0, 0));
        public static readonly Tile TEST_1 = new Tile(1, new Point(1, 0));
        public static readonly Tile TEST_2 = new Tile(2, new Point(2, 0));

        private int id;
        // Array form = basic tile animation
        private Point[] sprites;

        private Tile(int id, Point sprite) : this(id, new Point[] { sprite })
        {

        }

        private Tile(int id, Point[] sprites)
        {
            this.id = id;
            this.sprites = sprites;
            if (TILES.ContainsKey(id))
                throw new ArgumentException("Duplicated tile ID: " + id + "!");
            TILES.Add(id, this);
        }

        internal void Draw(GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime, double x, double y)
        {
            if (sprites != null)
            {
                // TODO: Draw animation instead of just first sprite
                Point spriteCoords = sprites[0];
                Texture2D texture = (Texture2D)AssetRegistry.TILESET.getSpriteAt(spriteCoords.X, spriteCoords.Y).GetData();
                Rectangle drawSize = new Rectangle((int)x, (int)y, texture.Width * GameEngine.GRAPHICS_SCALE, texture.Height * GameEngine.GRAPHICS_SCALE);
                spriteBatch.Draw(texture, drawSize, Color.White);
            }
        }
    }
}
