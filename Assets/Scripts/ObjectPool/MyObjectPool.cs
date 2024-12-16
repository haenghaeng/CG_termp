using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트를 실제로 생성/파괴하는 대신 Pool에 넣고 빼는 방식으로 관리합니다.
/// </summary>
public class MyObjectPool : MonoBehaviour
{
    [Tooltip("Pool에 들어가는 게임오브젝트 프리팹")]
    [SerializeField] private GameObject ObjectPrefab;

    [Tooltip("Pool에 처음부터 들어있는 아이템의 개수(기본값 = 10개)")]
    [SerializeField] private int defaultSize = 10;

    private Queue<GameObject> Pool = new Queue<GameObject>();

    private void Awake()
    {
        // defaultSize만큼 오브젝트를 생성하고 Pool에 넣습니다.
        for(int i=0; i<defaultSize; i++)
        {
            GameObject obj = Instantiate(ObjectPrefab, transform);
            obj.SetActive(false);
            obj.GetComponent<PoolableObject>().ObjectPool = this;
            Pool.Enqueue(obj);
        }
    }

    /// <summary>
    /// Pool에서 오브젝트를 꺼내 리턴합니다. Pool이 비어있다면 새로 생성하여 리턴합니다.
    /// Pool에서 꺼낸 뒤 SetActive(true)하여 리턴합니다.
    /// </summary>
    /// <returns></returns>
    public GameObject GetFromPool()
    {
        if(Pool.Count > 0)
        {
            GameObject obj = Pool.Dequeue();
            obj.SetActive(true);            
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(ObjectPrefab, transform);
            obj.GetComponent<PoolableObject>().ObjectPool = this;
            obj.SetActive(true);
            return obj;
        }
        
    }

    /// <summary>
    /// 오브젝트를 Pool에 돌려보냅니다.
    /// </summary>
    /// <param name="obj"></param>
    public void BackToPool(GameObject obj)
    {
        obj.SetActive(false);
        Pool.Enqueue(obj);
    }
}
