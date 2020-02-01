using System;
using System.Collections.Generic;

namespace DropGrid.Core.Environment
{
    public abstract class CorePlayerUnit : CoreAbstractEntity
    {
        public CorePlayerUnit(int gridWidth, int gridHeight) : base(gridWidth, gridHeight) 
        {
        }
    }
}
