using System;
using System.Collections.Generic;
using System.Text;

namespace DropGrid.Core.TurnPhase
//elisetrouw < band
{
    static  int phaseArraySize = 6;
    static int reposnseCode = 0;
    abstract class Phase
    {
        List<Enitity.Action> action = new List<Entity.Action>;
        //performMechanics will return 1 if the action requires both players to send info.
        //it returns 0 if it is an action the client can quite happily go to the next thing for
        abstract int performMechanics()
        {

        }
    }
    class deploymentPhase : Phase
    {
        int performMechanics()
        {
            //As user clicks & creates user, edit Action list accordingly to send to server
            
            return 1;
        }
    }
    class AttackPhase : Phase
    {
        public AttackPhase(List<Action> actions)
        {
            this.actions = actions;
        }
        //Example non->input phase. Run through given list of Actions and perform them
        int performMechanics()
        {

            return 0;
        }
    }



 


    class Turn
    {
        int deploymentPoints, movementPoints;
        Phase[] phases = new Phase[phaseArraySize];
        public void runTurn()
        {
            for(int i=0; i<phaseArraySize; i++)
            {
                if (phases[i].performMechanics() == 1)
                {
                    while(responseCode == 0)
                    {
                        //wait for a bit
                        //Check with server again
                    }
                }
            }
        }
       
    }
}
