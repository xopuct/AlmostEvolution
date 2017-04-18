
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
                UnityEngine.Profiling.Profiler.BeginSample("TEst");
                var isFree = FieldOld.Instance.IsFree(cell.Pos + dir);
                UnityEngine.Profiling.Profiler.EndSample();
                if (cell.energy > LevelManager.Instance.LowLevelCorpseEnergy)
                    cell.energy = (int)(cell.energy * LevelManager.Instance.CorpseEnergyReduction);
                else if (cell.energy > LevelManager.Instance.MinCorpseEnergy)
                    cell.energy -= 1;

                if (cell.energy <= 0)
                    Registry.Instance.Kill(cell.gameObject);
                if (isFree)                                                  // Пусто
                {
                    UnityEngine.Profiling.Profiler.BeginSample("Get POs");
                    var pos = cell.Pos;
                    UnityEngine.Profiling.Profiler.EndSample();
                    pos += dir;
                    UnityEngine.Profiling.Profiler.BeginSample("Set pos");
                    cell.Pos = new Vector2i(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
                    UnityEngine.Profiling.Profiler.EndSample();
                    continue;
                }
            }
        }
    }
}