
using System;
using Entitas;
using UnityEngine;


public class DeadSystem : IExecuteSystem
{
    readonly IGroup<GameEntity> _group;
    Vector2i dir = new Vector2i(0, -1);

    public DeadSystem(Contexts contexts)
    {
        _group = contexts.game.GetGroup(GameMatcher.Corpse);
    }

    public void Execute()
    {
        foreach (var e in _group.GetEntities())
        {
            e.ReplaceColor(Color.gray);
            var enegry = e.cell.energy;
            if (enegry > LevelManager.Instance.LowLevelCorpseEnergy)
                enegry = (int)(enegry * LevelManager.Instance.CorpseEnergyReduction);
            else if (enegry > LevelManager.Instance.MinCorpseEnergy)
                enegry -= 1;
            if (enegry <= 0)
            {
                e.isDestroyed = true;
                Field.Instance.Clear(e.position);
            }
            e.ReplaceEnergy(enegry);

            if (Field.Instance.IsFree(e.position + dir))
            {
                Vector2i pos = e.position;
                pos += dir;
                Field.Instance.Move(e, pos);
                continue;
            }
        }
    }
}
