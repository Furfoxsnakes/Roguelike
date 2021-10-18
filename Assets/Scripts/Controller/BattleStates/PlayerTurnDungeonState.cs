using Enums;
using GoRogue;

namespace Controller.BattleStates
{
    public class PlayerTurnDungeonState : DungeonState
    {
        protected override void OnMoveEvent(object sender, InfoEventArgs<Coord> e)
        {
            if (Player.TryMove(e.Info))
            {
                Map.UpdatePlayerFOV();
                StateMachine.ChangeState<MonsterTurnDungeonState>();
                return;
            }
            
            // try to attack
            var target = Player.CurrentMap.GetEntity<Actor>(Player.Position + e.Info);

            if (Player.GetComponent<AttackAction>().Perform(target, Player.Stats[StatTypes.Attack]))
                StateMachine.ChangeState<MonsterTurnDungeonState>();
        }
    }
}