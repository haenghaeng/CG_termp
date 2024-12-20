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
    private NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }
}
