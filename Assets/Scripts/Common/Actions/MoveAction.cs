using System;
using GoRogue;
using GoRogue.GameFramework;
using GoRogue.Pathing;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class MoveAction : MonoBehaviour
{
    protected Actor Owner;
    protected Coord NewPos = Coord.NONE;

    private void Start()
    {
        Owner = GetComponent<Actor>();
    }

    public virtual IGameObject Perform()
    {
        if (NewPos == Coord.NONE) return null;
        
        var map = Owner.CurrentMap;
        if (!map.WalkabilityView[NewPos])
        {
            var target = Owner.CurrentMap.GetObject(NewPos);
            if (target != null)
                return target;
        }

        Owner.Position = NewPos;
        NewPos = Coord.NONE;
        
        return null;
    }
}
