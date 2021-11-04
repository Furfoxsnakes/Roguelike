using System;
using System.Collections;
using System.Collections.Generic;
using Common.Actions;
using DamageNumbersPro;
using DG.Tweening;
using GoRogue;
using GoRogue.GameFramework;
using GoRogue.Pathing;
using UnityEngine;
using View_Component;
using GameObject = UnityEngine.GameObject;
using Random = UnityEngine.Random;

public class Actor : MonoBehaviour, IGameObject
{
    private IGameObject _go;
    
    [SerializeField] private GameObject _visuals;
    [SerializeField] private bool _isWalkable = false;
    [SerializeField] private bool _isTransparent = true;
    [SerializeField] private DamageNumber _damageNumberPrefab;

    public bool NeedsUserInput => _needsUserInput;
    [SerializeField] private bool _needsUserInput;

    public string Name;
    public int Awareness = 4;
    public Stats Stats;

    public AStar Pathing;
    public FOV Fov;

    public Vector3Int Vector3Pos => new Vector3Int(_go.Position.X, _go.Position.Y, 0);

    public void Init(Coord pos, Map map)
    {
        _go = new GoRogue.GameFramework.GameObject(pos, 1, this, isWalkable: _isWalkable, isTransparent: _isTransparent);
        gameObject.transform.position = new Vector3(pos.X, pos.Y, 0);
        map.AddEntity(this);
        Pathing = new AStar(map.WalkabilityView, Distance.CHEBYSHEV);
        Fov = new FOV(map.WalkabilityView);
        Moved += OnMoved;
        DungeonMap.FOVWasUpdated += OnFovUpdated;
    }

    private void OnFovUpdated(object sender, EventArgs e)
    {
        if (CurrentMap.FOV.BooleanFOV[_go.Position])
            _visuals.SetActive(true);
        else
            _visuals.SetActive(false);
    }

    /// <summary>
    /// Displays the floating combat text
    /// anything 0 and less is considered a miss
    /// </summary>
    /// <param name="amount"></param>
    public virtual void TakeDamage(int amount)
    {
        GameController.Instance.DungeonController.DisplayCombatText(amount, Vector3Pos);
        // _damageNumberPrefab.CreateNew(amount, transform.position + new Vector3(0.5f, 0.5f,0));
    }

    protected virtual void OnMoved(object sender, ItemMovedEventArgs<IGameObject> e)
    {
        gameObject.transform.DOMove(new Vector3(e.NewPosition.X, e.NewPosition.Y, 0), 0.15f);
    }

    public virtual bool BumpInto(IGameObject go)
    {
        if (go == null) return false;
        
        if (go is Actor actor)
        {
            var meleeAction = GetComponent<MeleeAttackAction>();
            if (meleeAction != null)
            {
                meleeAction.Perform(actor);
                return true;
            }
        }

        return false;
    }

    #region IGameObject

    public uint ID => _go.ID;

    public int Layer => _go.Layer;

    public void AddComponent(object component)
    {
        _go.AddComponent(component);
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

    #endregion
    
}
