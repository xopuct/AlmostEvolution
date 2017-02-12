using UnityEngine;
public class PositionController : MonoBehaviour
{
    public float deltaMove = 0.5f;
    void Update()
    {
        foreach (var cell in Registry.Instance.GetAllCorpsesDNA())
        {
            UpdatePos(cell);
        }
        foreach (var cell in Registry.Instance.GetAllCellsDNA())
        {
            UpdatePos(cell);
        }
    }

    void UpdatePos(DNA cell)
    {
        if (cell)
        {
            //var newPos = Vector3.MoveTowards(cell.transform.position, Field.Instance.GetPosition(cell.gameObject).ToVector2(), deltaMove);
            //if ((newPos - cell.transform.position).sqrMagnitude > deltaMove * deltaMove)
            //cell.transform.position = newPos;
            if (cell.transform.position != cell.Pos.ToVector3())
                cell.transform.position = cell.Pos.ToVector2();
        }
    }
}
