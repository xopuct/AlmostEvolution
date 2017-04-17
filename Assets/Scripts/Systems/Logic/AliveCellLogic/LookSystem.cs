
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class LookSystem : ReactiveSystem<GameEntity>
{
    public LookSystem(Contexts contexts) : base(contexts.game) { }

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

    protected override bool Filter(GameEntity entity)
    {
        return !entity.isCorpse && !entity.hasNewCell && entity.cell.CurrentGene == 0;
    }

    protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Cell);
    }
}
