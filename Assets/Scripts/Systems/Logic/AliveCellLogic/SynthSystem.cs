
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class SynthSystem : IExecuteSystem
{
    private IGroup<GameEntity> _group;

    public SynthSystem(Contexts contexts)
    {
        _group = contexts.game.GetGroup(GameMatcher.Cell);
    }
    public void Execute()
    {
        foreach (var e in _group.GetEntities())
        {
            if (e.isCorpse || e.cell.CurrentGene != 10)
                continue;
            e.ReplaceEnergy(e.cell.energy + (int)(e.position.Y * LevelManager.Instance.SynthMultipler));
            e.ReplaceController(e.cell.controller + 1);
            e.ReplaceColor(e.color.ChangeColor(-0.1f, 0.1f, -0.1f));
        }
    }
}
