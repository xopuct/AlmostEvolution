
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
            if (e.cell.energy > LevelManager.Instance.LowLevelCorpseEnergy)
                e.cell.energy = (int)(e.cell.energy * LevelManager.Instance.CorpseEnergyReduction);
            else if (e.cell.energy > LevelManager.Instance.MinCorpseEnergy)
                e.cell.energy -= 1;
            if (e.cell.energy <= 0)
                e.isDestroyed = true;

            if (Field.Instance.IsFree(e.position + dir))
            {
                Vector2i pos = e.position;
                pos += dir;
                e.ReplacePosition(pos.x, pos.y);
                continue;
            }
        }
    }
}
