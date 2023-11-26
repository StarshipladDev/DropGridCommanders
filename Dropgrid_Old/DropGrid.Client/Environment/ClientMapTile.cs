using DropGrid.Core.Environment;

namespace DropGrid.Client.Environment
{
    public sealed class ClientMapTile : CoreMapTile
    {
        public static readonly int TILE_WIDTH = 16 * GameEngine.GraphicsScale;
        public static readonly int TILE_HEIGHT = TILE_WIDTH;

        public float HeightOffset { get; set; }

        public ClientMapTile(TileType tileType) : base(tileType)
        {
            HeightOffset = 0;
        }
    }
}
