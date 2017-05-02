
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique]
public class FieldComponent : IComponent
{
    //Dictionary<GameObject, Vector2i> gameObjectsPositions = new Dictionary<GameObject, Vector2i>();
    //Dictionary<Vector2i, GameObject> field = new Dictionary<Vector2i, GameObject>();
    GameEntity[,] field;

    public int Width = 100; //{ get { return width; } }
    public int Height = 60; //{ get { return height; } }
    //int width = 100;
    //int height = 60;
    public FieldComponent()
    {
        field = new GameEntity[Width, Height];
    }

    public bool ValidateCoords(Vector2i pos)
    {
        return pos.x >= 0 && pos.x < Width && pos.y >= 0 && pos.y < Height;
    }

    //public Vector2i GetPosition(GameObject gameObject)
    //{
    //    Profiler.BeginSample("GetPosition");
    //    Vector2i pos;
    //    if (gameObjectsPositions.TryGetValue(gameObject, out pos))
    //    {
    //        Profiler.EndSample();
    //        return pos;
    //    }
    //    Profiler.EndSample();
    //    return new Vector2i(-1, -1);
    //}

    public bool Clear(Vector2i pos)
    {
        if (ValidateCoords(pos))
        {
            var hadSmth = GetObjectInPos(pos);
            field[pos.x, pos.y] = null;
            return hadSmth != null;
        }
        else
            return false;
    }

    public bool Move(GameEntity obj, Vector2i targetPosition)
    {
        var originPos = obj.position;
        if (!ValidateCoords(targetPosition))
            return false;

        if (IsFree(targetPosition))
        {
            var objInOriginalPos = GetObjectInPos(originPos);
            if (objInOriginalPos != null && objInOriginalPos.hasID && objInOriginalPos.iD == obj.iD)
                field[originPos.X, originPos.Y] = null;

            obj.ReplacePosition(targetPosition.x, targetPosition.y);
            field[targetPosition.x, targetPosition.y] = obj;
            return true;
        }
        return false;
    }

    public GameEntity GetObjectInPos(Vector2i pos)
    {
        if (ValidateCoords(pos))
        {
            var ret = field[pos.x, pos.y];
            //if (ret != null && ret.isDestroyed)
            //    return null;
            return ret;
        }
        return null;
    }

    //public List<GameObject> CircleCast(Vector2i center, int r)
    //{
    //    var res = new List<GameObject>();
    //    if (r == 0)
    //    {
    //        res.Add(GetObjectInPos(center));
    //    }
    //    else
    //    {
    //        for (int x = Mathf.Max(0, center.x - r); x < Mathf.Min(Width, center.x + r); x++)
    //            for (int y = Mathf.Max(0, center.y - r); y < Mathf.Min(Height, center.y + r); y++)
    //                res.Add(GetObjectInPos(new Vector2i(x, y)));
    //    }
    //    res.RemoveAll(g => !g);
    //    return res;
    //}

    public bool IsFree(Vector2i pos)
    {
        return ValidateCoords(pos) && GetObjectInPos(pos) == null;
    }
}
