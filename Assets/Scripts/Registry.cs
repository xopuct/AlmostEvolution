using UnityEngine;
using System.Collections.Generic;
using System;

public class Registry : Singleton<Registry>
{
    public GameObject DeadCell;

    Dictionary<int, GameObject> CellsObject = new Dictionary<int, GameObject>();
    Dictionary<int, DNA> CellsDNA = new Dictionary<int, DNA>();


    Dictionary<int, GameObject> Corpses = new Dictionary<int, GameObject>();
    Dictionary<int, DNA> CorpsesDNA = new Dictionary<int, DNA>();

    //HashSet<GameObject> Cells = new HashSet<GameObject>();
    //HashSet<GameObject> Corpses = new HashSet<GameObject>();

    Vector3 ValidatePos(Vector3 position)
    {
        return new Vector2(Mathf.Round(position.x), Mathf.Round(position.y));
    }

    public GameObject Add(Vector3 position, Quaternion rotation, DNA prefab)
    {
        //var inst = GameObjectPool.Instance.Instantiate(prefab.gameObject, ValidatePos(position), rotation);
        //inst.GetComponent<Bot>().Copy(prefab);
        var inst = (GameObject)Instantiate(prefab.gameObject, ValidatePos(position), rotation);


        CellsObject[inst.GetInstanceID()] = inst;
        CellsDNA[inst.GetInstanceID()] = inst.GetComponent<DNA>();
        CellsDNA[inst.GetInstanceID()].Inited = false;
        CellsDNA[inst.GetInstanceID()].Dead = false;
        return inst;
    }

    public void Kill(GameObject deadmen)
    {
        if (deadmen)
        {
            if (IsCell(deadmen))
            {
                MakeCorpse(CellsDNA[deadmen.GetInstanceID()]);
                CellsDNA.Remove(deadmen.GetInstanceID());
                CellsObject.Remove(deadmen.GetInstanceID());
            }
            else
                Remove(deadmen);
        }
    }

    public void MakeCorpse(DNA dna)
    {
        dna.Die();
        Corpses[dna.gameObject.GetInstanceID()] = dna.gameObject;
        CorpsesDNA[dna.gameObject.GetInstanceID()] = dna;
    }

    public void Remove(GameObject bot)
    {
        if (bot)
        {
            var removeResult = false;
            removeResult = removeResult || CellsObject.Remove(bot.GetInstanceID());
            removeResult = removeResult || CellsDNA.Remove(bot.GetInstanceID());
            removeResult = removeResult || Corpses.Remove(bot.GetInstanceID());
            removeResult = removeResult || CorpsesDNA.Remove(bot.GetInstanceID());

            if (removeResult)
                Destroy(bot.gameObject);
            //GameObjectPool.Instance.Destroy(bot.gameObject);
        }
    }
    public int GetCorpsesCount()
    {
        return Corpses.Count;
    }

    public int GetCellsCount()
    {
        return CellsObject.Count;
    }

    public DNA Get(GameObject cellObj)
    {
        if (cellObj)
        {
            DNA result;
            if (CellsDNA.TryGetValue(cellObj.GetInstanceID(), out result))
                return result;
            else if(CorpsesDNA.TryGetValue(cellObj.GetInstanceID(), out result))
                return result;
        }
        return null;
    }

    public bool IsCorpse(GameObject obj)
    {
        return Corpses.ContainsKey(obj.GetInstanceID());
    }

    public bool IsCell(GameObject obj)
    {
        return CellsObject.ContainsKey(obj.GetInstanceID());
    }

    public IEnumerable<DNA> GetAllCellsDNA()
    {
        return new List<DNA>(CellsDNA.Values);
    }

    //List<GameObject> GetInRadius(Vector3 position, float radius, Dictionary<int, GameObject> dict)
    //{
    //    var ret = new List<GameObject>();

    //    var sqrRadius = radius * radius;
    //    foreach (var obj in dict)
    //    {
    //        if ((obj.Value.transform.position - position).sqrMagnitude < sqrRadius)
    //            ret.Add(obj.Value);

    //    }
    //    return ret;
    //}

    //public List<GameObject> GetInRadius(Vector3 position, float radius)
    //{
    //    var ret = GetInRadius(position, radius, Cells);
    //    ret.AddRange(GetInRadius(position, radius, Corpses));
    //    return ret;
    //}
}
