using System;

namespace DropGrid.Core.Environment
{
    public class CoreMap
    {
        private CoreMapTile[] tiles;
        public int Width { get;  }
        public int Height { get; }

        public CoreMap(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            tiles = new CoreMapTile[width * height];
            for (int i = 0; i < tiles.Length; i++)
                tiles[i] = new CoreMapTile(1);
        }

        public CoreMapTile GetTile(int x, int y) => GetTile(x + y * Width);

        internal CoreMapTile GetTile(int i) => tiles[i];
    }
}
