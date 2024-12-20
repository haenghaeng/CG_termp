using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트 풀링에 사용되는 게임오브젝트에 반드시 들어가야 하는 컴포넌트입니다.
/// </summary>
public class PoolableObject : MonoBehaviour
{
    private MyObjectPool objectPool = null;

    /// <summary>
    /// 이 오브젝트와 연결된 Pool입니다. Pool에서 오브젝트를 생성할 때 지정되고 이후로는 변경할 수 없습니다.
    /// </summary>
    public MyObjectPool ObjectPool
    {
        get
        {
            return objectPool;
        }
        set
        {
            if(objectPool == null)
                objectPool = value;
        }
    }
}
