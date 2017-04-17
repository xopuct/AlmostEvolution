
using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class DivideSystem : ReactiveSystem<GameEntity>
{
    GameContext context;

    public DivideSystem(Contexts contexts) : base(contexts.game) { context = contexts.game; }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            for (int i = e.sensor.sensors.Length - 1; i >= 0; i--)
            {
                var newCellPos = e.position + e.sensor.sensors[i];
                if (Field.Instance.IsFree(newCellPos))
                {
                    context.CreateEntity().AddNewCell(e.cell.genome, e.color.Color, (int)e.sensor.rot, newCellPos);
                    break;
                }
            }
            e.isCorpse = true;
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isDividing;
    }

    protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Dividing);
    }
}
