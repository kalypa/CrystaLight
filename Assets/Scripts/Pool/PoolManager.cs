using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PoolManager : SingleMonobehaviour<PoolManager>
{
    private Dictionary<string, ObjectPool<PoolObject>> poolDictionary = new Dictionary<string, ObjectPool<PoolObject>>();

    [HideInInspector] public Transform parent;

    // �ش� Ÿ���� ������Ʈ�� Ǯ���ϴ� �Լ�
    public void CreatePool(PoolObject prefab, int count)
    {
        ObjectPool<PoolObject> pool = new ObjectPool<PoolObject>(prefab, parent, count);
        poolDictionary.Add(prefab.gameObject.name, pool);
    }

    public PoolObject Dequeue(string prefabName)
    {
        if (!poolDictionary.ContainsKey(prefabName))
        {
            Debug.LogError("Prefab doesnt exist on pool");
            return null;
        }

        PoolObject item = poolDictionary[prefabName].Dequeue();
        item.Reset();
        return item;
    }

    public void Enqueue(PoolObject obj)
    {
        poolDictionary[obj.name.Trim()].Enqueue(obj);
    }
}
