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

    [SerializeField] private Animator animator;

    private EnemyAgent enemyAgent;

    private int CurrentHealth
    {
        set
        {
            if(currentHealth > 0)
            {
                currentHealth = value;
                if (currentHealth <= 0)
                {
                    // 추적 종료
                    enemyAgent.ChangeChase(false);

                    // 현재 체력이 0이하가 되면 Death 애니메이션 재생
                    animator.SetTrigger("Death");
                }
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
        enemyAgent = GetComponent<EnemyAgent>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;

        // 추적 시작. 이걸 왜 EnemyHealth에서 하는 거야 으악
        enemyAgent.ChangeChase(true);
    }

    public void HealthDown(int damage)
    {
        CurrentHealth -= damage;
    }
}
