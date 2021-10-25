using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Controller.BattleStates;
using GoRogue;
using UnityEngine;

public class MonsterTurnDungeonState : DungeonState
{
    public override void Enter()
    {
        base.Enter();
        AnnouncementSystem.ShowMessage("Waiting...");
    }

    public override void Exit()
    {
        base.Exit();
        AnnouncementSystem.Hide();
    }

    private void Update()
    {
        Iterator.MoveNext();
        var actor = DungeonController.Iterator.Current;
        if (actor.NeedsUserInput)
        {
            DungeonController.IsPlayerTurn = true;
            StateMachine.ChangeState<PlayerTurnDungeonState>();
        }
        else
            actor.GetComponent<MonsterBehaviour>().Do();
    }
}
