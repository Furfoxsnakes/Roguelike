using Enums;
using GoRogue;

namespace Controller.BattleStates
{
    public class PlayerTurnDungeonState : DungeonState
    {
        protected override void OnMoveEvent(object sender, InfoEventArgs<Direction> e)
        {
            if (Player.MoveIn(e.Info))
            {
                Map.UpdatePlayerFOV();
                StateMachine.ChangeState<MonsterTurnDungeonState>();
                return;
            }
            
            // try to attack
            var target = Player.CurrentMap.GetEntity<Actor>(Player.Position + e.Info);

            if (target == null) return;
            
            if (Player.GetComponent<AttackAction>().Perform(target, Player.Stats[StatTypes.Attack]))
                StateMachine.ChangeState<MonsterTurnDungeonState>();
        }
    }
}