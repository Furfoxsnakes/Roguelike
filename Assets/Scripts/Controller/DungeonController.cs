using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Controller;
using Controller.BattleStates;
using DamageNumbersPro;
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
    
    [SerializeField] private DamageNumber FloatingCombatText;

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

    public void DisplayCombatText(int value, Vector3 pos)
    {
        if (FloatingCombatText == null)
        {
            Debug.LogWarning("Set a DamageNumber prefab");
            return;
        }

        FloatingCombatText.enableNumber = true;
        FloatingCombatText.CreateNew(value, pos);
    }

    public void DisplayCombatText(string value, Vector3 pos)
    {
        if (FloatingCombatText == null)
        {
            Debug.LogWarning("Set a DamageNumber prefab");
            return;
        }

        FloatingCombatText.enableNumber = false;
        var combatText = FloatingCombatText.CreateNew(0, pos + new Vector3(0.5f, 0.5f,0));
        combatText.prefix = value;
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
