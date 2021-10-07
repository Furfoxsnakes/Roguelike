using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController Instance => _instance;
    private static GameController _instance;

    public CinemachineVirtualCamera Cinemachine => _cinemachine;
    [SerializeField] private CinemachineVirtualCamera _cinemachine;

    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
            _instance = this;
        else
            DestroyImmediate(this);

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
