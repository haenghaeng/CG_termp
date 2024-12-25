using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 범위에 플레이어가 들어오면 공격 애니메이션을 재생합니다.
/// 공격 유형은 2개입니다.
/// </summary>
public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool isDetect = false;
    private bool isAttacking = false;

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 공격 가능 범위에 들어오면 공격 애니메이션을 재생합니다.
        // 공격 애니메이션은 Punch1, Punch2입니다.
        // 두 애니메이션 중 하나를 랜덤하게 선택하여 재생합니다.(확률은 50%로 같습니다)

        if (other.gameObject.CompareTag("Player"))
        {
            isDetect = true;
            if (!isAttacking)
            {
                isAttacking = true;
                StartCoroutine(Attack());
            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isDetect = false;
        }
    }

    /// <summary>
    /// 플레이어가 공격 가능 범위에 있는 동안 계속 공격하게 하는 코루틴입니다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attack()
    {
        while (isDetect)
        {
            animator.SetInteger("ChooseAttackType", Random.Range(0, 2));
            animator.SetTrigger("Attack");
            yield return null;
        }
        isAttacking = false;
    }
}
