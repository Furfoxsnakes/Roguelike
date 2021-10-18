using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class MonsterBehaviour : MonoBehaviour
{
    protected Actor Owner;
    protected Player Player;

    private void Start()
    {
        Owner = GetComponent<Actor>();
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public virtual bool Do()
    {
        return true;
    }
}
