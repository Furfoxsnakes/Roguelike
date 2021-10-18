using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Enums;
using GoRogue;
using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using GoRogue.MapViews;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonMap : MonoBehaviour
{
    public Map Data;

    public Tilemap Terrain;

    [SerializeField] private MapTile _wallTile;
    [SerializeField] private MapTile _floorTile;
    [SerializeField] private Actor _monsterPrefab;

    public Player Player;

    public static event EventHandler FOVWasUpdated;

    public void Generate(int width, int height)
    {
        var terrainMap = new ArrayMap2D<bool>(width, height);
        // QuickGenerators.GenerateRectangleMap(terrainMap);
        QuickGenerators.GenerateRandomRoomsMap(terrainMap, 20, 7, 12);
        
        Data = new Map(terrainMap.Width, terrainMap.Height, 1, Distance.CHEBYSHEV);

        foreach (var position in terrainMap.Positions())
        {
            if (terrainMap[position])
            {
                var floorTile = Instantiate(_floorTile);
                floorTile.Init(map: Data, tilemap: Terrain, position, isWalkable: true, isTransparent: true);
            }
            else
            {
                var wallTile = Instantiate(_wallTile);
                wallTile.Init(map: Data, tilemap: Terrain, position, isWalkable: false, isTransparent: false);
            }
        }
        
        // spawn some monsters
        for (var i = 0; i < 30; i++)
        {
            var randomPos = Data.WalkabilityView.RandomPosition(true);
            var monster = Instantiate(_monsterPrefab);
            monster.Init(randomPos, Data);
        }
        
        // spawn the player
        var playerPrefab = Resources.Load<Player>("Actors/Player");
        Player = Instantiate(playerPrefab);
        var randomStartingPos = Data.WalkabilityView.RandomPosition(true);
        Player.Init(randomStartingPos, Data);
        
        UpdatePlayerFOV();
    }

    public void UpdatePlayerFOV()
    {
        Data.CalculateFOV(Player.Position, Player.Stats[StatTypes.Awareness]);
        FOVWasUpdated?.Invoke(this, EventArgs.Empty);
    }
}
