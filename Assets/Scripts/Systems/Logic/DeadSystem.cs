
using System;
using Entitas;
using UnityEngine;


public class DeadSystem : IExecuteSystem
{
    readonly IGroup<GameEntity> _group;
    Vector2i dir = new Vector2i(0, -1);
    GameContext context;

    public DeadSystem(Contexts contexts)
    {
        _group = contexts.game.GetGroup(GameMatcher.Corpse);
        context = contexts.game;
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
            }
            e.ReplaceEnergy(enegry);
            context.Move(e, e.position + dir);
        }
    }
}
