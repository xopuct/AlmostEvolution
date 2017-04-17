
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class EatSystem : ReactiveSystem<GameEntity>
{
    public EatSystem(Contexts contexts) : base(contexts.game) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
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
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return !entity.isCorpse && entity.hasCell && entity.cell.CurrentGene == 8;
    }

    protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Cell);
    }
}
