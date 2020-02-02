using System;
using DropGrid.Core.Environment;

namespace DropGrid.Client.Environment
{
    public sealed class ClientMap : CoreMap
    {
        public new ClientMapTile this[int index]
        {
            get => (ClientMapTile) tiles[index];
            set => tiles[index] = value;
        }
        
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
