﻿
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

            e.ReplaceEnergy(e.cell.energy - 1);

            if (e.cell.energy > LevelManager.Instance.EnergyToDivide)
            {
                e.ReplaceEnergy(e.cell.energy / 2);
                e.isDividing = true;
            }
            if (e.cell.energy <= LevelManager.Instance.MinCorpseEnergy)
                e.isCorpse = true;

            if (e.cell.CurrentGene == 11)
            {
                int cnt = e.cell.controller + 1;
                if (cnt > 63) cnt -= 64;
                if (e.cell.energy < e.cell.genome[cnt] * 15)
                {
                    e.ReplaceController(e.cell.controller + 2);
                }
                if (e.cell.energy >= e.cell.genome[cnt] * 15)
                {
                    e.ReplaceController(e.cell.controller + 3);
                }
            }
            if (e.cell.CurrentGene > 11)
            {
                e.ReplaceController(e.cell.controller + e.cell.CurrentGene);
            }
        }
    }
}