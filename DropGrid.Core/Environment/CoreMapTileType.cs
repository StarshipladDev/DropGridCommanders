using System;

namespace DropGrid.Core.Environment
{
    public class CoreMapTileType
    {
        private static readonly CoreMapTileType[] Tiles = new CoreMapTileType[128];

        public static readonly CoreMapTileType EMPTY = new CoreMapTileType(0, true);
        public static readonly CoreMapTileType TEST1 = new CoreMapTileType(1, false);
        public static readonly CoreMapTileType TEST2 = new CoreMapTileType(2, true);

        public int Id { get; }
        public bool Solid { get; }

        private CoreMapTileType(int id, bool solid)
        {
            if (Tiles[id] != null)
                throw new ArgumentException("Duplicated tile id: " + id);
                
            Id = id;
            Solid = solid;
        }

        public static CoreMapTileType FromId(int id)
        {
            if (Tiles[id] == null)
                throw new ArgumentException("No matching tile type with id " + id);
            return Tiles[id];
        }
    }
}