using System;
using System.Collections.Generic;

namespace DropGrid.Core.Environment
{
    public abstract class PlayerUnit : AbstractEntity
    {
        public int Health { get; private set; }
        public int HealthMax { get; }

        public int DamageDefault { get; }
        public Dictionary<Type, int> DamageUnique;

        public PlayerUnit(int gridWidth, int gridHeight) : base(gridWidth, gridHeight) 
        {

        }

        public void Hurt(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }
    }
}
