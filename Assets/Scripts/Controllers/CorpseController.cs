﻿
using UnityEngine;


public class CorpseController : MonoBehaviour
{
    Vector3 dir = new Vector3(0, -0.63f, 0);
    void Update()
    {
        foreach (var cell in Registry.Instance.GetAllCorpsesDNA())
        {
            if (cell)
            {
                if (!cell.Obstacle || (cell.ObstaclePos - cell.Obstacle.transform.position).sqrMagnitude > 0.1f)
                {
                    var colliders = Physics2D.OverlapCircleAll(cell.transform.position + dir, 0.1f);
                    colliders = System.Array.FindAll(colliders, c => c.gameObject != cell.gameObject);
                    if (colliders.Length == 0)                                                  // Пусто
                    {
                        var pos = cell.transform.position;
                        pos += dir;
                        cell.transform.position = pos;
                        continue;
                    }
                    else
                    {
                        cell.Obstacle = colliders[0];
                        cell.ObstaclePos = colliders[0].transform.position;
                    }
                }
            }
        }
    }
}
