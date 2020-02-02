namespace DropGrid.Core.Environment
{
    public class CoreDestructibleEntity : CoreAbstractEntity
    {
        public CoreDestructibleEntity(int gridWidth, int gridHeight) 
            : base(EntityType.Destructible, gridWidth, gridHeight)
        {
            
        }
    }
}