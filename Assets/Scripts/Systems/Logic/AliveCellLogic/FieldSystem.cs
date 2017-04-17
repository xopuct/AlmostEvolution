
using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class FieldSystem : ReactiveSystem<GameEntity>
{

    public FieldSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        //Field.Instance.
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPosition;
    }

    protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Position);
    }
}
