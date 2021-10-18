using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class MoveTowardsAndAttackBehaviour : MonsterBehaviour
{
    public override bool Do()
    {
        var attackAction = GetComponent<AttackAction>();

        if (attackAction != null)
            if (attackAction.Perform(Player, Owner.Stats[StatTypes.Attack]))
                return true;
        
        var moveAction = GetComponent<MoveAction>();

        if (moveAction == null) return false;

        return moveAction.Perform();
    }
}
