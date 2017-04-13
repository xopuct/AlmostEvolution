
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class MoveSystem : ReactiveSystem<GameEntity>
{
    public MoveSystem(Contexts contexts) : base(contexts.game) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
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

                if (obstacle.isDead)
                    e.ReplaceCell(e.cell.genome, e.cell.energy + targetCalories, e.cell.controller + 3);
                else
                {
                    if (CellHelper.CheckRelations(e, obstacle))
                        e.ReplaceCell(e.cell.genome, e.cell.energy + targetCalories / 2, e.cell.controller + 4);
                    else
                        e.ReplaceCell(e.cell.genome, e.cell.energy + targetCalories, e.cell.controller + 5);
                }

                obstacle.isDestroy = true;
                e.ReplaceColor(e.color.ChangeColor(1, -1, 1));
                e.ReplacePosition(targetPos.x, targetPos.y);
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return !entity.isDead && !entity.hasNewCell && entity.cell.CurrentGene == 8;
    }

    protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Cell);
    }
}
