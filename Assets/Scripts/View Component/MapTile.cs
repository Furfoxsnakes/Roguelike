using System;
using System.Collections;
using System.Collections.Generic;
using GoRogue;
using GoRogue.GameFramework;
using UnityEngine;
using UnityEngine.Tilemaps;
using GameObject = UnityEngine.GameObject;

[CreateAssetMenu(fileName = "Map Tile", menuName = "Game/Tiles/Map Tile")]
public class MapTile : Tile, IGameObject
{
    private IGameObject _go;
    private Tilemap _tilemap;
    
    public float FadeAlpha = 0.15f;

    public Vector3Int Vector3Pos => new Vector3Int(_go.Position.X, _go.Position.Y, 0);

    public void Init(Map map, Tilemap tilemap, Coord pos, bool isWalkable, bool isTransparent, bool isStatic = true)
    {
        _go = new GoRogue.GameFramework.GameObject(position: pos, layer: 0, parentObject: this, isStatic: isStatic,
            isWalkable: isWalkable, isTransparent: isTransparent);
        map.SetTerrain(this);
        _tilemap = tilemap;
        _tilemap.SetTile(new Vector3Int(pos.X, pos.Y, 0), this);
        _tilemap.SetColor(Vector3Pos, new Color(255,255,255,0f));
    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        DungeonMap.FOVWasUpdated += OnFovUpdated;
        
        return base.StartUp(position, tilemap, go);
    }

    private void OnFovUpdated(object sender, EventArgs e)
    {
        if (!CurrentMap.Explored[_go.Position]) return;
        
        if (CurrentMap.FOV.BooleanFOV[_go.Position])
            _tilemap.SetColor(Vector3Pos, new Color(255, 255, 255, 1));
        else
            _tilemap.SetColor(Vector3Pos, new Color(255, 255, 255, FadeAlpha));
    }

    public uint ID => _go.ID;

    public int Layer => _go.Layer;

    public void AddComponent(object component)
    {
        _go.AddComponent(component);
    }

    public T GetComponent<T>()
    {
        return _go.GetComponent<T>();
    }

    public IEnumerable<T> GetComponents<T>()
    {
        return _go.GetComponents<T>();
    }

    public bool HasComponent(Type componentType)
    {
        return _go.HasComponent(componentType);
    }

    public bool HasComponent<T>()
    {
        return _go.HasComponent<T>();
    }

    public bool HasComponents(params Type[] componentTypes)
    {
        return _go.HasComponents(componentTypes);
    }

    public void RemoveComponent(object component)
    {
        _go.RemoveComponent(component);
    }

    public void RemoveComponents(params object[] components)
    {
        _go.RemoveComponents(components);
    }

    public bool MoveIn(Direction direction)
    {
        return _go.MoveIn(direction);
    }

    public void OnMapChanged(Map newMap)
    {
        _go.OnMapChanged(newMap);
    }

    public Map CurrentMap => _go.CurrentMap;

    public bool IsStatic => _go.IsStatic;

    public bool IsTransparent
    {
        get => _go.IsTransparent;
        set => _go.IsTransparent = value;
    }

    public bool IsWalkable
    {
        get => _go.IsWalkable;
        set => _go.IsWalkable = value;
    }

    public Coord Position
    {
        get => _go.Position;
        set => _go.Position = value;
    }

    public event EventHandler<ItemMovedEventArgs<IGameObject>> Moved
    {
        add => _go.Moved += value;
        remove => _go.Moved -= value;
    }
}
