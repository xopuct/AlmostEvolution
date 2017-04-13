using Entitas;
using Entitas.CodeGeneration.Attributes;
//using Entitas.CodeGenerator.Api;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionComponent : IComponent
{
    public int X;
    public int Y;

    public static implicit operator Vector2i(PositionComponent p)
    {
        return new Vector2i(p.X, p.Y);
    }

    public static implicit operator PositionComponent(Vector2i p)
    {
        return new PositionComponent() { X = p.x, Y = p.y };
    }
}
