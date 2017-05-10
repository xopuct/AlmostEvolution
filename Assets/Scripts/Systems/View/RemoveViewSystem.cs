using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class RemoveViewSystem : ReactiveSystem<GameEntity>
{

    public RemoveViewSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return new Collector<GameEntity>(
            new IGroup<GameEntity>[] {
                context.GetGroup(GameMatcher.View),
                context.GetGroup(GameMatcher.Destroyed),
            },
            new GroupEvent[] {
                GroupEvent.AddedOrRemoved,
                GroupEvent.Added
            }
        );
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.isDestroyed;
    }


    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            bool shouldDestroy = false;
            GameObjectPool.Instance.Destroy(e.view.gameObject, out shouldDestroy);
            if (shouldDestroy)
                Object.Destroy(e.view.gameObject);
            e.RemoveView();
        }
    }
}