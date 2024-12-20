using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy 게임오브젝트에 들어있는 컴포넌트입니다.
/// 플레이어가 Enemy Layer를 가진 오브젝트를 맞췄을 때, HealthDown이 호출됩니다.
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth;

    private PoolableObject poolableObject;

    private int CurrentHealth
    {
        set
        {
            currentHealth = value;
            if(currentHealth <= 0)
            {
                // 현재 체력이 0이하가 되면 Pool로 돌아감
                poolableObject.ObjectPool.BackToPool(gameObject);
            }
        }
        get
        {
            return currentHealth;
        }

    }

    private void Awake()
    {
        currentHealth = maxHealth;
        poolableObject = GetComponent<PoolableObject>();
    }

    private void OnEnable()
    {
        CurrentHealth = maxHealth;
    }


    public void HealthDown(int damage)
    {
        CurrentHealth -= damage;
    }   
}
