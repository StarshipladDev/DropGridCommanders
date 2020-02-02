namespace DropGrid.Core.Environment
{
    /// <summary>
    /// Represents one tile instance that belong to a map. Each tile is defined by a tile template --
    /// <c cref="CoreMapTileType">CoreMapTileType</c>, which supplies its inherent attributes.
    ///
    /// This class is open for inheritance. Clients should define its own attributes atop those provided 
    /// here. 
    /// </summary>
    public class CoreMapTile
    {
        /// <summary>
        /// The template attributes for the current tile. 
        /// </summary>
        private CoreMapTileType _tileType;
        
        /// <summary>
        /// Shortcut handle to supply the tile's id.
        /// </summary>
        public int Id => _tileType.Id;
        
        /// <summary>
        /// Shortcut handle to determine whether the tile can be stepped onto by entities. 
        /// </summary>
        public bool IsSolid => _tileType.Solid;

        /// <summary>
        /// Constructs an instance of map tile of a given type.
        /// </summary>
        /// <param name="tileType">The template tile type</param>
        public CoreMapTile(CoreMapTileType tileType)
        {
            _tileType = tileType;
        }

    }
}