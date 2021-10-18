using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Controller.BattleStates;
using GoRogue;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    public DungeonMap Map;
    public Player Player;
    public StateMachine StateMachine;
    public bool IsPlayerTurn = true;
    public IEnumerator<Actor> Iterator;
    public Actor CurrentActorTurn;

    // Start is called before the first frame update
    void Start()
    {
        StateMachine = GetComponent<StateMachine>();
        Map.Generate(100, 100);
        // set the camera to follow the player
        GameController.Instance.Cinemachine.Follow = Map.Player.transform;
        StateMachine.ChangeState<PlayerTurnDungeonState>();
        Iterator = CreateActorTurnEnumerable().GetEnumerator();
    }

    private IEnumerable<Actor> CreateActorTurnEnumerable()
    {
        while (true)
        {
            foreach (Actor actor in Map.Data.Entities.Items)
            {
                yield return actor;
            }
        }
    }
}
