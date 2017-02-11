using UnityEngine;
public class PositionController : MonoBehaviour
{
    public float deltaMove = 0.1f;
    void Update()
    {
        foreach (var cell in Registry.Instance.GetAllCorpsesDNA())
        {
            if (cell)
                cell.transform.position = Vector2.MoveTowards(cell.transform.position, Field.Instance.GetPosition(cell.gameObject).ToVector2(), deltaMove);
        }
        foreach (var cell in Registry.Instance.GetAllCellsDNA())
        {
            if (cell)
                cell.transform.position = Vector2.MoveTowards(cell.transform.position, Field.Instance.GetPosition(cell.gameObject).ToVector2(), deltaMove);
        }
    }
}
