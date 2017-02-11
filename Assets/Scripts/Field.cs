using System;
using System.Collections.Generic;
using UnityEngine;

internal class Field : Singleton<Field>
{
    Dictionary<GameObject, Vector2i> gameObjectsPositions = new Dictionary<GameObject, Vector2i>();
    Dictionary<Vector2i, GameObject> field = new Dictionary<Vector2i, GameObject>();

    public int Width = 100;
    public int Height = 60;

    public Vector2i GetPosition(GameObject gameObject)
    {
        Vector2i pos;
        if (gameObjectsPositions.TryGetValue(gameObject, out pos))
            return pos;
        return new Vector2i(-1, -1);
    }

    public bool SetPosition(GameObject gameObject, Vector2i targetPosition)
    {
        var originPos = GetPosition(gameObject);

        if (field.ContainsKey(targetPosition) && field[targetPosition])
            return false;
        else
        {
            field[originPos] = null;
            field.Remove(originPos);
            field[targetPosition] = gameObject;
            gameObjectsPositions[gameObject] = targetPosition;
            return true;
        }
    }

    public GameObject GetObjectInPos(Vector2i pos)
    {
        GameObject ret;
        if (field.TryGetValue(pos, out ret))
            return ret;
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
            for (int x = Mathf.Max(0, center.x - r); x < Mathf.Min(Width - 1, center.x + r); x++)
                for (int y = Mathf.Max(0, center.y - r); y < Mathf.Min(Height - 1, center.y + r); y++)
                    res.Add(GetObjectInPos(new Vector2i(x, y)));
        }
        res.RemoveAll(g => !g);
        return res;
    }


}