using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controller;
using Enums;
using GoRogue.MapViews;
using UnityEngine;

public class MoveTowardsAndAttackBehaviour : MonsterBehaviour
{
    public override bool Do()
    {
        Owner.Fov.Calculate(Owner.Position, AggroRange);
        if (!Owner.Fov.CurrentFOV.Contains(Player.Position)) return false;
        
        // TODO: check for ranged attack component
        // var attackAction = GetComponent<AttackAction>();
        //
        // if (attackAction != null)
        //     if (attackAction.Perform(Player))
        //         return true;
        
        var moveAction = GetComponent<MoveAction>();
        if (moveAction == null) return false;

        var target = moveAction.Perform();
        return Owner.BumpInto(target);
    }
}
