
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
                if (context.IsFree(newCellPos))
                {
                    var newCell = context.CreateEntity();
                    newCell.AddNewCell(e.cell.genome, e.cell.controller, e.cell.energy, e.color.Color, e.sensor.rot);
                    newCell.AddPosition(newCellPos.x, newCellPos.y);
                    context.Move(newCell, newCellPos);
                    e.isDividing = false;
                    break;
                }
            }
            if (e.isDividing)
            {
                e.isCorpse = true;
                e.isDividing = false;
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isDividing && !entity.isCorpse && !entity.isDestroyed;
    }

    protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Dividing);
    }
}
