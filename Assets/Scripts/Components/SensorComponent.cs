
using Entitas;
using UnityEngine;


public class SensorComponent : IComponent
{
    public Vector2i[] sensors = {
        new Vector2i(0, 1),
        new Vector2i(-1, 1),
        new Vector2i(-1, 0),
        new Vector2i(-1, -1),
        new Vector2i(0, -1),
        new Vector2i(1, -1),
        new Vector2i(1, 0),
        new Vector2i(1, 1)
    };

    public Vector2i sensor
    {
        get
        {
            var index = (int)(rot / 45f);
            return sensors[index];
        }

    }

    public float rot;
}
