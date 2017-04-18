
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class TurnSystem : IExecuteSystem
{
    private IGroup<GameEntity> _group;

    public TurnSystem(Contexts contexts)
    {
        _group = contexts.game.GetGroup(GameMatcher.Cell);
    }
    public void Execute()
    {
        foreach (var e in _group.GetEntities())
        {
            if (e.isCorpse || (e.cell.CurrentGene == 0 || e.cell.CurrentGene > 7))
                continue;

            var rot = e.sensor.rot;
            rot += 45 * e.cell.CurrentGene;
            if (rot >= 360) rot = (int)Mathf.Repeat(rot, 360);
            e.ReplaceRot(rot);
            e.ReplaceController(e.cell.controller + 1);
        }
    }
}
