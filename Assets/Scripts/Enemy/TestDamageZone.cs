using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 영역에 들어오면 Damage만큼 피해를 입힙니다.
/// </summary>
public class TestDamageZone : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth;
        if(other.TryGetComponent(out playerHealth))
        {
            playerHealth.TakeDamage(damage);
        }

    }
}
