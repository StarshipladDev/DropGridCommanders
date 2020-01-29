using System;
using System.Collections.Generic;
using System.Text;

namespace DropGrid.Core.TurnPhase
{   /*
    ROUGH OUTLINE:
    >PLAYER SENDS THROUGH VALID COMMANDS THAT ARE CHECKED EACH MOVEMENT (ARE YOU AIMING MORE THAN 1 SQUARE AWAY?)
    >PROGRAM GETS LIST OF THOSE VALID ACTIONS, CONCATS FRIENDLY 
    >PROGRAM RUNS MOVEMENT.
        IF FRIENDLY MOVES TO ENEMY GRID -> ADD A FIGHT BETWEEN THEM
        IF A FRIENDLY MOVES TO OTHER FRIENDLY -> ADD A 




    */
    abstract class Phase
    {
        public DopGrid.Core.Gamestate gs;
        public List<Enitity.Action> action = new List<Entity.Action>();
        public Turn turn;
        public Phase(List<Enitity.Action> action, String tag,DopGrid.Core.Gamestate gs,Turn turn)
        {
            this.gs = gs;
            this.turn = turn;
            SupplyActions(action);
        }
        public void ClearActions()
        {
            action = new List<Entity.Action>();
        }
        public void SupplyActions(List<Enitity.Action> action)
        {
            ClearActions();
            for (int i = 0; i < action.Count; i++)
            {
                if (action[i].type.equals(tag))
                {
                    this.action.Add(action[i]);
                    action.RemoveAt(i);
                    i -= 1;
                }
            }
        }
        public void AddAction(Enitity.Action action)
        {
            this.action.Add(action);
        }
        public abstract PerformAction() { }
    }
    class MovePhase : Phase
    {
        public override void PerformAction()
        {
            for(int i = 0; i < this.action.Length; i++)
            {
                action[i].PerformChange();
                if (action[i].ActionType.Equals("Move"))
                {
                    for(int f=0; f< gs.GetEntities(); f++)
                    {
                        if(! (gs.GetEntities()[f].Equals[action[i].GetActor()]) && 
                            gs.GetEntities()[f].Getx== action[i].GetActor().Getx &&
                            gs.GetEntities()[f].Gety == action[i].GetActor().Gety
                            )
                        {
                            //Perform an assault on actor at new grid if enemy
                            if (gs.GetEntityAt(f).GetPlayer() != action[i].GetActor().Player)
                            {
                                turn.assaultPhase.AddAction(new Entity.Action("Assault", action[i].GetActor,null, null, gs.GetEntityAt(f), "Assault"));
                            }
                            //Atttempt to move back if moved onto friendly grid
                            else
                            {
                                this.AddAction(new Entity.Action("Move", action[i].GetActor(),action[i].GetStartSquare(), action[i].GetTargetSquare(),null, "MoveBack"));
                            }
                            /*tag,actor,startsqyare,targetsquare,targetUnit,command */
                            
                        }
                    }
                }
            }
        }
    }
    public class CombatPhase : Phase
    {
        public override void PerformAction()
        {

        }
    }
    public class AssaultPhase : Phase
    {
        List<Enitity.Action> action = new List<Entity.Action>();
        public Phase(List<Enitity.Action> action, String tag)
        {
            for (int i = 0; i < action.Count; i++)
            {
                if (action[i].type.equals(tag))
                {
                    this.action.Add(action[i]);
                }
            }
        }
    }
   
    public class ActionPhase : Phase
    {   

    }








    class Turn
    {
        int deploymentPoints, movementPoints;
        Phase[] phases = new Phase[phaseArraySize];
        public void runTurn()
        {
            for(int i=0; i<phaseArraySize; i++)
            {
                phases[i].performMechanics();
            }
        }
       
    }
}
