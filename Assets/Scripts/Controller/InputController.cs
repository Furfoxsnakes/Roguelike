using System;
using System.Collections;
using System.Collections.Generic;
using GoRogue;
using UnityEngine;

public class Repeater
{
    private const float Threshold = 0.5f;
    private const float Rate = 0.15f;

    private float _next;
    private bool _hold;
    private string _axis;
    private KeyCode _key;

    public Repeater(KeyCode key)
    {
        _key = key;
    }

    public int Update()
    {
        var retValue = 0;
        var value = Mathf.RoundToInt(Input.GetAxisRaw(_axis));
        
        if (value != 0)
        {
            if (Time.time > _next)
            {
                retValue = value;
                _next = Time.time + (_hold ? Rate : Threshold);
                _hold = true;
            }
        }
        else
        {
            _hold = false;
            _next = 0;
        }

        return retValue;
    }
}

public class InputController : MonoBehaviour
{
    public static event EventHandler<InfoEventArgs<Direction>> MoveEvent;

    private Dictionary<KeyCode, Direction> _inputs;

    private void Start()
    {
        Direction.YIncreasesUpward = true;
        _inputs = new Dictionary<KeyCode, Direction>()
        {
            {KeyCode.Keypad1, Direction.DOWN_LEFT},
            {KeyCode.Keypad2, Direction.DOWN},
            {KeyCode.Keypad3, Direction.DOWN_RIGHT},
            {KeyCode.Keypad4, Direction.LEFT},
            {KeyCode.Keypad5, Direction.NONE},
            {KeyCode.Keypad6, Direction.RIGHT},
            {KeyCode.Keypad7, Direction.UP_LEFT},
            {KeyCode.Keypad8, Direction.UP},
            {KeyCode.Keypad9, Direction.UP_RIGHT}
        };
    }

    void Update()
    {
        foreach (var key in _inputs.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                MoveEvent?.Invoke(this, new InfoEventArgs<Direction>(_inputs[key]));
                break;
            }
        }
    }
}
