using System;
using System.Collections.Generic;
using GoRogue;

namespace Controller.BattleStates
{
    public class DungeonState : State
    {
        protected DungeonController DungeonController;
        protected StateMachine StateMachine => DungeonController.StateMachine;
        protected IEnumerator<Actor> Iterator => DungeonController.Iterator;
        protected Player Player => DungeonController.Map.Player;
        protected DungeonMap Map => DungeonController.Map;

        private void Awake()
        {
            DungeonController = GetComponent<DungeonController>();
        }

        protected override void AddListeners()
        {
            InputController.MoveEvent += OnMoveEvent;
        }

        protected override void RemoveListeners()
        {
            InputController.MoveEvent -= OnMoveEvent;
        }
        
        protected virtual void OnMoveEvent(object sender, InfoEventArgs<Coord> e)
        {
            
        }
    }
}