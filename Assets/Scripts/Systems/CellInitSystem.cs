
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
            var genome = (int[])e.newCell.genome.Clone();
            if (Random.Range(0, 3) == 1)
            {
                LevelManager.Instance.mutations++;
                genome[Random.Range(0, 63)] = Random.Range(0, 63);
            }

            var cell = context.CreateEntity();
            cell.AddCell(genome, e.newCell.energy, (int)Mathf.Repeat(e.newCell.controller, genome.Length));
            cell.AddColor(e.newCell.color);
            cell.AddSensor(SensorHelper.GetSensorValue(), e.newCell.rot);
            var pos = (Vector2i)e.position;
            e.RemovePosition();
            cell.AddPosition(pos.x, pos.y);
            cell.AddID(cell.creationIndex);
            context.Move(cell, cell.position);

            e.RemoveNewCell();
            e.isDestroyed = true;
            //context.DestroyEntity(e);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasNewCell && entity.hasPosition && !entity.isDestroyed;
    }

    protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.NewCell);
    }
}
