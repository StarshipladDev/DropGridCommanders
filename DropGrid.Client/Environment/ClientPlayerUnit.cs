using System;
using DropGrid.Client.Asset;
using DropGrid.Client.Graphics;
using DropGrid.Core.Environment;
using Microsoft.Xna.Framework.Graphics;
using OpenTK;

namespace DropGrid.Client.Environment
{
    public sealed class ClientPlayerUnit : CorePlayerUnit
    {
        /// <summary>
        /// Defines where the unit is rendered on screen, excluding offset, in pixel units.
        /// </summary>
        public Vector2 ScreenPosition { get; }

        public (int Width, int Height) SpriteSize
        {
            get
            {
                Texture2D data = (Texture2D) PlayerUnitAssets.Get(UnitType).GetAnimation(PlayerUnitAnimationType.Idle).GetFrame(0).Sprite.GetData();
                return (data.Width, data.Height);
            }
        }

        public new ClientPlayer Player => (ClientPlayer) base.Player;

        public ClientPlayerUnit(CorePlayerUnit unit) : base(unit)
        {
            if (!(unit.Player is ClientPlayer))
                throw new InvalidOperationException("Player owners of ClientPlayerUnit must be of type ClientPlayer");
            
            float x = unit.GetGridX() * ClientMapTile.TILE_WIDTH + ClientMapTile.TILE_WIDTH / 2;
            float y = unit.GetGridY() * ClientMapTile.TILE_HEIGHT;
            Console.WriteLine(x + "," + y);
            ScreenPosition = new Vector2(x, y);
        }
    }
    
    public enum PlayerUnitAnimationType
    {
        Idle,
        Move,
        Attack,
        Deployment,
        Death
    }
}