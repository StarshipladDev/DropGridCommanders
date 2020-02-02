using System;

namespace DropGrid.Core.Environment
{
    public static class PlayerUnitFactory
    {
        /// <summary>
        /// Instantiates a new copy of a unit of given type.
        /// </summary>
        /// <param name="unitType">The type of unit to create.</param>
        /// <returns>A newly generated instance of a given unit type.</returns>
        public static CorePlayerUnit CreateNew(PlayerUnitType unitType)
        {
            UnitTemplate templates = UnitTemplates.Get(unitType);
            if (templates == null)
                throw new ArgumentException("Cannot find unit template for type: " + unitType);

            int unitWidth = templates.UnitWidth;
            int unitHeight = templates.UnitHeight;
            UnitAttributes attributes = templates.Attributes;
            
            return new CorePlayerUnit(unitType, attributes, unitWidth, unitHeight);
        }
    }
}