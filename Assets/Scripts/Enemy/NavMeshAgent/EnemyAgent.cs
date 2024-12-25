using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

/// <summary>
/// 현재 오브젝트가 NavMesh를 따라 플레이어 캐릭터를 추적하게 하는 컴포넌트입니다.
/// </summary>
public class EnemyAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform _playerTransform = null;

    [SerializeField] private Animator animator;

    private void Update()
    {
        navMeshAgent.SetDestination(_playerTransform.position);
    }

    public void setPlayer(Transform playerTransform)
    {
        if(_playerTransform == null)
            _playerTransform = playerTransform;
    }

    /// <summary>
    /// 논리값에 따라 Agent가 목표를 추격하거나, 추격하지 않습니다.
    /// </summary>
    /// <param name="Chase"></param>
    public void ChangeChase(bool Chase)
    {
        navMeshAgent.isStopped = !Chase;
        animator.SetBool("ChasePlayer" , Chase);
    }
}
