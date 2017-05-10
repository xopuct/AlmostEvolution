﻿
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class MoveSystem : IExecuteSystem
{
    private IGroup<GameEntity> _group;
    GameContext context;
    public MoveSystem(Contexts contexts)
    {
        _group = contexts.game.GetGroup(GameMatcher.Cell);
        context = contexts.game;
    }

    public void Execute()
    {
        foreach (var e in _group.GetEntities())
        {
            if (e.isCorpse || (e.cell.CurrentGene != 9 && e.cell.CurrentGene != 8 && e.cell.CurrentGene != 0))
                continue;
            var move = e.cell.CurrentGene == 9;
            var eat = move || e.cell.CurrentGene == 8;
            var obstacle = SensorHelper.GetEntityInEyeLook(e);
            var targetPos = e.position + e.sensor.sensor;
            targetPos.x = (int)Mathf.Repeat(targetPos.x, context.fieldWidth);

            if (obstacle == null)
            {
                if (move)
                    context.Move(e, targetPos);
                e.ReplaceController(e.cell.controller + 1);
            }
            else
            {
                //if (cell.Obstacle.layer == wallLayer)    // стена
                //{
                //    e.ReplaceController(e.cell.controller + 1);
                //    return;
                //}
                //else
                if (CellHelper.IsCell(obstacle))
                {
                    var targetCalories = obstacle.cell.energy;
                    int newEnergy = e.cell.energy + targetCalories;
                    if (obstacle.isCorpse)
                    {
                        e.ReplaceController(e.cell.controller + 3);
                    }
                    else
                    {
                        if (CellHelper.CheckRelations(e, obstacle))
                        {
                            e.ReplaceController(e.cell.controller + 4);
                            newEnergy = e.cell.energy + targetCalories / 2;
                        }
                        else
                        {
                            e.ReplaceController(e.cell.controller + 5);
                        }
                    }
                    if (eat)
                    {
                        e.ReplaceEnergy(newEnergy);
                        obstacle.isDestroyed = true;
                        e.ReplaceColor(e.color.ChangeColor(1, -1, 1));
                    }
                    if (move)
                        context.Move(e, targetPos);
                }
            }
        }
    }
}
