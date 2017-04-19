
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

    public static Vector2i[] GetSensorValue()
    {
        return new[]
            {
                new Vector2i(0, 1),
                new Vector2i(-1, 1),
                new Vector2i(-1, 0),
                new Vector2i(-1, -1),
                new Vector2i(0, -1),
                new Vector2i(1, -1),
                new Vector2i(1, 0),
                new Vector2i(1, 1)
            };
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
