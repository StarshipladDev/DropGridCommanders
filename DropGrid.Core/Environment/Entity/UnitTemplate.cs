namespace DropGrid.Core.Environment
{
    public abstract class UnitTemplate
    {
        public int UnitWidth { get; set; } = 1;
        public int UnitHeight { get; set; } = 1;
        public UnitAttributes Attributes { get; set; } = new UnitAttributes();
        
    }
}