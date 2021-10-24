using System.Collections;
using System.Collections.Generic;
using Controller;
using GoRogue;
using GoRogue.GameFramework;
using GameObject = UnityEngine.GameObject;

public class MoveTowardsPlayerMoveAction : MoveAction
{
    public override IGameObject Perform()
    {
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();
        var path = Owner.Pathing.ShortestPath(Owner.Position, player.Position);
        if (path == null) return null;
        
        NewPos = path.GetStep(0);

        return base.Perform();
    }
}
