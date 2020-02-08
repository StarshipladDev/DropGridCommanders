using System;

namespace DropGrid.Core.Environment
{
    public class TileType
    {
        private static readonly TileType[] Tiles = new TileType[128];

        public static readonly TileType EMPTY = new TileType(0, true);
        public static readonly TileType TEST1 = new TileType(1, false);
        public static readonly TileType TEST2 = new TileType(2, true);

        public int Id { get; }
        public bool Solid { get; }

        private TileType(int id, bool solid)
        {
            if (Tiles[id] != null)
                throw new ArgumentException("Duplicated tile id: " + id);
                
            Id = id;
            Solid = solid;
        }

        public static TileType FromId(int id)
        {
            if (Tiles[id] == null)
                throw new ArgumentException("No matching tile type with id " + id);
            return Tiles[id];
        }
    }
}