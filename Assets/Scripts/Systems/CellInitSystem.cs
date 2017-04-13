
using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class CellInitSystem : ReactiveSystem<GameEntity>
{
    //readonly IGroup<InputEntity> _inputs;
    GameContext context;
    // get a reference to the group of entities with InputComponent attached 
    public CellInitSystem(Contexts contexts) : base(contexts.game)
    {
        //_inputs = _context.GetGroup(InputMatcher.Input);
        context = contexts.game;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {

            var genome = e.newCell.genome;
            if (Random.Range(0, 3) == 1)
            {
                LevelManager.Instance.mutations++;
                genome[Random.Range(0, 63)] = Random.Range(0, 63);
            }

            var cell = context.CreateEntity();
            cell.AddCell(genome, LevelManager.Instance.StartEnergy, 0);
            cell.AddColor(e.newCell.color);
            cell.AddSensor(new[]
            {
                new Vector2i(0, 1),
                new Vector2i(-1, 1),
                new Vector2i(-1, 0),
                new Vector2i(-1, -1),
                new Vector2i(0, -1),
                new Vector2i(1, -1),
                new Vector2i(1, 0),
                new Vector2i(1, 1)
            }, e.newCell.rot);
            cell.AddPosition(e.newCell.pos.x, e.newCell.pos.y);
            
            
            e.Destroy();
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasNewCell;
    }

    protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.NewCell);
    }
}
