using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 일정 시간마다 Enemy를 현재 위치에 소환합니다.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Header("Variable")]
    [SerializeField] private float spawnTimer = 5f;

    [Header("Essential")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private MyObjectPool enemyPool;
    
    private WaitForSeconds wfs;

    private bool isSpawn = false;
    private bool isCoroutine = false;

    private void Awake()
    {
        wfs = new WaitForSeconds(spawnTimer);
    }

    private IEnumerator SpawnCoroutine()
    {
        while (isSpawn)
        {
            yield return wfs;
            SpawnEnemy();
        }
        isCoroutine = false;
    }

    /// <summary>
    /// 현재 위치에 Pool에 있는 enemy를 가져옵니다.
    /// </summary>
    private void SpawnEnemy()
    {
        GameObject enemy = enemyPool.GetFromPool();
        enemy.GetComponent<EnemyAgent>().setPlayer(playerTransform);
        enemy.transform.position = transform.position;
    }   
    
    /// <summary>
    /// 논리값에 따라 enemy를 소환시작하거나, 종료합니다.
    /// </summary>
    /// <param name="bb"></param>
    public void SetIsSpawn(bool bb)
    {
        isSpawn = bb;
        if (bb)
        {
            isCoroutine = true;
            StartCoroutine(SpawnCoroutine());
        }
    }
}
