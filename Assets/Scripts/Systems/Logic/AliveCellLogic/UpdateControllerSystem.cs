
using System;
using System.Collections.Generic;
using Entitas;


public class UpdateControllerSystem : ReactiveSystem<GameEntity>
{
    public UpdateControllerSystem(Contexts contexts) : base(contexts.game) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var cnt = e.changeController.newControllerValue;
            if (cnt > 63)
                cnt -= 64;
            e.ReplaceCell(e.cell.genome, e.cell.energy, cnt);
            e.RemoveChangeController();
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasChangeController && entity.hasCell;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ChangeController.Added());
    }
}
