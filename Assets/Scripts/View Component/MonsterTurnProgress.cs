using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterTurnProgress : MonoBehaviour
{

    [SerializeField] private TextMeshPro _text;

    private void Awake()
    {
        this.AddObserver(OnMonsterTurnChanged, "MonsterTurnChanged");
    }

    private void OnMonsterTurnChanged(object sender, object args)
    {
        var currentMonster = (int)args;
        _text.text = $"{currentMonster} / {GameController.Instance.DungeonMap.Data.Entities.Count}";
    }
}
