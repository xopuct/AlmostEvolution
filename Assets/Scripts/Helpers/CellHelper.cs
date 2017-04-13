
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
        entity.ReplaceCell(entity.cell.genome, entity.cell.energy, newController);
    }

    public static void ReplaceEnergy(this GameEntity entity, int newEnergy)
    {
        entity.ReplaceCell(entity.cell.genome, newEnergy, entity.cell.controller);
    }
}
