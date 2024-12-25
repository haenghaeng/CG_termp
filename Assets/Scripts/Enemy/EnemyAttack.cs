using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 범위에 플레이어가 있다면 플레이어의 체력을 damage만큼 감소시킵니다.
/// </summary>
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
