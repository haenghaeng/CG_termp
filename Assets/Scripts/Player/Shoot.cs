using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
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

    private IEnumerator Shooting()
    {
        while(isKeyDown)
        {
            GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bullet.GetComponent<Collider>().enabled = false;
            bullet.transform.position = transform.position;
            bullet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            Rigidbody rb = bullet.AddComponent<Rigidbody>();
            rb.useGravity = false;

            rb.velocity = playerCamera.forward * speed;

            StartCoroutine(asdf(bullet));

            yield return cycleWFS;
        }

        yield return null;
    }

    private IEnumerator asdf(GameObject obj)
    {
        yield return new WaitForSeconds(3);
        Destroy(obj);
        yield return null;
    }

}
