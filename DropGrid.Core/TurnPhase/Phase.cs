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
    // Name !! means Name is an ecplicit phase
    //Deploy !!
    //Resolve deployment errors with assault step
    //Move step post-assault
    //Attack !!
    //Move !!
    //Resolve Enemy & friend movign onto same square with assault
    //Resolve assault resolution && double up friends with Movement
    //Ability !!
    public int ACTIONTYPES = 4;
    enum ActionType
    {
        DEPLOYMENT,
        ASSAULT,
        ATTACK,
        RESOLOUTION,
        MOVE,
        ABILITY
    }

    abstract class Phase
    {
        public DropGrid.Core.Gamestate gs;
        public List<Enitity> enitities = new List<Enitity>();
        public Turn turn;
        public ActionType saidAction=null;
        /// <summary>
        /// Phase Constructor to produce a single Phase of actions.
        ///by using the clearActions, SupplyActions  commands, Phases can be 'recycled' between turns
        /// </summary>
        /// <param name="enitities">The list of entities to action. Usualy will be PlayerUnit entities</param>
        /// <param name="gs">The gamestate to be passed. TESTING ONLY</param>
        /// <param name="turn">The turn this phase belongs to</param>
        /// <param name="saidAction">The action enum to run in this specific 'PerformActions'</param>
        public Phase(List<Enitity> enitities, Turn turn, ActionType saidAction, DopGrid.Core.Gamestate gs = null)
        {
            this.gs = gs;
            this.turn = turn;
            this.saidAction = saidAction;
            SupplyActions(action);
        }
        public void ClearActions()
        {
            action = new List<Entity.Action>();
        }
        public void SupplyActions(List<Enitity> enitities)
        {
            ClearActions();
            for (int i = 0; i < enitities.Count; i++)
            {
                if (enitities[i].type.equals(tag))
                {
                    this.enitities.Add(action[i]);
                    enitities.RemoveAt(i);
                    i -= 1;
                }
            }
        }
        public void AddEntity(Enitity entity)
        {
            this.enitities.Add(entity);
        }
        public void PerformActions()
        {
            List<Entity> ents = entites.Clone();
            for (int f = 0; f < ents.Count; f++)
            {
                for (int y = 0; y < ents[f].GetActions().Count; y++)
                {
                    AddEntity.Action actionToBeDone = ents[f].GetActions()[y];
                    if (actionToBeDone.Type == saidAction)
                    {
                        ents[f].Actioned = true;
                        if (actionToBeDone.Target != null && actionToBeDone.Target.Actioned == false)
                        {
                            CheckSamePriorityUnit(actionToBeDone.Target,saidAction);
                        }
                        actionToBeDone.EnactAction();
                        e.GetActions().Remove(actionToBeDone);
                        y--;
                    }
                }
            }

        }
        public class MovementPhase : Phase
        {
            PerformActions()
            {
                List<Entity> ents = entites.Clone();
                for (int f = 0; f < ents.Count; f++)
                {
                    for (int y = 0; y < ents[f].GetActions().Count; y++)
                    {
                        AddEntity.Action actionToBeDone = ents[f].GetActions()[y];
                        if (actionToBeDone.Type == saidAction)
                        {
                            ents[f].Actioned = true;
                            if (actionToBeDone.TargetGrid != null)
                            {
                                for(int z=0;z< actionToBeDone.TargetGrid.unitsOn.Count; z++)
                                {
                                    CheckSamePriorityUnit(actionToBeDone.Target, i);
                                }
                            }
                            actionToBeDone.EnactAction();
                            if (actionToBeDone.TargetGrid.unitsOn.Count>1)
                            {
                                mts[f].SetActions(new Action(RESOLOUTION, null, actionToBeDone.TargetGrid, actionToBeDone.StartGrid));
                            }
                            e.GetActions().Remove(actionToBeDone);
                            y--;
                        }
                    }
                }
            }
        }
        public class ResolutionPhase : Phase
        {
            PerformActions()
            {
                List<Entity> ents = entites.Clone();
                for (int f = 0; f < ents.Count; f++)
                {
                    for (int y = 0; y < ents[f].GetActions().Count; y++)
                    {
                        AddEntity.Action actionToBeDone = ents[f].GetActions()[y];
                        if (actionToBeDone.Type == saidAction)
                        {
                            ents[f].Actioned = true;
                            if (actionToBeDone.TargetGrid != null)
                            {
                                for (int z = 0; z < actionToBeDone.TargetGrid.unitsOn.Count; z++)
                                {
                                    CheckSamePriorityUnit(actionToBeDone.Target, i);
                                }
                            }
                            actionToBeDone.EnactAction();
                            if (actionToBeDone.TargetGrid.unitsOn.Count > 1)
                            {
                                mts[f].SetActions(new Action(RESOLOUTION, null, actionToBeDone.TargetGrid, actionToBeDone.StartGrid));
                            }
                            e.GetActions().Remove(actionToBeDone);
                            y--;
                        }
                    }
                }
            }
        }
        public void CheckSamePriorityUnit(Entity e, ActionType ActionPrio)
        {
            for (int y = 0; y < e.GetActions().Count; y++)
            {
                Entity.Action actionToBeDone = e.GetActions()[y];
                if (actionToBeDone.Type == ActionPrio)
                {
                    e.actioned = true;
                    if (actionToBeDone.Target != null && actionToBeDone.Target.actioned == false)
                    {
                        CheckSamePriorityUnit(actionToBeDone.Target);

                    }
                    actionToBeDone.EnactAction();
                    e.GetActions().Remove(actionToBeDone);
                    y--;
                }
            }
        }
    }
    class Turn
    {
        int deploymentPoints, movementPoints;
        Phase[] phases = new Phase[phaseArraySize];
        public void runTurn()
        {
            for (int i = 0; i < phaseArraySize; i++)
            {
                phases[i].performMechanics();
            }
        }

    }
}
