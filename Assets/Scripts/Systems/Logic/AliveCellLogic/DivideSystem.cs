
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
                    context.CreateEntity().AddNewCell(e.cell.genome, e.cell.controller, e.cell.energy, e.color.Color, (int)e.sensor.rot, newCellPos);
                    e.isDividing = false;
                    break;
                }
            }
            if (e.isDividing)
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
