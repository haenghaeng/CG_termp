using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 플레이어가 바라보는 방향으로 총을 발사합니다.
/// </summary>
public class Shoot : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [Header("Components")]

    [Tooltip("플레이어 오브젝트와 같이 붙어있는 카메라의 transform")]
    [SerializeField] private Transform playerCamera;

    [Tooltip("총알 자국 Pool")]
    [SerializeField] private MyObjectPool bulletMarkPool;    

    [Tooltip("금속 파편 이펙트 Pool")]
    [SerializeField] private MyObjectPool metalParticleEffectPool;

    [SerializeField] private MyObjectPool bloodpool; // 피 효과 pool

    [Header("Variables")]

    [Tooltip("연사속도 = 발사 후 다시 발사할 때까지 걸리는 시간(기본값 = 0.2초)")]
    [SerializeField] private float cycle = 0.2f;

    [Tooltip("총의 공격력(기본값 = 1)")]
    [SerializeField] private int damage = 1;

    private PlayerController playerController;
    [SerializeField] private Animator animator;

    private WaitForSeconds cycleWFS;
    private bool isKeyDown = false;
    private bool isShooting = false;
    private LayerMask Enemy;
    private LayerMask Wall;
    private LayerMask Start;
    private LayerMask Exit;
    private LayerMask Restart;

    public bool reloading = false;

    [SerializeField] private float reloadingDelay = 3.0f;
    public TextMeshProUGUI reloadText;

    public int bullets;
    public int maxBullets = 40;
    [SerializeField] private TextMeshProUGUI bulletBox;



    [SerializeField] private AudioClip shoot;
    public AudioClip reload;
    public AudioSource audioSource;

    [SerializeField] private GameObject shootps; // 총 불꽃 효과



    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        cycleWFS = new WaitForSeconds(cycle);
        Enemy = LayerMask.GetMask("Enemy");
        Wall = LayerMask.GetMask("Wall");
        Start = LayerMask.GetMask("Start");
        Exit = LayerMask.GetMask("Exit");
        Restart = LayerMask.GetMask("Restart");
        bullets = maxBullets;
    }

    private void Update()
    {
        // 마우스 왼쪽키를 누르면 총을 발사하기 시작합니다.
        // 키를 떼면 발사를 멈춥니다.
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isShooting)
        {
            isKeyDown = true;
            isShooting = true;
            StartCoroutine(Shooting());
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isKeyDown = false;
            shootps.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R) && bullets < maxBullets )
        {
            shootps.SetActive(false);
            ReloadStart();
        }

        ShowBulletBox();
    }

    /// <summary>
    /// cycleWFS 주기마다 플레이어의 중심에서 플레이어가가 바라보는 방향으로 Ray를 발사합니다.</br>
    /// 가장 먼저 부딪힌 Wall에 bulletMark를 남깁니다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shooting()
    {
        while(isKeyDown && bullets > 0 && !reloading)
        {
            Ray ray = new Ray();
            ray.origin = transform.position;
            ray.direction = playerCamera.forward;            

            bullets--;

            RaycastHit raycastHit;
            bool isHit = Physics.Raycast(ray, out raycastHit, 1000, (Enemy | Wall | Start | Exit | Restart));

            playerController.SetRecoil(new Vector2(Random.Range(-0.3f,0.3f), -0.3f));

            audioSource.PlayOneShot(shoot);

            shootps.SetActive(true);

            // 벽에 맞았을 때
            if (isHit && (1 << raycastHit.collider.gameObject.layer) == Wall)
            {
                GameObject bulletMark = bulletMarkPool.GetFromPool();
                bulletMark.transform.position = raycastHit.point + (raycastHit.normal * 0.01f);
                bulletMark.transform.rotation = Quaternion.FromToRotation(Vector3.up, raycastHit.normal);

                GameObject metalParticleEffect = metalParticleEffectPool.GetFromPool();
                metalParticleEffect.transform.position = raycastHit.point + (raycastHit.normal * 0.01f);
                metalParticleEffect.transform.rotation = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);
            }

            // 적에게 맞았을 때
            else if (isHit && (1 << raycastHit.collider.gameObject.layer) == Enemy)
            {                
                // 적에게 damage만큼의 피해를 줌
                EnemyHealth enemyHealth;
                raycastHit.collider.TryGetComponent(out enemyHealth);
                enemyHealth.HealthDown(damage);

                // 피 효과 생성
                GameObject obj = bloodpool.GetFromPool();
                obj.transform.position = raycastHit.point + (raycastHit.normal * 0.01f);
                obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, raycastHit.normal);
            }

            else if (isHit && (1 << raycastHit.collider.gameObject.layer) == Start)
            {
                Debug.Log("Start");
                gameManager.StartGame();
            }

            else if (isHit && (1 << raycastHit.collider.gameObject.layer) == Exit)
            {
                Debug.Log("Exit");
                Application.Quit();
            }

            else if (isHit && (1 << raycastHit.collider.gameObject.layer) == Restart)
            {
                Debug.Log("Restart");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            yield return cycleWFS;
        }

        if (bullets <= 0)
        {
            ReloadStart();
        }

        isShooting = false;

        yield return null;
    }

    private void ReloadStart()
    {
        if (!reloading)
        {
            reloading = true;
            reloadText.text = "Reloading";

            // 애니메이션 재생 시작
            animator.SetTrigger("Reload");
        }
    }

    private void ShowBulletBox()
    {
        bulletBox.text = bullets.ToString() + " / " + maxBullets.ToString();
    }
}
