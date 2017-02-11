using System;
using UnityEngine;
[Serializable]
public struct Vector2i
{
    public int x, y;

    public Vector2i(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Vector2i operator +(Vector2i a, Vector2i b)
    {
        return new Vector2i(a.x + b.x, a.y + b.y);
    }

    public static Vector2i operator +(Vector2i a, Vector2 b)
    {
        return new Vector2i(a.x + (int)b.x, a.y + (int)b.y);
    }

    public Vector2 ToVector2()
    {
        return new Vector2(x, y);
    }

    public static Vector2i operator -(Vector2i a, Vector2i b)
    {
        return new Vector2i(a.x - b.x, a.y - b.y);
    }

    public static Vector2i operator -(Vector2i a, Vector2 b)
    {
        return new Vector2i(a.x - (int)b.x, a.y - (int)b.y);
    }

    public static Vector2i operator *(Vector2i a, int b)
    {
        return new Vector2i(a.x * b, a.y * b);
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Vector2i)) return false;

        Vector2i p = (Vector2i)obj;
        return x == p.x & y == p.y;
    }

    public override int GetHashCode()
    {
        return x ^ y;
    }
    public override string ToString()
    {
        return string.Format("({0}, {1})", x, y);
    }
}
