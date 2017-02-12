using System;
using System.Collections.Generic;
using UnityEngine;

internal class Field : Singleton<Field>
{
    Dictionary<GameObject, Vector2i> gameObjectsPositions = new Dictionary<GameObject, Vector2i>();
    //Dictionary<Vector2i, GameObject> field = new Dictionary<Vector2i, GameObject>();
    public GameObject[,] field;

    public int Width = 100;
    public int Height = 60;
    protected override void Init()
    {
        base.Init();
        field = new GameObject[Width, Height];
    }

    public bool ValidateCoords(Vector2i pos)
    {
        return pos.x >= 0 && pos.x < Width && pos.y >= 0 && pos.y < Height;
    }
    public Vector2i GetPosition(GameObject gameObject)
    {
        Profiler.BeginSample("GetPosition");
        Vector2i pos;
        if (gameObjectsPositions.TryGetValue(gameObject, out pos))
        {
            Profiler.EndSample();
            return pos;
        }
        Profiler.EndSample();
        return new Vector2i(-1, -1);
    }

    public bool Clear(Vector2i pos)
    {
        if (ValidateCoords(pos))
        {
            var hadSmth = field[pos.x, pos.y];
            field[pos.x, pos.y] = null;
            return hadSmth;
        }
        else
            return false;
    }

    public bool SetPosition(GameObject gameObject, Vector2i targetPosition)
    {
        var originPos = GetPosition(gameObject);

        if (field[targetPosition.x, targetPosition.y])
            return false;
        else
        {
            if (ValidateCoords(originPos) && GetObjectInPos(originPos) == gameObject)
                field[originPos.x, originPos.y] = null;

            field[targetPosition.x, targetPosition.y] = gameObject;
            gameObjectsPositions[gameObject] = targetPosition;
            return true;
        }
    }

    public GameObject GetObjectInPos(Vector2i pos)
    {
        if (ValidateCoords(pos))
            return field[pos.x, pos.y];
        return null;
    }

    public List<GameObject> CircleCast(Vector2i center, int r)
    {
        var res = new List<GameObject>();
        if (r == 0)
        {
            res.Add(GetObjectInPos(center));
        }
        else
        {
            for (int x = Mathf.Max(0, center.x - r); x < Mathf.Min(Width, center.x + r); x++)
                for (int y = Mathf.Max(0, center.y - r); y < Mathf.Min(Height, center.y + r); y++)
                    res.Add(GetObjectInPos(new Vector2i(x, y)));
        }
        res.RemoveAll(g => !g);
        return res;
    }

    public bool IsFree(Vector2i pos)
    {
        return ValidateCoords(pos) && !GetObjectInPos(pos);
    }
}