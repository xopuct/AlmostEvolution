
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class TurnSystem : ReactiveSystem<GameEntity>
{
    public TurnSystem(Contexts contexts) : base(contexts.game) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var rot = e.sensor.rot;
            rot += 45 * e.cell.CurrentGene;
            if (e.sensor.rot >= 360) rot -= 360;
            e.ReplaceRot(rot);
            e.ReplaceCell(e.cell.genome, e.cell.energy, e.cell.controller + 1);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return !entity.isCorpse && !entity.hasNewCell && entity.cell.CurrentGene > 0 && entity.cell.CurrentGene < 8;
    }

    protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Cell);
    }
}
