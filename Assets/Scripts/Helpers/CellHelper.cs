
using Entitas;
using UnityEngine;


public static class CellHelper
{
    public static bool IsCell(GameEntity cell)
    {
        return cell.hasCell && cell.hasSensor && cell.hasPosition;
    }

    public static bool CheckRelations(GameEntity e, GameEntity target)
    {
        if (!IsCell(e))
            return false;
        if (!IsCell(target))
            return false;

        int dismatches = 0;

        for (int i = 0; i < e.cell.genome.Length; i++)
        {
            if (dismatches < 2)
            {
                if (e.cell.genome[i] != target.cell.genome[i]) dismatches++;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public static void ReplaceController(this GameEntity entity, int newController)
    {
        entity.AddChangeController(newController);
    }

    public static void ReplaceEnergy(this GameEntity entity, int newEnergy)
    {
        entity.ReplaceCell(entity.cell.genome, newEnergy, entity.cell.controller);
    }

    public static void ProduceNewCell(this GameContext context, int[] sourceGenome, int controller, int energy, Color color, int rot, Vector2i position)
    {
        var genome = (int[])sourceGenome.Clone();
        if (Random.Range(0, 3) == 1)
        {
            LevelManager.Instance.mutations++;
            genome[Random.Range(0, 63)] = Random.Range(0, 63);
        }

        var cell = context.CreateEntity();
        cell.AddCell(genome, energy, (int)Mathf.Repeat(controller, genome.Length));
        cell.AddColor(color);
        cell.AddSensor(SensorHelper.GetSensorValue(), rot);
        cell.AddPosition(position.x, position.y);
    }
}
