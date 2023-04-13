using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : PoolObject
{
    private Queue<T> pool = new Queue<T>();          // ������Ʈ Ǯ���� ���� ������

    private T prefab;

    private Transform parent;

    public ObjectPool(T _Obj, Transform p, int cnt = 10)
    {
        prefab = _Obj;
        parent = p;

        // �ʱ� ũ�⸸ŭ ������Ʈ�� �����ϰ� Ǯ ����Ʈ�� �߰�
        for (int i = 0; i < cnt; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    // ������Ʈ Ǯ������ ��� ������ ������Ʈ�� ��ȯ�ϴ� �Լ�
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
            obj = pool.Dequeue(); //ť�� �� �Ʒ��� �ִ� �༮�� ������ ����.
            obj.gameObject.SetActive(true);
        }
        return obj;
    }

    // ������Ʈ�� �ٽ� ������Ʈ Ǯ���� ��ȯ�ϴ� �Լ�
    public void Enqueue(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
