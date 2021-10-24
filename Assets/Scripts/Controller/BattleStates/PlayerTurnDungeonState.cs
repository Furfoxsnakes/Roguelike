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
            
            // bump into the target
            var target = Player.CurrentMap.GetObject(Player.Position + e.Info);
            if (Player.BumpInto(target))
                StateMachine.ChangeState<MonsterTurnDungeonState>();
        }
    }
}