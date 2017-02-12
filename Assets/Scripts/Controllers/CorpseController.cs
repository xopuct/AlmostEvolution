
using UnityEngine;


public class CorpseController : MonoBehaviour
{
    Vector2i dir = new Vector2i(0, -1);
    void Update()
    {
        foreach (var cell in Registry.Instance.GetAllCorpsesDNA())
        {
            if (cell)
            {
                Profiler.BeginSample("TEst");
                var obstacle = Field.Instance.GetObjectInPos(cell.Pos + dir);
                Profiler.EndSample();
                if (!obstacle)                                                  // Пусто
                {
                    Profiler.BeginSample("Get POs");
                    var pos = cell.Pos;
                    Profiler.EndSample();
                    pos += dir;
                    Profiler.BeginSample("Set pos");
                    cell.Pos = new Vector2i(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
                    Profiler.EndSample();
                    continue;
                }
            }
        }
    }
}