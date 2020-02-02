using System;
using System.Collections.Generic;
using DropGrid.Core.Environment.Template;

namespace DropGrid.Core.Environment
{
    public static class UnitTemplates
    {
        private static readonly Dictionary<PlayerUnitType, UnitTemplate> Templates;

        static UnitTemplates()
        {
            Templates = new Dictionary<PlayerUnitType, UnitTemplate>();
            Register(PlayerUnitType.TestSoldier, new TestSoldierUnitTemplate());
        }

        private static void Register(PlayerUnitType unitType, UnitTemplate templates)
        {
            if (Templates.ContainsKey(unitType))
                throw new ArgumentException("Another template already exists with the unit type: " + unitType);
            Templates[unitType] = templates;
        }

        public static UnitTemplate Get(PlayerUnitType unitType)
        {
            return Templates[unitType];
        }
    }
}