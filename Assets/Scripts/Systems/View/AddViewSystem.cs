
using Entitas;
using UnityEngine;
using System.Collections.Generic;
using System;

public class AddViewSystem : ReactiveSystem<GameEntity>
{
    Dictionary<string, GameObject> prototypes;

    public AddViewSystem(Contexts contexts) : base(contexts.game)
    {
        prototypes = new Dictionary<string, GameObject>();
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Cell);
    }

    protected override bool Filter(GameEntity entity)
    {
        return !entity.hasView;
    }

    readonly Transform _viewContainer = new GameObject("Views").transform;
    GameObject GetPrototype(string name)
    {
        GameObject prototype;
        if (!prototypes.TryGetValue(name, out prototype))
        {
            prototype = Resources.Load<GameObject>(name);
            prototypes[name] = prototype;
        }
        return prototype;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var assetName = GetPrototype("cell");
            GameObject gameObject = null;
            try
            {
                gameObject = GameObjectPool.Instance.Instantiate(assetName, Vector3.zero, Quaternion.identity);
            }
            catch (Exception)
            {
                Debug.Log("Cannot instantiate " + assetName);
            }

            if (gameObject != null)
            {
                gameObject.transform.parent = _viewContainer;
                e.AddView(gameObject, gameObject.GetComponentInChildren<SpriteRenderer>());
            }
        }
    }
}
