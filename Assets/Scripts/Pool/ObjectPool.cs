using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : PoolObject
{
    private Queue<T> pool = new Queue<T>();          // 오브젝트 풀링에 사용될 프리팹

    private T prefab;

    private Transform parent;

    public ObjectPool(T _Obj, Transform p, int cnt = 10)
    {
        prefab = _Obj;
        parent = p;

        // 초기 크기만큼 오브젝트를 생성하고 풀 리스트에 추가
        for (int i = 0; i < cnt; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    // 오브젝트 풀링에서 사용 가능한 오브젝트를 반환하는 함수
    public T Dequeue()
    {
        T obj = null;
        if(pool.Count <= 0)
        {
            obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            obj = pool.Dequeue(); //큐의 맨 아래에 있는 녀석을 가져다 쓴다.
            obj.gameObject.SetActive(true);
        }
        return obj;
    }

    // 오브젝트를 다시 오브젝트 풀링에 반환하는 함수
    public void Enqueue(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
