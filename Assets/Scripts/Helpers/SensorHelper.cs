
using UnityEngine;
using Entitas;

public static class SensorHelper
{
    public static bool IsCell(GameEntity cell)
    {
        return cell.hasCell && cell.hasSensor && cell.hasPosition;
    }

    public static void ReplaceRot(this GameEntity entity, int newRot)
    {
        entity.ReplaceSensor(entity.sensor.sensors, newRot);
    }

    public static GameEntity GetEntityInEyeLook(GameEntity cell)
    {
        if (IsCell(cell))
        {
            var entity = Contexts.sharedInstance.game.field.GetObjectInPos(cell.position + cell.sensor.sensor);
            //if (entity != cell)
            return entity;
            //return null;
        }
        return null;
    }
}
