using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components(Do not change)")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private CheckGround checkGround;
    private Rigidbody rb;

    [Header("Variables")]
    [SerializeField] private float mouseSensitivity = 400f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpScale = 5f;

    private float mouseX = 0;
    private float mouseY = 0;
    private float keyboardHorizontal = 0;
    private float keyboardVertical = 0;

    private Vector3 cameraForward = Vector3.zero; // 카메라가 바라보는 방향 기준 앞 방향. 크기 1
    private Vector3 cameraRight = Vector3.zero; // 카메라가 바라보는 방향 기준 오른쪽 방향. 크기 1

    private Vector3 playerCurrentSpeed = Vector3.zero;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        cameraForward = playerCamera.forward;
        cameraRight = playerCamera.right;

        Rotate();
        Move();
        Jump();
    }

    /// <summary>
    /// 마우스 움직임에 따라 캐릭터와 캐릭터에 부착된 카메라의 회전을 수행합니다.
    /// 캐릭터는 y축(수평) 기준으로 회전합니다.
    /// 카메라는 x축(수직), y축(수평) 기준으로 회전합니다.
    /// </summary>
    private void Rotate()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(mouseY, mouseX, 0);
        transform.localRotation = Quaternion.Euler(0, mouseX, 0);
    }


    /// <summary>
    /// 키보드 입력(WASD)에 따라 캐릭터의 전후좌우 움직임을 수행합니다.
    /// </summary>
    private void Move()
    {
        keyboardHorizontal = Input.GetAxis("Horizontal") * speed;
        keyboardVertical = Input.GetAxis("Vertical") * speed;

        playerCurrentSpeed = cameraForward * keyboardVertical + cameraRight * keyboardHorizontal;
        playerCurrentSpeed.y = rb.velocity.y;

        rb.velocity = playerCurrentSpeed;
    }

    /// <summary>
    /// 키보드 입력(Space)에 따라 캐릭터가 위쪽으로 점프하는 움직임을 수행합니다.
    /// </summary>
    private void Jump()
    {
        // 땅에 있을 때, Space키를 누르면 위쪽 방향으로 점프
        if (Input.GetKeyDown(KeyCode.Space) && checkGround.isGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpScale, rb.velocity.z);
        }        
    }
}
