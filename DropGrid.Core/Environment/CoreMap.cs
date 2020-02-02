using System;

namespace DropGrid.Core.Environment
{
    /// <summary>
    /// Represents a set of grids on which entities interact. 
    /// </summary>
    public class CoreMap
    {
        protected CoreMapTile[] tiles { get; set; }

        /// <summary>
        /// The array accessor for tiles that belong in this map.
        /// </summary>
        /// <param name="index"></param>
        private CoreMapTile this[int index]
        {
            get
            {
                if (index < 0 || index > Width * Height - 1)
                    return null;
                return tiles[index];
            }
            set
            {
                if (index < 0 || index > Width * Height - 1)
                    return;
                tiles[index] = value;
            }
        }

        /// <summary>
        /// Width of the game board, in grid unit.
        /// </summary>
        public int Width { get;  }
        
        /// <summary>
        /// Height of the game board, in grid unit.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Constructs an empty map of set grid width and height.
        /// </summary>
        /// <param name="width">Width of the game board, in grid unit</param>
        /// <param name="height">Height of the game board, in grid unit</param>
        public CoreMap(int width, int height)
        {
            Width = width;
            Height = height;
            tiles = new CoreMapTile[width * height];
            for (int i = 0; i < tiles.Length; i++)
                tiles[i] = new CoreMapTile(CoreMapTileType.EMPTY);
        }

        /// <summary>
        /// Retrieves a tile using the familiar cartesian coordinate system.
        /// </summary>
        /// <param name="x">X position of the tile</param>
        /// <param name="y">Y position of the tile</param>
        /// <returns>The <c cref="CoreMapTile">CoreMapTile</c> instance representing the tile at that point,
        /// or null if the coordinate is invalid.</returns>
        public CoreMapTile GetTile(int x, int y) => this[x + y * Width];
    }
}
