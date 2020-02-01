using System;
using System.Collections.Generic;

namespace DropGrid.Core.Environment
{
    public class CoreMapTile
    {
        public int Id { get; internal set; }

        public CoreMapTile(int id)
        {
            this.Id = id;
        }
    }
}