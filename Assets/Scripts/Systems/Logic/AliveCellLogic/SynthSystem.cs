
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class SynthSystem : ReactiveSystem<GameEntity>
{
    public SynthSystem(Contexts contexts) : base(contexts.game) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.cell.energy += (int)(e.position.Y * LevelManager.Instance.SynthMultipler);
            e.cell.controller++;
            e.ReplaceColor(e.color.ChangeColor(-0.1f, 0.1f, -0.1f));
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return !entity.isCorpse && !entity.hasNewCell && entity.cell.CurrentGene == 10;
    }

    protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Cell);
    }
}
