using System;
using System.Collections.Generic;
using System.Text;
using DropGrid.Client.Entity;
namespace DropGrid.Client.Entity
{
    /**
     * 
     * <summary>Unit is a type of </summary>
     * 
     */
    class Unit:RealEntity
    {
        int pointsCost=0, damage=0, range=0, rangeSniper=0, health=4, assaultDamage=0;
        Unit(String type,String name, int xPos, int yPos, bool interactable)
        {
            switch (type)
            {
                case "Infantry":
                    name = "Infantry";
                    pointsCost = 1;
                    damage = 2;
                    range = 1;
                    health = 4;
                    assaultDamage = 2;
                    break;
                default:
                    break;
            }


        }
    }
}
