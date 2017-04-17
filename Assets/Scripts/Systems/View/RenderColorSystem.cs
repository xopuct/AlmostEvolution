using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class RenderColorSystem : ReactiveSystem<GameEntity>
{

    public RenderColorSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Color);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.hasColor;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var pos = e.position;
            e.view.renderer.color = e.color.Color;
        }
    }
}
