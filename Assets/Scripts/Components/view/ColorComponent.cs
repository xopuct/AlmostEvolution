
using System;
using Entitas;
using UnityEngine;


public class ColorComponent : IComponent
{
    public Color Color;

    public Color ChangeColor(float deltaR, float deltaG, float deltaB)
    {
        var red = Mathf.Clamp01(Color.r + deltaR);
        var green = Mathf.Clamp01(Color.g + deltaG);
        var blue = Mathf.Clamp01(Color.b + deltaB);
        return new Color(red, green, blue);
    }
}
