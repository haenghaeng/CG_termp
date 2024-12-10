using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터의 발 아래에 위치하여 캐릭터가 "Ground" 태그를 가진 오브젝트와 닿았는지 판별합니다.
/// </summary>
public class CheckGround : MonoBehaviour
{
    public bool isGround = false;

    [SerializeField] private List<GameObject> grounds = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground") && !grounds.Contains(other.gameObject))
        {
            grounds.Add(other.gameObject);
            isGround = grounds.Count > 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ground") && grounds.Contains(other.gameObject))
        {
            grounds.Remove(other.gameObject);
            isGround = grounds.Count > 0;
        }
    }
}
