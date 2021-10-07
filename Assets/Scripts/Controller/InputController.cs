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

    public Repeater(string axisName)
    {
        _axis = axisName;
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
    public static event EventHandler<InfoEventArgs<Coord>> MoveEvent;

    private Repeater _hor = new Repeater("Horizontal");
    private Repeater _ver = new Repeater("Vertical");

    void Update()
    {
        var x = _hor.Update();
        var y = _ver.Update();
        
        if (x != 0 || y != 0)
            MoveEvent?.Invoke(this, new InfoEventArgs<Coord>(new Coord(x,y)));
    }
}
