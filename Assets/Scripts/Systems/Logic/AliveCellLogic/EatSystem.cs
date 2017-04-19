
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class EatSystem : IExecuteSystem
{
    private IGroup<GameEntity> _group;
    private GameContext context;
    public EatSystem(Contexts contexts)
    {
        _group = contexts.game.GetGroup(GameMatcher.Cell);
        context = contexts.game;
    }

    public void Execute()
    {
        foreach (var e in _group.GetEntities())
        {
            if (e.isCorpse || e.cell.CurrentGene != 8)
                continue;

            var obstacle = SensorHelper.GetEntityInEyeLook(e);
            if (obstacle == null)                                                  // Пусто
            {
                e.ReplaceController(e.cell.controller + 1);
                break;
            }

            //if (cell.Obstacle.layer == wallLayer)    // стена
            //{
            //    e.ReplaceController(e.cell.controller + 1);
            //    return;
            //}
            //else
            if (CellHelper.IsCell(obstacle))     // бот
            {
                var targetCalories = obstacle.cell.energy;

                if (obstacle.isCorpse)
                {
                    e.ReplaceEnergy(e.cell.energy + targetCalories);
                    e.ReplaceController(e.cell.controller + 3);
                }
                else
                {
                    if (CellHelper.CheckRelations(e, obstacle))
                    {
                        e.ReplaceController(e.cell.controller + 4);
                        e.ReplaceEnergy(e.cell.energy + targetCalories / 2);
                    }
                    else
                    {
                        e.ReplaceController(e.cell.controller + 5);
                        e.ReplaceEnergy(e.cell.energy + targetCalories);
                    }
                }

                obstacle.isDestroyed = true;
                context.field.Clear(obstacle.position);
                e.ReplaceColor(e.color.ChangeColor(1, -1, 1));
            }
        }
    }
}
