
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
                var colliders = Field.Instance.CircleCast(cell.Pos + dir, 1).FindAll(c => c.gameObject != cell.gameObject);
                if (colliders.Count == 0)                                                  // Пусто
                {
                    var pos = cell.Pos;
                    pos += dir;
                    cell.Pos = new Vector2i(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
                    continue;
                }
            }
        }
    }
}
