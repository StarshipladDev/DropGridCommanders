using System;
using System.Collections.Generic;
using System.Text;

namespace DropGrid.Client.Entity
{
    /**
     * <summary> Entity is a representation of a non-battlemap object displayed in game</summary>
     */
    abstract class Entity
    {
        private String name,assetID;
        private int xPos, yPos;
        private ActionCommand action;
        public void setValues(String name, int xPos, int yPos)
        {
            this.name=name;
            this.yPos = yPos;
            this.xPos = xPos;
        }
        public void setAction(ActionCommand a)
        {
            this.action=a;
        }
        public void setPos(int x, int y)
        {
            this.yPos = y;
            this.xPos = x;
        }
        public ActionCommand getAction()
        {
            return action;
        }
        public int getxPos()
        {
            return xPos;
        }
        public int getyPos()
        {
            return yPos;
        }
        public String getAssetIndentifer()
        {
            return assestID;
        }
        public void setAssetIndentifer(String assetID)
        {
            this.assetID = assetID;
        }
    }
    /**
     * <summary>A Real Entity is an abstract superclass used to represent objects that 'exsist' in the game</summary>
     */
    abstract class RealEntity : Entity
    {
        //interactabl probably not relevant to core data interaction- save for renderer
        bool interactable;
        public void setValues(String name, int xPos, int yPos, bool interactable)
        {
            setValues(name, xPos, yPos);
            this.interactable = interactable;
        }
    }
    /**
     * <summary>An Abstract Entity is an abstract superclass used to represent orders & uses of mechanics on the battlemap</summary>
     */
    abstract class AbstractEntity : Entity
    {
        int turnDeployed;
        public void setValues(String name, int xPos, int yPos,int turnDeployed)
        {
            setValues(name, xPos, yPos);
            this.turnDeployed = turnDeployed;
        }
    }
}
