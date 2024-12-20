using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트 풀링을 사용하는 오브젝트 중, 일정 시간이 지나면 자동으로 Pool로 돌아가는 오브젝트에 들어가는 컴포넌트 입니다.
/// 반드시 PoolableObject 컴포넌트와 같이 사용되야 합니다.
/// </summary>
[RequireComponent(typeof(PoolableObject))]
public class AutoBackToPool : MonoBehaviour
{
    [Tooltip("오브젝트가 존재하는 시간. 이 시간이 지나면 Pool로 돌아갑니다(기본값 = 2초)")]
    [SerializeField] private float time = 2f;
    private PoolableObject poolableObject;

    private void Awake()
    {
        poolableObject = GetComponent<PoolableObject>();
    }

    private void OnEnable()
    {
        StartCoroutine(BackToPool());
    }

    private IEnumerator BackToPool()
    {
        yield return new WaitForSeconds(time);
        poolableObject.ObjectPool.BackToPool(gameObject);
    }
}
