using DropGrid.Client.Graphics;
using DropGrid.Core.Environment;
using OpenTK;

namespace DropGrid.Client.Environment
{
    public sealed class ClientPlayerUnit : CorePlayerUnit, IClientEntity
    {
        /// <summary>
        /// Defines where the unit is rendered on screen, excluding offset, in pixel units.
        /// </summary>
        public Vector2 ScreenPosition { get; }

        public ClientPlayerUnit(CorePlayerUnit unit) : base(unit)
        {
            float x = unit.GetGridX() * ClientMapTile.TILE_WIDTH;
            float y = unit.GetGridY() * ClientMapTile.TILE_HEIGHT;
            ScreenPosition = new Vector2(x, y);
        }

        public EntityType GetEntityType()
        {
            return EntityType;
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