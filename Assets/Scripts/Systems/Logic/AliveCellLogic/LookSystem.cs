
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class LookSystem : IExecuteSystem
{
    private IGroup<GameEntity> _group;

    public LookSystem(Contexts contexts)
    {
        _group = contexts.game.GetGroup(GameMatcher.Cell);
    }

    public void Execute()
    {
        foreach (var e in _group.GetEntities())
        {
            if (e.isCorpse || e.cell.CurrentGene != 0)
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
                if (obstacle.isCorpse)
                {
                    e.ReplaceController(e.cell.controller + 3);

                }
                else
                {
                    if (CellHelper.CheckRelations(e, obstacle)) e.ReplaceController(e.cell.controller + 4);

                    else e.ReplaceController(e.cell.controller + 5);
                }
            }
        }
    }
}
