using System;
using GoRogue;
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

    public virtual bool Perform()
    {
        if (NewPos == Coord.NONE) return false;
        
        var map = Owner.CurrentMap;
        if (!map.WalkabilityView[NewPos]) return false;

        Owner.Position = NewPos;
        NewPos = Coord.NONE;
        
        return true;
    }
}
