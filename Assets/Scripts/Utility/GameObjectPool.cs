using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectPool : Singleton<GameObjectPool>
{
    public const int MaxObjectsInList = 100000;
    public Vector3 StorePosition = new Vector3(-10000.0f, 0.0f, -100000);

    Dictionary<string, LinkedList<GameObject>> freeObjects = new Dictionary<string, LinkedList<GameObject>>();

    LinkedList<GameObject> GetList(string name)
    {
        LinkedList<GameObject> list;
        if (freeObjects.TryGetValue(name, out list))
            return list;
        return null;
    }

    public IEnumerator Prepare(GameObject[] prefabs, int count = 10)
    {
        foreach (var prefab in prefabs)
        {
            LinkedList<GameObject> list;
            if (!freeObjects.TryGetValue(prefab.name, out list))
            {
                list = new LinkedList<GameObject>();
                freeObjects[prefab.name] = list;

                for (int i = 0; i < count; i++)
                    list.AddFirst(GameObject.Instantiate(prefab, StorePosition, Quaternion.identity) as GameObject);
                yield return 0;
            }
        }

        yield return 0;
    }

    public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        var list = GetList(prefab.name);
        if (list != null && list.Count > 0)
        {
            var obj = list.First.Value;
            list.RemoveFirst();

            obj.transform.position = position;
            obj.transform.rotation = rotation;

            return obj;
        }

        return GameObject.Instantiate(prefab, position, rotation) as GameObject;
    }

    public void Destroy(GameObject obj, out bool shouldDestroy)
    {
        var key = GameObjectNameToKey(obj);

        var list = GetList(key);
        if (list == null)
        {
            list = new LinkedList<GameObject>();
            freeObjects[key] = list;
        }

        if (list.Count < MaxObjectsInList)
        {
            obj.transform.parent = transform;
            obj.transform.position = StorePosition;

            list.AddFirst(obj);
            shouldDestroy = false;
        }
        else
            shouldDestroy = true;
    }

    string GameObjectNameToKey(GameObject obj)
    {
        return obj.name.Replace("(Clone)", string.Empty).TrimEnd();
    }
}
