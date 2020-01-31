using System;

namespace DropGrid.Core.Environment
{
    public class GameMap
    {
        MapTile[] tiles;
        public int Width { get;  }
        public int Height { get; }

        public GameMap(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            tiles = new MapTile[width * height];
            for (int i = 0; i < tiles.Length; i++)
                tiles[i] = new MapTile(1);
        }

        internal MapTile getTile(int i)
        {
            return tiles[i];
        }
    }
}
