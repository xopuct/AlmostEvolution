
using System;
using Entitas;
using UnityEngine;


public class EnergySystem : IExecuteSystem
{

    readonly IGroup<GameEntity> _group;

    public EnergySystem(Contexts contexts)
    {
        _group = contexts.game.GetGroup(GameMatcher.Cell);
    }

    public void Execute()
    {
        foreach (var e in _group.GetEntities())
        {
            if (e.isCorpse || e.isDividing)
                continue;

            if (e.cell.controller > 63) e.ReplaceCell(e.cell.genome, e.cell.energy - 1, e.cell.controller - 64);


            e.ReplaceCell(e.cell.genome, e.cell.energy - 1, e.cell.controller + 2);

            if (e.cell.energy > LevelManager.Instance.EnergyToDivide)
            {
                e.ReplaceCell(e.cell.genome, e.cell.energy / 2, e.cell.controller + 2);
                e.isDividing = true;
            }
            if (e.cell.energy <= 0)
                e.isCorpse = true;

            if (e.cell.CurrentGene == 11)
            {
                int cnt = e.cell.controller + 1;
                if (cnt > 63) cnt -= 64;
                if (e.cell.energy < e.cell.genome[cnt] * 15)
                {
                    e.ReplaceCell(e.cell.genome, e.cell.energy, e.cell.controller + 2);
                }
                if (e.cell.energy >= e.cell.genome[cnt] * 15)
                {
                    e.ReplaceCell(e.cell.genome, e.cell.energy, e.cell.controller + 2);
                }
            }
            if (e.cell.CurrentGene > 12)
            {
                e.ReplaceCell(e.cell.genome, e.cell.energy, e.cell.controller + e.cell.CurrentGene);
                return;
            }
        }
    }
}
