using System.Collections;
using System.Collections.Generic;
using Controller;
using GoRogue;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    public DungeonMap Map;
    public Player Player;

    public StateMachine StateMachine;
    
    // Start is called before the first frame update
    void Start()
    {
        Map.Generate(100, 100);
        // set the camera to follow the player
        GameController.Instance.Cinemachine.Follow = Map.Player.transform;
        InputController.MoveEvent += OnMoveEvent;
    }

    private void OnMoveEvent(object sender, InfoEventArgs<Coord> e)
    {
        Map.Player.TryMove(e.Info);
        Map.UpdatePlayerFOV();
    }
}
