namespace DropGrid.Core.Environment
{
    public class CoreStructuralEntity : CoreAbstractEntity
    {
        public CoreStructuralEntity(int gridWidth, int gridHeight) 
            : base(Environment.EntityType.Structure, gridWidth, gridHeight)
        {
        }
    }
}