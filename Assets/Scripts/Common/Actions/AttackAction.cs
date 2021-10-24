using System;
using System.Collections;
using System.Collections.Generic;
using GoRogue;
using Interfaces;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class AttackAction : MonoBehaviour
{
    protected Actor Owner;
    protected Actor Target;
    public int Range = 1;

    private void Start()
    {
        Owner = GetComponent<Actor>();
    }

    public virtual bool Perform(Actor target)
    {
        return true;
    }
}
