namespace DropGrid.Core.Environment
{
    public class CorePlayerUnit : CoreAbstractEntity
    {
        public PlayerUnitType UnitType { get; }
        private readonly UnitAttributes _attributes;

        /// <summary>
        /// Creates a player unit from a unit template.
        /// </summary>
        /// <param name="unitType"></param>
        /// <param name="attributes"></param>
        /// <param name="gridWidth"></param>
        /// <param name="gridHeight"></param>
        public CorePlayerUnit(PlayerUnitType unitType, UnitAttributes attributes, int gridWidth, int gridHeight) 
            : base(EntityType.PlayerUnit, gridWidth, gridHeight)
        {
            UnitType = unitType;
            _attributes = new UnitAttributes(attributes);
        }

        /// <summary>
        /// Creates a player unit from a copy of another unit.
        /// Use this in the client code to initialise unit fields properly.
        /// </summary>
        /// <param name="copy"></param>
        public CorePlayerUnit(CorePlayerUnit copy) : base(copy)
        {
            UnitType = copy.UnitType;
            _attributes = new UnitAttributes(copy._attributes);
        }
    }

    public sealed class UnitAttributes
    {
        public int Hp { get; set; }
        public int HpMax { get; set; }
        
        public int AttackDamageDefault { get; set; }
        public int DefenseDefault { get; set; }
        
        /// <summary>
        /// Creates a deep copy of a given set of attributes.
        /// </summary>
        /// <param name="copy">The set of attributes to copy from.</param>
        public UnitAttributes(UnitAttributes copy)
        {
            Hp = copy.Hp;
            HpMax = copy.HpMax;
            AttackDamageDefault = copy.AttackDamageDefault;
            DefenseDefault = copy.DefenseDefault;
        }

        /// <summary>
        /// Creates a default set of attributes for a dummy unit.
        /// Do not use this constructor to initialise attributes for production purposes. 
        /// </summary>
        public UnitAttributes()
        {
            HpMax = 1;
            Hp = 1;
            AttackDamageDefault = 2;
            DefenseDefault = 1;
        }
    }
}
