using System;
using System.Collections.Generic;

namespace DropGrid.Core.Map
{
    public class MapTile
    {
        public int Id { get; internal set; }

        public MapTile(int id)
        {
            this.Id = id;
        }
    }
}
