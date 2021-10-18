using System.Collections;
using System.Collections.Generic;
using Controller;
using GoRogue;
using UnityEngine;

public class MoveTowardsPlayerMoveAction : MoveAction
{
    public override bool Perform()
    {
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();
        var directionToPlayer = Direction.GetDirection(Owner.Position, player.Position);
        NewPos = Owner.Position + directionToPlayer;
        
        return base.Perform();
    }
}
