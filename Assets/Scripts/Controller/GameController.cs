using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using GoRogue;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance => _instance;
    private static GameController _instance;

    public CinemachineVirtualCamera Cinemachine => _cinemachine;
    [SerializeField] private CinemachineVirtualCamera _cinemachine;
    public DungeonMap DungeonMap;
    public DungeonController DungeonController;

    private void Awake()
    {
        Direction.YIncreasesUpward = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
            _instance = this;
        else
            DestroyImmediate(this);

        DontDestroyOnLoad(this);
    }
}
