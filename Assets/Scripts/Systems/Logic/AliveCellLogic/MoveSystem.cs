
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class MoveSystem : IExecuteSystem
{
    private IGroup<GameEntity> _group;

    public MoveSystem(Contexts contexts)
    {
        _group = contexts.game.GetGroup(GameMatcher.Cell);
    }

    public void Execute()
    {
        foreach (var e in _group.GetEntities())
        {
            if (e.isCorpse || e.cell.CurrentGene != 9)
                continue;
            var obstacle = SensorHelper.GetEntityInEyeLook(e);
            var targetPos = e.position + e.sensor.sensor;
            targetPos.x = (int)Mathf.Repeat(targetPos.x, Field.Instance.Width);

            if (obstacle == null)
            {
                if (Field.Instance.IsFree(targetPos))
                {
                    e.ReplacePosition(targetPos.x, targetPos.y);
                    e.ReplaceController(e.cell.controller + 1);
                }
                else
                    e.ReplaceController(e.cell.controller + 2);

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
                    e.ReplaceCell(e.cell.genome, e.cell.energy + targetCalories, e.cell.controller + 3);
                else
                {
                    if (CellHelper.CheckRelations(e, obstacle))
                        e.ReplaceCell(e.cell.genome, e.cell.energy + targetCalories / 2, e.cell.controller + 4);
                    else
                        e.ReplaceCell(e.cell.genome, e.cell.energy + targetCalories, e.cell.controller + 5);
                }

                obstacle.isDestroyed = true;
                e.ReplaceColor(e.color.ChangeColor(1, -1, 1));
                e.ReplacePosition(targetPos.x, targetPos.y);
            }
        }
    }
}
