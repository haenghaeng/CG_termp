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
    [SerializeField] private float speed = 10;
    [SerializeField] private float cycle = 0.2f; // 연사속도 = 발사 후 다시 발사할 때까지 걸리는 시간(단위 : 초)
    private WaitForSeconds cycleWFS;
    private bool isKeyDown = false;

    private void Awake()
    {
        cycleWFS = new WaitForSeconds(cycle);
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
    /// 가장 먼저 부딪힌 지점에 bulletMark를 남깁니다.
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
            

            if (isHit)
            {
                GameObject bulletMark = Instantiate(bulletMarkPrefab, raycastHit.point, bulletMarkPrefab.transform.rotation);
            }

            yield return cycleWFS;
        }

        yield return null;
    }

}
