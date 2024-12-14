using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 바라보는 방향으로 총을 발사합니다.
/// </summary>
public class Shoot : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private GameObject bulletMarkPrefab;

    [Header("Variables")]
    [SerializeField] private float cycle = 0.2f; // 연사속도 = 발사 후 다시 발사할 때까지 걸리는 시간(단위 : 초)
    [SerializeField] private int damage = 1; // 총의 공격력

    private WaitForSeconds cycleWFS;
    private bool isKeyDown = false;
    private LayerMask Enemy;
    private LayerMask Wall;

    private void Awake()
    {
        cycleWFS = new WaitForSeconds(cycle);
        Enemy = LayerMask.GetMask("Enemy");
        Wall = LayerMask.GetMask("Wall");
    }

    private void Update()
    {
        // 마우스 왼쪽키를 누르면 총을 발사하기 시작합니다.
        // 키를 떼면 발사를 멈춥니다.
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isKeyDown = true;
            StartCoroutine(Shooting());
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isKeyDown = false;
        }
    }

    /// <summary>
    /// cycleWFS 주기마다 플레이어의 중심에서 플레이어가가 바라보는 방향으로 Ray를 발사합니다.</br>
    /// 가장 먼저 부딪힌 Wall에 bulletMark를 남깁니다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shooting()
    {
        while(isKeyDown)
        {
            Ray ray = new Ray();
            ray.origin = transform.position;
            ray.direction = playerCamera.forward;

            RaycastHit raycastHit;
            bool isHit = Physics.Raycast(ray, out raycastHit);


            // 벽에 맞았을 때
            if (isHit && (1 << raycastHit.collider.gameObject.layer) == Wall)
            {
                GameObject bulletMark = Instantiate(bulletMarkPrefab, raycastHit.point, bulletMarkPrefab.transform.rotation);
            }

            // 적에게 맞았을 때
            else if (isHit && (1 << raycastHit.collider.gameObject.layer) == Enemy)
            {
                // 적에게 damage만큼의 피해를 줌
                EnemyHealth enemyHealth;
                raycastHit.collider.TryGetComponent(out enemyHealth);
                enemyHealth.HealthDown(damage);
            }

            yield return cycleWFS;
        }

        yield return null;
    }

}
