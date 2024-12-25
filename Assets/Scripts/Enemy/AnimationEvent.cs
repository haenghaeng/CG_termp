using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 애니메이션 이벤트로 호출하는 함수가 들어있는 컴포넌트입니다.
/// </summary>
public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject attackRange;

    /// <summary>
    /// Enemy의 Punch 애니메이션에서 호출되는 함수입니다.
    /// 공격 애니메이션에 맞춰서 공격 범위 게임오브젝트를 활성화합니다.
    /// </summary>
    public void ActivateAttackRange()
    {
        attackRange.SetActive(true);
    }

    /// <summary>
    /// Enemy의 Punch 애니메이션에서 호출되는 함수입니다.
    /// 공격 애니메이션에 맞춰서 공격 범위 게임오브젝트를 비활성화합니다.
    /// </summary>
    public void DeactivateAttackRange()
    { 
        attackRange.SetActive(false);
    }
}
