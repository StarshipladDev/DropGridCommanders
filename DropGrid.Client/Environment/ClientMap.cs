using System;
using System.Collections.Generic;
using DropGrid.Core.Environment;

namespace DropGrid.Client.Environment
{
    public sealed class ClientMap : CoreMap
    {
        public ClientMapTile this[int index]
        {
            get => (ClientMapTile) tiles[index];
            set => tiles[index] = value;
        }
        
        public List<CoreAbstractEntity> Entities { get; } = new List<CoreAbstractEntity>();

        public ClientMap(int width, int height) : base(width, height)
        {
            Random r = new Random();
            for (int i = 0; i < tiles.Length; i++)
            {
                int rr = r.Next();
                if (rr % 2 == 0)
                    this[i] = new ClientMapTile(TileType.TEST1);
                else
                    this[i] = new ClientMapTile(TileType.TEST2);
            }
        }
    }
}
