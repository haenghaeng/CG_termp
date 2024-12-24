using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 캐릭터의 체력을 관리하는 컴포넌트입니다.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [Tooltip("플레이어 캐릭터의 최대 체력입니다. 게임 시작시 현재 체력이 이 수치로 설정됩니다.")]
    [SerializeField] private int maxHealth;
    [Tooltip("플레이어 캐릭터의 현재 체력입니다. 0이 되면 게임오버됩니다.")]
    [SerializeField] private int _currentHealth;

    private int currentHealth
    {
        get
        {
            return _currentHealth;
        }

        set
        {
            if(value < 0)
            {
                _currentHealth = 0;
                // 게임 오버. 게임 오버시 실행할 이벤트나 함수를 아래에 넣어주세요
                Debug.Log("게임 오버");
                GameObject.Find("Timer").GetComponent<Timer>().StopTimer();
            }
            else
            {
                _currentHealth = value;
            }
        }
    }    

    /// <summary>
    /// 플레이어의 현재 체력을 최대체력으로 초기화합니다.
    /// 게임을 시작(재시작)할 때 호출합니다.
    /// </summary>
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }


    /// <summary>
    /// 외부에서 호출되어 플레이어의 현재 체력을 감소시키는 함수입니다.
    /// 0 미만으로 내려갈 수 없습니다.
    /// </summary>
    /// <param name="damage">플레이어의 현재 체력에서 감소할 수치</param>
    public void TakeDamage(int damage)
    {
        if(damage > 0 && currentHealth > 0)
            currentHealth -= damage;
    }

    /// <summary>
    /// 외부에서 호출되어 플레이어의 현재 체력을 증가시키는 함수입니다.
    /// 최대체력을 초과할 수 없습니다.
    /// </summary>
    /// <param name="Recovery"></param>
    public void RecoverHealth(int Recovery)
    {
        if (currentHealth > 0)
        {
            currentHealth += Recovery;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
        }
            
    }
}
